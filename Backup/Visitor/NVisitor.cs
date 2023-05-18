using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.ControllerModel;

namespace FlowChart2.Visitor
{
    public abstract class NVisitor
    {
        public abstract void Visit(NBox component);        
        public abstract void Visit(NConnector component);        
    }
}
