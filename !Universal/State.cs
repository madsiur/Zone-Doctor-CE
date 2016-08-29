using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR
{
    public sealed class State
    {
        static State instance = null;
        static readonly object padlock = new object();
        private byte[] privateKey = null;
        // Initial settings
        private bool layer1 = true; public bool Layer1 { get { return layer1; } set { layer1 = value; } }
        private bool layer2 = true; public bool Layer2 { get { return layer2; } set { layer2 = value; } }
        private bool layer3 = true; public bool Layer3 { get { return layer3; } set { layer3 = value; } }
        private bool priority1 = true; public bool Priority1 { get { return priority1; } set { priority1 = value; } }
        private bool bg = true; public bool BG { get { return bg; } set { bg = value; } }
        private bool solidity = false; public bool Solidity { get { return solidity; } set { solidity = value; } }
        private bool mask = false; public bool Mask { get { return mask; } set { mask = value; } }
        private bool zones = false; public bool Zones { get { return zones; } set { zones = value; } }
        private bool npcs = false; public bool NPCs { get { return npcs; } set { npcs = value; } }
        private bool treasures = false; public bool Treasures { get { return treasures; } set { treasures = value; } }
        private bool exits = false; public bool Exits { get { return exits; } set { exits = value; } }
        private bool events = false; public bool Events { get { return events; } set { events = value; } }
        private bool tile = false; public bool TileGrid { get { return tile; } set { tile = value; } }
        private bool isometricGrid = false; public bool IsometricGrid { get { return isometricGrid; } set { isometricGrid = value; } }
        private bool template = false; public bool Template { get { return template; } set { ClearDrawSelectErase(); template = value; } }
        private bool draw = false; public bool Draw { get { return draw; } set { ClearDrawSelectErase(); draw = value; } }
        private bool select = false; public bool Select { get { return select; } set { ClearDrawSelectErase(); select = value; } }
        private bool erase = false; public bool Erase { get { return erase; } set { ClearDrawSelectErase(); erase = value; } }
        private bool fill = false; public bool Fill { get { return fill; } set { ClearDrawSelectErase(); fill = value; } }
        private bool dropper = false; public bool Dropper { get { return dropper; } set { dropper = value; } }
        private bool move = false; public bool Move { get { return move; } set { move = value; } }
        private bool paste = false; public bool Paste { get { return paste; } set { paste = value; } }
        private bool autoPointerUpdate = true; public bool AutoPointerUpdate { get { return this.autoPointerUpdate; } set { this.autoPointerUpdate = value; } }
        private bool showEncryptionWarnings = true; public bool ShowEncryptionWarnings { get { return this.showEncryptionWarnings; } set { this.showEncryptionWarnings = value; } }
        private bool showBoundaries = false; public bool ShowBoundaries { get { return showBoundaries; } set { showBoundaries = value; } }
        private int createNewLocation_ProgressPercentage;
        public int CreateNewLocation_ProgressPercentage
        {
            get
            {
                return createNewLocation_ProgressPercentage;
            }
            set
            {
                createNewLocation_ProgressPercentage = value;
            }
        }
        State()
        {
        }
        // accessors
        public static State Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                    {
                        instance = new State();
                    }
                    return instance;
                }
            }
        }
        public byte[] PrivateKey
        {
            get
            {
                if (privateKey == null)
                    return null;
                byte[] temp = new byte[privateKey.Length];
                for (int i = 0; i < privateKey.Length; i++)
                    temp[i] = privateKey[i];
                return temp;
            }
            set
            {
                if (value == null)
                {
                    this.privateKey = null;
                    return;
                }
                this.privateKey = new byte[value.Length];
                for (int i = 0; i < value.Length; i++)
                    privateKey[i] = value[i];
            }
        } 
        private void ClearDrawSelectErase()
        {
            template = false;
            draw = false;
            select = false;
            erase = false;
            fill = false;
            dropper = false;
            move = false;
        }
    }
}
