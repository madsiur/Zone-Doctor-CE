using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Runtime.InteropServices;
using ZONEDOCTOR.ScriptsEditor.Commands;

namespace ZONEDOCTOR.ScriptsEditor
{
    public class TreeViewWrapper
    {
        #region Variables
        private NewTreeView treeView;
        private TreeNode selectedNode; public TreeNode SelectedNode { get { return selectedNode; } set { selectedNode = value; } }
        public EventActionCommand SelectedCommand
        {
            get
            {
                if (selectedNode != null)
                    return (EventActionCommand)selectedNode.Tag;
                return null;
            }
        }
        private TreeNode editedNode; public TreeNode EditedNode { get { return editedNode; } set { editedNode = value; } }
        public byte[] CurrentNodeData
        {
            get
            {
                EventCommand esc;
                ActionCommand asc;
                //
                TreeNode node = treeView.SelectedNode;
                if (node == null)
                    return new byte[0];
                int index = node.Index;
                if (!IsRootNode(node))  // we are editing an embedded action queue command
                    node = node.Parent;
                else    // we are editing an event script command
                {
                    esc = script.Commands[index];
                    return esc.CommandData;
                }
                if (!IsRootNode(node))
                    throw new Exception();
                //
                esc = script.Commands[node.Index];
                asc = esc.Queue.Commands[index];
                return asc.CommandData;
            }
        }
        public int SelectedIndex
        {
            get
            {
                return treeView.GetFullIndex();
            }
            set
            {
                treeView.SelectNode(value);
            }
        }
        private EventScript script; public EventScript Script { get { return this.script; } set { this.script = value; } }
        private int scriptDelta = 0; public int ScriptDelta { get { return this.scriptDelta; } set { this.scriptDelta = value; } }
        private ArrayList commandCopies;
        #endregion
        // constructor
        public TreeViewWrapper(NewTreeView control)
        {
            this.treeView = control;
        }
        #region Functions
        public void ChangeScript(EventScript script)
        {
            this.script = script;
            foreach (EventCommand esc in script.Commands)
                esc.ResetOriginalOffset();
            Populate();
        }
        private void Populate()
        {
            this.treeView.BeginUpdate();
            List<EventCommand> scriptcmds;
            this.treeView.Nodes.Clear();
            scriptcmds = script.Commands;
            for (int i = 0; i < scriptcmds.Count; i++)
                AddCommand(scriptcmds[i]);
            this.treeView.ExpandAll();
            this.treeView.EndUpdate();
        }
        // treeview managers
        private void AddCommand(EventCommand esc)
        {
            TreeNode node = esc.Node;
            if (esc.QueueTrigger || esc.NonEmbeddedVehicle || esc.NonEmbeddedMap)
            {
                if (esc.Queue == null)
                    return;
                List<ActionCommand> queue = esc.Queue.Commands;
                for (int i = 0; queue != null && i < queue.Count; i++)
                {
                    ActionCommand asc = queue[i];
                    TreeNode child = asc.Node;
                    node.Nodes.Add(child);
                }
            }
            // Add command
            this.treeView.Nodes.Add(node);
        }
        private void AddCommand(ActionCommand asc)
        {
            // Add command
            this.treeView.Nodes.Add(asc.Node);
        }
        public void InsertNode(EventCommand esc)
        {
            if (esc.Terminator)
            {
                if (MessageBox.Show("You are about to insert a termination command -- doing so will remove all commands after the new command in this script.\n\n" +
                    "Go ahead with process?", "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            try
            {
                foreach (EventCommand es in script.Commands)
                {
                    es.Modified = false;
                    if (es.Queue == null) continue;
                    foreach (ActionCommand aq in es.Queue.Commands)
                        aq.Modified = false;
                }
                this.treeView.BeginUpdate();
                TreeNode node = treeView.SelectedNode;
                // Get index to insert at
                int index = node != null ? treeView.SelectedNode.Index + 1 : 0;
                if (index > this.treeView.LastNode.Index)
                    index = this.treeView.LastNode.Index;
                if (node == null || IsRootNode(node)) // EvenScript Command
                {
                    // Insert into treeview
                    selectedNode = esc.Node;
                    this.treeView.Nodes.Insert(index, selectedNode);
                    // update special pointers if needed
                    int delta = esc.Length;
                    if (esc.Terminator && esc.QueueTrigger)
                    {
                        for (int i = index; i < esc.Queue.Commands.Count; i++)
                            delta -= esc.Queue.Commands[i].Length;
                    }
                    esc.Offset = this.script.Commands[index].Offset;
                    Model.UpdateSpecialPointers(esc, delta);
                    // Insert into script at same index
                    esc.Modified = true;
                    this.script.Insert(index, esc);
                    this.scriptDelta += esc.Length;
                    // remove following commands if terminator
                    if (esc.Terminator)
                    {
                        while (index + 1 < this.script.Commands.Count)
                        {
                            this.scriptDelta -= this.script.Commands[index + 1].Length;
                            this.script.RemoveAt(index + 1);
                        }
                    }
                }
            }
            finally
            {
                RefreshScript(); // Update offsets and descriptions
                this.treeView.EndUpdate();
            }
        }
        public void InsertNode(ActionCommand asc)
        {
            if (asc.Terminator)
            {
                if (MessageBox.Show("You are about to insert a termination command -- doing so will remove all commands after the new command in this script.\n\n" +
                    "Go ahead with process?", "ZONE DOCTOR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                    return;
            }
            try
            {
                foreach (EventCommand command in script.Commands)
                {
                    command.Modified = false;
                    if (command.Queue == null) continue;
                    foreach (ActionCommand aqc in command.Queue.Commands)
                        aqc.Modified = false;
                }
                this.treeView.BeginUpdate();
                int index;
                TreeNode node = treeView.SelectedNode;
                // embedded action queue
                if (node == null)
                    return;
                // Get index to insert at
                index = treeView.SelectedNode.Index + 1;
                if (node.Parent == null)
                {
                    if ((script.Commands[treeView.SelectedNode.Index]).QueueTrigger)
                        index = 0;
                    else
                    {
                        MessageBox.Show(
                            "Cannot insert an action queue command outside of an action queue.",
                            "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                    node = node.Parent;
                if (node.LastNode != null && index > node.LastNode.Index)
                    index = node.LastNode.Index;
                // Increase queue length option byte
                EventCommand esc = script.Commands[node.Index];
                if (esc.TriggerType == ScriptType.Character)
                {
                    if ((esc.Length - 2 + asc.CommandData.Length) > 127)
                    {
                        MessageBox.Show(
                            "Could not add any more action queue commands to the queue trigger.",
                            "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                // Insert into treeview
                selectedNode = asc.Node;
                node.Nodes.Insert(index, selectedNode);
                // update special pointers if needed
                int delta = asc.Length;
                if (asc.Terminator)
                {
                    for (int i = index; i < esc.Queue.Commands.Count; i++)
                        delta -= esc.Queue.Commands[i].Length;
                }
                if (esc.TriggerType == ScriptType.Character)
                    esc.Param1 += (byte)delta;
                if (esc.Queue.Commands.Count > index)
                    asc.Offset = esc.Queue.Commands[index].Offset;
                else
                    asc.Offset = esc.Offset + esc.Length;
                Model.UpdateSpecialPointers(asc, delta);
                // Insert into action queue at same index
                asc.Modified = true;
                this.script.Insert(node.Index, index, asc);
                this.scriptDelta += asc.CommandData.Length;
                // remove following commands if terminator
                if (asc.Terminator)
                {
                    while (index + 1 < esc.Queue.Commands.Count)
                    {
                        this.scriptDelta -= esc.Queue.Commands[index + 1].Length;
                        this.script.RemoveAt(node.Index, index + 1);
                    }
                }
                //
                treeView.ExpandAll();
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
                this.treeView.EndUpdate();
            }
        }
        public void ReplaceNode(EventCommand esc)
        {
            try
            {
                TreeNode node = editedNode;
                if (node == null)
                    return;
                this.treeView.BeginUpdate();
                // Get index to insert at
                int index = editedNode.Index;
                selectedNode = new TreeNode(esc.ToString());
                // EvenScript Command
                if (IsRootNode(node))
                {
                    // Insert into treeview
                    this.treeView.Nodes.RemoveAt(index);
                    this.treeView.Nodes.Insert(index, esc.ToString());
                    // update special pointers if needed
                    EventCommand old = this.script.Commands[index];
                    Model.UpdateSpecialPointers(old, esc.Length - old.Length);
                    // Insert into script at same index
                    this.script.RemoveAt(index);
                    this.script.Insert(index, esc);
                    treeView.SelectedNode = this.treeView.Nodes[index];
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
                this.treeView.EndUpdate();
            }
        }
        public void ReplaceNode(ActionCommand asc)
        {
            try
            {
                this.treeView.BeginUpdate();
                TreeNode node = editedNode;
                if (node == null)
                    return;
                // Get index to insert at
                int index = editedNode.Index;
                selectedNode = new TreeNode(asc.ToString());
                if (IsRootNode(node))
                    return;
                node = node.Parent;
                if (IsRootNode(node))
                {
                    // Insert into treeview
                    node.Nodes.RemoveAt(index);
                    node.Nodes.Insert(index, asc.ToString());
                    // update special pointers if needed
                    EventCommand esc = this.script.Commands[index];
                    ActionCommand old = esc.Queue.Commands[index];
                    Model.UpdateSpecialPointers(old, asc.Length - old.Length);
                    // Insert into action queue at same index
                    this.script.RemoveAt(node.Index, index);
                    this.script.Insert(node.Index, index, asc);
                    treeView.SelectedNode = node.Nodes[index];
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
                this.treeView.EndUpdate();
            }
        }
        public void RemoveNode()
        {
            try
            {
                this.treeView.BeginUpdate();
                int delta, index;
                TreeNode node;
                TreeNode parent, child;
                for (int i = treeView.Nodes.Count - 1; i >= 0; i--)
                {
                    parent = treeView.Nodes[i];
                    for (int a = parent.Nodes.Count - 1; a >= 0; a--)
                    {
                        child = parent.Nodes[a];
                        if (!child.Checked)
                            continue;
                        delta = -((ActionCommand)child.Tag).CommandData.Length;
                        node = child;
                        if (node == null)
                            return;
                        index = child.Index;
                        node = node.Parent;
                        // Decrease queue length option byte
                        EventCommand esc = script.Commands[node.Index];
                        ActionCommand aqc = esc.Queue.Commands[index];
                        if (esc.TriggerType == ScriptType.Character)
                            esc.Param1 -= (byte)aqc.Length;
                        // update special pointers if needed
                        Model.UpdateSpecialPointers(aqc, -aqc.Length);
                        // Remove action command
                        child.Remove();
                        this.script.RemoveAt(parent.Index, child.Index);
                        this.scriptDelta += delta;
                    }
                    if (!parent.Checked)
                        continue;
                    delta = -((EventCommand)parent.Tag).CommandData.Length;
                    node = parent;
                    if (node == null)
                        return;
                    index = parent.Index;
                    if (this.script.Commands[index].NonEmbedded)
                    {
                        MessageBox.Show("One or more command(s) could not be removed -- non-embedded scripts cannot be removed.",
                            "ZONE DOCTOR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        continue;
                    }
                    // update special pointers if needed
                    EventCommand old = script.Commands[index];
                    Model.UpdateSpecialPointers(old, -old.Length);
                    // Remove event command
                    parent.Remove();
                    this.script.RemoveAt(parent.Index);
                    this.scriptDelta += delta;
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
                this.treeView.EndUpdate();
            }
        }
        public void MoveUp()
        {
            this.treeView.BeginUpdate();
            try
            {
                int index1, index2;
                foreach (TreeNode parent in treeView.Nodes)
                {
                    foreach (TreeNode child in parent.Nodes)
                    {
                        if (!child.Checked)
                            continue;
                        if (child.Index == 0)
                            break;
                        if (child == null)
                            return;
                        index1 = child.Index;
                        if (child.PrevNode == null)
                            return;
                        index2 = child.PrevNode.Index;
                        Reverse(index1, index2);
                        // if selected node is one of the checked ones
                        if (child == selectedNode)
                            selectedNode = child.PrevNode;
                    }
                    if (!parent.Checked)
                        continue;
                    if (parent.Index == 0)
                        break;
                    if (parent == null)
                        return;
                    index1 = parent.Index;
                    if (parent.PrevNode == null)
                        return;
                    index2 = parent.PrevNode.Index;
                    Reverse(index1, index2);
                    // if selected node is one of the checked ones
                    if (parent == selectedNode)
                        selectedNode = parent.PrevNode;
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
            }
            this.treeView.EndUpdate();
        }
        public void MoveDown()
        {
            this.treeView.BeginUpdate();
            try
            {
                int index1, index2;
                TreeNode parent, child;
                // final termination command in scripts and queues must remain
                for (int i = treeView.Nodes.Count - 2; i >= 0; i--)
                {
                    parent = treeView.Nodes[i];
                    for (int a = parent.Nodes.Count - 2; a >= 0; a--)
                    {
                        child = parent.Nodes[a];
                        if (!child.Checked)
                            continue;
                        if (child.Index == parent.Nodes.Count - 2)
                            break;
                        if (child == null)
                            return;
                        index1 = child.Index;
                        if (child.NextNode == null)
                            return;
                        index2 = child.NextNode.Index;
                        Reverse(index1, index2);
                        // if selected node is one of the checked ones
                        if (child == selectedNode)
                            selectedNode = child.NextNode;
                    }
                    //
                    if (!parent.Checked)
                        continue;
                    if (parent.Index == treeView.Nodes.Count - 2)
                        break;
                    if (parent == null)
                        return;
                    index1 = parent.Index;
                    if (parent.NextNode == null)
                        return;
                    index2 = parent.NextNode.Index;
                    Reverse(index1, index2);
                    // if selected node is one of the checked ones
                    if (parent == selectedNode)
                        selectedNode = parent.NextNode;
                }
            }
            finally
            {
                // Update offsets and descriptions
                RefreshScript();
            }
            this.treeView.EndUpdate();
        }
        private void Reverse(int index1, int index2)
        {
            if (IsRootNode(treeView.SelectedNode))
            {
                // update special pointers if needed, twice for each command modified
                if (index1 < index2)
                {
                    // if moving down
                    UpdateSpecialPointers(this.script.Commands[index1], this.script.Commands[index2].Length);
                    UpdateSpecialPointers(this.script.Commands[index2], -this.script.Commands[index1].Length);
                }
                else
                {
                    // if moving up
                    UpdateSpecialPointers(this.script.Commands[index2], this.script.Commands[index1].Length);
                    UpdateSpecialPointers(this.script.Commands[index1], -this.script.Commands[index2].Length);
                }
                script.Reverse(index1, index2);
            }
            else
            {
                int parent = treeView.SelectedNode.Parent.Index;
                EventCommand esc = script.Commands[parent];
                // update special pointers if needed, twice for each command modified
                if (index1 < index2)
                {
                    // if moving down
                    UpdateSpecialPointers(esc.Queue.Commands[index1], esc.Queue.Commands[index2].Length);
                    UpdateSpecialPointers(esc.Queue.Commands[index2], -esc.Queue.Commands[index1].Length);
                }
                else
                {
                    // if moving up
                    UpdateSpecialPointers(esc.Queue.Commands[index2], esc.Queue.Commands[index1].Length);
                    UpdateSpecialPointers(esc.Queue.Commands[index1], -esc.Queue.Commands[index2].Length);
                }
                esc.Queue.Reverse(index1, index2);
            }
        }
        public void Copy()
        {
            try
            {
                this.treeView.BeginUpdate();
                EventCommand esc;
                ActionCommand asc;
                TreeNode node = this.treeView.SelectedNode;
                if (node == null)
                    return;
                int index = this.treeView.SelectedNode.Index;
                bool parentChecked = false, childChecked = false;
                commandCopies = new ArrayList();
                foreach (TreeNode parent in treeView.Nodes)
                {
                    foreach (TreeNode child in parent.Nodes)
                    {
                        if (!child.Checked)
                            continue;
                        childChecked = true;
                        if (parentChecked)
                        {
                            MessageBox.Show(
                                "Cannot create a copy buffer that contains both event and action\n" +
                                "commands. Please uncheck all action OR event commands.",
                                "ZONE DOCTOR");
                            commandCopies = null;
                            return;
                        }
                        asc = (ActionCommand)child.Tag;
                        asc.Assemble();
                        commandCopies.Add(asc.Copy());
                    }
                    if (!parent.Checked)
                        continue;
                    parentChecked = true;
                    if (childChecked)
                    {
                        MessageBox.Show(
                            "Cannot create a copy buffer that contains both event and action\n" +
                            "commands. Please uncheck all action OR event commands.",
                            "ZONE DOCTOR");
                        commandCopies = null;
                        return;
                    }
                    esc = (EventCommand)parent.Tag;
                    esc.Assemble();
                    commandCopies.Add(esc.Copy());
                }
            }
            finally
            {
                this.treeView.Select();
                this.treeView.EndUpdate();
            }
        }
        public void Paste()
        {
            foreach (TreeNode parent in treeView.Nodes)
            {
                if (parent.Checked)
                    parent.Checked = false;
                foreach (TreeNode child in parent.Nodes)
                    if (child.Checked)
                        child.Checked = false;
            }
            TreeNode temp = treeView.SelectedNode;
            // pasting event command in event script
            if (commandCopies != null && (treeView.SelectedNode == null || IsRootNode(treeView.SelectedNode)))
            {
                try
                {
                    foreach (EventCommand copy in commandCopies)
                    {
                        InsertNode(copy.Copy());
                        treeView.SelectedNode = temp;
                    }
                }
                catch
                {
                    if (treeView.SelectedNode != null && treeView.SelectedNode.BackColor == Color.FromArgb(192, 224, 255))
                    {
                        foreach (ActionCommand copy in commandCopies)
                        {
                            InsertNode(copy.Copy());
                            treeView.SelectedNode = temp;
                        }
                    }
                    else
                    {
                        MessageBox.Show("You cannot paste action commands outside of an action queue.", "ZONE DOCTOR");
                        return;
                    }
                }
            }
            // pasting action command in event script
            else if (commandCopies != null)
            {
                try
                {
                    foreach (ActionCommand ascCopy in commandCopies)
                    {
                        InsertNode(ascCopy.Copy());
                        treeView.SelectedNode = temp;
                    }
                }
                catch
                {
                    MessageBox.Show("You cannot paste event commands inside of an action queue.", "ZONE DOCTOR");
                    return;
                }
            }
            this.treeView.Select();
        }
        /// <summary>
        /// Selects a node in the treeView, tagged with the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void SelectNode(EventActionCommand command)
        {
            if (command != null)
            {
                foreach (TreeNode node in treeView.Nodes)
                {
                    if (node.Tag == command)
                    {
                        treeView.SelectedNode = node;
                        return;
                    }
                    foreach (TreeNode child in node.Nodes)
                    {
                        if (child.Tag == command)
                        {
                            treeView.SelectedNode = child;
                            return;
                        }
                    }
                }
            }
        }
        private bool IsRootNode(TreeNode node)
        {
            if (node == null)
                return false;
            return node.Text.CompareTo(node.FullPath) == 0;
        }
        public void ExpandAll()
        {
            this.treeView.ExpandAll();
        }
        public void CollapseAll()
        {
            this.treeView.CollapseAll();
        }
        public void ClearAll()
        {
            this.script.Assemble();
            this.treeView.BeginUpdate();
            this.scriptDelta -= script.Length;
            this.script.Clear();
            RefreshScript();
            this.treeView.EndUpdate();
        }
        //
        public void RefreshScript()
        {
            script.Refresh();
            Populate();
            //
            if (treeView.Nodes.Count != 0 && selectedNode != null)
            {
                if (selectedNode.Parent != null)
                    selectedNode = treeView.Nodes[selectedNode.Parent.Index].Nodes[selectedNode.Index];
                else
                    selectedNode = treeView.Nodes[selectedNode.Index];
                treeView.SelectedNode = selectedNode;
            }
        }
        /// <summary>
        /// Refreshes the script and sets the selected node to the specified (full) index.
        /// </summary>
        /// <param name="fullIndex">The full index of the node to select.</param>
        public void RefreshScript(int fullIndex)
        {
            script.Refresh();
            Populate();
            //
            treeView.SelectNode(fullIndex);
        }
        /// <summary>
        /// Updates any special pointers pointing to a given command by a delta.
        /// </summary>
        /// <param name="eac">The command whose offset will be compared.</param>
        /// <param name="delta">The delta to modify any special pointers by.</param>
        private void UpdateSpecialPointers(EventActionCommand eac, int delta)
        {
            // must move backward
            if (eac.GetType() == typeof(EventCommand))
            {
                EventCommand esc = (EventCommand)eac;
                if (esc.QueueTrigger)
                {
                    for (int i = esc.Queue.Commands.Count - 1; i >= 0; i--)
                    {
                        ActionCommand asc = esc.Queue.Commands[i];
                        if (asc.SpecialOffsetVehicle != -1)
                            Model.SpecialPointers_Vehicle[asc.SpecialOffsetVehicle] += delta;
                        else if (asc.SpecialOffsetMap != -1)
                            Model.SpecialPointers_Map[asc.SpecialOffsetMap] += delta;
                        else if (asc.SpecialOffsetASM != -1)
                            Model.SpecialPointers_ASM[asc.SpecialOffsetASM] += delta;
                        else if (asc.SpecialOffsetCancel != -1)
                            Model.SpecialPointers_Cancel[asc.SpecialOffsetCancel] += delta;
                    }
                }
            }
            if (eac.SpecialOffsetVehicle != -1)
                Model.SpecialPointers_Vehicle[eac.SpecialOffsetVehicle] += delta;
            else if (eac.SpecialOffsetMap != -1)
                Model.SpecialPointers_Map[eac.SpecialOffsetMap] += delta;
            else if (eac.SpecialOffsetASM != -1)
                Model.SpecialPointers_ASM[eac.SpecialOffsetASM] += delta;
            else if (eac.SpecialOffsetCancel != -1)
                Model.SpecialPointers_Cancel[eac.SpecialOffsetCancel] += delta;
        }
        /// <summary>
        /// Nulls any special pointers pointing to a given command.
        /// </summary>
        /// <param name="eac">The command whose offset will be compared.</param>
        private void NullSpecialPointers(EventActionCommand eac)
        {
            // must move backward
            if (eac.GetType() == typeof(EventCommand))
            {
                EventCommand esc = (EventCommand)eac;
                if (esc.QueueTrigger)
                {
                    for (int i = esc.Queue.Commands.Count - 1; i >= 0; i--)
                    {
                        ActionCommand asc = esc.Queue.Commands[i];
                        if (asc.SpecialOffsetVehicle != -1)
                            Model.SpecialPointers_Vehicle[asc.SpecialOffsetVehicle] = 0;
                        else if (asc.SpecialOffsetMap != -1)
                            Model.SpecialPointers_Map[asc.SpecialOffsetMap] = 0;
                        else if (asc.SpecialOffsetASM != -1)
                            Model.SpecialPointers_ASM[asc.SpecialOffsetASM] = 0;
                        else if (asc.SpecialOffsetCancel != -1)
                            Model.SpecialPointers_Cancel[asc.SpecialOffsetCancel] = 0;
                    }
                }
            }
            if (eac.SpecialOffsetVehicle != -1)
                Model.SpecialPointers_Vehicle[eac.SpecialOffsetVehicle] = 0;
            else if (eac.SpecialOffsetMap != -1)
                Model.SpecialPointers_Map[eac.SpecialOffsetMap] = 0;
            else if (eac.SpecialOffsetASM != -1)
                Model.SpecialPointers_ASM[eac.SpecialOffsetASM] = 0;
            else if (eac.SpecialOffsetCancel != -1)
                Model.SpecialPointers_Cancel[eac.SpecialOffsetCancel] = 0;
        }
        #endregion
    }
}
