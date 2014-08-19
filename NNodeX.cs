using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    public class NNodeX
    {
        public NNodeX Parent;
        public List<NNodeX> Children = new List<NNodeX>();
        public NPoint Point;
        public int Level;

        public bool Blocked { get; set; }
        public bool Visited { get; set; }        
    }
}
