using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR
{
    [Serializable()]
    public class ProjectDB
    {
        // project information
        private string title; public string Title { get { return title; } set { title = value; } }
        private string author; public string Author { get { return author; } set { author = value; } }
        private string date; public string Date { get { return date; } set { date = value; } }
        private string webpage; public string Webpage { get { return webpage; } set { webpage = value; } }
        private string description; public string Description { get { return description; } set { description = value; } }
        private string generalNotes; public string OtherInfo { get { return generalNotes; } set { generalNotes = value; } }
        // element notes
        private List<EIndex> eventScripts; public List<EIndex> EventScripts { get { return eventScripts; } set { eventScripts = value; } }
        private List<EIndex> locations; public List<EIndex> Locations { get { return locations; } set { locations = value; } }
        private List<EIndex> memoryBits; public List<EIndex> MemoryBits { get { return memoryBits; } }
        // element lists
        private List<EList> elists; public List<EList> ELists { get { return elists; } set { elists = value; } }
        // constructor
        public ProjectDB()
        {
            // project information
            title = "";
            author = "";
            date = "";
            webpage = "";
            description = "";
            generalNotes = "";
            // element notes
            eventScripts = new List<EIndex>();
            locations = new List<EIndex>();
            memoryBits = new List<EIndex>();
            // element lists
            elists = new List<EList>();
            foreach (EList elist in Model.ELists)
                elists.Add(elist.Copy());
        }
        // public functions
        public void AddIndex(int index, List<EIndex> arrayList)
        {
            arrayList.Insert(index, new EIndex());
        }
        public void DeleteIndex(int index, List<EIndex> arrayList)
        {
            arrayList.RemoveAt(index);
        }
        public void SwitchIndex(int index, List<EIndex> arrayList)
        {
            arrayList.Reverse(index, 2);
        }
    }
    [Serializable()]
    public class EIndex
    {
        private int index; public int Index { get { return index; } set { index = value; } }
        private string label; public string Label { get { return label; } set { label = value; } }
        private string description; public string Description { get { return description; } set { description = value; } }
        private int address; public int Address { get { return address; } set { address = value; } }
        private int addressBit; public int AddressBit { get { return addressBit; } set { addressBit = value; } }
        public EIndex()
        {
            this.index = 0;
            this.label = "(no label)";
            this.description = "(no description)";
            this.address = 0;
            this.addressBit = 0;
        }
        public EIndex(string label, int index)
        {
            this.index = index;
            this.label = label;
            this.description = "(no description)";
            this.address = 0;
            this.addressBit = 0;
        }
        public EIndex(NotesDB.Index index)
        {
            this.index = index.IndexNumber;
            this.label = index.IndexLabel;
            this.description = index.IndexDescription;
            this.address = index.Address;
            this.addressBit = index.AddressBit;
        }
        public override string ToString()
        {
            return label;
        }
    }
    [Serializable()]
    public class EList
    {
        public string Name;
        public string[] Labels
        {
            get
            {
                string[] labels = new string[Indexes.Length];
                for (int i = 0; i < labels.Length; i++)
                    labels[i] = Indexes[i].Label;
                return labels;
            }
        }
        public EIndex[] Indexes;
        public EList(string name, string[] labels)
        {
            Name = name;
            Indexes = new EIndex[labels.Length];
            for (int i = 0; i < labels.Length; i++)
                Indexes[i] = new EIndex(labels[i], i);
        }
        public EList Copy()
        {
            return new EList(Name, Lists.Copy(Labels));
        }
        public void Reset()
        {
            EList source = Model.ELists.Find(delegate(EList list)
            {
                return list.Name == Name;
            });
            if (source == null)
                return;
            for (int i = 0; i < source.Indexes.Length && i < Indexes.Length; i++)
            {
                Indexes[i].Address = source.Indexes[i].Address;
                Indexes[i].AddressBit = source.Indexes[i].AddressBit;
                Indexes[i].Description = source.Indexes[i].Description;
                Indexes[i].Label = source.Indexes[i].Label;
                Indexes[i].Index = source.Indexes[i].Index;
            }
        }
        public override string ToString()
        {
            return Name;
        }
    }
}
