using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart2.Visitor;
using FlowChart2.Utility;
using System.Windows.Forms;

namespace FlowChart2.ControllerModel
{
    public class NRound:NBox
    {
        public override void Accept(NViewVisitor visitor)
        {
            visitor.Visit(this);
        }        
    }
}
