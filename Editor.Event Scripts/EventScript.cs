using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Windows.Forms;
using ZONEDOCTOR.ScriptsEditor.Commands;

namespace ZONEDOCTOR.ScriptsEditor
{
    [Serializable()]
    public class EventScript : Element
    {
        // universal variables
        private int index; public override int Index { get { return index; } set { index = value; } }
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // class variables, accessors
        private byte[] script; public byte[] Script { get { return script; } set { script = value; } }
        private List<EventCommand> commands;
        public List<EventCommand> Commands
        {
            get
            {
                if (this.commands == null)
                    this.commands = new List<EventCommand>();
                return this.commands;
            }
            set { this.commands = value; }
        }
        public EventCommand LastCommand
        {
            get
            {
                if (commands.Count > 0)
                    return commands[commands.Count - 1];
                else
                    return null;
            }
        }
        private int baseOffset; public int BaseOffset { get { return this.baseOffset; } set { this.baseOffset = value; } }
        private int endInternalOffset
        {
            get
            {
                if (LastCommand != null)
                    return LastCommand.InternalOffset + LastCommand.Length;
                return 0;
            }
        }
        private int endOffset
        {
            get
            {
                if (LastCommand != null)
                    return LastCommand.Offset + LastCommand.Length;
                return 0;
            }
        }
        public int Length
        {
            get
            {
                return this.script.Length;
            }
        }
        public bool Undoing;
        // constructors
        public EventScript(int index, ref int baseOffset)
        {
            this.index = index;
            this.baseOffset = baseOffset;
            Disassemble(ref baseOffset);
        }
        // assemblers
        private void Disassemble(ref int offset)
        {
            ParseScript(ref offset);
        }
        public void Assemble()
        {
            if (commands == null)
            {
                script = new byte[0]; return;
            }
            int offset = 0;
            foreach (EventCommand esc in commands)
            {
                esc.Assemble();
                offset += esc.Length;
            }
            script = new byte[offset];
            offset = 0;
            foreach (EventCommand esc in commands)
            {
                esc.CommandData.CopyTo(script, offset);
                offset += esc.Length;
            }
        }
        // class functions
        public void ParseScript(ref int offset)
        {
            int length = 0;
            EventCommand temp;
            if (this.commands == null)
                commands = new List<EventCommand>();
            byte opcode = 0;
            while (opcode != 0xFE && offset < 0x0CE600)
            {
                opcode = rom[offset];
                length = GetCommandLength(rom, offset);
                temp = new EventCommand(Bits.GetBytes(rom, offset, length), offset, Bits.Register);
                commands.Add(temp);
                offset += length;
            }
            script = new byte[offset - baseOffset];
            int offset_ = 0;
            foreach (EventCommand esc in commands)
            {
                esc.CommandData.CopyTo(script, offset_);
                offset_ += esc.Length;
            }
        }
        public void ParseScript()
        {
            int length = 0;
            int offset = 0;
            EventCommand temp;
            if (this.commands == null)
                commands = new List<EventCommand>();
            byte opcode = 0;
            while (opcode != 0xFE && offset < script.Length)
            {
                opcode = script[offset];
                length = GetCommandLength(script, offset);
                temp = new EventCommand(Bits.GetBytes(script, offset, length), this.baseOffset + offset, Bits.Register);
                commands.Add(temp);
                offset += length;
            }
        }
        public int GetCommandLength(byte[] data, int offset)
        {
            byte opcode = data[offset];
            byte option = 0;
            if (data.Length - offset > 1)
                option = data[offset + 1];
            //
            int length;
            if (opcode == 0x73 || opcode == 0x74)
                length = ScriptEnums.GetEventCommandLength(opcode, option, data[offset + 3], data[offset + 4]);
            else if (opcode == 0xB6)
                length = ScriptEnums.GetEventCommandLength(opcode, option, Bits.Register);
            else
                length = ScriptEnums.GetEventCommandLength(opcode, option);
            // store a register if needed
            if (opcode == 0x4B)
            {
                int index = Bits.GetShort(data, offset + 1) & 0x3FFF;
                Bits.Register = Model.Dialogues[index].GetOptionCount();
            }
            // Handles special cases
            int i = 0;
            byte aq_opcode = 0, aq_option;
            // if parsing from undo/redo, need to add base offset
            int finalOffset = offset;
            if (offset < 0x0A0000)
                finalOffset = this.baseOffset + offset;
            // finalOffset only used for checking special cases
            if (Do.SpecialCaseVehicle(finalOffset))
            {
                length = 0;
                i = 0;
                while (aq_opcode != 0xFF && aq_opcode != 0xD2)
                {
                    aq_opcode = data[offset + i];
                    if (data.Length - (offset + i) > 1)
                        aq_option = data[offset + 1 + i];
                    else
                        aq_option = 0;
                    i += ScriptEnums.GetVehicleCommandLength(aq_opcode, aq_option);
                    if (aq_opcode == 0xD3)
                    {
                        int location = Bits.GetShort(data, offset + i + 1) & 0x1FF;
                        if (location >= 3 && location <= 0x1FD)
                            break;
                    }
                }
                offset += i + 1;
            }
            else if (Do.SpecialCaseMap(finalOffset))
            {
                length = 0;
                i = 0;
                while (aq_opcode != 0xFF && aq_opcode != 0xD2)
                {
                    aq_opcode = data[offset + i];
                    if (data.Length - (offset + i) > 1)
                        aq_option = data[offset + 1 + i];
                    else
                        aq_option = 0;
                    i += ScriptEnums.GetMapCommandLength(aq_opcode, aq_option);
                    if (aq_opcode == 0xD3)
                    {
                        int location = Bits.GetShort(data, offset + i + 1) & 0x1FF;
                        if (location >= 3 && location <= 0x1FD)
                            break;
                    }
                }
                offset += i + 1;
            }
            else if (opcode == 0x6A || opcode == 0x6B)
            {
                i = 0;
                int temp = Bits.GetShort(data, offset + 1) & 0x01FF;
                while ((temp <= 2 || temp >= 0x1FE) && aq_opcode != 0xFF &&
                    aq_opcode != 0xD2 && !Do.CancelVehicleQueue(finalOffset + i + 6))
                {
                    aq_opcode = data[offset + 6 + i];
                    if (data.Length - (offset + i + 6) > 1)
                        aq_option = data[offset + 6 + 1 + i];
                    else
                        aq_option = 0;
                    if ((data[offset + 5] & 3) == 0)
                        i += ScriptEnums.GetMapCommandLength(aq_opcode, aq_option);
                    else
                        i += ScriptEnums.GetVehicleCommandLength(aq_opcode, aq_option);
                    if (aq_opcode == 0xD3)
                    {
                        int location = Bits.GetShort(data, offset + i + 1) & 0x1FF;
                        if (location >= 3 && location <= 0x1FD)
                            break;
                    }
                }
                offset += i + 1;
            }
            else if (opcode == 0xFC)
            {
                i = 0;
                while (aq_opcode != 0xFF && aq_opcode != 0xD2)
                {
                    aq_opcode = data[offset + 2 + i];
                    if (data.Length - (offset + i + 2) > 1)
                        aq_option = data[offset + 2 + 1 + i];
                    else
                        aq_option = 0;
                    if ((data[offset + 1] & 3) == 0)
                        i += ScriptEnums.GetMapCommandLength(aq_opcode, aq_option);
                    else
                        i += ScriptEnums.GetVehicleCommandLength(aq_opcode, aq_option);
                    if (aq_opcode == 0xD3)
                    {
                        int location = Bits.GetShort(data, offset + i + 1) & 0x1FF;
                        if (location >= 3 && location <= 0x1FD)
                            break;
                    }
                }
                offset += i + 1;
            }
            return length + i;
        }
        private int GetPrevLength(int offset, int length)
        {
            while (rom[offset] == 0xFF)
            {
                length--;
                offset--;
            }
            return length;
        }
        // list managers
        public void Add(EventCommand esc)
        {
            commands.Add(esc);
        }
        public void Add(int parentIndex, ActionCommand aqc)
        {
            EventCommand esc = commands[parentIndex];
            if (esc.QueueTrigger)
            {
                esc.Queue.Add(aqc);
            }
        }
        public void Insert(int index, EventCommand esc)
        {
            try
            {
                commands.Insert(index, esc);
            }
            catch
            {
                throw new Exception("Event Script insert index invalid");
            }
        }
        public void Insert(int parentIndex, int childIndex, ActionCommand aqc)
        {
            try
            {
                EventCommand esc = commands[parentIndex];
                if (esc.QueueTrigger)
                {
                    esc.Queue.Insert(childIndex, aqc);
                }
            }
            catch
            {
                throw new Exception("Event Script insert index invalid");
            }
        }
        public int RemoveAt(int index)
        {
            try
            {
                EventCommand esc = commands[index];
                int len = esc.Length;
                commands.RemoveAt(index);
                return len;
            }
            catch
            {
                throw new Exception("Event Script remove index invalid");
            }
        }
        public int RemoveAt(int parentIndex, int childIndex)
        {
            try
            {
                EventCommand esc = commands[parentIndex];
                ActionCommand aqc;
                int len = 0;
                if (esc.QueueTrigger)
                {
                    aqc = esc.Queue.Commands[childIndex];
                    len = aqc.CommandData.Length;
                    esc.Queue.RemoveAt(childIndex);
                }
                return len;
            }
            catch
            {
                throw new Exception("Event Script insert index invalid");
            }
        }
        public void Reverse(int index1, int index2)
        {
            EventCommand esc = commands[index1];
            commands[index1] = commands[index2];
            commands[index2] = esc;
        }
        public void Refresh()
        {
            if (commands == null)
                return;
            Assemble();
            // refresh offsets
            int offset = baseOffset;
            foreach (EventCommand esc in commands)
            {
                esc.RefreshOffsets(offset);
                offset += esc.Length;
            }
            // update internal pointers
            EventActionCommand eac;
            ScriptIterator it = new ScriptIterator(this);
            while (!it.IsDone)
            {
                eac = it.Next();
                eac.PointerChanged = new bool[256];
            }
            // if undo/redo, pointers update by raw script change
            if (!Undoing && State.Instance.AutoPointerUpdate)
                UpdatePointersAfterScript();
            it = new ScriptIterator(this);
            while (!it.IsDone)
            {
                eac = it.Next();
                if (!Undoing && State.Instance.AutoPointerUpdate)
                    UpdatePointersToCommand(eac);
                eac.InternalOffset = eac.Offset;
            }
        }
        /// <summary>
        /// Updates all of the pointers in the script pointing directly to or after a given command's offset.
        /// </summary>
        /// <param name="reference">The reference command in the script.</param>
        private void UpdatePointersToCommand(EventActionCommand reference)
        {
            ScriptIterator it = new ScriptIterator(this);
            while (!it.IsDone)
            {
                EventActionCommand eac = it.Next();
                int pointer;
                // anything pointing directly to the command and inside the script
                if (eac.GetType() == typeof(EventCommand) && !((EventCommand)eac).NonEmbedded)
                {
                    EventCommand esc = (EventCommand)eac;
                    if (esc.Opcode == 0xB6)
                    {
                        int offset = 1;
                        int counter = 0;
                        while (counter < esc.Register)
                        {
                            pointer = esc.ReadPointer(offset);
                            if (pointer + 0x0A0000 == reference.InternalOffset && !esc.PointerChanged[counter])
                            {
                                esc.WritePointer(offset, (ushort)reference.Offset);
                                esc.PointerChanged[counter] = true;
                            }
                            offset += 3;
                            counter++;
                        }
                    }
                    else if (esc.Opcode == 0xBE)
                    {
                        int counter = 0;
                        while (counter < (esc.Param1 & 0x7F))
                        {
                            pointer = esc.ReadPointer(counter * 3 + 2);
                            if (pointer + 0x0A0000 == reference.InternalOffset && !esc.PointerChanged[counter])
                            {
                                esc.WritePointer(counter * 3 + 2, (ushort)reference.Offset);
                                esc.PointerChanged[counter] = true;
                            }
                            counter++;
                        }
                    }
                    else
                    {
                        pointer = esc.ReadPointer();
                        if (pointer + 0x0A0000 == reference.InternalOffset && !esc.PointerChanged[0])
                        {
                            esc.WritePointer((ushort)reference.Offset);
                            esc.PointerChanged[0] = true;
                        }
                    }
                }
                else if (eac.GetType() == typeof(ActionCommand))
                {
                    pointer = eac.ReadPointer();
                    if (pointer + 0x0A0000 == reference.InternalOffset && !eac.PointerChanged[0])
                    {
                        eac.WritePointer((ushort)(reference.Offset));
                        eac.PointerChanged[0] = true;
                    }
                }
            }
        }
        /// <summary>
        /// Adds/subtracts a value from all of the pointers in the script that point to an offset after the script.
        /// </summary>
        /// <param name="delta">The value to add/subtract.</param>
        private void UpdatePointersAfterScript()
        {
            int delta = this.endOffset - this.endInternalOffset;
            //
            ScriptIterator it = new ScriptIterator(this);
            while (!it.IsDone)
            {
                EventActionCommand eac = it.Next();
                int pointer;
                // anything pointing directly to the command and inside the script
                if (eac.GetType() == typeof(EventCommand) && !((EventCommand)eac).NonEmbedded)
                {
                    EventCommand esc = (EventCommand)eac;
                    if (esc.Opcode == 0xB6)
                    {
                        int offset = 1;
                        int counter = 0;
                        while (counter < esc.Register)
                        {
                            pointer = esc.ReadPointer(offset);
                            if (pointer + 0x0A0000 >= this.endInternalOffset && !esc.PointerChanged[counter])
                            {
                                esc.WritePointer(offset, (ushort)(pointer + delta));
                                esc.PointerChanged[counter] = true;
                            }
                            offset += 3;
                            counter++;
                        }
                    }
                    else if (esc.Opcode == 0xBE)
                    {
                        int counter = 0;
                        while (counter < (esc.Param1 & 0x7F))
                        {
                            pointer = esc.ReadPointer(counter * 3 + 2);
                            if (pointer + 0x0A0000 >= this.endInternalOffset && !esc.PointerChanged[counter])
                            {
                                esc.WritePointer(counter * 3 + 2, (ushort)(pointer + delta));
                                esc.PointerChanged[counter] = true;
                            }
                            counter++;
                        }
                    }
                    else
                    {
                        pointer = esc.ReadPointer();
                        if (pointer + 0x0A0000 >= this.endInternalOffset && !esc.PointerChanged[0])
                        {
                            esc.WritePointer((ushort)(pointer + delta));
                            esc.PointerChanged[0] = true;
                        }
                    }
                }
                else if (eac.GetType() == typeof(ActionCommand))
                {
                    pointer = eac.ReadPointer();
                    if (pointer + 0x0A0000 >= this.endInternalOffset && !eac.PointerChanged[0])
                    {
                        eac.WritePointer((ushort)(pointer + delta));
                        eac.PointerChanged[0] = true;
                    }
                }
            }
        }
        // universal functions
        public override void Clear()
        {
            if (commands == null)
                return;
            for (int i = 0; i < commands.Count - 1; )
            {
                EventCommand esc = commands[i];
                if (esc.QueueTrigger)
                {
                    for (int c = 0; c < esc.Queue.Commands.Count; c++)
                    {
                        ActionCommand asc = esc.Queue.Commands[c];
                        // do not remove if special offset command
                        if (!esc.SpecialOffset)
                        {
                            esc.Queue.Commands.Remove(asc);
                            Model.SpecialDelta -= asc.Length;
                        }
                        else
                        {
                            // update all special pointers each time we encounter a command that is a special offset
                            Model.UpdateSpecialPointers(asc, Model.SpecialDelta);
                            Model.SpecialDelta = 0; c++;
                            // must reset delta for the next possible special offsets update
                        }
                    }
                }
                // do not remove if special offset command
                if (!esc.SpecialOffset)
                {
                    commands.Remove(esc);
                    Model.SpecialDelta -= esc.Length;
                }
                else
                {
                    // update all special pointers each time we encounter a command that is a special offset
                    Model.UpdateSpecialPointers(esc, Model.SpecialDelta);
                    Model.SpecialDelta = 0; i++;
                    // must reset delta for the next possible special offsets update
                }
            }
            Assemble();
        }
        // public functions
        public void UpdateAllOffsets(int delta, int conditionOffset)
        {
            if (this.baseOffset >= conditionOffset || conditionOffset == 0x7fffffff)
                this.baseOffset += delta;
            if (commands == null)
                return;
            foreach (EventCommand esc in commands)
                esc.UpdatePointer(delta, conditionOffset);
        }
    }
}
