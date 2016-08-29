using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR
{
    [Serializable()]
    public class Location
    {
        // universal variables
        private byte[] rom { get { return Model.ROM; } set { Model.ROM = value; } }
        private int index; public int Index { get { return index; } set { index = value; } }
        // class variables
        private LocationMap locationMap;
        private LocationNPCs locationNPCs;
        private LocationExits locationExits;
        private LocationEvents locationEvents;
        private LocationTreasures locationTreasures;
        // accessors        
        public LocationMap LocationMap { get { return locationMap; } set { locationMap = value; } }
        public LocationNPCs LocationNPCs { get { return locationNPCs; } set { locationNPCs = value; } }
        public LocationTreasures LocationTreasures { get { return locationTreasures; } set { locationTreasures = value; } }
        public LocationExits LocationExits { get { return locationExits; } set { locationExits = value; } }
        public LocationEvents LocationEvents { get { return locationEvents; } set { locationEvents = value; } }
        // constructor, functions
        public Location(int index)
        {
            this.index = index;
            Disassemble();
        }
        // assemblers
        private void Disassemble()
        {
            this.locationMap = new LocationMap(index);
            this.locationNPCs = new LocationNPCs(index);
            this.locationTreasures = new LocationTreasures(index);
            this.locationExits = new LocationExits(index);
            this.locationEvents = new LocationEvents(index);
        }
        public void Assemble()
        {
            locationMap.Assemble();
        }
        // universal functions
        public void Clear()
        {
            locationMap.Clear();
            locationEvents.Clear();
            locationExits.Clear();
            locationNPCs.Clear();
            locationTreasures.Clear();
        }
    }
}
