using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using ZONEDOCTOR.ScriptsEditor.Commands;
using ZONEDOCTOR.ScriptsEditor;

namespace ZONEDOCTOR.ScriptsEditor
{
    [Serializable()]
    public class ActionScript : Element
    {
        // universal variables
        private int index; public override int Index { get { return index; } set { index = value; } }
        // class variables
        private List<ActionCommand> commands;
        public List<ActionCommand> Commands { get { return this.commands; } set { this.commands = value; } }
        public ActionCommand LastCommand
        {
            get
            {
                if (commands.Count > 0)
                    return commands[commands.Count - 1];
                else
                    return null;
            }
        }
        private byte[] script; public byte[] Script { get { return script; } set { script = value; } }
        public int Length
        {
            get
            {
                return script.Length;
            }
        }
        private ScriptType type;
        public ScriptType Type { get { return type; } set { type = value; } }
        private int baseOffset; public int BaseOffset { get { return this.baseOffset; } set { this.baseOffset = value; } }
        private bool embedded = false;
        public bool Embedded { get { return this.embedded; } set { this.embedded = value; } }
        // constructors
        public ActionScript(byte[] commandData, int index, int offset, ScriptType type)
        {
            this.script = commandData;
            this.index = index;
            this.baseOffset = offset;
            this.type = type;
            this.embedded = true;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            this.commands = new List<ActionCommand>();
            ParseScript();
        }
        public void Assemble()
        {
            int offset = 0;
            if (commands == null) { script = new byte[offset]; return; }
            foreach (ActionCommand aqc in commands)
            {
                aqc.Assemble();
                offset += aqc.CommandData.Length;
            }
            script = new byte[offset];
            offset = 0;
            foreach (ActionCommand aqc in commands)
            {
                aqc.CommandData.CopyTo(script, offset);
                offset += aqc.CommandData.Length;
            }
        }
        // class functions
        public void ParseScript()
        {
            int offset = 0, length;
            while (offset < script.Length)
            {
                length = GetLength(script, offset);
                commands.Add(new ActionCommand(Bits.GetBytes(script, offset, length), this.baseOffset + offset, type));
                offset += length;
            }
        }
        private int GetLength(byte[] script, int offset)
        {
            byte opcode, option;
            opcode = script[offset];
            if (script.Length - offset > 1)
                option = script[offset + 1];
            else
                option = 0;
            if (type == ScriptType.Map)
                return ScriptEnums.GetMapCommandLength(opcode, option);
            else if (type == ScriptType.Vehicle)
                return ScriptEnums.GetVehicleCommandLength(opcode, option);
            else
                return ScriptEnums.GetCharacterCommandLength(opcode, option);
        }
        // list management
        public void Add(ActionCommand asc)
        {
            commands.Add(asc);
        }
        public void Insert(int index, ActionCommand asc)
        {
            try
            {
                commands.Insert(index, asc);
            }
            catch
            {
                throw new Exception("Invalid index.");
            }
        }
        public int RemoveAt(int index)
        {
            try
            {
                ActionCommand asc = commands[index];
                int len = asc.CommandData.Length;
                commands.RemoveAt(index);
                return len;
            }
            catch
            {
                throw new Exception("Invalid index.");
            }
        }
        public void Reverse(int index1, int index2)
        {
            ActionCommand asc = commands[index1];
            commands[index1] = commands[index2];
            commands[index2] = asc;
        }
        public override void Clear()
        {
            if (commands != null)
                commands.Clear();
            Assemble();
        }
        // public functions
        public void UpdateOffsets(int delta, int conditionOffset)
        {
            if (this.baseOffset >= conditionOffset || conditionOffset == 0x7FFFFFFF)
                this.baseOffset += delta;
            if (commands == null)
                return;
            foreach (ActionCommand aqc in commands)
                aqc.UpdatePointer(delta, conditionOffset);
        }
    }
}
