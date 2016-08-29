using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Collections;
using System.Windows.Forms;

namespace ZONEDOCTOR.ScriptsEditor.Commands
{
    [Serializable()]
    public class ActionCommand : EventActionCommand
    {
        // class variables
        private byte[] commandData;
        public byte[] CommandData { get { return this.commandData; } set { this.commandData = value; } }
        private bool modified; public bool Modified { get { return this.modified; } set { this.modified = value; } }
        public int Length { get { return this.commandData.Length; } }
        public bool Terminator
        {
            get
            {
                if (type == ScriptType.Character)
                    return commandData[0] == 0xFF;
                else if (type == ScriptType.Vehicle || type == ScriptType.Map)
                    return commandData[0] == 0xD2 || commandData[0] == 0xFF;
                return false;
            }
        }
        private ScriptType type; public ScriptType Type { get { return type; } set { type = value; } }
        private bool embedded = false;
        public bool Embedded { get { return this.embedded; } set { this.embedded = value; } }
        // accessors
        protected override byte GetOpcode()
        {
            if (this.commandData.Length > 0)
                return this.commandData[0];
            else return 0;
        }
        protected override void SetOpcode(byte opcode)
        {
            this.commandData[0] = opcode;
        }
        protected override byte GetParam(int index)
        {
            if (this.commandData.Length > 1)
                return this.commandData[index];
            else return 0;
        }
        protected override void SetParam(byte param, int index)
        {
            this.commandData[index] = param;
        }
        // constructor
        public ActionCommand(byte[] commandData, int offset, ScriptType type)
        {
            this.commandData = commandData;
            this.offset = offset;
            this.originalOffset = offset;
            this.internalOffset = offset;
            this.type = type;
        }
        // assemblers
        public void Assemble()
        {
        }
        // data managers
        /// <summary>
        /// Adds/subtracts a value from the command's offset and any pointers in the command that point to or after a given offset.
        /// </summary>
        /// <param name="delta">The value to add/subtract from any pointers.</param>
        /// <param name="conditionOffset">The offset to compare to.</param>
        public void UpdatePointer(int delta, int conditionOffset)
        {
            int pointer;
            if (this.offset >= conditionOffset || conditionOffset == 0x7FFFFFFF)
            {
                this.offset += delta;
                this.internalOffset += delta;   // 2009-01-07
            }
            pointer = ReadPointer();
            if (pointer + 0x0A0000 >= conditionOffset)
                WritePointer(pointer + delta);
        }
        public override int ReadPointer()
        {
            switch (Opcode)
            {
                case 0xD4:
                    if (type != ScriptType.Vehicle)
                        return Bits.GetInt24(commandData, 1) & 0x3FFFF; break;
                case 0xF9:
                    if (type != ScriptType.Vehicle)
                        return Bits.GetInt24(commandData, 1) & 0x3FFFF; break;
                default:
                    if (Opcode >= 0xB0 && Opcode <= 0xBF)
                        return Bits.GetInt24(commandData, (((Opcode & 7) * 2) + 3)) & 0x3FFFF;
                    break;
            }
            return 0;
        }
        /// <summary>
        /// Used by commands with more than 1 pointer.
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public override int ReadPointer(int offset)
        {
            return Bits.GetInt24(commandData, offset) & 0x3FFFF;
        }
        public override void WritePointer(int pointer)
        {
            switch (Opcode)
            {
                case 0xD4:
                    if (type != ScriptType.Vehicle)
                        Bits.SetInt24(commandData, 1, pointer, 0x3FFFF); break;
                case 0xF9:
                    if (type != ScriptType.Vehicle)
                        Bits.SetInt24(commandData, 1, pointer, 0x3FFFF); break;
                default:
                    if (Opcode >= 0xB0 && Opcode <= 0xBF)
                        Bits.SetInt24(commandData, ((Opcode & 7) * 2) + 3, pointer, 0x3FFFF);
                    break;
            }
        }
        /// <summary>
        /// Used by commands with more than 1 pointer.
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="pointer"></param>
        /// <returns></returns>
        public override void WritePointer(int offset, int pointer)
        {
            Bits.SetInt24(commandData, offset, pointer, 0x3FFFF);
        }
        public void ResetOriginalOffset()
        {
            this.originalOffset = this.offset;
        }
        // spawning
        public TreeNode Node
        {
            get
            {
                TreeNode node = new TreeNode("[" + (offset + 0xC00000).ToString("X6") + "]   " + ToString());
                if (Opcode >= 0xFF)
                    node.BackColor = Color.FromArgb(255, 255, 160);
                node.ForeColor = modified ? Color.Red : SystemColors.ControlText;
                node.Checked = modified;
                node.Tag = this;
                return node;
            }
        }
        public ActionCommand Copy()
        {
            return new ActionCommand(Bits.Copy(commandData), this.offset, this.type);
        }
        // universal functions
        public override string ToString()
        {
            if (type == ScriptType.Map)
                return Interpreter.Instance.InterpretMapCommand(this);
            else if (type == ScriptType.Vehicle)
                return Interpreter.Instance.InterpretVehicleCommand(this);
            else
                return Interpreter.Instance.InterpretCharacterCommand(this);
        }
    }
}
