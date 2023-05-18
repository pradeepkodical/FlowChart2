using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart2.ControllerModel;
using FlowChart2.Utility;
using FlowChart2.Visitor;

namespace FlowChart2.Modifiers
{
    public class NPathResolver
    {
        #region Path
        private NPoint up;
        private NPoint down;
        private NPoint left;
        private NPoint right;

        #endregion

        private int width = 1500;
        private int height = 1000;

        
        private List<NNodeX> originalAllPoints = new List<NNodeX>();
        private List<NNodeX> allPoints = new List<NNodeX>();

        public NPathResolver()
        {
            this.up = new NPoint {  X=0, Y=-NConfig.BLOCK_SIZE };
            this.down = new NPoint { X = 0, Y = NConfig.BLOCK_SIZE };
            this.left = new NPoint { X = -NConfig.BLOCK_SIZE, Y = 0 };
            this.right = new NPoint { X = NConfig.BLOCK_SIZE, Y = 0 };            
        }

        private int PointToId(NPoint point)
        {
            if (point.X >= this.width) return -1;
            if (point.Y >= this.height) return -1;
            if (point.X < 0) return -1;
            if (point.Y < 0) return -1;

            return (int)((point.Y / NConfig.BLOCK_SIZE) * (width / NConfig.BLOCK_SIZE) + (point.X / NConfig.BLOCK_SIZE));
        }       

        public NConnector Connector { get; set; }
        public NBox BoundingBox { get; set; }
        public List<NComponent> Components { get; set; }
        public NPoint StartPoint { get; set; }
        public NPoint EndPoint { get; set; }

        public void BuildGraph()
        {
            using (new MethodTracer())
            {
                this.width = ((int)(this.BoundingBox.Width / NConfig.BLOCK_SIZE)) * NConfig.BLOCK_SIZE;
                this.height = ((int)(this.BoundingBox.Height / NConfig.BLOCK_SIZE)) * NConfig.BLOCK_SIZE;

                allPoints.Clear();
                for (int h = 0; h < height; h += NConfig.BLOCK_SIZE)
                {
                    for (int w = 0; w < width; w += NConfig.BLOCK_SIZE)
                    {
                        NPoint point = new NPoint { X = w, Y = h };
                        allPoints.Add(new NNodeX
                        {
                            Point = point,
                            Blocked = !(
                                  //point.Equals(StartPoint) ||
                                  //point.Equals(EndPoint) ||
                                  !BoundingBox.Contains(point) ||
                                  HitTest(Components, point) == null
                            )
                        });
                    }
                }

                allPoints.FindAll(x => !x.Blocked).ForEach(point =>
                {

                    int id = PointToId(point.Point.Clone().Add(up));
                    if (id >= 0 && id < allPoints.Count)
                    {
                        if (!allPoints[id].Blocked)
                        {
                            point.Neighbors.Add(id);
                        }
                    }

                    id = PointToId(point.Point.Clone().Add(down));
                    if (id >= 0 && id < allPoints.Count)
                    {
                        if (!allPoints[id].Blocked)
                        {
                            point.Neighbors.Add(id);
                        }
                    }

                    id = PointToId(point.Point.Clone().Add(left));
                    if (id >= 0 && id < allPoints.Count)
                    {
                        if (!allPoints[id].Blocked)
                        {
                            point.Neighbors.Add(id);
                        }
                    }

                    id = PointToId(point.Point.Clone().Add(right));
                    if (id >= 0 && id < allPoints.Count)
                    {
                        if (!allPoints[id].Blocked)
                        {
                            point.Neighbors.Add(id);
                        }
                    }
                });                
            }
            CopyPoints(allPoints, originalAllPoints);
        }

        private void CopyPoints(List<NNodeX> srcPoints, List<NNodeX> dstPoints)
        {
            using (MethodTracer tracer = new MethodTracer())
            {
                dstPoints.Clear();
                srcPoints.ForEach(x =>
                {
                    dstPoints.Add(new NNodeX
                    {
                        Blocked = x.Blocked,
                        Neighbors = x.Neighbors.ToList(),
                        Point = x.Point.Clone()
                    });
                });                
            }
        }
        public void RebuildGraph()
        {
            CopyPoints(originalAllPoints, allPoints);
            using (MethodTracer tracer = new MethodTracer())
            {                
                allPoints.ForEach(x =>
                {
                    if (x.Blocked)
                    {
                        x.Blocked = !(x.Point.Equals(StartPoint) || x.Point.Equals(EndPoint));
                    }
                });
            }
        }
        public void Resolve()
        {
            using (MethodTracer tracer = new MethodTracer())
            {                   
                Connector.EndNode = Process(StartPoint);
                Connector.TraversedNodes = new List<NNodeX>(this.allPoints);
                Connector.PathNodes = new List<NNodeX>();
                Connector.MakePath(Connector.EndNode);
            }
        }        

        private NNodeX Process(NPoint point)
        {
            using (MethodTracer tracer = new MethodTracer())
            {
                int level = 0;
                List<NNodeX> masterList = new List<NNodeX>();
                List<NNodeX> neighbours = new List<NNodeX>();
                List<NNodeX> tempList = new List<NNodeX>();
                int id = PointToId(point);
                if (id < 0 || id >= allPoints.Count) return null;
                NNodeX startRoot = allPoints[id];
                startRoot.Level = level;
                startRoot.Visited = true;
                List<NNodeX> currNodes = new List<NNodeX> { startRoot };
                Connector.StartNode = startRoot;

                if (startRoot.Point.Equals(EndPoint))
                {
                    return startRoot;
                }
                do
                {
                    level++;
                    neighbours.Clear();
                    foreach (NNodeX currNode in currNodes)
                    {
                        NNodeX node = null;

                        foreach(int x in currNode.Neighbors){
                            if (allPoints[x].Point.Equals(EndPoint)) {
                                node = allPoints[x];
                                break;
                            }
                        }
                        
                        if (node != null)
                        {
                            node.Parent = currNode;
                            return node;
                        }

                        foreach(int x in currNode.Neighbors)
                        {
                            NNodeX nChild = allPoints[x];
                            if (!nChild.Blocked && !nChild.Visited)
                            {
                                nChild.Visited = true;
                                nChild.Parent = currNode;
                                neighbours.Add(nChild);
                            }
                        }                        
                    }
                    currNodes.Clear();
                    currNodes.AddRange(neighbours);

                } while (neighbours.Count > 0);

                return null;
            }
        }

        protected NComponent HitTest(List<NComponent> boxes, NPoint point)
        {
            NHitTestVisitor visitor = new NHitTestVisitor(point);
            foreach (NComponent box in boxes)
            {
                box.Accept(visitor);
                if (visitor.IsHit)
                {
                    return box;
                }
            }
            return null;
        }        
    }
}
