using System;
using System.Collections.Generic;
using System.Collections;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ZONEDOCTOR.ScriptsEditor.Commands
{
    [Serializable()]
    public class EventCommand : EventActionCommand
    {
        // class variables
        private byte[] commandData;
        public byte[] CommandData { get { return this.commandData; } set { this.commandData = value; } }
        private ActionScript queue; public ActionScript Queue { get { return this.queue; } set { this.queue = value; } }
        private bool modified; public bool Modified { get { return this.modified; } set { this.modified = value; } }
        private int register; public int Register { get { return register; } set { register = value; } }
        public int Length
        {
            get
            {
                return this.commandData.Length;
            }
        }
        public bool QueueTrigger
        {
            get
            {
                return
                    (commandData[0] >= 0 && commandData[0] <= 0x34) ||
                    (commandData[0] == 0x6A || commandData[0] == 0x6B) ||
                    commandData[0] == 0xFC || NonEmbeddedMap || NonEmbeddedVehicle;
            }
        }
        public bool Terminator
        {
            get
            {
                return commandData[0] == 0xFE;
            }
        }
        public ScriptType Type
        {
            get
            {
                return ScriptType.Event;
            }
        }
        public ScriptType TriggerType
        {
            get
            {
                if (NonEmbeddedMap) return ScriptType.Map;
                if (NonEmbeddedVehicle) return ScriptType.Vehicle;
                //
                if (commandData[0] >= 0 && commandData[0] <= 0x34)
                    return ScriptType.Character;
                if (commandData[0] == 0x6A || commandData[0] == 0x6B)
                {
                    if ((commandData[5] & 3) != 0)
                        return ScriptType.Vehicle;
                    else
                        return ScriptType.Map;
                }
                if (commandData[0] >= 0 && commandData[0] <= 0x34)
                {
                    if ((commandData[1] & 3) != 0)
                        return ScriptType.Vehicle;
                    else
                        return ScriptType.Map;
                }
                return ScriptType.Event;
            }
        }
        // accessors
        protected override byte GetOpcode()
        {
            if (this.commandData.Length > 0)
                return this.commandData[0];
            else
                return 0;
        }
        protected override void SetOpcode(byte opcode)
        {
            this.commandData[0] = opcode;
        }
        protected override byte GetParam(int index)
        {
            if (this.commandData.Length > 1)
                return this.commandData[index];
            else
                return 0;
        }
        protected override void SetParam(byte param, int index)
        {
            this.commandData[index] = param;
        }
        // this is only used for the special cases
        public bool NonEmbeddedVehicle = false;
        public bool NonEmbeddedMap = false;
        public bool NonEmbedded
        {
            get
            {
                return NonEmbeddedVehicle || NonEmbeddedMap;
            }
        }
        // constructor
        public EventCommand(byte[] commandData, int offset, int register)
        {
            this.commandData = commandData;
            this.offset = offset;
            this.originalOffset = offset;
            this.internalOffset = offset;
            this.register = register;
            if (Do.SpecialCaseVehicle(offset))
            {
                NonEmbeddedVehicle = true;
                queue = new ActionScript(Bits.GetBytes(commandData, 0, commandData.Length), -1, offset, ScriptType.Vehicle);
            }
            else if (Do.SpecialCaseMap(offset))
            {
                NonEmbeddedMap = true;
                queue = new ActionScript(Bits.GetBytes(commandData, 0, commandData.Length), -1, offset, ScriptType.Map);
            }
            else if (commandData[0] >= 0 && commandData[0] <= 0x34)   // $00 to $34
                queue = new ActionScript(Bits.GetBytes(commandData, 2, commandData.Length - 2), -1, offset + 2, ScriptType.Character);
            else if (commandData[0] == 0x6A || commandData[0] == 0x6B)    // $6A or $6B
                queue = new ActionScript(Bits.GetBytes(commandData, 6, commandData.Length - 6), -1, offset + 6,
                    (commandData[5] & 3) != 0 ? ScriptType.Vehicle : ScriptType.Map);
            else if (commandData[0] == 0xFC)
                queue = new ActionScript(Bits.GetBytes(commandData, 1, commandData.Length - 1), -1, offset + 1,
                    (commandData[1] & 3) != 0 ? ScriptType.Vehicle : ScriptType.Map);
        }
        // assemblers
        public void Assemble()
        {
            int start = 0;
            int offset = 0;
            if (QueueTrigger && queue != null)
            {
                if (!NonEmbedded)
                {
                    if (commandData[0] >= 0x00 && commandData[0] <= 0x34)
                        offset = start = 2;
                    else if (commandData[0] == 0x6A || commandData[0] == 0x6B)
                        offset = start = 6;
                    else if (commandData[0] == 0xFC)
                        offset = start = 1;
                }
                byte[] old = new byte[start];
                for (int i = 0; i < old.Length; i++)
                    old[i] = commandData[i];
                foreach (ActionCommand aqc in queue.Commands)
                {
                    aqc.Assemble();
                    offset += aqc.Length;
                }
                commandData = new byte[offset];
                for (int i = 0; i < old.Length; i++)
                    commandData[i] = old[i];
                foreach (ActionCommand aqc in queue.Commands)
                {
                    aqc.CommandData.CopyTo(commandData, start);
                    start += aqc.Length;
                }
            }
        }
        // class functions
        public void RefreshOffsets(int offset)
        {
            this.offset = offset;
            Assemble(); // added 2008-12-20
            if ((NonEmbedded || QueueTrigger) && queue != null)
            {
                if (!NonEmbedded)
                {
                    if (commandData[0] >= 0 && commandData[0] <= 0x34)   // $00 to $34
                        offset += 2;
                    else if (commandData[0] == 0x6A || commandData[0] == 0x6B)    // $6A or $6B
                        offset += 6;
                    else if (commandData[0] == 0xFC)  // $FC
                        offset += 1;
                }
                foreach (ActionCommand aqc in queue.Commands)
                {
                    aqc.Offset = offset;
                    offset += aqc.Length;
                }
            }
        }
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
            if ((this.NonEmbeddedMap || this.NonEmbeddedVehicle || this.QueueTrigger) && this.queue != null)
                queue.UpdateOffsets(delta, conditionOffset);
            //
            if (commandData[0] == 0xB6)
            {
                int offset = 1;
                while (offset < commandData.Length)
                {
                    pointer = ReadPointer(offset);
                    if (pointer + 0x0A0000 >= conditionOffset)
                        WritePointer(offset, pointer + delta);
                    offset += 3;
                }
            }
            else if (commandData[0] == 0xBE)
            {
                int counter = 0;
                while (counter < (commandData[1] & 0x7F))
                {
                    pointer = ReadPointer(counter * 3 + 2);
                    if (pointer + 0x0A0000 >= conditionOffset)
                        WritePointer(counter * 3 + 2, pointer + delta);
                    counter++;
                }
            }
            else
            {
                pointer = ReadPointer();
                if (pointer + 0x0A0000 >= conditionOffset)
                    WritePointer(pointer + delta);
            }
        }
        public override int ReadPointer()
        {
            switch (commandData[0])
            {
                case 0xB2:
                case 0xBD: return Bits.GetInt24(commandData, 1) & 0x3FFFF;
                case 0xB3:
                case 0xB7: return Bits.GetInt24(commandData, 2) & 0x3FFFF;
                case 0xA0: return Bits.GetInt24(commandData, 3) & 0x3FFFF;
                default:
                    if (commandData[0] >= 0xC0 && commandData[0] <= 0xCF)
                        return Bits.GetInt24(commandData, (((commandData[0] & 7) * 2) + 3)) & 0x3FFFF;
                    return 0;
            }
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
            switch (commandData[0])
            {
                case 0xB2:
                case 0xBD: Bits.SetInt24(commandData, 1, pointer, 0x3FFFF); break;
                case 0xB3:
                case 0xB7: Bits.SetInt24(commandData, 2, pointer, 0x3FFFF); break;
                case 0xA0: Bits.SetInt24(commandData, 3, pointer, 0x3FFFF); break;
                default:
                    if (commandData[0] >= 0xC0 && commandData[0] <= 0xCF)
                        Bits.SetInt24(commandData, ((commandData[0] & 7) * 2) + 3, pointer, 0x3FFFF);
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
            if (this.QueueTrigger && this.queue != null && this.queue.Commands != null)
            {
                foreach (ActionCommand aqc in queue.Commands)
                    aqc.ResetOriginalOffset();
            }
        }
        // spawning
        public TreeNode Node
        {
            get
            {
                TreeNode node = new TreeNode("[" + (offset + 0xC00000).ToString("X6") + "]   " + ToString());
                if (NonEmbeddedVehicle)
                    node.Text = "NON-EMBEDDED VEHICLE SCRIPT";
                if (NonEmbeddedMap)
                    node.Text = "NON-EMBEDDED MAP SCRIPT";
                if (QueueTrigger || NonEmbeddedVehicle || NonEmbeddedMap)
                    node.BackColor = Color.FromArgb(192, 224, 255);
                else if (Opcode >= 0xFE)
                    node.BackColor = Color.FromArgb(255, 255, 160);
                node.ForeColor = modified ? Color.Red : SystemColors.ControlText;
                node.Checked = modified;
                node.Tag = this;
                return node;
            }
        }
        public EventCommand Copy()
        {
            return new EventCommand(Bits.Copy(commandData), this.offset, this.register);
        }
        // universal functions
        public override string ToString()
        {
            return Interpreter.Instance.InterpretCommand(this);
        }
    }
}
