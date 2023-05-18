using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.ControllerModel;
using FlowChart2.Utility;
using System.Threading;
using FlowChart2.Modifiers;

namespace FlowChart2.Visitor
{
    public class NVertexMovedVisitor:NVisitor
    {
        private NConnectVisitor connectVisitor;
        private NBoxMovedVisitor boxVisitor;
        private List<NComponent> components;
        
        public NVertexMovedVisitor(NPathResolver pathResolver, List<NComponent> components)
        {
            this.components = components;
            connectVisitor = new NConnectVisitor { PathResolver = pathResolver };
            boxVisitor = new NBoxMovedVisitor { PathResolver = pathResolver };
        }

        public override void Visit(NBox component)
        {
            component.RecomputeConnectPoints();
            component.VertexMoved += new Action<NBox, NPoint>(Box_VertexMoved);     
        }
        
        public override void Visit(NConnector component)
        {
            component.VertexMoved += new Action<NConnector, NPoint>(Connector_VertexMoved);
            component.VertexMoving += new Action<NConnector, NPoint>(Connector_VertexMoving);
        }

        void Box_VertexMoved(NBox component, NPoint selectedPoint)
        {
            using (new MethodTracer())
            {
                boxVisitor.BoxComponent = component;
                boxVisitor.PathResolver.BuildGraph();
                this.components.ForEach(x =>
                {
                    x.Accept(boxVisitor);
                });                
            }
        }
        
        void Connector_VertexMoving(NConnector component, NPoint selectedPoint)
        {
            Connect(component, selectedPoint, false);
        }

        void Connector_VertexMoved(NConnector component, NPoint selectedPoint)
        {
            Connect(component, selectedPoint, true);            
        }

        void Connect(NConnector component, NPoint selectedPoint, bool connect)
        {
            connectVisitor.Connector = component;
            connectVisitor.SelectedPoint = selectedPoint;
            connectVisitor.Connect = connect;
       
            if (component.Model.StartPoint == selectedPoint)
            {
                component.StartBox = null;
                component.Model.StartBoxID = null;                
            }
            else
            {
                component.EndBox = null;
                component.Model.EndBoxID = null;
            }

            this.components.ForEach(x =>
            {
                x.Accept(connectVisitor);
            });
        }        
    }
}
