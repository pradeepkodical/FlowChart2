using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowChart2.ControllerModel
{
    public class NNodeX
    {
        public NNodeX Parent;
        //public List<NNodeX> Neighbors = new List<NNodeX>();
        public List<int> Neighbors = new List<int>();
        public NPoint Point;
        public int Level;

        public bool Blocked;
        public bool Visited;    
    }
}
