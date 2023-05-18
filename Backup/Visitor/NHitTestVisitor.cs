using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.ControllerModel;

namespace FlowChart2.Visitor
{
    public class NHitTestVisitor:NVisitor
    {
        public bool IsHit { get; private set; }
        private NPoint point;

        public NHitTestVisitor(NPoint point)
        {
            this.point = point;
        }

        public override void Visit(NBox component)
        {
            IsHit = false;
            if (component.Contains(point))
            {
                for (int i = 0; i < component.ConnectPoints.Length; i++)
                {
                    if (component.ConnectPoints[i].Equals(point))
                    {
                        return;
                    }
                }
                IsHit = true;
            }
        }

        public override void Visit(NConnector component)
        {
            IsHit = false;
            if (component.Contains(point))
            {
                if (component.PathNodes.Count > 4)
                {
                    foreach(NNodeX x in component.PathNodes.Take(4))
                    {
                        if (x.Point.Equals(point))
                        {
                            return;
                        }
                    };

                    foreach(NNodeX x in component.PathNodes.Skip(component.PathNodes.Count - 4))
                    {
                        if (x.Point.Equals(point))
                        {
                            return;
                        }
                    };
                    IsHit = true;
                }                
            }
        }
    }
}
