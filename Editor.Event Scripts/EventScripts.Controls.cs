using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZONEDOCTOR.ScriptsEditor;
using ZONEDOCTOR.ScriptsEditor.Commands;

namespace ZONEDOCTOR
{
    public partial class EventScripts : NewForm
    {
        private void ControlDisassembleEvent()
        {
            this.Updating = true;
            panelCommands.SuspendDrawing();
            int[] tree = categoryCommand;
            if (tree != null)
            {
                categories.SelectedIndex = tree[0];
                commands.SelectedIndex = tree[1];
            }
            switch (esc.Opcode)
            {
                // Objects	
                default: 	// Action queue...
                    if (esc.Opcode <= 0x34)
                    {
                        groupBoxA.Text = commandText;
                        labelEvtA1.Text = "Object";
                        evtNameA1.Items.AddRange(Lists.ObjectNames);
                        evtNameA1.Enabled = true;
                        evtNameA1.SelectedIndex = esc.Opcode;
                        //
                        evtEffects.Items.AddRange(new string[] { "wait until complete" }); evtEffects.Enabled = true;
                        evtEffects.SetItemChecked(0, (esc.Param1 & 0x80) == 0x80);
                    }
                    break;
                case 0x35: 	// Pause execution until action queue for object is complete...
                case 0x36: 	// Disable ability to pass through other objects for object...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    evtNameA1.Items.AddRange(Lists.ObjectNames);
                    evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    break;
                case 0x37: 	// Assign graphics to object...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    labelEvtA2.Text = "Graphics";
                    evtNameA1.Items.AddRange(Lists.ObjectNames);
                    evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    evtNumA2.Enabled = true;
                    evtNumA2.Hexadecimal = true;
                    evtNumA2.Value = esc.Param2;
                    break;
                case 0x3C: 	// Set up the party as follows...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Slot 0";
                    labelEvtA2.Text = "Slot 1";
                    labelEvtA3.Text = "Slot 2";
                    labelEvtA4.Text = "Slot 3";
                    evtNameA1.Items.AddRange(Model.ObjectNames); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    evtNameA2.Items.AddRange(Model.ObjectNames); evtNameA2.Enabled = true;
                    evtNameA2.SelectedIndex = esc.Param2;
                    evtNameA3.Items.AddRange(Model.ObjectNames); evtNameA3.Enabled = true;
                    evtNameA3.SelectedIndex = esc.Param3;
                    evtNameA4.Items.AddRange(Model.ObjectNames); evtNameA4.Enabled = true;
                    evtNameA4.SelectedIndex = esc.Param4;
                    break;
                case 0x3D: 	// Create object...
                case 0x3E: 	// Delete object...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object $";
                    evtNumA1.Hexadecimal = true;
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0x3F: 	// Assign/remove character in party...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Change";
                    labelEvtA2.Text = "Character";
                    labelEvtA3.Text = "Party";
                    evtNameA1.Items.AddRange(new string[] { "Assign", "Remove" });
                    evtNameA1.SelectedIndex = esc.Param2 != 0 ? 0 : 1; evtNameA1.Enabled = true;
                    evtNameA2.Items.AddRange(Model.ObjectNames); evtNameA2.Enabled = true;
                    evtNameA2.SelectedIndex = esc.Param1;
                    if (esc.Param2 != 0)
                    {
                        evtNumA3.Value = esc.Param2;
                        evtNumA3.Enabled = true;
                    }
                    break;
                case 0x40: 	// Assign properties to character...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Properties";
                    labelEvtA2.Text = "Character";
                    evtNumA1.Value = esc.Param2; evtNumA1.Enabled = true;
                    evtNameA2.Items.AddRange(Model.ObjectNames); evtNameA2.Enabled = true;
                    evtNameA2.SelectedIndex = esc.Param1; evtNameA2.Enabled = true;
                    break;
                case 0x41: 	// Show object...
                case 0x42: 	// Hide object...
                    goto case 0x3E;
                case 0x43: 	// Assign palette to character...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Palette";
                    labelEvtA2.Text = "Character";
                    evtNumA1.Value = esc.Param2; evtNumA1.Enabled = true;
                    evtNameA2.Items.AddRange(Model.ObjectNames); evtNameA2.Enabled = true;
                    evtNameA2.SelectedIndex = esc.Param1; evtNameA2.Enabled = true;
                    break;
                case 0x44: 	// Place character on vehicle...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Character";
                    labelEvtA2.Text = "Vehicle";
                    evtNameA1.Items.AddRange(Model.ObjectNames); evtNameA1.Enabled = true;
                    evtNameA2.Items.AddRange(Lists.Vehicles); evtNameA2.Enabled = true;
                    evtEffects.Items.AddRange(new string[] { "Character is shown" }); evtEffects.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    evtNameA2.SelectedIndex = esc.Param2 >> 5;
                    evtEffects.SetItemChecked(0, (esc.Param2 & 0x80) == 0x80);
                    break;
                case 0x46: 	// Make party # the current party...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Party";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0x78: 	// Enable ability to pass through other objects for object...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    evtNameA1.Items.AddRange(Model.ObjectNames); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    break;
                case 0x7A: 	// Change event address for object to address...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Object";
                    labelEvtA2.Text = "Address";
                    evtNumA2.Maximum = 0x03FFFF; evtNumA2.Hexadecimal = true;
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    evtNumA2.Value = Bits.GetInt24(esc.CommandData, 2); evtNumA2.Enabled = true;
                    break;
                case 0x7C: 	// Enable activation of event for object if it comes into contact with any party...
                case 0x7D: 	// Disable activation of event for object...
                    goto case 0x78;
                // Party	
                case 0x77: 	// Perform level averaging on character and calculate new maximum HP/MP...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Character";
                    evtNameA1.Items.AddRange(Model.CharacterNames.Names); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    break;
                case 0x7F: 	// Change character's name to...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Character";
                    labelEvtA2.Text = "New name";
                    evtNameA1.Items.AddRange(Model.CharacterNames.Names); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    evtNameA2.Items.AddRange(Model.CharacterNames.Names); evtNameA2.Enabled = true;
                    evtNameA2.SelectedIndex = esc.Param2;
                    break;
                case 0x80: 	// Add item to inventory...
                case 0x81: 	// Remove item from inventory...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Item";
                    evtNameA1.Items.AddRange(Lists.Numerize(Model.ItemNames.Names)); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    break;
                case 0x84: 	// Give GP to party...
                case 0x85: 	// Take GP from party...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "GP";
                    evtNumA1.Maximum = 65535;
                    evtNumA1.Value = Bits.GetShort(esc.CommandData, 1); evtNumA1.Enabled = true;
                    break;
                case 0x86: 	// Give esper to party...
                case 0x87: 	// Take esper from party...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Esper";
                    evtNameA1.Items.AddRange(Model.EsperNames.Names); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = Math.Max(0, esc.Param1 - 0x36);
                    break;
                case 0x88: 	// Remove the following status ailments from character...
                case 0x89: 	// Inflict the following status ailments on character...
                case 0x8A: 	// Toggle the following status ailments for character...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Character";
                    evtNameA1.Items.AddRange(Model.CharacterNames.Names); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    evtEffects.Height = 68 * 2;
                    evtEffects.Items.AddRange(Lists.StatusNames);
                    for (int i = 0; i < 16; i++)
                        evtEffects.SetItemChecked(i, Bits.GetBit(esc.CommandData, 2, i));
                    evtEffects.Enabled = true;
                    break;
                case 0x8B: 	// For character, take HP and set to maximum...
                case 0x8C: 	// For character, take MP and set to maximum...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Character";
                    labelEvtA2.Text = "Change";
                    labelEvtA3.Text = "2^";
                    evtNameA1.Items.AddRange(Model.CharacterNames.Names); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    evtNameA2.Items.AddRange(new string[] { "Add", "Subtract" });
                    evtNameA2.SelectedIndex = esc.Param2 >> 7;
                    evtNumA3.Value = esc.Param2 & 0x7F; evtNumA3.Enabled = true;
                    break;
                case 0x8D: 	// Remove all equipment from character...
                case 0x9C: 	// Place optimum equipment on character...
                    goto case 0x77;
                // Battle	
                case 0x4C: 	// Center screen on party and invoke battle...
                case 0x4D: 	// Invoke battle...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Enemy set";
                    labelEvtA2.Text = "Background";
                    evtNumA1.Value = esc.Param1; evtNumA1.Hexadecimal = true;
                    evtNumA1.Enabled = true;
                    evtNameA2.Items.AddRange(Lists.Numerize(Lists.BattlefieldNames));
                    evtNameA2.DropDownWidth = 300;
                    evtNameA2.SelectedIndex = esc.Param2 & 0x3F; evtNameA2.Enabled = true;
                    evtEffects.Items.AddRange(new string[] { "swoosh sound disabled", "mosaic effect disabled" });
                    evtEffects.SetItemChecked(0, Bits.GetBit(esc.CommandData, 2, 6));
                    evtEffects.SetItemChecked(1, Bits.GetBit(esc.CommandData, 2, 7));
                    evtEffects.Enabled = true;
                    break;
                // Locations	
                case 0x6A: 	// Load map after fade out...
                case 0x6B: 	// Load map instantly...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Location";
                    labelEvtA2.Text = "Upper bits";
                    labelEvtA3.Text = "Party mode";
                    labelEvtA4.Text = "Facing";
                    labelEvtA5.Text = "@ X";
                    labelEvtA6.Text = "@ Y";
                    int map = Bits.GetShort(esc.CommandData, 1);
                    evtNameA1.DropDownWidth = 300;
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.LocationNames));
                    evtNameA1.Items.Add("previous world map, skipping reposition function");
                    evtNameA1.Items.Add("world map");
                    evtNameA1.Items.Add("<unknown or out-of-range map index>");
                    if ((map & 0x01FF) <= 0x019E)
                        evtNameA1.SelectedIndex = map & 0x01FF;
                    else if ((map & 0x01FF) == 0x01FE)
                        evtNameA1.SelectedIndex = 0x019F;
                    else if ((map & 0x01FF) == 0x01FF)
                        evtNameA1.SelectedIndex = 0x01A0;
                    else
                        evtNameA1.SelectedIndex = 0x01A1;
                    evtNameA1.Enabled = true;
                    evtNumA2.Value = map >> 9; evtNumA2.Maximum = 127; evtNumA2.Enabled = true;
                    evtNameA3.Items.AddRange(new string[] { "none", 
                        "party is in the airship", "party is in overworld chocobo mode", "flags" });
                    if (esc.Param5 <= 2)
                        evtNameA3.SelectedIndex = esc.Param5;
                    else
                        evtNameA3.SelectedIndex = 3;
                    evtNameA3.DropDownWidth = 200;
                    evtNameA3.Enabled = true;
                    evtNumA3.Value = esc.Param5; evtNumA3.Enabled = esc.Param5 > 2;
                    evtNameA4.Items.AddRange(new string[] { "up", "right", "down", "left" });
                    evtNameA4.SelectedIndex = (map >> 12) & 3; evtNameA4.Enabled = true;
                    evtNumA5.Value = esc.Param3; evtNumA5.Enabled = true;
                    evtNumA6.Value = esc.Param4; evtNumA6.Enabled = true;
                    break;
                case 0x6C: 	// Set parent map...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Location";
                    labelEvtA4.Text = "Facing";
                    labelEvtA5.Text = "@ X";
                    labelEvtA6.Text = "@ Y";
                    map = Bits.GetShort(esc.CommandData, 1);
                    evtNameA1.Items.AddRange(Lists.LocationNames);
                    evtNameA1.Items.Add("previous world map, skipping reposition function");
                    evtNameA1.Items.Add("world map");
                    evtNameA1.Items.Add("<unknown or out-of-range map index>");
                    if ((map & 0x01FF) <= 0x019E)
                        evtNameA1.SelectedIndex = map & 0x01FF;
                    else if ((map & 0x01FF) == 0x01FE)
                        evtNameA1.SelectedIndex = 0x019F;
                    else if ((map & 0x01FF) == 0x01FF)
                        evtNameA1.SelectedIndex = 0x01A0;
                    else
                        evtNameA1.SelectedIndex = 0x01A1;
                    evtNameA4.Items.AddRange(new string[] { "up", "right", "down", "left" });
                    evtNameA4.SelectedIndex = esc.Param5; evtNameA4.Enabled = true;
                    evtNumA5.Value = esc.Param3; evtNumA5.Enabled = true;
                    evtNumA6.Value = esc.Param4; evtNumA6.Enabled = true;
                    break;
                case 0x73: 	// Replace current map's Layer 1 with the following chunk...
                case 0x74: 	// Replace current map's Layer 2 with the following chunk...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "From layer";
                    labelEvtA2.Text = "Refresh";
                    labelEvtA3.Text = "@ X";
                    labelEvtA4.Text = "@ Y";
                    labelEvtA5.Text = "Width";
                    labelEvtA6.Text = "Height";
                    evtNumA1.Value = esc.Param2 >> 6;
                    evtNameA2.Items.AddRange(new string[] { "refresh immediately", "wait for opcode $75 to refresh", "error" });
                    if (esc.Opcode == 0x73)
                        evtNameA2.SelectedIndex = 0;
                    else if (esc.Opcode == 0x74)
                        evtNameA2.SelectedIndex = 1;
                    else
                        evtNameA2.SelectedIndex = 2;
                    evtNumA3.Value = esc.Param1;
                    evtNumA4.Value = esc.Param2 & 0x3F;
                    evtNumA5.Value = esc.Param4;
                    evtNumA6.Value = esc.Param3;
                    break;
                case 0x79: 	// Place party # on map...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Party";
                    labelEvtA2.Text = "Map";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    map = Bits.GetShort(esc.CommandData, 2);
                    evtNameA2.Items.AddRange(Lists.Numerize(Lists.LocationNames));
                    evtNameA2.DropDownWidth = 300;
                    evtNameA2.Items.Add("previous world map, skipping reposition function");
                    evtNameA2.Items.Add("world map");
                    evtNameA2.Items.Add("<unknown or out-of-range map index>");
                    if ((map & 0x01FF) <= 0x019E)
                        evtNameA2.SelectedIndex = map & 0x01FF;
                    else if ((map & 0x01FF) == 0x01FE)
                        evtNameA2.SelectedIndex = 0x019F;
                    else if ((map & 0x01FF) == 0x01FF)
                        evtNameA2.SelectedIndex = 0x01A0;
                    else
                        evtNameA2.SelectedIndex = 0x01A1;
                    evtNameA2.Enabled = true;
                    break;
                case 0x7E: 	// Move characters to coords...
                    groupBoxC.Text = commandText;
                    labelEvtC1.Text = "X";
                    labelEvtC2.Text = "Y";
                    evtNumC1.Value = esc.Param1; evtNumC1.Enabled = true;
                    evtNumC2.Value = esc.Param2; evtNumC2.Enabled = true;
                    break;
                // Menus	
                case 0x98: 	// Invoke name change screen for character...
                    goto case 0x77;
                case 0x99: 	// Invoke party selection screen...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Groups";
                    groupBoxB.Text = "Characters";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    evtEffects.Height = 68 * 2;
                    evtEffects.Items.AddRange(Lists.Resize(Model.CharacterNames.Names, 16));
                    int characters = Bits.GetShort(esc.CommandData, 2);
                    for (int i = 0; i < 16; i++)
                        evtEffects.SetItemChecked(i, Bits.GetBit(characters, i));
                    evtEffects.Enabled = true;
                    break;
                case 0x9B: 	// Invoke shop...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Shop $";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                // Dialogues	
                case 0x48: 	// Display dialogue message, continue executing commands...
                case 0x4B: 	// Display dialogue message, wait for button press...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Dialogue";
                    int temp = Bits.GetShort(esc.CommandData, 1);
                    evtNameA1.Items.AddRange(Lists.Numerize(DialogueNames())); evtNameA1.DropDownWidth = 300;
                    evtNameA1.SelectedIndex = temp & 0x3FFF; evtNameA1.Enabled = true;
                    evtEffects.Items.AddRange(new string[] { "Show text only", "At bottom of screen" });
                    evtEffects.SetItemChecked(0, (temp & 0x4000) == 0x4000);
                    evtEffects.SetItemChecked(1, (temp & 0x8000) == 0x8000);
                    evtEffects.Enabled = true;
                    break;
                case 0xB6: 	// Indexed branch based on prior dialogue selection...
                    groupBoxD.Text = commandText;
                    evtButtonD4.Text = "New Branch"; evtButtonD4.Enabled = true;
                    evtButtonD5.Text = "Delete Branch";
                    if (esc.CommandData.Length < 4)
                        esc.CommandData = new byte[] { 0xB6, 0x00, 0x00, 0x00 };
                    int offset = 1;
                    for (; offset + 2 < esc.CommandData.Length; offset += 3)
                    {
                        int pointer = Bits.GetInt24(esc.CommandData, offset);
                        evtListBoxD.Items.Add("Branch to $" + (pointer + 0xCA0000).ToString("X6"));
                    }
                    evtListBoxD.SelectedIndex = 0; evtListBoxD.Enabled = true;
                    evtButtonD5.Enabled = evtListBoxD.Items.Count > 1;
                    labelEvtD1.Text = "Offset";
                    evtNumD1.Minimum = 0xCA0000; evtNumD1.Maximum = 0xCCE5FF; evtNumD1.Hexadecimal = true;
                    evtNumD1.Value = Bits.GetInt24(esc.CommandData, 1) + 0xCA0000; evtNumD1.Enabled = true;
                    break;
                // Events	
                case 0xA7: 	// Create a rotating pyramid around character...
                    goto case 0x36;
                case 0xBA: 	// Play ending cinematic...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Ending $";
                    evtNumA1.Hexadecimal = true;
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                // Jump to	
                case 0xA1: 	// Reset timer...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Timer";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0xB0: 	// Execute the following commands until $B1 # times...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Times";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0xB2: 	// Call subroutine...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Subroutine";
                    int subroutine = Bits.GetInt24(esc.CommandData, 1);
                    evtNumA1.Maximum = 0xCCE5FF; evtNumA1.Minimum = 0xCA0000;
                    evtNumA1.Hexadecimal = true; evtNumA1.Width = 70;
                    evtNumA1.Value = subroutine + 0xCA0000; evtNumA1.Enabled = true;
                    break;
                case 0xB3: 	// Call subroutine # times...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Subroutine";
                    labelEvtA2.Text = "Times";
                    subroutine = Bits.GetInt24(esc.CommandData, 2);
                    evtNumA1.Maximum = 0xCCE5FF; evtNumA1.Minimum = 0xCA0000;
                    evtNumA1.Value = subroutine + 0xCA0000; evtNumA1.Enabled = true;
                    evtNumA1.Hexadecimal = true; evtNumA1.Width = 70;
                    evtNumA2.Value = esc.Param1; evtNumA2.Enabled = true;
                    break;
                case 0xB7: 	// If bit $1DC9 is clear, branch to...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Address";
                    labelEvtA2.Text = "Bit";
                    labelEvtA3.Text = "Jump to";
                    evtNumA1.Maximum = 0x1DC9 + 0x1F; evtNumA1.Minimum = 0x1DC9;
                    evtNumA1.Value = 0x1DC9 + (esc.Param1 / 8); evtNumA1.Enabled = true;
                    evtNumA1.Hexadecimal = true;
                    evtNumA2.Value = esc.Param1 & 7; evtNumA2.Enabled = true;
                    offset = Bits.GetInt24(esc.CommandData, 2);
                    evtNumA3.Maximum = 0xCCE5FF; evtNumA3.Minimum = 0xCA0000;
                    evtNumA3.Hexadecimal = true; evtNumA3.Width = 70;
                    evtNumA3.Value = offset + 0xCA0000; evtNumA3.Enabled = true;
                    break;
                case 0xB8: 	// Set bit $1DC9...
                case 0xB9: 	// Clear bit $1DC9...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Address";
                    labelEvtA2.Text = "Bit";
                    evtNumA1.Maximum = 0x1DC9 + 0x1F; evtNumA1.Minimum = 0x1DC9;
                    evtNumA1.Value = 0x1DC9 + (esc.Param1 / 8); evtNumA1.Enabled = true;
                    evtNumA1.Hexadecimal = true;
                    evtNumA2.Maximum = 7; evtNumA2.Enabled = true;
                    evtNumA2.Value = esc.Param1 & 7;
                    break;
                case 0xBC: 	// If bit $1E80...
                    labelEvtA1.Text = "Address";
                    labelEvtA2.Text = "Bit";
                    int memory = Bits.GetShort(esc.CommandData, 1);
                    evtNumA1.Maximum = 0x1E80 + 0xFFF; evtNumA1.Minimum = 0x1E80;
                    evtNumA1.Value = (memory & 0x7FFF) / 8 + 0x1E80; evtNumA1.Enabled = true;
                    evtNumA1.Hexadecimal = true;
                    evtNumA2.Maximum = 7; evtNumA2.Enabled = true;
                    evtNumA2.Value = memory & 7;
                    evtNameA3.Items.AddRange(new string[] { "is clear", "is set" });
                    evtNameA3.SelectedIndex = memory >> 15; evtNameA3.Enabled = true;
                    break;
                case 0xBD: 	// Pseudo-randomly jump to offset 50% of the time...
                    labelEvtA3.Text = "Jump to";
                    offset = Bits.GetInt24(esc.CommandData, 1);
                    evtNumA3.Maximum = 0xCCE5FF; evtNumA3.Minimum = 0xCA0000;
                    evtNumA3.Value = offset + 0xCA0000; evtNumA3.Enabled = true;
                    evtNumA3.Hexadecimal = true; evtNumA3.Width = 70;
                    break;
                case 0xBE: 	// If # is in the current CaseWord, jump to subroutine...
                    groupBoxD.Text = commandText;
                    labelEvtD1.Text = "Value";
                    labelEvtD2.Text = "Jump to";
                    evtButtonD4.Text = "New Condition"; evtButtonD4.Enabled = true;
                    evtButtonD5.Text = "Delete Condition";
                    if (esc.CommandData.Length < 5)
                        esc.CommandData = new byte[] { 0xBE, 0x01, 0x00, 0x00, 0x00 };
                    int counter = 0;
                    while (counter < esc.Param1)
                    {
                        int value = Bits.GetInt24(esc.CommandData, (counter * 3) + 2);
                        if (counter == 0)
                            evtListBoxD.Items.Add("if CW = " + ((value & 0xF00000) >> 20) + ", goto $" + ((value & 0x0FFFFF) + 0xCA0000).ToString("X6"));
                        else
                            evtListBoxD.Items.Add("else if CW = " + ((value & 0xF00000) >> 20) + ", goto $" + ((value & 0x0FFFFF) + 0xCA0000).ToString("X6"));
                        counter++;
                    }
                    evtListBoxD.SelectedIndex = 0; evtListBoxD.Enabled = true;
                    evtButtonD5.Enabled = evtListBoxD.Items.Count > 1;
                    evtNumD1.Maximum = 15;
                    evtNumD1.Value = esc.Param4 >> 4; evtNumD1.Enabled = true;
                    evtNumD2.Minimum = 0xCA0000; evtNumD2.Maximum = 0xCCE5FF; evtNumD2.Hexadecimal = true;
                    evtNumD2.Value = Bits.GetInt24(esc.CommandData, 2) + 0xCA0000; evtNumD2.Enabled = true;
                    evtEffects.Items.Add("bit 7"); evtEffects.Enabled = true;
                    evtEffects.SetItemChecked(0, Bits.GetBit(esc.Param1, 7));
                    break;
                // Screen	
                case 0x50: 	// Tint screen (cumulative) with color...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Color $";
                    evtNumA1.Hexadecimal = true;
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0x51: 	// Modify background color range...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Operation";
                    labelEvtA2.Text = "Color";
                    labelEvtA3.Text = "Intensity";
                    labelEvtA5.Text = "From";
                    labelEvtA6.Text = "To";
                    evtNameA1.Items.AddRange(new string[] { "Add", "Subtract", "Do <unknown operation>" });
                    if ((esc.Param1 & 0xE0) == 0x20)
                        evtNameA1.SelectedIndex = 0;
                    else if ((esc.Param1 & 0xE0) == 0xA0)
                        evtNameA1.SelectedIndex = 1;
                    else
                        evtNameA1.SelectedIndex = 2;
                    evtNameA1.Enabled = true;
                    evtNumA2.Maximum = 7; evtNumA2.Enabled = true;
                    evtNumA2.Value = esc.Param1 & 0x1C >> 2;
                    evtNumA3.Value = esc.Param1 & 3; evtNumA3.Enabled = true;
                    evtNumA5.Value = esc.Param2; evtNumA5.Hexadecimal = true; evtNumA5.Enabled = true;
                    evtNumA6.Value = esc.Param3; evtNumA6.Hexadecimal = true; evtNumA6.Enabled = true;
                    break;
                case 0x52: 	// Tint characters (cumulative) with color...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Color $";
                    evtNumA1.Hexadecimal = true;
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0x53: 	// Modify object color range from...
                    goto case 0x51;
                case 0x55: 	// Flash screen with color component(s)...
                case 0x56: 	// Increase color component(s)...
                case 0x57: 	// Decrease color component(s)...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Color";
                    labelEvtA2.Text = "Intensity";
                    int x = (esc.Param1 & 0xF0) >> 4;
                    int y = (esc.Param1 & 0x0F);
                    evtNameA1.Items.AddRange(Lists.ColorNames);
                    evtNameA1.SelectedIndex = x; evtNameA1.Enabled = true;
                    evtNumA2.Value = y; evtNumA2.Enabled = true;
                    evtNumA2.Maximum = 15;
                    break;
                case 0x58: 	// Shake screen...
                case 0x59: 	// Unfade screen at speed...
                case 0x5A: 	// Fade screen at speed...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Speed $";
                    evtNumA1.Hexadecimal = true;
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0x5D: 	// Scroll Layer 1, speed...
                case 0x5E: 	// Scroll Layer 2, speed...
                case 0x5F: 	// Scroll Layer 3, speed...
                    groupBoxA.Text = commandText;
                    labelEvtA5.Text = "Speed";
                    labelEvtA6.Text = "by";
                    evtNumA5.Value = esc.Param1; evtNumA5.Enabled = true;
                    evtNumA6.Value = esc.Param2; evtNumA6.Enabled = true;
                    break;
                case 0x60: 	// Change background layer to palette...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "BG layer";
                    labelEvtA2.Text = "Palette";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    evtNumA2.Value = esc.Param2; evtNumA2.Enabled = true;
                    evtNumA2.Hexadecimal = true;
                    break;
                case 0x61: 	// Colorize color range to color...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Color";
                    labelEvtA5.Text = "Low";
                    labelEvtA6.Text = "High";
                    evtNumA1.Hexadecimal = true;
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    evtNumA5.Hexadecimal = true;
                    evtNumA5.Value = esc.Param2; evtNumA5.Enabled = true;
                    evtNumA6.Hexadecimal = true;
                    evtNumA6.Value = esc.Param3; evtNumA6.Enabled = true;
                    break;
                case 0x62: 	// Mosaic screen, speed...
                case 0x63: 	// Create spotlight effect with radius...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Speed";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0x70: 	// Scroll Layer 1...
                case 0x71: 	// Scroll Layer 2...
                case 0x72: 	// Scroll Layer 3...
                    groupBoxA.Text = commandText;
                    labelEvtA5.Text = "256 x";
                    labelEvtA6.Text = "+";
                    evtNumA5.Value = esc.Param1; evtNumA5.Enabled = true;
                    evtNumA6.Value = esc.Param2; evtNumA6.Enabled = true;
                    break;
                // Audio	
                case 0xEF: 	// Play song at volume...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Song";
                    labelEvtA2.Text = "Volume";
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.MusicNames));
                    evtNameA1.DropDownWidth = 200;
                    evtNameA1.SelectedIndex = esc.Param1 & 0x7F; evtNameA1.Enabled = true;
                    evtNumA2.Value = esc.Param2; evtNumA2.Enabled = true;
                    evtEffects.Items.Add("high bit set"); evtEffects.Enabled = true;
                    evtEffects.SetItemChecked(0, (esc.Param1 & 0x80) == 0x80);
                    break;
                case 0xF0: 	// Play song at full volume...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Song";
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.MusicNames));
                    evtNameA1.DropDownWidth = 200;
                    evtNameA1.SelectedIndex = esc.Param1 & 0x7F; evtNameA1.Enabled = true;
                    evtEffects.Items.Add("high bit set"); evtEffects.Enabled = true;
                    evtEffects.SetItemChecked(0, (esc.Param1 & 0x80) == 0x80);
                    break;
                case 0xF1: 	// Fade in song with transition time...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Song";
                    labelEvtA2.Text = "Time";
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.MusicNames));
                    evtNameA1.DropDownWidth = 200;
                    evtNameA1.SelectedIndex = esc.Param1 & 0x7F; evtNameA1.Enabled = true;
                    evtNumA2.Value = esc.Param2; evtNumA2.Enabled = true;
                    evtEffects.Items.Add("high bit set"); evtEffects.Enabled = true;
                    evtEffects.SetItemChecked(0, (esc.Param1 & 0x80) == 0x80);
                    break;
                case 0xF2: 	// Fade out current song with transition time...
                case 0xF3: 	// Fade in previously faded out song with transition time...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Time";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0xF4: 	// Play sound effect...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Sound";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0xF5: 	// Play sound effect (w/transition time & speaker balance)...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Sound";
                    labelEvtA2.Text = "Time";
                    labelEvtA3.Text = "Balance";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    evtNumA2.Value = esc.Param2; evtNumA2.Enabled = true;
                    evtNumA3.Value = esc.Param3; evtNumA3.Enabled = true;
                    break;
                case 0xF6: 	// Play song at volume (subcommands)...
                    labelEvtA1.Text = "Subcommand";
                    evtNameA1.Items.AddRange(Lists.SubCommands);
                    evtNameA1.DropDownWidth = 250;
                    switch (esc.Param1)
                    {
                        case 0x10: evtNameA1.SelectedIndex = 0; goto case 0x11;
                        case 0x11:
                            if (esc.Param1 == 0x11)
                                evtNameA1.SelectedIndex = 1;
                            groupBoxA.Text = "Play song at volume (other/unknown effects)";
                            labelEvtA2.Text = "Song";
                            labelEvtA3.Text = "Volume";
                            evtNameA2.Items.AddRange(Lists.MusicNames);
                            evtNameA2.SelectedIndex = esc.Param2 & 0x7F; evtNameA2.Enabled = true;
                            evtNumA3.Value = esc.Param3; evtNumA3.Enabled = true;
                            break;
                        case 0x80:
                            groupBoxA.Text = "Set volume of currently playing song/sound...";
                            evtNameA1.SelectedIndex = 2;
                            labelEvtA2.Text = "Volume";
                            goto case 0x85;
                        case 0x81:
                            groupBoxA.Text = "Change volume of currently playing song...";
                            evtNameA1.SelectedIndex = 3;
                            labelEvtA2.Text = "Volume";
                            goto case 0x85;
                        case 0x82:
                            groupBoxA.Text = "Change volume of currently playing sound...";
                            evtNameA1.SelectedIndex = 4;
                            labelEvtA2.Text = "Volume";
                            goto case 0x85;
                        case 0x83:
                            groupBoxA.Text = "Change pan control of currently playing sound...";
                            evtNameA1.SelectedIndex = 5;
                            labelEvtA2.Text = "Pan control";
                            goto case 0x85;
                        case 0x84:
                            groupBoxA.Text = "Change tempo of currently playing song...";
                            evtNameA1.SelectedIndex = 6;
                            labelEvtA2.Text = "Tempo";
                            goto case 0x85;
                        case 0x85:
                            if (esc.Param1 == 0x85)
                            {
                                groupBoxA.Text = "Change pitch of currently playing song...";
                                evtNameA1.SelectedIndex = 7;
                                labelEvtA1.Text = "Pitch";
                            }
                            labelEvtA3.Text = "Time";
                            evtNumA2.Value = esc.Param3; evtNumA2.Enabled = true;
                            evtNumA3.Value = esc.Param2; evtNumA3.Enabled = true;
                            break;
                        case 0xF1:
                            groupBoxA.Text = "Stop currently playing song, unused bytes...";
                            evtNameA1.SelectedIndex = 8;
                            goto default;
                        case 0xF2:
                            groupBoxA.Text = "Stop currently playing sound, unused bytes...";
                            evtNameA1.SelectedIndex = 9;
                            goto default;
                        default:
                            if (groupBoxA.Text == "")
                            {
                                groupBoxA.Text = "Unknown bytes...";
                                evtNameA1.SelectedIndex = 10;
                            }
                            labelEvtA2.Text = "Byte 2";
                            labelEvtA3.Text = "Byte 3";
                            evtNumA2.Value = esc.Param3; evtNumA2.Enabled = true;
                            evtNumA3.Value = esc.Param2; evtNumA3.Enabled = true;
                            break;
                    }
                    evtNameA1.Enabled = true;
                    break;
                case 0xF9: 	// Pause execution until the music passes through predetermined point...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Point";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                // Memory	
                case 0xC0: 	// If $1E80 bit is set/clear, branch to...
                case 0xC1: 	// If $1E80 bit is set/clear, branch to (2 OR conditionals)...
                case 0xC2: 	// If $1E80 bit is set/clear, branch to (3 OR conditionals)...
                case 0xC3: 	// If $1E80 bit is set/clear, branch to (4 OR conditionals)...
                case 0xC4: 	// If $1E80 bit is set/clear, branch to (5 OR conditionals)...
                case 0xC5: 	// If $1E80 bit is set/clear, branch to (6 OR conditionals)...
                case 0xC6: 	// If $1E80 bit is set/clear, branch to (7 OR conditionals)...
                case 0xC7: 	// If $1E80 bit is set/clear, branch to (8 OR conditionals)...
                case 0xC8: 	// If $1E80 bit is set/clear, branch to...
                case 0xC9: 	// If $1E80 bit is set/clear, branch to (2 AND conditionals)...
                case 0xCA: 	// If $1E80 bit is set/clear, branch to (3 AND conditionals)...
                case 0xCB: 	// If $1E80 bit is set/clear, branch to (4 AND conditionals)...
                case 0xCC: 	// If $1E80 bit is set/clear, branch to (5 AND conditionals)...
                case 0xCD: 	// If $1E80 bit is set/clear, branch to (6 AND conditionals)...
                case 0xCE: 	// If $1E80 bit is set/clear, branch to (7 AND conditionals)...
                case 0xCF: 	// If $1E80 bit is set/clear, branch to (8 AND conditionals)...
                    groupBoxC.Text = commandText;
                    labelEvtD1.Text = "Address";
                    labelEvtD2.Text = "Bit";
                    evtButtonD4.Text = "Is clear";
                    evtButtonD5.Text = "Is set";
                    labelEvtC1.Text = "Branch to";
                    memory = Bits.GetShort(esc.CommandData, 1);
                    evtNumD1.Maximum = 0x1E80 + 0xFFF; evtNumD1.Minimum = 0x1E80;
                    evtNumD1.Hexadecimal = true;
                    evtNumD1.Value = (memory & 0x7FFF) / 8 + 0x1E80; evtNumD1.Enabled = true;
                    evtNumD2.Maximum = 7; evtNumD2.Enabled = true;
                    evtNumD2.Value = memory & 7;
                    evtNameD3.Items.AddRange(new string[] { "is clear", "is set" });
                    evtNameD3.SelectedIndex = memory >> 15; evtNameD3.Enabled = true;
                    counter = 0;
                    while (counter <= (esc.Opcode & 7))
                    {
                        string conditional = "";
                        if (counter > 0)
                            conditional = (esc.Opcode & 8) == 8 ? "& " : "or ";
                        else
                            conditional = "if ";
                        conditional += "address $";
                        memory = Bits.GetShort(esc.CommandData, counter++ * 2 + 3);
                        conditional += ((memory & 0x7FFF) / 8 + 0x1E80).ToString("X4"); evtNumA1.Enabled = true;
                        conditional += ", bit " + (memory & 7);
                        if ((memory & 0x8000) == 0x8000)
                            conditional += " is set";
                        else
                            conditional += " is clear";
                        evtListBoxD.Items.Add(conditional);
                    }
                    evtListBoxD.SelectedIndex = 0; evtListBoxD.Enabled = true;
                    offset = Bits.GetInt24(esc.CommandData, ((esc.Opcode & 7) * 2) + 3);
                    evtNumC1.Maximum = 0xCCE5FF; evtNumC1.Minimum = 0xCA0000;
                    evtNumC1.Hexadecimal = true; evtNumC1.Width = 70;
                    evtNumC1.Value = offset + 0xCA0000; evtNumC1.Enabled = true;
                    break;
                // Event bits	
                case 0xD0: 	// Set event bit $1E80($0...
                case 0xD1: 	// Clear event bit $1E80($0...
                case 0xD2: 	// Set event bit $1E80($1...
                case 0xD3: 	// Clear event bit $1E80($1...
                case 0xD4: 	// Set event bit $1E80($2...
                case 0xD5: 	// Clear event bit $1E80($2...
                case 0xD6: 	// Set event bit $1E80($3...
                case 0xD7: 	// Clear event bit $1E80($3...
                case 0xD8: 	// Set event bit $1E80($4...
                case 0xD9: 	// Clear event bit $1E80($4...
                case 0xDA: 	// Set event bit $1E80($5...
                case 0xDB: 	// Clear event bit $1E80($5...
                case 0xDC: 	// Set event bit $1E80($6...
                case 0xDD: 	// Clear event bit $1E80($6...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Address";
                    labelEvtA2.Text = "Bit";
                    evtNumA1.Maximum = 0x1E80 + 0x1F; evtNumA1.Minimum = 0x1E80;
                    evtNumA1.Value = 0x1E80 + (esc.Param1 / 8); evtNumA1.Enabled = true;
                    evtNumA1.Hexadecimal = true;
                    evtNumA2.Value = esc.Param1 & 7; evtNumA2.Maximum = 7;
                    evtNumA2.Enabled = true;
                    break;
                // Caseword	
                case 0xE7: 	// Unknown command, parameter...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Character";
                    evtNameA1.Items.AddRange(Model.CharacterNames.Names); evtNameA1.Enabled = true;
                    evtNameA1.SelectedIndex = esc.Param1;
                    break;
                case 0xE8: 	// Set word $1FC2...
                case 0xE9: 	// Increment word $1FC2...
                case 0xEA: 	// Decrement word $1FC2...
                case 0xEB: 	// Compare word $1FC2...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Word";
                    labelEvtA2.Text = "Value";
                    evtNumA1.Maximum = 0x1FC2 + (0xFF * 2); evtNumA1.Minimum = 0x1FC2;
                    evtNumA1.Increment = 2; evtNumA1.Hexadecimal = true;
                    evtNumA1.Value = 0x1FC2 + (esc.Param1 * 2); evtNumA1.Enabled = true;
                    evtNumA2.Maximum = 0xFFFF; evtNumA2.Hexadecimal = true;
                    evtNumA2.Value = Bits.GetShort(esc.CommandData, 2); evtNumA2.Enabled = true;
                    break;
                // Pause	
                case 0xA0: 	// Set timer, jump to subroutine if it expires...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Timer";
                    labelEvtA2.Text = "Ticks";
                    labelEvtA3.Text = "Jump to";
                    int ticks = Bits.GetShort(esc.CommandData, 1);
                    int timer = Bits.GetInt24(esc.CommandData, 3);
                    evtNumA1.Maximum = 3; evtNumA1.Enabled = true;
                    evtNumA1.Value = (timer & 0x0C0000) >> 18;
                    evtNumA2.Maximum = 0xFFFF;
                    evtNumA2.Value = ticks; evtNumA2.Enabled = true;
                    evtNumA3.Maximum = 0xCCE5FF; evtNumA3.Minimum = 0xCA0000;
                    evtNumA3.Width = 70; evtNumA3.Hexadecimal = true;
                    evtNumA3.Value = (timer & 0x3FFFF) + 0xCA0000; evtNumA3.Enabled = true;
                    evtEffects.MultiColumn = false;
                    evtEffects.Items.AddRange(new string[] 
                    { 
                        "override game clock display in menu",
                        "enable exit menu/battle on expiration",
                        "digits display while on the map.",
                        "pause while in menu or battle."
                    });
                    for (int i = 0; i < 4; i++)
                        evtEffects.SetItemChecked(i, Bits.GetBit(timer >> 20, i));
                    evtEffects.Enabled = true;
                    break;
                case 0xB4: 	// Pause for # units...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Units";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
                case 0xB5: 	// Pause for 15 * # of units...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "15x units";
                    evtNumA1.Value = esc.Param1; evtNumA1.Enabled = true;
                    break;
            }
            OrganizeControls();
            //
            panelCommands.ResumeDrawing();
            this.Updating = false;
        }
        private void ControlAssembleEvent()
        {
            switch (esc.Opcode)
            {
                // Objects	
                default: 	// Action queue...
                    if (esc.Opcode <= 0x34)
                    {
                        if (esc.Queue.Commands.Count == 0)
                            esc.CommandData = new byte[]
                            {
                                (byte)evtNameA1.SelectedIndex, 0x01, 0xFF // must insert an "End queue" command
                            };
                        Bits.SetBit(esc.CommandData, 1, 7, evtEffects.GetItemChecked(0));
                    }
                    break;
                case 0x35: 	// Pause execution until action queue for object is complete...
                case 0x36: 	// Disable ability to pass through other objects for object...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0x37: 	// Assign graphics to object...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    esc.Param2 = (byte)evtNumA2.Value;
                    break;
                case 0x3C: 	// Set up the party as follows...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    esc.Param2 = (byte)evtNameA2.SelectedIndex;
                    esc.Param3 = (byte)evtNameA3.SelectedIndex;
                    esc.Param4 = (byte)evtNameA4.SelectedIndex;
                    break;
                case 0x3D: 	// Create object...
                case 0x3E: 	// Delete object...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0x3F: 	// Assign/remove character in party...
                    if (evtNameA1.SelectedIndex == 0)
                        esc.Param2 = (byte)evtNumA3.Value;
                    else
                        esc.Param2 = 0;
                    esc.Param1 = (byte)evtNameA2.SelectedIndex;
                    break;
                case 0x40: 	// Assign properties to character...
                    esc.Param2 = (byte)evtNumA1.Value; evtNumA1.Hexadecimal = true;
                    esc.Param1 = (byte)evtNameA2.SelectedIndex;
                    break;
                case 0x41: 	// Show object...
                case 0x42: 	// Hide object...
                    goto case 0x3E;
                case 0x43: 	// Assign palette to character...
                    esc.Param2 = (byte)evtNumA1.Value; evtNumA1.Hexadecimal = true;
                    esc.Param1 = (byte)evtNameA2.SelectedIndex;
                    break;
                case 0x44: 	// Place character on vehicle...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    esc.Param2 = (byte)(evtNameA2.SelectedIndex << 5);
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(0));
                    break;
                case 0x46: 	// Make party # the current party...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0x78: 	// Enable ability to pass through other objects for object...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0x7A: 	// Change event address for object to address...
                    esc.Param1 = (byte)evtNumA1.Value; evtNumA1.Hexadecimal = true;
                    Bits.SetInt24(esc.CommandData, 2, (int)evtNumA2.Value);
                    break;
                case 0x7C: 	// Enable activation of event for object if it comes into contact with any party...
                case 0x7D: 	// Disable activation of event for object...
                    goto case 0x78;
                // Party	
                case 0x77: 	// Perform level averaging on character and calculate new maximum HP/MP...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0x7F: 	// Change character's name to...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    esc.Param2 = (byte)evtNameA2.SelectedIndex;
                    break;
                case 0x80: 	// Add item to inventory...
                case 0x81: 	// Remove item from inventory...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0x84: 	// Give GP to party...
                case 0x85: 	// Take GP from party...
                    Bits.SetShort(esc.CommandData, 1, (int)evtNumA1.Value);
                    break;
                case 0x86: 	// Give esper to party...
                case 0x87: 	// Take esper from party...
                    esc.Param1 = (byte)(evtNameA1.SelectedIndex + 0x36);
                    break;
                case 0x88: 	// Remove the following status ailments from character...
                case 0x89: 	// Inflict the following status ailments on character...
                case 0x8A: 	// Toggle the following status ailments for character...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    for (int i = 0; i < 16; i++)
                        Bits.SetBit(esc.CommandData, 2, i, evtEffects.GetItemChecked(i));
                    break;
                case 0x8B: 	// For character, take HP and set to maximum...
                case 0x8C: 	// For character, take MP and set to maximum...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    esc.Param2 = (byte)(evtNameA2.SelectedIndex << 7);
                    esc.Param2 |= (byte)evtNumA3.Value;
                    break;
                case 0x8D: 	// Remove all equipment from character...
                case 0x9C: 	// Place optimum equipment on character...
                    goto case 0x77;
                // Battle	
                case 0x4C: 	// Center screen on party and invoke battle...
                case 0x4D: 	// Invoke battle...
                    esc.Param1 = (byte)evtNumA1.Value;
                    esc.Param2 = (byte)evtNameA2.SelectedIndex;
                    Bits.SetBit(esc.CommandData, 2, 6, evtEffects.GetItemChecked(0));
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(1));
                    break;
                // Locations	
                case 0x6A: 	// Load map after fade out...
                case 0x6B: 	// Load map instantly...
                    if (esc.Queue.Commands.Count == 0)
                        esc.CommandData = new byte[] { 
                            esc.Opcode, esc.Param1, 
                            esc.Param2, esc.Param3, 
                            esc.Param4, esc.Param5, 
                            0xFF };  // must manually insert a termination command (0xFF)
                    if (evtNameA1.SelectedIndex < 0x019F)
                        Bits.SetShort(esc.CommandData, 1, evtNameA1.SelectedIndex);
                    else if (evtNameA1.SelectedIndex == 0x019F)
                        Bits.SetShort(esc.CommandData, 1, 0x01FE);
                    else if (evtNameA1.SelectedIndex == 0x01A0)
                        Bits.SetShort(esc.CommandData, 1, 0x01FF);
                    esc.Param2 |= (byte)((int)evtNumA2.Value << 1);
                    if (evtNameA3.SelectedIndex < 3)
                        esc.Param5 = (byte)evtNameA3.SelectedIndex;
                    else
                        esc.Param5 = (byte)evtNumA3.Value;
                    esc.Param2 |= (byte)(evtNameA4.SelectedIndex << 4);
                    esc.Param3 = (byte)evtNumA5.Value;
                    esc.Param4 = (byte)evtNumA6.Value;
                    break;
                case 0x6C: 	// Set parent map...
                    if (evtNameA1.SelectedIndex < 0x019F)
                        Bits.SetShort(esc.CommandData, 1, evtNameA1.SelectedIndex);
                    else if (evtNameA1.SelectedIndex == 0x019F)
                        Bits.SetShort(esc.CommandData, 1, 0x01FE);
                    else if (evtNameA1.SelectedIndex == 0x01A0)
                        Bits.SetShort(esc.CommandData, 1, 0x01FF);
                    esc.Param2 |= (byte)((int)evtNumA2.Value << 1);
                    esc.Param2 |= (byte)(evtNameA4.SelectedIndex << 4);
                    esc.Param3 = (byte)evtNumA5.Value;
                    esc.Param4 = (byte)evtNumA6.Value;
                    break;
                case 0x73: 	// Replace current map's Layer 1 with the following chunk...
                case 0x74: 	// Replace current map's Layer 2 with the following chunk...
                    esc.Param2 = (byte)((int)evtNumA1.Value << 6);
                    if (evtNameA2.SelectedIndex == 0)
                        esc.Opcode = 0x73;
                    else if (evtNameA2.SelectedIndex == 1)
                        esc.Opcode = 0x74;
                    esc.Param1 = (byte)evtNumA3.Value;
                    esc.Param2 |= (byte)evtNumA4.Value;
                    esc.Param4 = (byte)evtNumA5.Value;
                    esc.Param3 = (byte)evtNumA6.Value;
                    break;
                case 0x79: 	// Place party # on map...
                    esc.Param1 = (byte)evtNumA1.Value;
                    if (evtNameA2.SelectedIndex < 0x019F)
                        Bits.SetShort(esc.CommandData, 2, evtNameA2.SelectedIndex);
                    else if (evtNameA2.SelectedIndex == 0x019F)
                        Bits.SetShort(esc.CommandData, 2, 0x01FE);
                    else if (evtNameA2.SelectedIndex == 0x01A0)
                        Bits.SetShort(esc.CommandData, 2, 0x01FF);
                    break;
                case 0x7E: 	// Move characters to coords...
                    esc.Param1 = (byte)evtNumC1.Value;
                    esc.Param2 = (byte)evtNumC2.Value;
                    break;
                // Menus	
                case 0x98: 	// Invoke name change screen for character...
                    goto case 0x36;
                case 0x99: 	// Invoke party selection screen...
                    esc.Param1 = (byte)evtNumA1.Value;
                    for (int i = 0; i < 16; i++)
                        Bits.SetBit(esc.CommandData, 2, i, evtEffects.GetItemChecked(i));
                    break;
                case 0x9B: 	// Invoke shop...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                // Dialogues	
                case 0x48: 	// Display dialogue message, continue executing commands...
                case 0x4B: 	// Display dialogue message, wait for button press...
                    Bits.SetShort(esc.CommandData, 1, evtNameA1.SelectedIndex);
                    Bits.SetBit(esc.CommandData, 2, 6, evtEffects.GetItemChecked(0));
                    Bits.SetBit(esc.CommandData, 2, 7, evtEffects.GetItemChecked(1));
                    break;
                case 0xB6: 	// Indexed branch based on prior dialogue selection...
                    esc.CommandData = new byte[evtListBoxD.Items.Count * 3 + 1];
                    esc.Opcode = 0xB6;
                    esc.Register = evtListBoxD.Items.Count;
                    for (int i = 0; i < evtListBoxD.Items.Count; i++)
                    {
                        string item = (string)evtListBoxD.Items[i];
                        item = item.Replace("Branch to $", "");
                        int value = Convert.ToInt32(item, 16);
                        Bits.SetInt24(esc.CommandData, i * 3 + 1, value - 0xCA0000);
                    }
                    break;
                // Events	
                case 0xA7: 	// Create a rotating pyramid around character...
                    goto case 0x36;
                case 0xBA: 	// Play ending cinematic...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                // Jump to	
                case 0xA1: 	// Reset timer...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0xB0: 	// Execute the following commands until $B1 # times...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0xB2: 	// Call subroutine...
                    Bits.SetInt24(esc.CommandData, 1, (int)evtNumA1.Value - 0xCA0000);
                    break;
                case 0xB3: 	// Call subroutine # times...
                    esc.Param1 = (byte)evtNumA2.Value;
                    Bits.SetInt24(esc.CommandData, 2, (int)evtNumA1.Value - 0xCA0000);
                    break;
                case 0xB7: 	// If bit $1DC9 is clear, branch to...
                    esc.Param1 = (byte)((evtNumA1.Value - 0x1DC9) * 8);
                    esc.Param1 |= (byte)evtNumA2.Value;
                    Bits.SetInt24(esc.CommandData, 2, (int)evtNumA3.Value - 0xCA0000);
                    break;
                case 0xB8: 	// Set bit $1DC9...
                case 0xB9: 	// Clear bit $1DC9...
                    esc.Param1 = (byte)((evtNumA1.Value - 0x1DC9) * 8);
                    esc.Param1 |= (byte)evtNumA2.Value;
                    break;
                case 0xBC: 	// If bit $1E80...
                    Bits.SetShort(esc.CommandData, 1, (int)((evtNumA1.Value - 0x1E80) * 8));
                    esc.Param1 |= (byte)evtNumA2.Value;
                    Bits.SetBit(esc.CommandData, 2, 7, evtNameA3.SelectedIndex == 1);
                    break;
                case 0xBD: 	// Pseudo-randomly jump to offset 50% of the time...
                    Bits.SetInt24(esc.CommandData, 1, (int)evtNumA3.Value - 0xCA0000);
                    break;
                case 0xBE: 	// If # is in the current CaseWord, jump to subroutine...
                    esc.CommandData = new byte[evtListBoxD.Items.Count * 3 + 2];
                    esc.Opcode = 0xBE; esc.Param1 = (byte)evtListBoxD.Items.Count;
                    for (int i = 0; i < evtListBoxD.Items.Count; i++)
                    {
                        string item = (string)evtListBoxD.Items[i];
                        if (i == 0)
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
                        item = item.Replace(", goto $", "");
                        Bits.SetInt24(esc.CommandData, i * 3 + 2, Convert.ToInt32(item, 16) - 0xCA0000);
                        esc.CommandData[i * 3 + 4] |= (byte)(Convert.ToInt32(value, 10) << 4);
                    }
                    Bits.SetBit(esc.CommandData, 1, 7, evtEffects.GetItemChecked(0));
                    break;
                // Screen	
                case 0x50: 	// Tint screen (cumulative) with color...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0x51: 	// Modify background color range...
                    if (evtNameA1.SelectedIndex == 0)
                        esc.Param1 = 0x20;
                    else if (evtNameA1.SelectedIndex == 1)
                        esc.Param1 = 0xA0;
                    esc.Param1 |= (byte)((int)evtNumA2.Value << 2);
                    esc.Param1 |= (byte)evtNumA3.Value;
                    esc.Param2 = (byte)evtNumA5.Value;
                    esc.Param3 = (byte)evtNumA6.Value;
                    break;
                case 0x52: 	// Tint characters (cumulative) with color...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0x53: 	// Modify object color range from...
                    goto case 0x51;
                case 0x55: 	// Flash screen with color component(s)...
                case 0x56: 	// Increase color component(s)...
                case 0x57: 	// Decrease color component(s)...
                    esc.Param1 = (byte)(evtNameA1.SelectedIndex << 4);
                    esc.Param1 |= (byte)evtNumA2.Value;
                    break;
                case 0x58: 	// Shake screen...
                case 0x59: 	// Unfade screen at speed...
                case 0x5A: 	// Fade screen at speed...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0x5D: 	// Scroll Layer 1, speed...
                case 0x5E: 	// Scroll Layer 2, speed...
                case 0x5F: 	// Scroll Layer 3, speed...
                    esc.Param1 = (byte)evtNumA5.Value;
                    esc.Param2 = (byte)evtNumA6.Value;
                    break;
                case 0x60: 	// Change background layer to palette...
                    esc.Param1 = (byte)evtNumA1.Value;
                    esc.Param2 = (byte)evtNumA2.Value;
                    break;
                case 0x61: 	// Colorize color range to color...
                    esc.Param1 = (byte)evtNumA1.Value;
                    esc.Param2 = (byte)evtNumA5.Value;
                    esc.Param3 = (byte)evtNumA6.Value;
                    break;
                case 0x62: 	// Mosaic screen, speed...
                case 0x63: 	// Create spotlight effect with radius...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0x70: 	// Scroll Layer 1...
                case 0x71: 	// Scroll Layer 2...
                case 0x72: 	// Scroll Layer 3...
                    esc.Param1 = (byte)evtNumA5.Value;
                    esc.Param2 = (byte)evtNumA6.Value;
                    break;
                // Audio	
                case 0xEF: 	// Play song at volume...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    esc.Param2 = (byte)evtNumA2.Value;
                    Bits.SetBit(esc.CommandData, 1, 7, evtEffects.GetItemChecked(0));
                    break;
                case 0xF0: 	// Play song at full volume...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    Bits.SetBit(esc.CommandData, 1, 7, evtEffects.GetItemChecked(0));
                    break;
                case 0xF1: 	// Fade in song with transition time...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    esc.Param2 = (byte)evtNumA2.Value;
                    Bits.SetBit(esc.CommandData, 1, 7, evtEffects.GetItemChecked(0));
                    break;
                case 0xF2: 	// Fade out current song with transition time...
                case 0xF3: 	// Fade in previously faded out song with transition time...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0xF4: 	// Play sound effect...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0xF5: 	// Play sound effect (w/transition time & speaker balance)...
                    esc.Param1 = (byte)evtNumA1.Value;
                    esc.Param2 = (byte)evtNumA2.Value;
                    esc.Param3 = (byte)evtNumA3.Value;
                    break;
                case 0xF6: 	// Play song at volume (subcommands)...
                    switch (evtNameA1.SelectedIndex)
                    {
                        case 0: esc.Param1 = 0x10; goto case 1;
                        case 1:
                            if (evtNameA1.SelectedIndex == 1)
                                esc.Param1 = 0x11;
                            esc.Param2 = (byte)evtNameA2.SelectedIndex;
                            esc.Param3 = (byte)evtNumA3.Value;
                            break;
                        case 2:
                            esc.Param1 = 0x80;
                            goto case 7;
                        case 3:
                            esc.Param1 = 0x81;
                            goto case 7;
                        case 4:
                            esc.Param1 = 0x82;
                            goto case 7;
                        case 5:
                            esc.Param1 = 0x83;
                            goto case 7;
                        case 6:
                            esc.Param1 = 0x84;
                            goto case 7;
                        case 7:
                            if (evtNameA1.SelectedIndex == 7)
                                esc.Param1 = 0x85;
                            esc.Param3 = (byte)evtNumA2.Value;
                            esc.Param2 = (byte)evtNumA3.Value;
                            break;
                        case 8:
                            esc.Param1 = 0xF1;
                            goto default;
                        case 9:
                            esc.Param1 = 0xF2;
                            goto default;
                        default:
                            esc.Param3 = (byte)evtNumA2.Value;
                            esc.Param2 = (byte)evtNumA3.Value;
                            break;
                    }
                    break;
                case 0xF9: 	// Pause execution until the music passes through predetermined point...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                // Memory	
                case 0xC0: 	// If $1E80 bit is set/clear, branch to...
                case 0xC1: 	// If $1E80 bit is set/clear, branch to (2 OR conditionals)...
                case 0xC2: 	// If $1E80 bit is set/clear, branch to (3 OR conditionals)...
                case 0xC3: 	// If $1E80 bit is set/clear, branch to (4 OR conditionals)...
                case 0xC4: 	// If $1E80 bit is set/clear, branch to (5 OR conditionals)...
                case 0xC5: 	// If $1E80 bit is set/clear, branch to (6 OR conditionals)...
                case 0xC6: 	// If $1E80 bit is set/clear, branch to (7 OR conditionals)...
                case 0xC7: 	// If $1E80 bit is set/clear, branch to (8 OR conditionals)...
                case 0xC8: 	// If $1E80 bit is set/clear, branch to...
                case 0xC9: 	// If $1E80 bit is set/clear, branch to (2 AND conditionals)...
                case 0xCA: 	// If $1E80 bit is set/clear, branch to (3 AND conditionals)...
                case 0xCB: 	// If $1E80 bit is set/clear, branch to (4 AND conditionals)...
                case 0xCC: 	// If $1E80 bit is set/clear, branch to (5 AND conditionals)...
                case 0xCD: 	// If $1E80 bit is set/clear, branch to (6 AND conditionals)...
                case 0xCE: 	// If $1E80 bit is set/clear, branch to (7 AND conditionals)...
                case 0xCF: 	// If $1E80 bit is set/clear, branch to (8 AND conditionals)...
                    for (int i = 0; i < evtListBoxD.Items.Count; i++)
                    {
                        string item = (string)evtListBoxD.Items[i];
                        if (i == 0)
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
                        Bits.SetShort(esc.CommandData, i * 2 + 1, (Convert.ToInt32(address, 16) - 0x1E80) * 8);
                        Bits.SetBit(esc.CommandData, i * 2 + 2, 7, item == "is set");
                        esc.CommandData[i * 2 + 1] |= (byte)Convert.ToInt32(bit, 10);
                    }
                    Bits.SetInt24(esc.CommandData, ((esc.Opcode & 7) * 2) + 3, (int)(evtNumC1.Value - 0xCA0000));
                    break;
                // Event bits	
                case 0xD0: 	// Set event bit $1E80($0...
                case 0xD1: 	// Clear event bit $1E80($0...
                case 0xD2: 	// Set event bit $1E80($1...
                case 0xD3: 	// Clear event bit $1E80($1...
                case 0xD4: 	// Set event bit $1E80($2...
                case 0xD5: 	// Clear event bit $1E80($2...
                case 0xD6: 	// Set event bit $1E80($3...
                case 0xD7: 	// Clear event bit $1E80($3...
                case 0xD8: 	// Set event bit $1E80($4...
                case 0xD9: 	// Clear event bit $1E80($4...
                case 0xDA: 	// Set event bit $1E80($5...
                case 0xDB: 	// Clear event bit $1E80($5...
                case 0xDC: 	// Set event bit $1E80($6...
                case 0xDD: 	// Clear event bit $1E80($6...
                    esc.Param1 = (byte)((evtNumA1.Value - 0x1E80) * 8);
                    esc.Param1 |= (byte)evtNumA2.Value;
                    break;
                // Caseword	
                case 0xE7: 	// Unknown command, parameter...
                    esc.Param1 = (byte)evtNameA1.SelectedIndex;
                    break;
                case 0xE8: 	// Set word $1FC2...
                case 0xE9: 	// Increment word $1FC2...
                case 0xEA: 	// Decrement word $1FC2...
                case 0xEB: 	// Compare word $1FC2...
                    esc.Param1 = (byte)((evtNumA1.Value - 0x1FC2) / 2);
                    Bits.SetShort(esc.CommandData, 2, (int)evtNumA2.Value);
                    break;
                // Pause	
                case 0xA0: 	// Set timer, jump to subroutine if it expires...
                    Bits.SetShort(esc.CommandData, 1, (int)evtNumA2.Value);
                    Bits.SetInt24(esc.CommandData, 3, (int)(evtNumA3.Value - 0xCA0000));
                    esc.Param5 |= (byte)((int)evtNumA1.Value << 2);
                    for (int i = 0; i < 4; i++)
                        Bits.SetBit(esc.CommandData, 5, i + 4, evtEffects.GetItemChecked(i));
                    break;
                case 0xB4: 	// Pause for # units...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0xB5: 	// Pause for 15 * # of units...
                    esc.Param1 = (byte)evtNumA1.Value;
                    break;
            }
        }
        //
        private void ControlDisassembleAction()
        {
            this.Updating = true;
            panelCommands.SuspendDrawing();
            int[] tree = categoryCommand;
            if (tree != null)
            {
                categories.SelectedIndex = tree[0];
                commands.SelectedIndex = tree[1];
            }
            switch (asc.Opcode)
            {
                // If memory (OR)	
                case 0xB0:	// If $1E80 bit is set/clear, branch to...
                case 0xB1:	// If $1E80 bit is set/clear, branch to (2 OR conditionals)...
                case 0xB2:	// If $1E80 bit is set/clear, branch to (3 OR conditionals)...
                case 0xB3:	// If $1E80 bit is set/clear, branch to (4 OR conditionals)...
                case 0xB4:	// If $1E80 bit is set/clear, branch to (5 OR conditionals)...
                case 0xB5:	// If $1E80 bit is set/clear, branch to (6 OR conditionals)...
                case 0xB6:	// If $1E80 bit is set/clear, branch to (7 OR conditionals)...
                case 0xB7:	// If $1E80 bit is set/clear, branch to (8 OR conditionals)...
                // If memory (AND)	
                case 0xB8:	// If $1E80 bit is set/clear, branch to...
                case 0xB9:	// If $1E80 bit is set/clear, branch to (2 AND conditionals)...
                case 0xBA:	// If $1E80 bit is set/clear, branch to (3 AND conditionals)...
                case 0xBB:	// If $1E80 bit is set/clear, branch to (4 AND conditionals)...
                case 0xBC:	// If $1E80 bit is set/clear, branch to (5 AND conditionals)...
                case 0xBD:	// If $1E80 bit is set/clear, branch to (6 AND conditionals)...
                case 0xBE:	// If $1E80 bit is set/clear, branch to (7 AND conditionals)...
                case 0xBF:	// If $1E80 bit is set/clear, branch to (8 AND conditionals)...
                    groupBoxC.Text = commandText;
                    labelEvtD1.Text = "Address";
                    labelEvtD2.Text = "Bit";
                    evtButtonD4.Text = "Is clear";
                    evtButtonD5.Text = "Is set";
                    labelEvtC1.Text = "Branch to";
                    int memory = Bits.GetShort(asc.CommandData, 1);
                    evtNumD1.Maximum = 0x1E80 + 0xFFF; evtNumD1.Minimum = 0x1E80;
                    evtNumD1.Hexadecimal = true;
                    evtNumD1.Value = (memory & 0x7FFF) / 8 + 0x1E80; evtNumD1.Enabled = true;
                    evtNumD2.Maximum = 7; evtNumD2.Enabled = true;
                    evtNumD2.Value = memory & 7;
                    evtNameD3.Items.AddRange(new string[] { "is clear", "is set" });
                    evtNameD3.SelectedIndex = memory >> 15; evtNameD3.Enabled = true;
                    int counter = 0;
                    while (counter <= (asc.Opcode & 7))
                    {
                        string conditional = "";
                        if (counter > 0)
                            conditional = (asc.Opcode & 8) == 8 ? "& " : "or ";
                        else
                            conditional = "if ";
                        conditional += "address $";
                        memory = Bits.GetShort(asc.CommandData, counter++ * 2 + 3);
                        conditional += ((memory & 0x7FFF) / 8 + 0x1E80).ToString("X4"); evtNumA1.Enabled = true;
                        conditional += ", bit " + (memory & 7);
                        if ((memory & 0x8000) == 0x8000)
                            conditional += " is set";
                        else
                            conditional += " is clear";
                        evtListBoxD.Items.Add(conditional);
                    }
                    evtListBoxD.SelectedIndex = 0; evtListBoxD.Enabled = true;
                    int offset = Bits.GetInt24(asc.CommandData, ((asc.Opcode & 7) * 2) + 3);
                    evtNumC1.Maximum = 0xCCE5FF; evtNumC1.Minimum = 0xCA0000;
                    evtNumC1.Hexadecimal = true; evtNumC1.Width = 70;
                    evtNumC1.Value = offset + 0xCA0000; evtNumC1.Enabled = true;
                    break;
                // Properties	
                case 0xC0:  // Modify vehicle script behavior... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Mode";
                    evtNameA1.Items.AddRange(new string[] { 
                        "none",
                        "Change mode to mode 7 persepective", 
                        "Allow ship to propel without changing direction facing", 
                        "Unknown mode change" });
                    evtNameA1.SelectedIndex = (asc.Param1 & 0x30) >> 4;
                    evtNameA1.Enabled = true;
                    break;
                case 0xC1:  // Set vehicle's facing direction... (vehicles only)
                case 0xC2:  // Set vehicle's propulsion direction... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Degrees";
                    evtNumA1.Maximum = 0xFFFF; evtNumA1.Enabled = true;
                    evtNumA1.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0xC3:  // Vehicle script command... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Command $";
                    evtNumA1.Hexadecimal = true;
                    evtNumA1.Value = asc.Opcode; evtNumA1.Enabled = true;
                    break;
                case 0xC4:  // Set mode 7 height perspective... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Value";
                    evtNumA1.Maximum = 0xFFFF; evtNumA1.Enabled = true;
                    evtNumA1.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0xC5:  // Set vehicle height... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Height";
                    labelEvtA2.Text = "Unknown";
                    evtNumA1.Value = asc.Param2; evtNumA1.Enabled = true;
                    evtNumA2.Value = asc.Param1; evtNumA2.Enabled = true;
                    break;
                case 0xC8:	// Set object layering priority...
                    groupBoxA.Text = commandText;
                    if (asc.Type != ScriptType.Vehicle)
                    {
                        labelEvtA1.Text = "Priority";
                        labelEvtA2.Text = "Low nibble";
                        evtNumA1.Maximum = 15;
                        evtNumA1.Value = asc.Param1 >> 4; evtNumA1.Enabled = true;
                        evtNumA2.Maximum = 15;
                        evtNumA2.Value = asc.Param1 & 0x0F; evtNumA2.Enabled = true;
                    }
                    else
                    {
                        // Set bit $1E80... (vehicles only)
                        labelEvtA1.Text = "Address";
                        labelEvtA2.Text = "Bit";
                        evtNumA1.Minimum = 0x1E80;
                        evtNumA1.Maximum = evtNumA1.Minimum + 0xFFF;
                        evtNumA1.Value = evtNumA1.Minimum + (asc.Param1 / 8); evtNumA1.Enabled = true;
                        evtNumA1.Hexadecimal = true;
                        evtNumA2.Maximum = 7; evtNumA2.Enabled = true;
                        evtNumA2.Value = asc.Param1 & 7;
                    }
                    break;
                case 0xC9:	// Place object on vehicle...
                    groupBoxA.Text = commandText;
                    if (asc.Type != ScriptType.Vehicle)
                    {
                        labelEvtA1.Text = "Vehicle";
                        evtNumA1.Value = asc.Param1; evtNumA1.Enabled = true;
                    }
                    else
                    {
                        // Clear bit $1E80... (vehicles only)
                        labelEvtA1.Text = "Address";
                        labelEvtA2.Text = "Bit";
                        evtNumA1.Minimum = 0x1E80;
                        evtNumA1.Maximum = evtNumA1.Minimum + 0xFFF;
                        evtNumA1.Value = evtNumA1.Minimum + (asc.Param1 / 8); evtNumA1.Enabled = true;
                        evtNumA1.Hexadecimal = true;
                        evtNumA2.Maximum = 7; evtNumA2.Enabled = true;
                        evtNumA2.Value = asc.Param1 & 7;
                    }
                    break;
                // Action	
                default:	// Graphical action...
                    if (asc.Opcode <= 0x7F)
                    {
                        if (asc.Type != ScriptType.Vehicle)
                        {
                            groupBoxA.Text = "Do graphical action...";
                            labelEvtA1.Text = "Action";
                            evtNumA1.Maximum = 63;
                            evtNumA1.Value = asc.Opcode & 0x3F; evtNumA1.Enabled = true;
                            evtEffects.Items.Add("flipped horizontally");
                            evtEffects.SetItemChecked(0, (asc.Opcode & 0x40) == 0x40);
                            evtEffects.Enabled = true;
                        }
                        else
                        {
                            groupBoxA.Text = "Move vehicle as follows...";
                            labelEvtA1.Text = "Units";
                            evtNumA1.Value = asc.Param1; evtNumA1.Enabled = true;
                            evtEffects.Items.AddRange(new string[] 
                            {
                                "double speed of turns",
                                "decrease speed by 255",
                                "move forward",
                                "turn right",
                                "turn left",
                                "go up",
                                "go down"
                            });
                            for (int i = 0; i < 7; i++)
                                evtEffects.SetItemChecked(i, Bits.GetBit(asc.Opcode, i));
                            evtEffects.Enabled = true;
                        }
                    }
                    break;
                case 0xC6:  // Propel vehicle at speed... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Speed";
                    evtNumA1.Maximum = 0xFFFF; evtNumA1.Enabled = true;
                    evtNumA1.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0xC7:  // Place airship at position... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA5.Text = "X";
                    labelEvtA6.Text = "Y";
                    evtNumA5.Value = asc.Param1; evtNumA5.Enabled = true;
                    evtNumA6.Value = asc.Param2; evtNumA6.Enabled = true;
                    break;
                case 0xCA:  // Invoke battle... (vehicles only)
                case 0xCB:  // Invoke battle... (vehicles only)
                case 0xCC:  // Invoke battle... (vehicles only)
                case 0xCD:  // Invoke battle... (vehicles only)
                case 0xCE:  // Invoke battle... (vehicles only)
                case 0xCF:  // Invoke battle... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Enemy set";
                    labelEvtA2.Text = "Background";
                    labelEvtA3.Text = "Other flags";
                    evtNumA1.Hexadecimal = true;
                    evtNumA1.Value = asc.Param1; evtNumA1.Enabled = true;
                    evtNameA2.Items.AddRange(Lists.Numerize(Lists.BattlefieldNames));
                    evtNameA2.SelectedIndex = asc.Param2 & 0x3F; evtNameA2.Enabled = true;
                    evtNumA3.Maximum = 3;
                    evtNumA3.Value = asc.Param2 >> 6; evtNumA3.Enabled = true;
                    break;
                case 0xD4:	// If ..., branch to...
                    if (asc.Type == ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Offset";
                    evtNumA1.Maximum = 0xCCE5FF; evtNumA1.Minimum = 0xCA0000;
                    evtNumA1.Hexadecimal = true; evtNumA1.Width = 70;
                    evtNumA1.Value = Bits.GetInt24(asc.CommandData, 1) + 0xCA0000;
                    evtNumA1.Enabled = true;
                    break;
                case 0xD5:	// Set position to...
                    if (asc.Type == ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA5.Text = "X";
                    labelEvtA6.Text = "Y";
                    evtNumA5.Value = asc.Param1; evtNumA5.Enabled = true;
                    evtNumA6.Value = asc.Param2; evtNumA6.Enabled = true;
                    break;
                case 0xE0: // Pause for 4 * # frames...
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "4x frames";
                    evtNumA1.Value = asc.Param1; evtNumA1.Enabled = true;
                    break;
                // Event bits	
                case 0xE1:	// Set event bit $1E80($0...
                case 0xE2:	// Set event bit $1E80($1...
                case 0xE3:	// Set event bit $1E80($2...
                case 0xE4:	// Clear event bit $1E80($0...
                case 0xE5:	// Clear event bit $1E80($1...
                case 0xE6:	// Clear event bit $1E80($2...
                    labelEvtA1.Text = "Address";
                    labelEvtA2.Text = "Bit";
                    if (asc.Opcode == 0xE1 || asc.Opcode == 0xE4)
                        evtNumA1.Minimum = 0x1E80;
                    else if (asc.Opcode == 0xE2 || asc.Opcode == 0xE5)
                        evtNumA1.Minimum = 0x1EA0;
                    else if (asc.Opcode == 0xE3 || asc.Opcode == 0xE6)
                        evtNumA1.Minimum = 0x1EC0;
                    evtNumA1.Maximum = evtNumA1.Minimum + 0xFFF;
                    evtNumA1.Value = evtNumA1.Minimum + (asc.Param1 / 8); evtNumA1.Enabled = true;
                    evtNumA1.Hexadecimal = true;
                    evtNumA2.Maximum = 7; evtNumA2.Enabled = true;
                    evtNumA2.Value = asc.Param1 & 7;
                    break;
                // Jump to	
                case 0xF9:	// Branch out of queue to...
                    if (asc.Type != ScriptType.Vehicle)
                    {
                        groupBoxA.Text = commandText;
                        labelEvtA1.Text = "Offset";
                        evtNumA1.Maximum = 0xCCE5FF; evtNumA1.Minimum = 0xCA0000;
                        evtNumA1.Hexadecimal = true; evtNumA1.Width = 70;
                        evtNumA1.Value = Bits.GetInt24(asc.CommandData, 1) + 0xCA0000;
                        evtNumA1.Enabled = true;
                    }
                    break;
                case 0xFA:	// Pseudo-randomly choose to branch backwards...
                case 0xFB:	// Pseudo-randomly choose to branch forwards...
                    if (asc.Type != ScriptType.Vehicle)
                    {
                        groupBoxA.Text = commandText;
                        labelEvtA1.Text = "Bytes";
                        evtNumA1.Value = asc.Param1; evtNumA1.Enabled = true;
                    }
                    break;
                case 0xFC:	// Branch backwards...
                case 0xFD:	// Branch forwards...
                    if (asc.Type == ScriptType.Character)
                    {
                        groupBoxA.Text = commandText;
                        labelEvtA1.Text = "Bytes";
                        evtNumA1.Value = asc.Param1; evtNumA1.Enabled = true;
                    }
                    break;
                // Maps
                case 0xD2:	// Load map (end queue)...
                case 0xD3:	// Load map...
                    if (asc.Type == ScriptType.Character)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Location";
                    labelEvtA3.Text = "Mode";
                    labelEvtA5.Text = "@ X";
                    labelEvtA6.Text = "@ Y";
                    int map = Bits.GetShort(asc.CommandData, 1);
                    evtNameA1.DropDownWidth = 300;
                    evtNameA1.Items.AddRange(Lists.Numerize(Lists.LocationNames));
                    evtNameA1.Items.Add("previous world map, skipping reposition function");
                    evtNameA1.Items.Add("world map");
                    evtNameA1.Items.Add("<unknown or out-of-range map index>");
                    if ((map & 0x01FF) <= 0x019E)
                        evtNameA1.SelectedIndex = map & 0x01FF;
                    else if ((map & 0x01FF) == 0x01FE)
                        evtNameA1.SelectedIndex = 0x019F;
                    else if ((map & 0x01FF) == 0x01FF)
                        evtNameA1.SelectedIndex = 0x01A0;
                    else
                        evtNameA1.SelectedIndex = 0x01A1;
                    evtNameA1.Enabled = true;
                    evtNumA3.Value = asc.Param5; evtNumA3.Enabled = true;
                    evtNumA5.Value = asc.Param3; evtNumA5.Enabled = true;
                    evtNumA6.Value = asc.Param4; evtNumA6.Enabled = true;
                    break;
            }
            OrganizeControls();
            //
            panelCommands.ResumeDrawing();
            this.Updating = false;
        }
        private void ControlAssembleAction()
        {
            switch (asc.Opcode)
            {
                // If memory (OR)	
                case 0xB0:	// If $1E80 bit is set/clear, branch to...
                case 0xB1:	// If $1E80 bit is set/clear, branch to (2 OR conditionals)...
                case 0xB2:	// If $1E80 bit is set/clear, branch to (3 OR conditionals)...
                case 0xB3:	// If $1E80 bit is set/clear, branch to (4 OR conditionals)...
                case 0xB4:	// If $1E80 bit is set/clear, branch to (5 OR conditionals)...
                case 0xB5:	// If $1E80 bit is set/clear, branch to (6 OR conditionals)...
                case 0xB6:	// If $1E80 bit is set/clear, branch to (7 OR conditionals)...
                case 0xB7:	// If $1E80 bit is set/clear, branch to (8 OR conditionals)...
                // If memory (AND)	
                case 0xB8:	// If $1E80 bit is set/clear, branch to...
                case 0xB9:	// If $1E80 bit is set/clear, branch to (2 AND conditionals)...
                case 0xBA:	// If $1E80 bit is set/clear, branch to (3 AND conditionals)...
                case 0xBB:	// If $1E80 bit is set/clear, branch to (4 AND conditionals)...
                case 0xBC:	// If $1E80 bit is set/clear, branch to (5 AND conditionals)...
                case 0xBD:	// If $1E80 bit is set/clear, branch to (6 AND conditionals)...
                case 0xBE:	// If $1E80 bit is set/clear, branch to (7 AND conditionals)...
                case 0xBF:	// If $1E80 bit is set/clear, branch to (8 AND conditionals)...
                    for (int i = 0; i < evtListBoxD.Items.Count; i++)
                    {
                        string item = (string)evtListBoxD.Items[i];
                        if (i == 0)
                            item = item.Replace("if address $", "");
                        else if ((asc.Opcode & 8) == 8)
                            item = item.Replace("& address $", "");
                        else
                            item = item.Replace("or address $", "");
                        string address = item.Substring(0, 4);
                        item = item.Substring(4);
                        item = item.Replace(", bit ", "");
                        string bit = item.Substring(0, 1);
                        item = item.Substring(2); // is set or is clear starts here
                        Bits.SetShort(asc.CommandData, i * 2 + 1, (Convert.ToInt32(address, 16) - 0x1E80) * 8);
                        Bits.SetBit(asc.CommandData, i * 2 + 2, 7, item == "is set");
                        asc.CommandData[i * 2 + 1] |= (byte)Convert.ToInt32(bit, 10);
                    }
                    Bits.SetInt24(asc.CommandData, ((asc.Opcode & 7) * 2) + 3, (int)(evtNumC1.Value - 0xCA0000));
                    break;
                // Properties	
                case 0xC0:  // Modify vehicle script behavior... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Mode";
                    evtNameA1.Items.AddRange(new string[] { 
                        "none",
                        "Change mode to mode 7 persepective", 
                        "Allow ship to propel without changing direction facing", 
                        "Unknown mode change" });
                    evtNameA1.SelectedIndex = (asc.Param1 & 0x30) >> 4;
                    evtNameA1.Enabled = true;
                    break;
                case 0xC1:  // Set vehicle's facing direction... (vehicles only)
                case 0xC2:  // Set vehicle's propulsion direction... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Degrees";
                    evtNumA1.Maximum = 0xFFFF; evtNumA1.Enabled = true;
                    evtNumA1.Value = Bits.GetShort(asc.CommandData, 1);
                    break;
                case 0xC3:  // Vehicle script command... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Command $";
                    evtNumA1.Hexadecimal = true;
                    evtNumA1.Value = asc.Opcode; evtNumA1.Enabled = true;
                    break;
                case 0xC4:  // Set mode 7 height perspective... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    Bits.SetShort(asc.CommandData, 1, (int)evtNumA1.Value);
                    break;
                case 0xC5:  // Set vehicle height... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    groupBoxA.Text = commandText;
                    labelEvtA1.Text = "Height";
                    labelEvtA2.Text = "Unknown";
                    evtNumA1.Value = asc.Param2; evtNumA1.Enabled = true;
                    evtNumA2.Value = asc.Param1; evtNumA2.Enabled = true;
                    break;
                case 0xC8:	// Set object layering priority...
                    if (asc.Type != ScriptType.Vehicle)
                    {
                        asc.Param1 = (byte)evtNumA2.Value;
                        asc.Param1 |= (byte)((int)evtNumA1.Value << 4);
                    }
                    else
                    {
                        // Set bit $1E80... (vehicles only)
                        asc.Param1 = (byte)((evtNumA1.Value - evtNumA1.Minimum) * 8);
                        asc.Param1 |= (byte)evtNumA2.Value;
                    }
                    break;
                case 0xC9:	// Place object on vehicle...
                    if (asc.Type != ScriptType.Vehicle)
                        asc.Param1 = (byte)evtNumA1.Value;
                    else
                    {
                        // Clear bit $1E80... (vehicles only)
                        asc.Param1 = (byte)((evtNumA1.Value - evtNumA1.Minimum) * 8);
                        asc.Param1 |= (byte)evtNumA2.Value;
                    }
                    break;
                // Action	
                default:	// Graphical action...
                    if (asc.Opcode <= 0x7F)
                    {
                        if (asc.Type != ScriptType.Vehicle)
                        {
                            asc.Opcode = (byte)evtNumA1.Value;
                            Bits.SetBit(asc.CommandData, 0, 6, evtEffects.GetItemChecked(0));
                        }
                        else
                        {

                            asc.Param1 = (byte)evtNumA1.Value;
                            asc.Opcode = 0;
                            for (int i = 0; i < 7; i++)
                                Bits.SetBit(asc.CommandData, 0, i, evtEffects.GetItemChecked(i));
                        }
                    }
                    break;
                case 0xC6:  // Propel vehicle at speed... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    Bits.SetShort(asc.CommandData, 1, (int)evtNumA1.Value);
                    break;
                case 0xC7:  // Place airship at position... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    asc.Param1 = (byte)evtNumA5.Value;
                    asc.Param2 = (byte)evtNumA6.Value;
                    break;
                case 0xCA:  // Invoke battle... (vehicles only)
                case 0xCB:  // Invoke battle... (vehicles only)
                case 0xCC:  // Invoke battle... (vehicles only)
                case 0xCD:  // Invoke battle... (vehicles only)
                case 0xCE:  // Invoke battle... (vehicles only)
                case 0xCF:  // Invoke battle... (vehicles only)
                    if (asc.Type != ScriptType.Vehicle)
                        break;
                    asc.Param1 = (byte)evtNumA1.Value;
                    asc.Param2 = (byte)evtNameA2.SelectedIndex;
                    asc.Param2 |= (byte)((int)evtNumA3.Value << 6);
                    break;
                case 0xD4:	// If ($08 & 0x80 == 0), branch to...
                    if (asc.Type == ScriptType.Vehicle)
                        break;
                    Bits.SetInt24(asc.CommandData, 1, (int)evtNumA1.Value - 0xCA0000);
                    break;
                case 0xD5:	// Set position to...
                    if (asc.Type == ScriptType.Vehicle)
                        break;
                    asc.Param1 = (byte)evtNumA5.Value;
                    asc.Param2 = (byte)evtNumA6.Value;
                    break;
                case 0xE0: // Pause for 4 * # frames...
                    asc.Param1 = (byte)evtNumA1.Value;
                    break;
                // Event bits	
                case 0xE1:	// Set event bit $1E80($0...
                case 0xE2:	// Set event bit $1E80($1...
                case 0xE3:	// Set event bit $1E80($2...
                case 0xE4:	// Clear event bit $1E80($0...
                case 0xE5:	// Clear event bit $1E80($1...
                case 0xE6:	// Clear event bit $1E80($2...
                    asc.Param1 = (byte)((evtNumA1.Value - evtNumA1.Minimum) * 8);
                    asc.Param1 |= (byte)evtNumA2.Value;
                    break;
                // Jump to	
                case 0xF9:	// Branch out of queue to...
                    if (asc.Type != ScriptType.Vehicle)
                        Bits.SetInt24(asc.CommandData, 1, (int)evtNumA1.Value - 0xCA0000);
                    break;
                case 0xFA:	// Pseudo-randomly choose to branch backwards...
                case 0xFB:	// Pseudo-randomly choose to branch forwards...
                    if (asc.Type != ScriptType.Vehicle)
                        asc.Param1 = (byte)evtNumA1.Value;
                    break;
                case 0xFC:	// Branch backwards...
                case 0xFD:	// Branch forwards...
                    if (asc.Type == ScriptType.Character)
                        asc.Param1 = (byte)evtNumA1.Value;
                    break;
                // Maps
                case 0xD2:	// Load map (end queue)...
                case 0xD3:	// Load map...
                    if (asc.Type == ScriptType.Character)
                        break;
                    if (evtNameA1.SelectedIndex < 0x019F)
                        Bits.SetShort(asc.CommandData, 1, evtNameA1.SelectedIndex);
                    else if (evtNameA1.SelectedIndex == 0x019F)
                        Bits.SetShort(asc.CommandData, 1, 0x01FE);
                    else if (evtNameA1.SelectedIndex == 0x01A0)
                        Bits.SetShort(asc.CommandData, 1, 0x01FF);
                    asc.Param5 = (byte)evtNumA3.Value;
                    asc.Param3 = (byte)evtNumA5.Value;
                    asc.Param4 = (byte)evtNumA6.Value;
                    break;
            }
        }
        //
        private void ResetControls()
        {
            this.Updating = true;
            evtNameA1.DropDownWidth = evtNameA1.Width; evtNameA1.Items.Clear(); evtNameA1.ResetText(); evtNameA1.Enabled = false;
            evtNameA2.DropDownWidth = evtNameA2.Width; evtNameA2.Items.Clear(); evtNameA2.ResetText(); evtNameA2.Enabled = false;
            evtNameA3.DropDownWidth = evtNameA3.Width; evtNameA3.Items.Clear(); evtNameA3.ResetText(); evtNameA3.Enabled = false;
            evtNameA4.DropDownWidth = evtNameA4.Width; evtNameA4.Items.Clear(); evtNameA4.ResetText(); evtNameA4.Enabled = false;
            evtNumA1.Maximum = 255; evtNumA1.Minimum = 0; evtNumA1.Hexadecimal = false; evtNumA1.Value = 0; evtNumA1.Width = 49; evtNumA1.Enabled = false;
            evtNumA2.Maximum = 255; evtNumA2.Minimum = 0; evtNumA2.Hexadecimal = false; evtNumA2.Value = 0; evtNumA2.Width = 49; evtNumA2.Enabled = false;
            evtNumA3.Maximum = 255; evtNumA3.Minimum = 0; evtNumA3.Hexadecimal = false; evtNumA3.Value = 0; evtNumA3.Width = 49; evtNumA3.Enabled = false;
            evtNumA4.Maximum = 255; evtNumA4.Minimum = 0; evtNumA4.Hexadecimal = false; evtNumA4.Value = 0; evtNumA4.Width = 49; evtNumA4.Enabled = false;
            evtNumA5.Maximum = 255; evtNumA5.Minimum = 0; evtNumA5.Hexadecimal = false; evtNumA5.Increment = 1; evtNumA5.Value = 0; evtNumA5.Enabled = false;
            evtNumA6.Maximum = 255; evtNumA6.Minimum = 0; evtNumA6.Hexadecimal = false; evtNumA6.Increment = 1; evtNumA6.Value = 0; evtNumA6.Enabled = false;
            evtNumC1.Maximum = 255; evtNumC1.Minimum = 0; evtNumC1.Hexadecimal = false; evtNumC1.Value = 0; evtNumC1.Width = 49; evtNumC1.Enabled = false;
            evtNumC2.Maximum = 255; evtNumC2.Minimum = 0; evtNumC2.Value = 0; evtNumC2.Enabled = false;
            evtNumD1.Maximum = 255; evtNumD1.Minimum = 0; evtNumD1.Value = 0; evtNumD1.Enabled = false;
            evtNumD2.Maximum = 255; evtNumD2.Minimum = 0; evtNumD2.Value = 0; evtNumD2.Enabled = false;
            evtListBoxD.Items.Clear(); evtListBoxD.Enabled = false;
            evtNameD3.Items.Clear(); evtNameD3.Enabled = false;
            evtButtonD4.Text = ""; evtButtonD4.Enabled = false;
            evtButtonD5.Text = ""; evtButtonD5.Enabled = false;
            evtEffects.Height = 68; evtEffects.ColumnWidth = 132; evtEffects.MultiColumn = true;
            evtEffects.Items.Clear(); evtEffects.Enabled = false;
            groupBoxA.Text = "";
            groupBoxB.Text = "";
            groupBoxC.Text = "";
            groupBoxD.Text = "";
            labelEvtA1.Text = "";
            labelEvtA2.Text = "";
            labelEvtA3.Text = "";
            labelEvtA4.Text = "";
            labelEvtA5.Text = "";
            labelEvtA6.Text = "";
            labelEvtC1.Text = "";
            labelEvtC2.Text = "";
            labelEvtD1.Text = "";
            labelEvtD2.Text = "";
            labelEvtD3.Text = "";
            this.Updating = false;
        }
        private void OrganizeControls()
        {
            groupBoxA.Visible =
            groupBoxA.Text != "" ||
            labelEvtA1.Text != "" ||
            labelEvtA2.Text != "" ||
            labelEvtA3.Text != "" ||
            labelEvtA4.Text != "" ||
            labelEvtA5.Text != "" ||
            labelEvtA6.Text != "";
            groupBoxB.Visible =
                groupBoxB.Text != "" ||
                evtEffects.Items.Count > 0;
            groupBoxC.Visible =
                groupBoxC.Text != "" ||
                labelEvtC1.Text != "" ||
                labelEvtC2.Text != "";
            groupBoxD.Visible =
                groupBoxD.Text != "" ||
                labelEvtD1.Text != "" ||
                labelEvtD2.Text != "" ||
                labelEvtD3.Text != "" ||
                evtButtonD4.Text != "" ||
                evtButtonD5.Text != "" ||
                evtListBoxD.Items.Count > 0;
            panelEvtA1.Visible = evtNumA1.Enabled || evtNameA1.Enabled;
            panelEvtA2.Visible = evtNumA2.Enabled || evtNameA2.Enabled;
            panelEvtA3.Visible = evtNumA3.Enabled || evtNameA3.Enabled;
            panelEvtA4.Visible = evtNumA4.Enabled || evtNameA4.Enabled;
            panelEvtA5.Visible = evtNumA5.Enabled || evtNumA6.Enabled;
            if (evtEffects.Items.Count < 8)
                evtEffects.Height = evtEffects.Items.Count * 16 + 4;
            else
                evtEffects.Height = (evtEffects.Items.Count / 8) * 68;
            labelEvtA1.Visible = labelEvtA1.Text != "";
            labelEvtA2.Visible = labelEvtA2.Text != "";
            labelEvtA3.Visible = labelEvtA3.Text != "";
            labelEvtA4.Visible = labelEvtA4.Text != "";
            labelEvtA5.Visible = labelEvtA5.Text != "";
            labelEvtA6.Visible = labelEvtA6.Text != "";
            evtNumA1.Visible = evtNumA1.Enabled;
            evtNumA2.Visible = evtNumA2.Enabled;
            evtNumA3.Visible = evtNumA3.Enabled;
            evtNumA4.Visible = evtNumA4.Enabled;
            evtNumA5.Visible = evtNumA5.Enabled;
            evtNumA6.Visible = evtNumA6.Enabled;
            evtNameA1.Visible = evtNameA1.Enabled;
            evtNameA2.Visible = evtNameA2.Enabled;
            evtNameA3.Visible = evtNameA3.Enabled;
            evtNameA4.Visible = evtNameA4.Enabled;
            evtNumC2.Visible = evtNumC2.Enabled;
            panelEvtD1.Visible = evtNumD1.Enabled;
            panelEvtD2.Visible = evtNumD2.Enabled;
            panelEvtD3.Visible = evtNameD3.Enabled;
            panelEvtD4.Visible = evtButtonD4.Enabled || evtButtonD5.Enabled;
            evtListBoxD.Visible = evtListBoxD.Enabled;
            // organize
            groupBoxA.BringToFront();
            groupBoxB.BringToFront();
            groupBoxC.BringToFront();
            groupBoxD.BringToFront();
            panel1.BringToFront();
            //
            panelEvtA1.BringToFront();
            panelEvtA2.BringToFront();
            panelEvtA3.BringToFront();
            panelEvtA4.BringToFront();
            panelEvtA5.BringToFront();
            labelEvtA1.BringToFront();
            evtNameA1.BringToFront();
            evtNumA1.BringToFront();
            labelEvtA2.BringToFront();
            evtNameA2.BringToFront();
            evtNumA2.BringToFront();
            labelEvtA3.BringToFront();
            evtNameA3.BringToFront();
            evtNumA3.BringToFront();
            labelEvtA4.BringToFront();
            evtNameA4.BringToFront();
            evtNumA4.BringToFront();
            //
            labelEvtA5.BringToFront();
            evtNumA5.BringToFront();
            labelEvtA6.BringToFront();
            evtNumA6.BringToFront();
            labelEvtC1.BringToFront();
            evtNumC1.BringToFront();
            labelEvtC2.BringToFront();
            evtNumC2.BringToFront();
            //
            panelEvtD1.BringToFront();
            panelEvtD2.BringToFront();
            panelEvtD3.BringToFront();
            panelEvtD4.BringToFront();
            labelEvtD1.BringToFront();
            evtNumD1.BringToFront();
            labelEvtD2.BringToFront();
            evtNumD2.BringToFront();
            labelEvtD3.BringToFront();
            evtNameD3.BringToFront();
        }
    }
}
