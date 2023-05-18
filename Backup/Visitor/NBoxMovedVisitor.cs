using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.ControllerModel;
using FlowChart2.Modifiers;

namespace FlowChart2.Visitor
{
    public class NBoxMovedVisitor : NVisitor
    {
        public NPathResolver PathResolver { get; set; }
        public NBox BoxComponent {get; set;}
                
        public override void Visit(NConnector connector)
        {
            connector.Reset();

            if (BoxComponent == connector.StartBox)
            {
                connector.Model.StartPoint.Set(BoxComponent.ConnectPoints[connector.Model.StartBoxIndex]);
            }
            else if (BoxComponent == connector.EndBox)
            {
                connector.Model.EndPoint.Set(BoxComponent.ConnectPoints[connector.Model.EndBoxIndex]);
            }

            PathResolver.Connector = connector;
            PathResolver.StartPoint = connector.Model.StartPoint;
            PathResolver.EndPoint = connector.Model.EndPoint;
            PathResolver.RebuildGraph();
            PathResolver.Resolve();
        }

        public override void Visit(NBox component)
        {            
        }        
    }
}
