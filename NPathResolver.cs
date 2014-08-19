using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class NPathResolver
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

        int width = 1500;
        int height = 1000;

        public List<NNodeX> AllPoints = new List<NNodeX>();

        public NPathResolver(int blockSize)
        {
            this.blockSize = blockSize;
            up = new NPoint { X = 0, Y = -blockSize };
            down = new NPoint { X = 0, Y = blockSize };
            left = new NPoint { X = -blockSize, Y = 0 };
            right = new NPoint { X = blockSize, Y = 0 };            
        }

        private int PointToId(NPoint point)
        {
            if (point.X > this.width) return -1;
            if (point.Y > this.height) return -1;
            if (point.X < 0) return -1;
            if (point.Y < 0) return -1;

            return (int)((point.Y / blockSize) * (width / blockSize) + (point.X / blockSize));
        }

        private void BuildGraph()
        {
            for (int h = 0; h < height; h += blockSize)
            {
                for (int w = 0; w < width; w+=blockSize)
                {
                    NPoint point = new NPoint { X = w, Y = h };
                    AllPoints.Add(new NNodeX 
                    { 
                        Point = point,
                        Blocked = !(point.Equals(startPoint) || point.Equals(endPoint) || HitTest(boxes, point)==null)                        
                    });                    
                }
            }

            AllPoints.FindAll(x=>!x.Blocked).ForEach(point => { 

                int id = PointToId(point.Point.Clone().Add(up));
                if (id >= 0 && id < AllPoints.Count)
                {
                    if (!AllPoints[id].Blocked)
                    {
                        point.Children.Add(AllPoints[id]);
                    }
                }

                id = PointToId(point.Point.Clone().Add(down));
                if (id >= 0 && id < AllPoints.Count)
                {
                    if (!AllPoints[id].Blocked)
                    {
                        point.Children.Add(AllPoints[id]);
                    }
                }

                id = PointToId(point.Point.Clone().Add(left));
                if (id >= 0 && id < AllPoints.Count)
                {
                    if (!AllPoints[id].Blocked)
                    {
                        point.Children.Add(AllPoints[id]);
                    }
                }

                id = PointToId(point.Point.Clone().Add(right));
                if (id >= 0 && id < AllPoints.Count)
                {
                    if (!AllPoints[id].Blocked)
                    {
                        point.Children.Add(AllPoints[id]);
                    }
                }
            });
        }

        public NNodeX Resolve(NBox boundry, List<NBox> boxes, NPoint startPoint, NPoint endPoint)
        {
            //using (MethodTracer tracer = new MethodTracer())
            {
                this.boundry = boundry.Rectangle;
                this.boxes = boxes;

                this.width = ((int)(this.boundry.Width / blockSize)) * blockSize;
                this.height = ((int)(this.boundry.Height / blockSize)) * blockSize;

                this.startPoint = startPoint;
                this.endPoint = endPoint;

                BuildGraph();

                return Process(startPoint);
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

                NNodeX startRoot = AllPoints[PointToId(point)];// new NNodeX { Point = point, Parent = null, Level = level };
                startRoot.Level = level;
                startRoot.Visited = true;
                List<NNodeX> currNodes = new List<NNodeX> { startRoot };

                do
                {
                    level++;
                    neighbours.Clear();
                    foreach (NNodeX currNode in currNodes)
                    {
                        NNodeX node = currNode.Children.Find(x => x.Point.Equals(endPoint));
                        if (node != null)
                        {
                            node.Parent = currNode;
                            return node;
                        }

                        foreach (NNodeX nChild in currNode.Children)
                        {
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
