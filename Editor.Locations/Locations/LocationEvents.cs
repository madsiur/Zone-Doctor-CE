using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace ZONEDOCTOR
{
    [Serializable()]
    public class LocationEvents
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // local variables
        private List<Event> events = new List<Event>();
        private int currentEvent;
        private int selectedEvent;
        private Event thisEvent;
        // accessors
        public int CurrentEvent
        {
            get { return currentEvent; }
            set
            {
                if (this.events.Count > value)
                {
                    thisEvent = (Event)events[value];
                    this.currentEvent = value;
                }
            }
        }
        public List<Event> Events { get { return events; } }
        public Event Event { get { return thisEvent; } set { thisEvent = value; } }
        public int SelectedEvent { get { return selectedEvent; } set { selectedEvent = value; } }
        public int Count { get { return events.Count; } }
        // location properties
        private int entranceEvent; public int EntranceEvent { get { return entranceEvent; } set { entranceEvent = value; } }
        // event properties
        public int EventPointer { get { return thisEvent.EventPointer; } set { thisEvent.EventPointer = value; } }
        public byte X { get { return thisEvent.X; } set { thisEvent.X = value; } }
        public byte Y { get { return thisEvent.Y; } set { thisEvent.Y = value; } }
        // constructor, functions
        public LocationEvents(int index)
        {
            this.index = index;
            Disassemble();
        }

        private void Disassemble()
        {
            entranceEvent = Bits.GetInt24(rom, (index * 3) + 0x11FA00);

            int pointerOffset = (index * 2) + Model.BASE_EVENT_PTR;

            ushort offsetStart = Bits.GetShort(rom, pointerOffset); pointerOffset += 2;
            ushort offsetEnd = Bits.GetShort(rom, pointerOffset);
            // no event fields for location
            if (offsetStart >= offsetEnd) 
                return;

            int offset = offsetStart + Model.BASE_EVENT_PTR;
            while (offset < offsetEnd + Model.BASE_EVENT_PTR)
            {
                Event tEvent = new Event();
                tEvent.Disassemble(offset);
                events.Add(tEvent);
                offset += 5;
            }
        }

        public void Assemble(ref int offsetStart)
        {
            Bits.SetShort(rom, (index * 3) + 0x11FA00, (ushort)entranceEvent);
            Bits.SetByte(rom, (index * 3) + 0x11FA02, (byte)(entranceEvent >> 16));

            int pointerOffset = (index * 2) + Model.BASE_EVENT_PTR;

            // set the new pointer for the fields
            Bits.SetShort(rom, pointerOffset, offsetStart);  
            // no event fields for location
            if (events.Count == 0) 
                return;

            int offset = offsetStart + Model.BASE_EVENT_PTR;

            foreach (Event e in events)
            {
                e.Assemble(offset);
                offset += 5;
            }

            offsetStart = (ushort)(offset - Model.BASE_EVENT_PTR);
        }
        // list managers
        public void Clear()
        {
            events.Clear();
            this.currentEvent = 0;
        }
        public void New(int index, Point p)
        {
            Event e = new Event();
            e.X = (byte)p.X;
            e.Y = (byte)p.Y;
            if (index < events.Count)
                events.Insert(index, e);
            else
                events.Add(e);
        }
        public void New(int index, Event copy)
        {
            if (index < events.Count)
                events.Insert(index, copy);
            else
                events.Add(copy);
        }
        public void Remove()
        {
            if (currentEvent < events.Count)
            {
                events.Remove(events[currentEvent]);
                this.currentEvent = 0;
            }
        }
    }
    [Serializable()]
    public class Event
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        // event properties
        private byte x; public byte X { get { return x; } set { x = value; } }
        private byte y; public byte Y { get { return y; } set { y = value; } }
        private int eventPointer; public int EventPointer { get { return eventPointer; } set { eventPointer = value; } }
        // assemblers
        public void Disassemble(int offset)
        {
            x = rom[offset++];
            y = rom[offset++];
            eventPointer = (int)Bits.GetInt24(rom, offset);
        }
        public void Assemble(int offset)
        {
            rom[offset++] = x;
            rom[offset++] = y;
            rom[offset++] = (byte)(eventPointer & 0xFF);
            rom[offset++] = (byte)((eventPointer & 0xFF00) >> 8);
            rom[offset++] = (byte)((eventPointer & 0xFF0000) >> 16);
        }
        public Event Copy()
        {
            Event copy = new Event();
            copy.X = x;
            copy.Y = y;
            copy.EventPointer = eventPointer;
            return copy;
        }
    }
}
