using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FaultyComponents.Classes
{
    public class Module
    {
        public string Line { get; set; }
        public byte? Number { get; set; }
        public List<Slot> Slots { get; set; }
        public string date { get; set; }

    }
}