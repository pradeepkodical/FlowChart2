using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.ControllerModel;
using FlowChart2.Modifiers;

namespace FlowChart2.Visitor
{
    public class NLoadVisitor:NVisitor
    {
        private List<NComponent> components;
        public NLoadVisitor(List<NComponent> components)
        {
            this.components = components;
        }
        public override void Visit(NBox component)
        {
            
        }

        public override void Visit(NConnector component)
        {
            NVisitor visitor = new NStartConnectVisitor(component);
            
            NComponent box = components.Find(x=>x.GetFlatModel().ID == component.Model.StartBoxID);
            if (box != null)
            {
                box.Accept(visitor);                
            }

            visitor = new NEndConnectVisitor(component);
            box = components.Find(x => x.GetFlatModel().ID == component.Model.EndBoxID);
            if (box != null)
            {
                box.Accept(visitor);                
            }
        }
    }

    public class NStartConnectVisitor : NVisitor
    {
        private NConnector connector;
        public NStartConnectVisitor(NConnector connector)
        {
            this.connector = connector;
        }

        public override void Visit(NBox component)
        {
            connector.StartBox = component;
        }

        public override void Visit(NConnector component)
        {
            
        }
    }

    public class NEndConnectVisitor : NVisitor
    {
        private NConnector connector;
        public NEndConnectVisitor(NConnector connector)
        {
            this.connector = connector;
        }

        public override void Visit(NBox component)
        {
            connector.EndBox = component;
        }

        public override void Visit(NConnector component)
        {

        }
    }

    public class NConnectorConnetVisitor : NVisitor
    {
        private NPathResolver resolver;

        public NConnectorConnetVisitor(NPathResolver resolver)        
        {
            this.resolver = resolver;
        }

        public override void Visit(NBox component)
        {
        }

        public override void Visit(NConnector component)
        {
            resolver.Connector = component;
            resolver.StartPoint = component.Model.StartPoint;
            resolver.EndPoint = component.Model.EndPoint;
            
            resolver.RebuildGraph();
            resolver.Resolve();
        }        
    }
}
