using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class PathResolver
    {
        #region Path
        private int blockSize;
        private NPoint up;
        private NPoint down;
        private NPoint left;
        private NPoint right;

        private RectangleF boundry;
        
        private List<NBox> boxes;
        private NPoint startPoint;
        private NPoint endPoint;
        
        #endregion

        public PathResolver(int blockSize)
        {
            this.blockSize = blockSize;
            up = new NPoint { X = 0, Y = -blockSize };
            down = new NPoint { X = 0, Y = blockSize };
            left = new NPoint { X = -blockSize, Y = 0 };
            right = new NPoint { X = blockSize, Y = 0 };
        }

        public NNodeX Resolve(NBox boundry, List<NBox> boxes, NPoint startPoint, NPoint endPoint)
        {
            //using (MethodTracer tracer = new MethodTracer())
            {
                this.boundry = boundry.Rectangle;
                this.boxes = boxes;

                this.startPoint = startPoint;
                this.endPoint = endPoint;

                BuildGraph();

                return Process(startPoint);
            }
        }
        
        private List<NNodeX> GetNeighbours(NNodeX parent,int level)
        {
            //using (MethodTracer tracer = new MethodTracer())
            {
                NPoint point = parent.Point;
                List<NPoint> neighbours = graph.Edges.Where(x => x.EndPoint.Equals(point)).Select(x => x.StartPoint.Clone()).ToList();
                neighbours.AddRange(graph.Edges.Where(x => x.StartPoint.Equals(point)).Select(x => x.EndPoint.Clone()));
                return neighbours.Select(x => new NNodeX { Point = x, Parent = parent, Level = level }).ToList();
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

                NNodeX startRoot = new NNodeX { Point = point, Parent = null, Level = level };
                List<NNodeX> currNodes = new List<NNodeX> { startRoot };

                do
                {
                    level++;
                    neighbours.Clear();
                    foreach (NNodeX currNode in currNodes)
                    {
                        masterList.Add(currNode);
                        tempList = GetNeighbours(currNode, level);                        

                        NNodeX node = tempList.Find(x => x.Point.Equals(endPoint));
                        if (node != null)
                        {                            
                            return node;
                        }

                        foreach (NNodeX nChild in tempList)
                        {
                            if (!neighbours.Exists(x => x.Point.Equals(nChild.Point)) &&
                                !masterList.Exists(y => y.Point.Equals(nChild.Point)))
                            {
                                neighbours.Add(nChild);
                            }
                        }
                        currNode.Children.AddRange(tempList);                        
                    }
                    currNodes.Clear();
                    currNodes.AddRange(neighbours);

                } while (neighbours.Count > 0);

                return null;
            }
        }   

        private void BuildGraph()
        {
            //using (MethodTracer tracer = new MethodTracer())
            {
                NPoint prevPoint = new NPoint { X = 0, Y = 0 };
                NPoint nextPoint = new NPoint { X = 0, Y = 0 };
                for (int x = blockSize; x < boundry.Width; x += blockSize)
                {
                    prevPoint.Set(x, 0);
                    for (int y = blockSize; y < boundry.Height; y += blockSize)
                    {
                        nextPoint.Set(x, y);
                        if (nextPoint.Equals(startPoint) || nextPoint.Equals(endPoint) || HitTest(boxes, nextPoint) == null)
                        {
                            graph.Edges.Add(new NEdge { StartPoint = prevPoint.Clone(), EndPoint = nextPoint.Clone() });
                            prevPoint.Set(nextPoint);
                        }
                        else
                        {
                            while (y < boundry.Height
                                && !nextPoint.Equals(startPoint)
                                && !nextPoint.Equals(endPoint)
                                && HitTest(boxes, nextPoint) != null)
                            {
                                y += blockSize;
                                nextPoint.Set(x, y);
                                prevPoint.Set(nextPoint);
                            }
                        }
                    }
                }

                for (int y = blockSize; y < boundry.Height; y += blockSize)
                {
                    prevPoint.Set(0, y);
                    for (int x = blockSize; x < boundry.Width; x += blockSize)
                    {
                        nextPoint.Set(x, y);
                        if (nextPoint.Equals(startPoint) || nextPoint.Equals(endPoint) || HitTest(boxes, nextPoint) == null)
                        {
                            graph.Edges.Add(new NEdge { StartPoint = prevPoint.Clone(), EndPoint = nextPoint.Clone() });
                            prevPoint.Set(nextPoint);
                        }
                        else
                        {
                            while (x < boundry.Width
                                && !nextPoint.Equals(startPoint)
                                && !nextPoint.Equals(endPoint)
                                && HitTest(boxes, nextPoint) != null)
                            {
                                x += blockSize;
                                nextPoint.Set(x, y);
                                prevPoint.Set(nextPoint);
                            }
                        }
                    }
                }
            }
        }        
        
        protected NBox HitTest(List<NBox> boxes, NPoint point)
        {
            foreach (NBox box in boxes)
            {
                if(box.Rectangle.Contains(point.X, point.Y))
                {
                    return box;
                }
            }
            return null;
        }        
    }
}
