using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.Visitor;

namespace FlowChart2.ControllerModel
{
    public class NCloud : NBox
    {        
        public override void Accept(NViewVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
