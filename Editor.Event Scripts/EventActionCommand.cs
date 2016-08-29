using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR.ScriptsEditor.Commands
{
    [Serializable()]
    public abstract class EventActionCommand
    {
        protected int offset; public int Offset { get { return this.offset; } set { this.offset = value; } }
        // Used to link up pointers outside of current script
        protected int originalOffset; public int OriginalOffset { get { return this.originalOffset; } set { this.originalOffset = value; } }
        // Used to link up pointers inside of current script
        protected int internalOffset; public int InternalOffset { get { return this.internalOffset; } set { this.internalOffset = value; } }
        // Used for updating internal offsets and pointers
        protected bool[] pointerChanged; public bool[] PointerChanged { get { return this.pointerChanged; } set { this.pointerChanged = value; } }
        // this is only used for the special cases
        public int SpecialOffsetVehicle
        {
            get
            {
                return Array.IndexOf(Model.SpecialPointers_Vehicle, this.offset);
            }
        }
        public int SpecialOffsetMap
        {
            get
            {
                return Array.IndexOf(Model.SpecialPointers_Map, this.offset);
            }
        }
        public int SpecialOffsetASM
        {
            get
            {
                return Array.IndexOf(Model.SpecialPointers_ASM, this.offset);
            }
        }
        public int SpecialOffsetCancel
        {
            get
            {
                return Array.IndexOf(Model.SpecialPointers_Cancel, this.offset);
            }
        }
        public bool SpecialOffset
        {
            get
            {
                return SpecialOffsetASM != -1 && SpecialOffsetCancel != -1 &&
                    SpecialOffsetMap != -1 && SpecialOffsetVehicle != -1;
            }
        }
        public int Delta { get { return this.offset - originalOffset; } }
        public byte Opcode { get { return GetOpcode(); } set { SetOpcode(value); } }
        public byte Param1 { get { return GetParam(1); } set { SetParam(value, 1); } }
        public byte Param2 { get { return GetParam(2); } set { SetParam(value, 2); } }
        public byte Param3 { get { return GetParam(3); } set { SetParam(value, 3); } }
        public byte Param4 { get { return GetParam(4); } set { SetParam(value, 4); } }
        public byte Param5 { get { return GetParam(5); } set { SetParam(value, 5); } }
        public abstract int ReadPointer();
        public abstract void WritePointer(int pointer);
        public abstract int ReadPointer(int offset);
        public abstract void WritePointer(int offset, int pointer);
        protected abstract byte GetOpcode();
        protected abstract void SetOpcode(byte opcode);
        protected abstract byte GetParam(int index);
        protected abstract void SetParam(byte param, int index);
    }
}
