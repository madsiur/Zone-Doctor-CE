Zone Doctor CE 0.2
------------------
1.0 Expansions
2.0 Changes
	2.1 Setting window
	2.2 Setting file
	2.3 Location names
	2.4 Message Names
	2.5 Others
3.0 Links


1.0 EXPANSIONS
--------------
See ROM_map.txt for details

This version of Zone Doctor allow many expansion: Tilemaps, Tilemaps max space, max Locations (maps), 
NPC max space, Exits max space, Event Triggers max space, Chests max space, Chests SRAM bits and Message Names. This version
of FF6LE does not add any of the mentioned data (except blank tilemaps and location data to have working new maps), it only allow
to have more of the said data in your project. 

The default L1 tilemap ID ($15F) on maps $19F to $1FE is shadow's dream (256x256 black map with layer 1 only). 
Tilemaps $15F to $1FE are all the same, they take $1A bytes of space each.  To make the map bigger, you select your new 
tilemaps ID(s) (L1, L2 and L3 if neccesary). Then you resize the layers (from 256x256 to 1024x1024 as an example) and set 
the other map properties. Then you can either import tilemaps (which should cover the correct layers sizes) or hand draw.


2.0 CHANGES
-----------

2.1 SETTING WINDOW

In the setting window you can select a setting file path. When you click "Expand Maps",
the setting file is created. Map expansion give access to Chests expansion. Both expansion have an ASM validation that will
prompt a warning message if a ASM change does not have the expected value at its offset.

If you were using Zone Doctor+ or FF6LE+ before, you must check the checkbox before expansion. This will ensure
all your data will be carried on from your project.

If you want to use a "vanilla" ROM with this editor, blank out the setting file path and click "Apply". If you want to allow
expansion on a new ROM, change the filepath name or choose another directory for your XML file and click "Apply".

Once your ROM is expanded, you can increase the number of banks for tilemap space if at some point you are at the limit of the size 
originally input.


2.2 SETTING FILE

The settings are stored in a XML file. This file is read at editor loading to see if the project has expansions done. It is therefore
important to have a valid setting file path in the setting window. I would suggest to not change settings in this files other than location
names. Doing otherwise if you are not sure of what each setting does could limit or crash the editor depending on the ROM loaded.


2.3 LOCATIONS NAMES

You can change the names of the maps in the editor. This is mainly to name new maps. This feature doesn't change anything in the ROM,
it was made to make map edit easier and to know which map is what. Location names are saved in the setting file. This feature is disabled if your 
ROM doesn't have the map expansion done. The [$XXX] string starting the name is trimmed in setting file and in the editor textbox. 
It is applied automatically.


2.4 MESSAGE NAMES

In-Game map names can also be edited. Expansion allow 255 entries and max space is also expanded. Max length for a name is 37 characters.
Allowed characters are A-Z a-z 0-9 ! ? : \ ' - . , ; # + ( ) % ~


2.5 OTHERS

Maximum Event offsets for Event Triggers and Entrance Events are now $35FFFE ($FFFFFE) instead of $2FFFF ($CCFFFF). 
This allow to put directly events outside the $CA0000-$CCFFFF range. This feature was not possible for NPC event offsets 
since the NPC data only allow 18 bits for the event (and not 3 full bytes).


3.0 Links
---------
Forum Thread: http://www.ff6hacking.com/forums/showthread.php?tid=3327

Contact: themadsiur@gmail.com
http://madsiur.net

Big thanks to:
Warrax, B-Run and James White for testing different builds!
