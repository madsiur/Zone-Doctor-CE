using System;
using System.Collections.Generic;
using System.Text;

namespace ZONEDOCTOR.Undo
{
    interface Command
    {
        bool AutoRedo();
        void Execute();
    }
}
