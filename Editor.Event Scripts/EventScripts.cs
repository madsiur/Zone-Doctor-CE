using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ZONEDOCTOR.Undo;
using ZONEDOCTOR.Properties;
using ZONEDOCTOR.ScriptsEditor;
using ZONEDOCTOR.ScriptsEditor.Commands;

namespace ZONEDOCTOR
{
    public partial class EventScripts : NewForm
    {
        #region Variables
        // main
        private Settings settings = Settings.Default;
        //
        private List<EventScript> eventScripts { get { return Model.EventScripts; } set { Model.EventScripts = value; } }
        public List<EventScript> EVENTScripts { get { return eventScripts; } set { eventScripts = value; } }
        private EventScript eventScript { get { return eventScripts[index]; } set { eventScripts[index] = value; } }
        private byte[] scriptData
        {
            get
            {
                return eventScript.Script;
            }
        }
        //
        private TreeViewWrapper treeViewWrapper; public TreeViewWrapper TreeViewWrapper { get { return treeViewWrapper; } }
        private bool isActionSelected = false;
        private int index { get { return (int)eventNum.Value; } set { eventNum.Value = value; } }
        private int scriptOffset
        {
            get { return eventScript.BaseOffset; }
        }
        private int scriptLength
        {
            get
            {
                return eventScript.Length;
            }
        }
        private CommandStack commandStack = new CommandStack();
        private Stack<int> navigateBackward = new Stack<int>();
        private Stack<int> navigateForward = new Stack<int>();
        private int lastNavigate;
        private bool disableNavigate;
        // private accessors
        private string commandText
        {
            get
            {
                int[] tree = categoryCommand;
                if (tree != null)
                {
                    if (asc == null)
                        return Lists.EventNames(Lists.EventCategoryNames[tree[0]])[tree[1]];
                    else if (asc.Type == ScriptType.Character)
                        return Lists.CharacterNames(Lists.CharacterCategoryNames[tree[0]])[tree[1]];
                    else if (asc.Type == ScriptType.Map)
                        return Lists.MapNames(Lists.MapCategoryNames[tree[0]])[tree[1]];
                    else if (asc.Type == ScriptType.Vehicle)
                        return Lists.VehicleNames(Lists.VehicleCategoryNames[tree[0]])[tree[1]];
                    else
                        return "";
                }
                else
                    return "INVALID";
            }
        }
        private int[] categoryCommand
        {
            get
            {
                int opcode;
                int param1;
                int[][] listBoxOpcodes;
                if (asc == null)
                {
                    listBoxOpcodes = Lists.EventOpcodes;
                    opcode = esc.Opcode;
                    param1 = esc.Param1;
                    if (opcode <= 0x2F) opcode = 0;
                }
                else
                {
                    if (asc.Type == ScriptType.Character)
                        listBoxOpcodes = Lists.CharacterOpcodes;
                    else if (asc.Type == ScriptType.Map)
                        listBoxOpcodes = Lists.MapOpcodes;
                    else
                        listBoxOpcodes = Lists.VehicleOpcodes;
                    opcode = asc.Opcode;
                    param1 = asc.Param1;
                }
                for (int a = 0; a < listBoxOpcodes.Length; a++)
                    for (int b = 0; b < listBoxOpcodes[a].Length; b++)
                        if (opcode == listBoxOpcodes[a][b])
                            return new int[] { a, b };
                return null;
            }
        }
        // reference variables
        private EventCommand esc;
        private ActionCommand asc;
        private TreeNode modifiedNode;
        private ScriptType commandType;
        // externally accessed controls
        public int Index { get { return index; } set { index = value; } }
        public string EventHexText { get { return eventHexText.Text; } set { eventHexText.Text = value; } }
        // pointer recalibration
        private bool apply; public bool Apply { get { return apply; } set { apply = value; } }
        private int delta; public int Delta { get { return delta; } set { delta = value; } }
        // other
        private Previewer previewer;
        private Search searchWindow;
        private EditLabel labelWindow;
        #endregion
        #region Functions
        // constructor
        public EventScripts()
        {
            InitializeComponent();
            this.eventNum.Maximum = Model.EventScripts.Count - 1;
            Do.AddShortcut(toolStrip4, Keys.Control | Keys.S, new EventHandler(save_Click));
            Do.AddShortcut(toolStrip4, Keys.F2, showDecHex);
            InitializeEditor();
            searchWindow = new Search(eventNum, searchBox, searchLabels, Lists.EventLabels);
            labelWindow = new EditLabel(eventLabel, eventNum, "Event Scripts", true);
            new ToolTipLabel(this, showDecHex, null);
            new History(this, null, eventNum);
            disableNavigate = true;
            if (settings.RememberLastIndex)
            {
                int lastEventOffset = settings.LastEventOffset;
                GotoAddress(Math.Min(0x0CE5FF, lastEventOffset));
            }
            categories.SelectedIndex = 0;
            //
            disableNavigate = false;
            lastNavigate = index;
            this.Modified = false;
        }
        //
        private void InitializeEditor()
        {
            treeViewWrapper = new TreeViewWrapper(this.commandTree);
            treeViewWrapper.ChangeScript(eventScripts[index]);
            this.autoPointerUpdate.Checked = autoPointerUpdate.Checked;
            UpdateEventScriptsFreeSpace();
        }
        public void RefreshEditor()
        {
            RefreshEditor(null);
        }
        public void RefreshEditor(EventActionCommand eac)
        {
            if (this.Refreshing)
                return;
            bool modified = this.Modified;
            foreach (EventCommand es in eventScripts[index].Commands)
            {
                es.Modified = false;
                if (es.Queue == null) continue;
                foreach (ActionCommand aq in es.Queue.Commands)
                    aq.Modified = false;
            }
            ResetLists();
            UpdateScriptOffsets();
            treeViewWrapper.ChangeScript(eventScripts[index]);
            if (eac != null)
            {
                foreach (TreeNode node in commandTree.Nodes)
                {
                    if (node.Tag == eac)
                    {
                        commandTree.SelectedNode = node;
                        return;
                    }
                    foreach (TreeNode child in node.Nodes)
                    {
                        if (child.Tag == eac)
                        {
                            commandTree.SelectedNode = child;
                            return;
                        }
                    }
                }
            }
            UpdateCommandData();
            this.Modified = modified;
        }
        public void GotoAddress(int input)
        {
            if (input < 0x0A0000)
            {
                MessageBox.Show("Offset too low. Must be between $CA0000 and $CCE5FF.", "ZONE DOCTOR",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (input >= 0x0CE600)
            {
                MessageBox.Show("Offset too high. Must be between $CA0000 and $CCE5FF.", "ZONE DOCTOR",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            foreach (EventScript script in eventScripts)
            {
                foreach (EventCommand command in script.Commands)
                {
                    if (command.Queue != null)
                    {
                        foreach (ActionCommand action in command.Queue.Commands)
                        {
                            if (action.Offset + action.CommandData.Length > input || action.Offset >= input)
                            {
                                index = script.Index;
                                RefreshEditor(action);
                                settings.LastEventOffset = this.scriptOffset;
                                return;
                            }
                        }
                    }
                    if (command.Offset + command.Length > input || command.Offset >= input)
                    {
                        index = script.Index;
                        RefreshEditor(command);
                        settings.LastEventOffset = this.scriptOffset;
                        return;
                    }
                }
            }
        }
        // GUI settings
        private string[] DialogueNames()
        {
            String[] names = new String[Model.Dialogues.Length];
            for (int i = 0; i < Model.Dialogues.Length; i++)
                names[i] = Model.Dialogues[i].GetStub(true);
            return names;
        }
        // Editing
        private void InsertEventCommand()
        {
            esc = esc.Copy();
            ControlAssembleEvent();
            treeViewWrapper.InsertNode(esc.Copy());
        }
        private void InsertActionCommand()
        {
            asc = asc.Copy();
            ControlAssembleAction();
            treeViewWrapper.InsertNode(asc.Copy());
        }
        private void PushCommand(ScriptBuffer scriptBuffer)
        {
            commandStack.Push(new CommandEdit(eventScripts, index, scriptBuffer, this));
        }
        // update offsets
        public void UpdateScriptOffsets()
        {
            UpdateScriptOffsets(treeViewWrapper.Script.Index);
        }
        public void UpdateScriptOffsets(int index)
        {
            int conditionOffset = 0;
            if (index < eventScripts.Count - 1)
                conditionOffset = eventScripts[index + 1].BaseOffset;
            else
                conditionOffset = eventScripts[index].BaseOffset + eventScripts[index].Length;
            // set the conditionOffset based on the earliest command whose offset was changed in the current script
            foreach (EventCommand esc in eventScripts[index].Commands)
            {
                if (esc.Offset != esc.OriginalOffset)
                {
                    conditionOffset = esc.Offset;
                    break;
                }
            }
            //
            foreach (EventScript script in eventScripts)
            {
                if (script.Index != index)
                    script.UpdateAllOffsets(treeViewWrapper.ScriptDelta, conditionOffset);
            }
            // update the triggers for location elements
            foreach (Location location in Model.Locations)
            {
                if (location.LocationEvents.EntranceEvent + 0x0A0000 >= conditionOffset)
                    location.LocationEvents.EntranceEvent += treeViewWrapper.ScriptDelta;
                foreach (Event EVENT in location.LocationEvents.Events)
                    if (EVENT.EventPointer + 0x0A0000 >= conditionOffset)
                        EVENT.EventPointer += treeViewWrapper.ScriptDelta;
                foreach (NPC npc in location.LocationNPCs.Npcs)
                    if (npc.EventPointer + 0x0A0000 >= conditionOffset)
                        npc.EventPointer += treeViewWrapper.ScriptDelta;
            }
            treeViewWrapper.ScriptDelta = 0;
        }
        // update controls
        private void UpdateEventScriptsFreeSpace()
        {
            int left = CalculateEventScriptsLength();
            this.EvtScrLabel3.Text = " " + left.ToString() + " bytes left ";
            this.EvtScrLabel3.BackColor = left < 0 ? Color.Red : SystemColors.Control;
        }
        private int CalculateEventScriptsLength()
        {
            int length = 0;
            foreach (EventScript es in eventScripts)
                length += es.Script.Length;
            return 0x0CE600 - (0x0A0000 + length);
        }
        private void ResetLists()
        {
            panelCommands.SuspendDrawing();
            ResetControls();
            buttonInsertEvent.Enabled = false;
            buttonApplyEvent.Enabled = false;
            categories.BringToFront();
            categories.SelectedIndex = 0;
            categories_SelectedIndexChanged(null, null);
            panelCommands.ResumeDrawing();
        }
        //
        private void RefreshCategoryList()
        {
            categories.BeginUpdate();
            categories.Items.Clear();
            if (commandType == ScriptType.Event)
                categories.Items.AddRange(Lists.EventCategoryNames);
            if (commandType == ScriptType.Character)
                categories.Items.AddRange(Lists.CharacterCategoryNames);
            if (commandType == ScriptType.Map)
                categories.Items.AddRange(Lists.MapCategoryNames);
            if (commandType == ScriptType.Vehicle)
                categories.Items.AddRange(Lists.VehicleCategoryNames);
            categories.SelectedIndex = 0;
            categories.EndUpdate();
        }
        private void RefreshCommandList()
        {
            commands.BeginUpdate();
            commands.Items.Clear();
            if (commandType == ScriptType.Event)
                commands.Items.AddRange(Lists.EventNames((string)categories.SelectedItem));
            else if (commandType == ScriptType.Character)
                commands.Items.AddRange(Lists.CharacterNames((string)categories.SelectedItem));
            else if (commandType == ScriptType.Map)
                commands.Items.AddRange(Lists.MapNames((string)categories.SelectedItem));
            else if (commandType == ScriptType.Vehicle)
                commands.Items.AddRange(Lists.VehicleNames((string)categories.SelectedItem));
            isActionSelected = commandType != ScriptType.Event;
            commands.EndUpdate();
            commands.SelectedIndex = 0;
        }
        private void RefreshCommandGUI()
        {
            byte[] temp;
            int opcode;
            int param1;
            if (commandType == ScriptType.Event)
            {
                opcode = Lists.EventOpcodes[categories.SelectedIndex][commands.SelectedIndex];
                param1 = 0; // Lists.EventParams[categories.SelectedIndex][commands.SelectedIndex];
                temp = new byte[ScriptEnums.GetEventCommandLength(opcode, param1)];
                temp[0] = (byte)opcode;
                if (temp.Length > 1)
                    temp[1] = (byte)param1;
                esc = new EventCommand(temp, 0, 0);
                asc = null;
            }
            else if (commandType == ScriptType.Character)
            {
                opcode = Lists.CharacterOpcodes[categories.SelectedIndex][commands.SelectedIndex];
                param1 = 0; // Lists.ActionParams[categories.SelectedIndex][commands.SelectedIndex];
                temp = new byte[ScriptEnums.GetCharacterCommandLength(opcode, param1)];
                temp[0] = (byte)opcode;
                if (temp.Length > 1)
                    temp[1] = (byte)param1;
                asc = new ActionCommand(temp, 0, ScriptType.Character);
            }
            else if (commandType == ScriptType.Map)
            {
                opcode = Lists.MapOpcodes[categories.SelectedIndex][commands.SelectedIndex];
                param1 = 0; // Lists.ActionParams[categories.SelectedIndex][commands.SelectedIndex];
                temp = new byte[ScriptEnums.GetMapCommandLength(opcode, param1)];
                temp[0] = (byte)opcode;
                if (temp.Length > 1)
                    temp[1] = (byte)param1;
                asc = new ActionCommand(temp, 0, ScriptType.Map);
            }
            else if (commandType == ScriptType.Vehicle)
            {
                opcode = Lists.VehicleOpcodes[categories.SelectedIndex][commands.SelectedIndex];
                param1 = 0; // Lists.VehicleParams[categories.SelectedIndex][commands.SelectedIndex];
                temp = new byte[ScriptEnums.GetVehicleCommandLength(opcode, param1)];
                temp[0] = (byte)opcode;
                if (temp.Length > 1)
                    temp[1] = (byte)param1;
                asc = new ActionCommand(temp, 0, ScriptType.Vehicle);
            }
            modifiedNode = null;  // the COMMAND PROPERTIES panel now contains a new node instead (2008-11-09)
            panelCommands.SuspendDrawing();
            ResetControls();
            if (!isActionSelected)
                ControlDisassembleEvent();
            else
                ControlDisassembleAction();
            panelCommands.ResumeDrawing();
            buttonInsertEvent.Enabled = true;
            buttonApplyEvent.Enabled = false;
        }
        private void UpdateCommandData()
        {
            this.eventHexText.Text = BitConverter.ToString(treeViewWrapper.CurrentNodeData);
            eventScripts[index].Assemble();
            UpdateEventScriptsFreeSpace();
        }
        //
        public void Assemble()
        {
            UpdateScriptOffsets();
            // Save current script first
            settings.Save();
            if (CalculateEventScriptsLength() >= 0)
                AssembleAllEventScripts();
            else
                MessageBox.Show("There is not enough available space to save the event scripts to.\n\nThe event scripts were not saved.", "ZONE DOCTOR");
            if (Model.HexEditor != null)
            {
                Model.HexEditor.SetOffset(eventScript.BaseOffset);
                Model.HexEditor.Compare();
            }
            for (int i = 0; i < Model.SpecialPointers_Vehicle.Length; i++)
                Bits.SetInt24(Model.ROM, i * 3 + 0x0CFF01, Model.SpecialPointers_Vehicle[i]);
            for (int i = 0; i < Model.SpecialPointers_Map.Length; i++)
                Bits.SetInt24(Model.ROM, i * 3 + 0x0CFF40, Model.SpecialPointers_Map[i]);
            for (int i = 0; i < Model.SpecialPointers_Cancel.Length; i++)
                Bits.SetInt24(Model.ROM, i * 3 + 0x0CFF4F, Model.SpecialPointers_Cancel[i]);
            for (int i = 0; i < Model.SpecialPointers_ASM.Length; i++)
                Bits.SetShort(Model.ROM, Lists.SpecialPointers_ASMPointers[i], (ushort)Model.SpecialPointers_ASM[i]);
            this.Modified = false;
        }
        public void AssembleAllEventScripts()
        {
            foreach (EventScript es in eventScripts)
                es.Assemble();
            int offset = 0x0A0000;
            foreach (EventScript es in eventScripts)
            {
                Bits.SetBytes(Model.ROM, offset, es.Script);
                offset += es.Script.Length;
            }
        }
        // other
        private void PreviewEventOrAction()
        {
            if (previewer == null || !previewer.Visible)
                previewer = new Previewer(scriptOffset + 0xC00000, 0);
            else
                previewer.Reload(scriptOffset + 0xC00000, 0);
            previewer.Show();
        }
        #endregion
        #region Event Handlers
        private void eventNum_ValueChanged(object sender, EventArgs e)
        {
            RefreshEditor();
            //
            if (!disableNavigate)
            {
                navigateBackward.Push(lastNavigate);
                navigateBck.Enabled = true;
            }
            if (!disableNavigate)
                lastNavigate = index;
            settings.LastEventOffset = this.scriptOffset;
        }
        private void gotoAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                try
                {
                    int input = Convert.ToInt32(gotoAddress.Text, 16) - 0xC00000;
                    GotoAddress(input);
                }
                catch
                {
                    MessageBox.Show("Not a valid hexadecimal offset.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
        private void gotoAddrButton_Click(object sender, EventArgs e)
        {
            if (gotoAddress.Text == "")
                return;
            try
            {
                int input = Convert.ToInt32(gotoAddress.Text, 16) - 0xC00000;
                GotoAddress(input);
            }
            catch
            {
                MessageBox.Show("Not a valid hexadecimal offset.", "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void EventScripts_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!this.Modified)
                goto Close;
            DialogResult result;
            result = MessageBox.Show("Event Scripts have not been saved.\n\nWould you like to save changes?", "ZONE DOCTOR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
                Assemble();
            else if (result == DialogResult.No)
            {
                Model.EventScripts = null;
                Model.SpecialPointers_Vehicle = null;
                Model.SpecialPointers_Map = null;
                Model.SpecialPointers_Cancel = null;
                Model.SpecialPointers_ASM = null;
            }
            else if (result == DialogResult.Cancel)
            {
                e.Cancel = true;
                return;
            }
        Close:
            settings.Save();
            searchWindow.Close();
            if (previewer != null)
                previewer.Close();
        }
        private void navigateBck_Click(object sender, EventArgs e)
        {
            if (navigateBackward.Count < 1)
                return;
            navigateForward.Push(index);
            //
            disableNavigate = true;
            index = navigateBackward.Peek();
            disableNavigate = false;
            //
            RefreshEditor();
            lastNavigate = index;
            navigateBackward.Pop();
            navigateBck.Enabled = navigateBackward.Count > 0;
            navigateFwd.Enabled = true;
        }
        private void navigateFwd_Click(object sender, EventArgs e)
        {
            if (navigateForward.Count < 1)
                return;
            navigateBackward.Push(index);
            //
            disableNavigate = true;
            index = navigateForward.Peek();
            disableNavigate = false;
            //
            RefreshEditor();
            lastNavigate = index;
            navigateForward.Pop();
            navigateFwd.Enabled = navigateForward.Count > 0;
            navigateBck.Enabled = true;
        }
        // tree
        private void commandTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            UpdateCommandData();
            if (this.Refreshing)
                return;
            if (!commandTree.Enabled)
                return;
            if (eventScript.Commands.Count == 0)
                return;
            //
            panelCommands.SuspendDrawing();
            EventCommand tempEsc;
            ActionCommand tempAqc;
            ScriptType tempType = commandType;
            // if selecting an action queue/script command
            if (commandTree.SelectedNode.Parent != null)
            {
                button1.Visible = false;
                isActionSelected = true;
                tempEsc = eventScripts[index].Commands[commandTree.SelectedNode.Parent.Index];
                tempAqc = tempEsc.Queue.Commands[commandTree.SelectedNode.Index];
                commandType = tempAqc.Type;
                if (asc == null)    // if an event command is in the COMMAND PROPERTIES panel
                {
                    ResetControls();
                    buttonInsertEvent.Enabled = false;
                }
            }
            // if selecting an event script command
            else
            {
                tempEsc = eventScripts[index].Commands[commandTree.SelectedNode.Index];
                button1.Checked = false;
                button1.Visible = tempEsc.QueueTrigger;
                isActionSelected = false;
                commandType = tempEsc.Type;
                if (asc != null)    // if an action queue command is in the COMMAND PROPERTIES panel
                {
                    ResetControls();
                    buttonInsertEvent.Enabled = false;
                }
            }
            // only if node selected different type than last
            if (commandType != tempType)
                RefreshCategoryList();
            //
            treeViewWrapper.SelectedNode = commandTree.SelectedNode;
            //
            panelCommands.ResumeDrawing();
        }
        private void commandTree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!commandTree.Enabled)
                return;
            // Edit Event/ActionQueue
            EvtScrEditCommand.PerformClick();
        }
        private void commandTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            Do.AddHistory(this, index, e.Node, "NodeMouseClick");
            //
            commandTree.SelectedNode = e.Node;
            if (e.Button != MouseButtons.Right)
                return;
            if (commandTree.SelectedNode.Tag.GetType() == typeof(EventCommand))
            {
                EventCommand temp = (EventCommand)commandTree.SelectedNode.Tag;
                // 0xa0 - 0xa6  // 0xd8 - 0xde
                if (temp.ReadPointer() != 0)
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Goto offset...";
                    goToToolStripMenuItem.Click += new EventHandler(goToOffset_Click);
                }
                else if ((temp.Opcode >= 0xB7 && temp.Opcode <= 0xB9) || temp.Opcode == 0xBC ||
                    (temp.Opcode >= 0xC0 && temp.Opcode <= 0xDD))
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Add to project database...";
                    goToToolStripMenuItem.Click += new EventHandler(addMemoryToNotesDatabase_Click);
                }
            }
            else
            {
                ActionCommand temp = (ActionCommand)commandTree.SelectedNode.Tag;
                if (temp.ReadPointer() != 0)
                {
                    e.Node.ContextMenuStrip = contextMenuStripGoto;
                    goToToolStripMenuItem.Text = "Goto offset...";
                    goToToolStripMenuItem.Click += new EventHandler(goToOffset_Click);
                }
                // 0xa0 - 0xa6  // 0xd8 - 0xde
                else
                {
                    bool valid = false;
                    if (temp.Opcode >= 0xB0 && temp.Opcode <= 0xBF)
                        valid = true;
                    if (temp.Type == ScriptType.Vehicle)
                    {
                        if (temp.Opcode == 0xC8 || temp.Opcode == 0xC9)
                            valid = true;
                    }
                    if (temp.Type != ScriptType.Vehicle)
                    {
                        if (temp.Opcode == 0xE1 || temp.Opcode == 0xE4) valid = true;
                        if (temp.Opcode == 0xE2 || temp.Opcode == 0xE5) valid = true;
                        if (temp.Opcode == 0xE3 || temp.Opcode == 0xE6) valid = true;
                    }
                    if (valid)
                    {
                        e.Node.ContextMenuStrip = contextMenuStripGoto;
                        goToToolStripMenuItem.Text = "Add to project database...";
                        goToToolStripMenuItem.Click += new EventHandler(addMemoryToNotesDatabase_Click);
                    }
                }
            }
        }
        private void commandTree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            EventCommand esc;
            ActionCommand asc;
            if (e.Node.Parent != null)
            {
                asc = (ActionCommand)e.Node.Tag;
                asc.Modified = e.Node.Checked;
            }
            else
            {
                esc = (EventCommand)e.Node.Tag;
                esc.Modified = e.Node.Checked;
            }
        }
        private void commandTree_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            EventCommand esc;
            ActionCommand asc;
            if (e.Node.Parent != null)
            {
                esc = (EventCommand)e.Node.Parent.Tag;
                asc = (ActionCommand)e.Node.Tag;
                if (asc == esc.Queue.LastCommand)
                {
                    MessageBox.Show(
                        "Cannot check command(s).\n\nThe last command of an action queue must be a termination command and cannot be moved or modified. " +
                        "To delete this command, insert a termination command immediately before it.",
                        "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                else
                    asc.Modified = e.Node.Checked;
            }
            else
            {
                esc = (EventCommand)e.Node.Tag;
                if (esc == eventScript.LastCommand)
                {
                    MessageBox.Show(
                        "Cannot check command(s).\n\nThe last command of an event script must be a termination command and cannot be moved or modified. " +
                        "To delete this command, insert a termination command immediately before it.",
                        "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    e.Cancel = true;
                }
                else
                    esc.Modified = e.Node.Checked;
            }
        }
        private void commandTree_KeyDown(object sender, KeyEventArgs e)
        {
            if (!commandTree.Enabled)
                return;
            if (!commandTree.Focused)
                return;
            //
            switch (e.KeyData)
            {
                case Keys.Control | Keys.A: Do.SelectAllNodes(commandTree.Nodes, true); break;
                case Keys.Control | Keys.D: Do.SelectAllNodes(commandTree.Nodes, false); break;
                case Keys.Control | Keys.C: EvtScrCopyCommand.PerformClick(); break;
                case Keys.Control | Keys.V: EvtScrPasteCommand.PerformClick(); break;
                case Keys.Shift | Keys.Up:
                case Keys.Control | Keys.Up:
                    e.SuppressKeyPress = true;
                    EvtScrMoveUp.PerformClick();
                    break;
                case Keys.Shift | Keys.Down:
                case Keys.Control | Keys.Down:
                    e.SuppressKeyPress = true;
                    EvtScrMoveDown.PerformClick();
                    break;
                case Keys.Delete: EvtScrDeleteCommand.PerformClick(); break;
                case Keys.Control | Keys.Z: undo.PerformClick(); break;
                case Keys.Control | Keys.Y: redo.PerformClick(); break;
            }
        }
        // functions
        private void EvtScrMoveUp_Click(object sender, EventArgs e)
        {
            this.Refreshing = true;
            if (commandTree.SelectedNode == null)
                return;
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), new SpecialPointers(), treeViewWrapper.SelectedIndex);
            //
            if (commandTree.SelectedNode != modifiedNode)
            {
                modifiedNode = null;
                buttonApplyEvent.Enabled = false;
            }
            try
            {
                esc = eventScripts[index].Commands[commandTree.SelectedNode.Index];
            }
            catch
            {
            }
            treeViewWrapper.MoveUp();
            this.Refreshing = false;
            Do.AddHistory(this, index, commandTree.SelectedNode, "MoveUpCommand");
            //
            PushCommand(buffer);
        }
        private void EvtScrMoveDown_Click(object sender, EventArgs e)
        {
            this.Refreshing = true;
            if (commandTree.SelectedNode == null)
                return;
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), new SpecialPointers(), treeViewWrapper.SelectedIndex);
            //
            if (commandTree.SelectedNode != modifiedNode)
            {
                modifiedNode = null;
                buttonApplyEvent.Enabled = false;
            }
            try
            {
                esc = eventScripts[index].Commands[commandTree.SelectedNode.Index];
            }
            catch
            {
            }
            treeViewWrapper.MoveDown();
            this.Refreshing = false;
            Do.AddHistory(this, index, commandTree.SelectedNode, "MoveDownCommand");
            //
            PushCommand(buffer);
        }
        private void EvtScrCopyCommand_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            treeViewWrapper.Copy();
            Do.AddHistory(this, index, commandTree.SelectedNode, "CopyCommand");
        }
        private void EvtScrPasteCommand_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), new SpecialPointers(), treeViewWrapper.SelectedIndex);
            //
            if (commandTree.SelectedNode != modifiedNode)
            {
                modifiedNode = null;
                buttonApplyEvent.Enabled = false;
            }
            treeViewWrapper.Paste();
            UpdateCommandData();
            //
            commandTree.SelectedNode = treeViewWrapper.SelectedNode;
            Do.AddHistory(this, index, commandTree.SelectedNode, "PasteCommand");
            //
            PushCommand(buffer);
        }
        private void EvtScrDeleteCommand_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), new SpecialPointers(), treeViewWrapper.SelectedIndex);
            //
            if (commandTree.SelectedNode != null && commandTree.SelectedNode == modifiedNode)
            {
                modifiedNode = null;
                buttonApplyEvent.Enabled = false;
            }
            treeViewWrapper.RemoveNode();
            UpdateCommandData();
            //
            commandTree.SelectedNode = treeViewWrapper.SelectedNode;
            Do.AddHistory(this, index, commandTree.SelectedNode, "DeleteCommand");
            //
            PushCommand(buffer);
        }
        private void undo_Click(object sender, EventArgs e)
        {
            commandTree.BeginUpdate();
            commandStack.UndoCommand();
            commandTree.EndUpdate();
        }
        private void redo_Click(object sender, EventArgs e)
        {
            commandTree.BeginUpdate();
            commandStack.RedoCommand();
            commandTree.EndUpdate();
        }
        private void EvtScrEditCommand_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            panelCommands.SuspendDrawing();
            ResetControls();
            // action queue command
            if (commandTree.SelectedNode.Parent != null)
            {
                EventCommand esc = eventScripts[index].Commands[commandTree.SelectedNode.Parent.Index];
                ActionCommand asc = esc.Queue.Commands[commandTree.SelectedNode.Index];
                if (asc == esc.Queue.LastCommand)
                {
                    MessageBox.Show(
                        "Cannot edit command(s).\n\nThe last command of an action queue must be a termination command and cannot be moved or modified. " +
                        "To delete this command, insert a termination command immediately before it.",
                        "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.esc = esc;
                this.asc = asc;
                ControlDisassembleAction();
            }
            // event script command
            else
            {
                EventCommand esc = eventScripts[index].Commands[commandTree.SelectedNode.Index];
                if (esc.NonEmbedded)
                    return;
                else if (esc == eventScript.LastCommand)
                {
                    MessageBox.Show(
                        "Cannot edit command(s).\n\nThe last command of an event script must be a termination command and cannot be moved or modified. " +
                        "To delete this command, insert a termination command immediately before it.",
                        "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                this.esc = esc;
                this.asc = null;
                ControlDisassembleEvent();
            }
            panelCommands.ResumeDrawing();
            //
            buttonApplyEvent.Enabled = true;
            UpdateCommandData();
            //
            modifiedNode = commandTree.SelectedNode;
            commandTree.SelectedNode = treeViewWrapper.SelectedNode;
            treeViewWrapper.EditedNode = modifiedNode;
        }
        private void EvtScrCollapseAll_Click(object sender, EventArgs e)
        {
            treeViewWrapper.CollapseAll();
            UpdateCommandData();
        }
        private void EvtScrExpandAll_Click(object sender, EventArgs e)
        {
            treeViewWrapper.ExpandAll();
            UpdateCommandData();
        }
        private void EvtScrClearAll_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
            "You are about to clear the current script of all commands.\n\nGo ahead with process?",
            "ZONE DOCTOR", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
            if (result != DialogResult.Yes)
                return;
            treeViewWrapper.ClearAll();
            UpdateCommandData();
            Do.AddHistory(this, index, "ClearAll");
        }
        private void EventPreview_Click(object sender, EventArgs e)
        {
            PreviewEventOrAction();
        }
        // GUI command editor
        private void categories_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshCommandList();
        }
        private void commands_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            RefreshCommandGUI();
        }
        private void button1_CheckedChanged(object sender, EventArgs e)
        {
            EventCommand temp = eventScripts[index].Commands[commandTree.SelectedNode.Index];
            commandType = button1.Checked ? temp.TriggerType : ScriptType.Event;
            RefreshCategoryList();
        }
        private void evtNameA1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            panelCommands.SuspendDrawing();
            if (asc == null && esc.Opcode == 0x3F)
            {
                evtNumA3.Enabled = evtNameA1.SelectedIndex == 0;
                OrganizeControls();
            }
            else if (asc == null && esc.Opcode == 0xF6)
            {
                labelEvtA2.Text = "";
                labelEvtA3.Text = "";
                evtNameA2.Enabled = false; evtNumA2.Enabled = false;
                evtNameA3.Enabled = false; evtNumA3.Enabled = false;
                switch (evtNameA1.SelectedIndex)
                {
                    case 0:
                    case 1:
                        groupBoxA.Text = "Play song at volume (other/unknown effects)";
                        labelEvtA2.Text = "Song";
                        labelEvtA3.Text = "Volume";
                        evtNameA2.Items.AddRange(Lists.MusicNames);
                        evtNameA2.SelectedIndex = esc.Param2 & 0x7F; evtNameA2.Enabled = true;
                        evtNumA3.Value = esc.Param3; evtNumA3.Enabled = true;
                        break;
                    case 2:
                        groupBoxA.Text = "Set volume of currently playing song/sound...";
                        labelEvtA2.Text = "Volume";
                        goto case 7;
                    case 3:
                        groupBoxA.Text = "Change volume of currently playing song...";
                        labelEvtA2.Text = "Volume";
                        goto case 7;
                    case 4:
                        groupBoxA.Text = "Change volume of currently playing sound...";
                        labelEvtA2.Text = "Volume";
                        goto case 7;
                    case 5:
                        groupBoxA.Text = "Change pan control of currently playing sound...";
                        labelEvtA2.Text = "Pan control";
                        goto case 7;
                    case 6:
                        groupBoxA.Text = "Change tempo of currently playing song...";
                        labelEvtA2.Text = "Tempo";
                        goto case 7;
                    case 7:
                        if (evtNameA1.SelectedIndex == 7)
                        {
                            groupBoxA.Text = "Change pitch of currently playing song...";
                            labelEvtA2.Text = "Pitch";
                        }
                        labelEvtA3.Text = "Time";
                        evtNumA2.Value = esc.Param3; evtNumA2.Enabled = true;
                        evtNumA3.Value = esc.Param2; evtNumA3.Enabled = true;
                        break;
                    case 8:
                        groupBoxA.Text = "Stop currently playing song, unused bytes...";
                        goto default;
                    case 9:
                        groupBoxA.Text = "Stop currently playing sound, unused bytes...";
                        goto default;
                    default:
                        if (evtNameA1.SelectedIndex == 10)
                            groupBoxA.Text = "Unknown bytes...";
                        labelEvtA2.Text = "Byte 2";
                        labelEvtA3.Text = "Byte 3";
                        evtNumA2.Value = esc.Param3; evtNumA2.Enabled = true;
                        evtNumA3.Value = esc.Param2; evtNumA3.Enabled = true;
                        break;
                }
                evtNameA1.Enabled = true;
                OrganizeControls();
            }
            panelCommands.ResumeDrawing();
        }
        private void evtNameA3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (asc == null && (esc.Opcode == 0x6A || esc.Opcode == 0x6B))
                evtNumA3.Enabled = evtNameA3.SelectedIndex == 3;
        }
        private void evtListBoxD_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            string item = (string)evtListBoxD.SelectedItem;
            if (asc == null && esc.Opcode == 0xB6) 	// Indexed branch based on prior dialogue selection...
            {
                item = item.Replace("Branch to $", "");
                evtNumD1.Value = Convert.ToInt32(item, 16);
            }
            else if (asc == null && esc.Opcode == 0xBE) 	// If # is in the current CaseWord, jump to subroutine...
            {
                if (evtListBoxD.SelectedIndex == 0)
                    item = item.Replace("if CW = ", "");
                else
                    item = item.Replace("else if CW = ", "");
                string value = "";
                if (item[1] == ',')
                {
                    value = item.Substring(0, 1); // 1-digit #
                    item = item.Substring(1);
                }
                else
                {
                    value = item.Substring(0, 2); // 2-digit #
                    item = item.Substring(2);
                }
                evtNumD1.Value = Convert.ToInt32(value, 10);
                item = item.Replace(", goto $", "");
                evtNumD2.Value = Convert.ToInt32(item, 16);
            }
            else if (
                (asc != null && asc.Opcode >= 0xB0 && asc.Opcode <= 0xBF) ||
                (asc == null && esc.Opcode >= 0xC0 && esc.Opcode <= 0xCF))
            {
                if (evtListBoxD.SelectedIndex == 0)
                    item = item.Replace("if address $", "");
                else if ((esc.Opcode & 8) == 8)
                    item = item.Replace("& address $", "");
                else
                    item = item.Replace("or address $", "");
                string address = item.Substring(0, 4);
                item = item.Substring(4);
                item = item.Replace(", bit ", "");
                string bit = item.Substring(0, 1);
                item = item.Substring(2); // is set or is clear starts here
                if (item == "is clear")
                    evtNameD3.SelectedIndex = 0;
                else if (item == "is set")
                    evtNameD3.SelectedIndex = 1;
                evtNumD1.Value = Convert.ToInt32(address, 16);
                evtNumD2.Value = Convert.ToInt32(bit, 10);
            }
        }
        private void evtNumD1_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            int index = evtListBoxD.SelectedIndex;
            if (asc == null && esc.Opcode == 0xB6)
            {
                int offset = (int)evtNumD1.Value;
                evtListBoxD.Items[index] = "Branch to $" + offset.ToString("X6");
            }
            else if (asc == null && esc.Opcode == 0xBE)
            {
                int value = (int)evtNumD1.Value;
                int offset = (int)evtNumD2.Value;
                if (evtListBoxD.SelectedIndex == 0)
                    evtListBoxD.Items[index] = "if CW = " + value + ", goto $" + offset.ToString("X6");
                else
                    evtListBoxD.Items[index] = "else if CW = " + value + ", goto $" + offset.ToString("X6");
            }
            else if (
                (asc != null && asc.Opcode >= 0xB0 && asc.Opcode <= 0xBF) ||
                (asc == null && esc.Opcode >= 0xC0 && esc.Opcode <= 0xCF))
            {
                string conditional = "";
                if (evtListBoxD.SelectedIndex > 0)
                    conditional = (esc.Opcode & 8) == 8 ? "& " : "or ";
                else
                    conditional = "if ";
                conditional += "address $";
                conditional += ((int)evtNumD1.Value).ToString("X4"); evtNumA1.Enabled = true;
                conditional += ", bit " + ((int)evtNumD2.Value & 7);
                conditional += " " + evtNameD3.SelectedItem;
                evtListBoxD.Items[index] = conditional;
            }
            this.Updating = false;
        }
        private void evtNumD2_ValueChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            int index = evtListBoxD.SelectedIndex;
            if (asc == null && esc.Opcode == 0xBE)
            {
                int value = (int)evtNumD1.Value;
                int offset = (int)evtNumD2.Value;
                if (evtListBoxD.SelectedIndex == 0)
                    evtListBoxD.Items[index] = "if CW = " + value + ", goto $" + offset.ToString("X6");
                else
                    evtListBoxD.Items[index] = "else if CW = " + value + ", goto $" + offset.ToString("X6");
            }
            else if (
                (asc != null && asc.Opcode >= 0xB0 && asc.Opcode <= 0xBF) ||
                (asc == null && esc.Opcode >= 0xC0 && esc.Opcode <= 0xCF))
            {
                string conditional = "";
                if (evtListBoxD.SelectedIndex > 0)
                    conditional = (esc.Opcode & 8) == 8 ? "& " : "or ";
                else
                    conditional = "if ";
                conditional += "address $";
                conditional += ((int)evtNumD1.Value).ToString("X4"); evtNumA1.Enabled = true;
                conditional += ", bit " + ((int)evtNumD2.Value & 7);
                conditional += " " + evtNameD3.SelectedItem;
                evtListBoxD.Items[index] = conditional;
            }
            this.Updating = false;
        }
        private void evtNameD3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.Updating)
                return;
            this.Updating = true;
            int index = evtListBoxD.SelectedIndex;
            if (asc == null && esc.Opcode == 0xB6)
            {
                int offset = (int)evtNumD1.Value;
                evtListBoxD.Items[index] = "Branch to $" + offset.ToString("X6");
            }
            else if (
                (asc != null && asc.Opcode >= 0xB0 && asc.Opcode <= 0xBF) ||
                (asc == null && esc.Opcode >= 0xC0 && esc.Opcode <= 0xCF))
            {
                string conditional = "";
                if (evtListBoxD.SelectedIndex > 0)
                    conditional = (esc.Opcode & 8) == 8 ? "& " : "or ";
                else
                    conditional = "if ";
                conditional += "address $";
                conditional += ((int)evtNumD1.Value).ToString("X4"); evtNumA1.Enabled = true;
                conditional += ", bit " + ((int)evtNumD2.Value & 7);
                conditional += " " + evtNameD3.SelectedItem;
                evtListBoxD.Items[index] = conditional;
            }
            this.Updating = false;
        }
        private void evtButtonD4_Click(object sender, EventArgs e)
        {
            this.Updating = true;
            int index = evtListBoxD.SelectedIndex;
            if (asc == null && esc.Opcode == 0xB6)
                evtListBoxD.Items.Add("Branch to $" + ((int)evtNumD1.Value).ToString("X6"));
            else if (asc == null && esc.Opcode == 0xBE)
            {
                int value = (int)evtNumD1.Value;
                int offset = (int)evtNumD2.Value;
                evtListBoxD.Items.Add("else if CW = " + value + ", goto $" + offset.ToString("X6"));
            }
            evtButtonD5.Enabled = evtListBoxD.Items.Count > 1;
            this.Updating = false;
            //
            evtListBoxD.SelectedIndex = Math.Min(evtListBoxD.Items.Count - 1, index + 1);
        }
        private void evtButtonD5_Click(object sender, EventArgs e)
        {
            if (evtListBoxD.Items.Count < 2)
                return;
            this.Updating = true;
            int index = evtListBoxD.SelectedIndex;
            evtListBoxD.Items.RemoveAt(evtListBoxD.SelectedIndex);
            evtButtonD5.Enabled = evtListBoxD.Items.Count > 1;
            this.Updating = false;
            //
            evtListBoxD.SelectedIndex = Math.Min(evtListBoxD.Items.Count - 1, index);
        }
        private void buttonInsertEvent_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), new SpecialPointers(), treeViewWrapper.SelectedIndex);
            //
            EventCommand esc;
            // if editing a non-blank script
            if (commandTree.SelectedNode != null)
            {
                // if inserting action queue/script command
                if (commandTree.SelectedNode.Parent != null)
                    InsertActionCommand();
                else
                {
                    esc = eventScripts[index].Commands[commandTree.SelectedNode.Index];
                    // if adding action queue command to an empty queue trigger
                    if (esc.QueueTrigger && (isActionSelected || esc.NonEmbedded))
                        InsertActionCommand();
                    // if inserting an event command
                    else
                        InsertEventCommand();
                }
            }
            // if inserting event command to a blank event script
            else
                InsertEventCommand();
            UpdateEventScriptsFreeSpace();
            UpdateCommandData();
            //
            if (modifiedNode != null)
            {
                modifiedNode = commandTree.SelectedNode;
                treeViewWrapper.EditedNode = modifiedNode;
            }
            //
            PushCommand(buffer);
        }
        private void buttonApplyEvent_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), new SpecialPointers(), treeViewWrapper.SelectedIndex);
            //
            if (modifiedNode != null)
            {
                ControlAssembleEvent();
                treeViewWrapper.ReplaceNode(esc);
                UpdateEventScriptsFreeSpace();
            }
            UpdateCommandData();
            EvtScrEditCommand.PerformClick();
            Do.AddHistory(this, index, commandTree.SelectedNode, "EditCommand");
            //
            PushCommand(buffer);
        }
        // menustrip
        private void save_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Assemble();
            Cursor.Current = Cursors.Arrow;
        }
        private void hexViewer_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode != null)
            {
                EventActionCommand eac = (EventActionCommand)commandTree.SelectedNode.Tag;
                Model.HexEditor.SetOffset(eac.Offset);
            }
            else
                Model.HexEditor.SetOffset(eventScript.BaseOffset);
            Model.HexEditor.Compare();
            Model.HexEditor.Show();
        }
        private void autoPointerUpdate_Click(object sender, EventArgs e)
        {
            if (autoPointerUpdate.Checked)
            {
                if (MessageBox.Show("AutoUpdatePointer maintains pointer references throughout the Event Scripts and Action Scripts. Disabling it can cause unexpected results. Would you like to disable it?", "ZONE DOCTOR", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.autoPointerUpdate.Checked = !this.autoPointerUpdate.Checked;
                }
            }
            else
                this.autoPointerUpdate.Checked = !this.autoPointerUpdate.Checked;
            this.autoPointerUpdate.Checked = this.autoPointerUpdate.Checked;
        }
        // IO elements
        private void importEventScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), new SpecialPointers(), treeViewWrapper.SelectedIndex);
            //
            int[] baseOffsets = new int[Model.EventScripts.Count];
            int[] lengths = new int[Model.EventScripts.Count];
            for (int i = 0; i < lengths.Length; i++)
            {
                baseOffsets[i] = Model.EventScripts[i].BaseOffset;
                lengths[i] = Model.EventScripts[i].Length;
            }
            //
            IOElements ioelements = new IOElements(Model.EventScripts.ToArray(), index, "IMPORT EVENT SCRIPTS...");
            ioelements.ShowDialog();
            if (ioelements.DialogResult != DialogResult.OK)
                return;
            bool importAll = (bool)ioelements.Tag;
            if (importAll)
            {
                // first, update offsets for any changes made in current script
                if (treeViewWrapper.ScriptDelta != 0)
                    UpdateScriptOffsets();
                // now, update offsets following each newly imported script w/new length
                int baseOffset = 0x0A0000;
                int lastImported = 1;
                for (int i = 0; i < eventScripts.Count; i++)
                {
                    int delta = Model.EventScripts[i].Length - lengths[i];
                    treeViewWrapper.ScriptDelta += delta;
                    Model.EventScripts[i].BaseOffset = baseOffset;
                    // only refresh script if a new one was imported
                    if (delta != 0 || baseOffset != baseOffsets[i])
                        Model.EventScripts[i].Refresh();
                    // only need to update if new length
                    if (delta != 0 && lastImported != i - 1)
                    {
                        lastImported = i;
                        UpdateScriptOffsets(i);
                        treeViewWrapper.ScriptDelta = 0;
                    }
                    baseOffset += Model.EventScripts[i].Length;
                }
            }
            else if (!importAll) // if importing single script into current
            {
                UpdateScriptOffsets(index);
                eventScript.BaseOffset = baseOffsets[index];
            }
            treeViewWrapper.ChangeScript(eventScript);
            treeViewWrapper.RefreshScript();
            //
            if (!Bits.Compare(buffer.OldScript, scriptData))
                PushCommand(buffer);
        }
        private void exportEventScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new IOElements(Model.EventScripts.ToArray(), index, "EXPORT EVENT SCRIPTS...").ShowDialog();
        }
        private void dumpEventScriptTextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            saveFileDialog.FilterIndex = 0;
            saveFileDialog.FileName = "eventScripts.txt";
            saveFileDialog.RestoreDirectory = true;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                StreamWriter evtscr = File.CreateText(saveFileDialog.FileName);
                foreach (EventScript script in eventScripts)
                {
                    foreach (EventCommand esc in script.Commands)
                    {
                        if (esc.Queue != null && !esc.NonEmbeddedVehicle && !esc.NonEmbeddedMap)
                        {
                            evtscr.Write((esc.Offset + 0xC00000).ToString("X6") + ": ");
                            evtscr.Write("{" + BitConverter.ToString(esc.CommandData, 0, 2) + "}               ");
                            evtscr.Write(esc.ToString() + "\n");
                            if (esc.Queue.Commands != null)
                            {
                                foreach (ActionCommand aqc in esc.Queue.Commands)
                                {
                                    evtscr.Write("   " + (aqc.Offset + 0xC00000).ToString("X6") + ": ");
                                    evtscr.Write("{" + BitConverter.ToString(aqc.CommandData) + "}");
                                    for (int l = aqc.CommandData.Length; l < 7; l++)
                                        evtscr.Write("   ");
                                    evtscr.Write(aqc.ToString() + "\n");
                                }
                            }
                        }
                        else if (esc.NonEmbeddedVehicle || esc.NonEmbeddedMap)   // 0xd01 and 0xe91 only
                        {
                            if (esc.NonEmbeddedVehicle)
                                evtscr.Write("NON-EMBEDDED VEHICLE SCRIPT\n");
                            else if (esc.NonEmbeddedMap)
                                evtscr.Write("NON-EMBEDDED MAP SCRIPT\n");
                            if (esc.Queue.Commands != null)
                            {
                                foreach (ActionCommand aqc in esc.Queue.Commands)
                                {
                                    evtscr.Write("   " + (aqc.Offset + 0xC00000).ToString("X6") + ": ");
                                    evtscr.Write("{" + BitConverter.ToString(aqc.CommandData) + "}");
                                    for (int l = aqc.CommandData.Length; l < 7; l++)
                                        evtscr.Write("   ");
                                    evtscr.Write(aqc.ToString() + "\n");
                                }
                            }
                        }
                        else
                        {
                            evtscr.Write((esc.Offset + 0xC00000).ToString("X6") + ": ");
                            evtscr.Write("{" + BitConverter.ToString(esc.CommandData) + "}");
                            for (int k = esc.Length; k < 7; k++)
                                evtscr.Write("   ");
                            evtscr.Write(esc.ToString() + "\n");
                        }
                    }
                    evtscr.Write("\n");
                }
                evtscr.Close();
            }
        }
        private void clearEventScriptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ScriptBuffer buffer = new ScriptBuffer(Bits.Copy(scriptData), new SpecialPointers(), treeViewWrapper.SelectedIndex);
            //
            int[] lengths = new int[Model.EventScripts.Count];
            for (int i = 0; i < lengths.Length; i++)
                lengths[i] = Model.EventScripts[i].Length;
            //
            ClearElements window = new ClearElements(null, index, "CLEAR EVENT SCRIPTS...", index);
            window.ShowDialog();
            if (window.DialogResult != DialogResult.OK)
                return;
            //
            Point tag = (Point)window.Tag;
            int start = tag.X;
            int end = tag.Y;
            for (int i = start; i <= end; i++)
                treeViewWrapper.ScriptDelta += Model.EventScripts[i].Length - lengths[i];
            UpdateScriptOffsets(start);
            treeViewWrapper.RefreshScript();
            //
            if (!Bits.Compare(buffer.OldScript, scriptData))
                PushCommand(buffer);
        }
        private void reset_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("You're about to undo all changes to the current script. Go ahead with reset?",
                "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                return;
            commandStack.Clear();
            commandTree.BeginUpdate();
            //
            int length = eventScript.Length;
            // must go through every script to get actual original offset
            int offset = 0x0A0000;
            int index = 0;
            List<EventScript> temp = new List<EventScript>();
            int offsetEnd = 0x0CE600;
            while (Model.ROM[--offsetEnd] == 0xFF) ;
            while (index < eventScript.Index && offset <= offsetEnd)
                temp.Add(new EventScript(index++, ref offset));
            eventScript = new EventScript(index, ref offset);
            //
            treeViewWrapper.SelectedNode = null;
            treeViewWrapper.ScriptDelta += eventScript.Length - length;
            treeViewWrapper.ChangeScript(eventScript);
            treeViewWrapper.RefreshScript();
            //
            commandTree.EndUpdate();
        }
        // context menustrip
        private void goToDialogue_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            EventCommand temp = (EventCommand)commandTree.SelectedNode.Tag;
            int num = Bits.GetShort(temp.CommandData, 1) & 0xFFF;
            //if (Model.Program.Dialogues == null)
            //    Model.Program.CreateDialoguesWindow();
            //Model.Program.Dialogues.DialogueNum.Value = num;
            //Model.Program.Dialogues.BringToFront();
        }
        private void addMemoryToNotesDatabase_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            int address = 0;
            int addressBit = 0;
            string label = "";
            string description = "";
            if (commandTree.SelectedNode.Tag.GetType() == typeof(EventCommand))
            {
                EventCommand temp = (EventCommand)commandTree.SelectedNode.Tag;
                if (temp.Opcode == 0xBC ||
                    (temp.Opcode >= 0xC0 && temp.Opcode <= 0xCF))
                    address = ((Bits.GetShort(temp.CommandData, 1) & 0x7FFF) / 8) + 0x1E80;
                if (temp.Opcode >= 0xB7 && temp.Opcode <= 0xB9 ||
                    (temp.Opcode >= 0xD0 && temp.Opcode <= 0xDD))
                    address = 0x1DC9 + (temp.Param1 / 8);
                addressBit = temp.Param1 & 0x07;
            }
            else
            {
                ActionCommand temp = (ActionCommand)commandTree.SelectedNode.Tag;
                if (temp.Opcode >= 0xB0 && temp.Opcode <= 0xBF)
                    address = ((Bits.GetShort(temp.CommandData, 1) & 0x7FFF) / 8) + 0x1E80;
                if (temp.Type == ScriptType.Vehicle)
                {
                    if (temp.Opcode == 0xC8 || temp.Opcode == 0xC9)
                        address = 0x1E80 + (temp.Param1 / 8);
                }
                if (temp.Type != ScriptType.Vehicle)
                {
                    if (temp.Opcode == 0xE1 || temp.Opcode == 0xE4)
                        address = 0x1E80 + (temp.Param1 / 8);
                    if (temp.Opcode == 0xE2 || temp.Opcode == 0xE5)
                        address = 0x1EA0 + (temp.Param1 / 8);
                    if (temp.Opcode == 0xE3 || temp.Opcode == 0xE6)
                        address = 0x1EC0 + (temp.Param1 / 8);
                }
                addressBit = temp.Param1 & 0x07;
            }
            label = description = "[" + address.ToString("X4") + ", bit: " + addressBit.ToString() + "]";
            if (Model.Program.Project == null || !Model.Program.Project.Visible)
                Model.Program.CreateProjectWindow();
            Project note = Model.Program.Project;
            if (Model.Project == null)
                note.LoadProject();
            if (Model.Project != null)
            {
                note.AddingFromEditor("Memory Bits", address, addressBit, label, description);
                note.BringToFront();
            }
            else
            {
                MessageBox.Show("Could not add element to notes database.", "ZONE DOCTOR",
                    MessageBoxButtons.OK);
            }
        }
        private void goToOffset_Click(object sender, EventArgs e)
        {
            if (commandTree.SelectedNode == null)
                return;
            EventActionCommand temp = (EventActionCommand)commandTree.SelectedNode.Tag;
            int pointer = temp.ReadPointer() + 0x0A0000;
            foreach (EventScript script in eventScripts)
            {
                foreach (EventCommand command in script.Commands)
                {
                    if (command.Queue != null)
                    {
                        foreach (ActionCommand action in command.Queue.Commands)
                        {
                            if (action.Offset + action.CommandData.Length > pointer || action.Offset >= pointer)
                            {
                                if (command.Offset + command.Length > pointer || command.Offset >= pointer)
                                {
                                    index = script.Index;
                                    treeViewWrapper.SelectNode(command);
                                    return;
                                }
                                index = script.Index;
                                treeViewWrapper.SelectNode(action);
                                return;
                            }
                        }
                    }
                    if (command.Offset + command.Length > pointer || command.Offset >= pointer)
                    {
                        index = script.Index;
                        treeViewWrapper.SelectNode(command);
                        return;
                    }
                }
            }
        }
        #endregion
        private class CommandEdit : Command
        {
            private int index;
            private EventScripts form;
            private ScriptBuffer buffer;
            private List<EventScript> eventScripts;
            private EventScript eventScript
            {
                get
                {
                    return eventScripts[index];
                }
                set { eventScripts[index] = value; }
            }
            private TreeViewWrapper treeViewWrapper;
            private bool autoRedo = false; public bool AutoRedo() { return this.autoRedo; }
            public CommandEdit(List<EventScript> eventScripts, int index, ScriptBuffer buffer, EventScripts form)
            {
                this.form = form;
                this.index = index;
                this.buffer = buffer;
                this.eventScripts = eventScripts;
                this.treeViewWrapper = form.TreeViewWrapper;
            }
            public void Execute()
            {
                if (eventScripts != null)
                {
                    eventScripts[index].Undoing = true;
                    //
                    this.form.index = index; // then switch back to script in index
                    // now get difference in lengths
                    int length = eventScript.Length;
                    int delta = buffer.OldScript.Length - length;
                    treeViewWrapper.ScriptDelta += delta;
                    // must update special offsets before parsing the script
                    SpecialPointers newOffsets = new SpecialPointers();
                    buffer.OldPointers.Update();
                    buffer.OldPointers = newOffsets.Copy();
                    // next, switch the scripts
                    byte[] temp = Bits.Copy(eventScript.Script);
                    eventScripts[index].Script = Bits.Copy(buffer.OldScript);
                    eventScripts[index].Commands = null;
                    eventScripts[index].ParseScript();
                    buffer.OldScript = temp;
                    //
                    int newSelectedIndex = treeViewWrapper.SelectedIndex;
                    treeViewWrapper.RefreshScript(buffer.OldSelectedIndex);
                    buffer.OldSelectedIndex = newSelectedIndex;
                    //
                    eventScripts[index].Undoing = false;
                }
            }
        }
        private class ScriptBuffer
        {
            public byte[] OldScript;
            public SpecialPointers OldPointers;
            public int OldSelectedIndex;
            public ScriptBuffer(byte[] oldScript, SpecialPointers oldPointers, int oldSelectedIndex)
            {
                this.OldPointers = oldPointers;
                this.OldScript = oldScript;
                this.OldSelectedIndex = oldSelectedIndex;
            }
        }
        private class SpecialPointers
        {
            private int[] specialPointers_Vehicle; public int[] SpecialPointers_Vehicle { get { return specialPointers_Vehicle; } set { specialPointers_Vehicle = value; } }
            private int[] specialPointers_Map; public int[] SpecialPointers_Map { get { return specialPointers_Map; } set { specialPointers_Map = value; } }
            private int[] specialPointers_Cancel; public int[] SpecialPointers_Cancel { get { return specialPointers_Cancel; } set { specialPointers_Cancel = value; } }
            private int[] specialPointers_ASM; public int[] SpecialPointers_ASM { get { return specialPointers_ASM; } set { specialPointers_ASM = value; } }
            public SpecialPointers()
            {
                specialPointers_ASM = Bits.Copy(Model.SpecialPointers_ASM);
                specialPointers_Cancel = Bits.Copy(Model.SpecialPointers_Cancel);
                specialPointers_Map = Bits.Copy(Model.SpecialPointers_Map);
                specialPointers_Vehicle = Bits.Copy(Model.SpecialPointers_Vehicle);
            }
            public SpecialPointers Copy()
            {
                SpecialPointers copy = new SpecialPointers();
                copy.SpecialPointers_ASM = Bits.Copy(this.specialPointers_ASM);
                copy.SpecialPointers_Cancel = Bits.Copy(this.specialPointers_Cancel);
                copy.SpecialPointers_Map = Bits.Copy(this.specialPointers_Map);
                copy.SpecialPointers_Vehicle = Bits.Copy(this.specialPointers_Vehicle);
                return copy;
            }
            /// <summary>
            /// Stores this instance of the special offsets to the model.
            /// </summary>
            public void Update()
            {
                Model.SpecialPointers_ASM = Bits.Copy(this.specialPointers_ASM);
                Model.SpecialPointers_Cancel = Bits.Copy(this.specialPointers_Cancel);
                Model.SpecialPointers_Map = Bits.Copy(this.specialPointers_Map);
                Model.SpecialPointers_Vehicle = Bits.Copy(this.specialPointers_Vehicle);
            }
        }
    }
}