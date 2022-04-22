using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaultyComponents.Classes
{
    public class Slot
    {
        public short? Number { get; set; }
        public List<PGdatas> PGs { get; set; }
    }
}