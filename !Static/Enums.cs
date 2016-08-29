using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR
{
    // other
    public enum EType
    {
        EventScript,
        Location
    }
    public enum MessageIcon
    {
        None,
        Error,
        Info,
        Warning
    }
    public enum ScriptType
    {
        Event, Character, Vehicle, Map, Null
    }
    public enum TilesetType
    {
        Location, World
    }
    public enum TilemapType
    {
        Location, Template, None
    }
    public enum Drawing
    {
        None, Draw, Erase, Fill, FillAll, ReplaceColor, Dropper, Select
    }
}
