using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.ControllerModel;
using FlowChart2.Utility;
using FlowChart2.Modifiers;

namespace FlowChart2.Visitor
{
    internal class NConnectVisitor : NVisitor
    {
        public NPathResolver PathResolver;        

        public NConnector Connector { get; set; }
        public NPoint SelectedPoint { get; set; }
        public bool Connect { get; set; }                
        
        public override void Visit(NBox component)
        {            
            component.IsCloseEnough = false;            

            for (int i = 0; i < component.ConnectPoints.Length; i++)
            {
                double d = NMathHelper.Distance(SelectedPoint, component.ConnectPoints[i]);
                if (d <= NConfig.CONNECT_DISTANCE)
                {
                    component.IsCloseEnough = true;
                    if (Connect)
                    {
                        if (Connector.Model.StartPoint == SelectedPoint)
                        {
                            Connector.StartBox = component;
                            Connector.Model.StartBoxIndex = i;
                            Connector.Model.StartBoxID = component.Model.ID;
                        }
                        else
                        {
                            Connector.EndBox = component;
                            Connector.Model.EndBoxIndex = i;
                            Connector.Model.EndBoxID = component.Model.ID;
                        }
                        SelectedPoint.Set(component.ConnectPoints[i]);


                        PathResolver.Connector = Connector;
                        PathResolver.StartPoint = Connector.Model.StartPoint;
                        PathResolver.EndPoint = Connector.Model.EndPoint;

                        PathResolver.RebuildGraph();
                        PathResolver.Resolve();

                        return;
                    }
                }
            }            
        }

        public override void Visit(NConnector component)
        {            
        }
    }
}
