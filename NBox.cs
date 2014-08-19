using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace WindowsFormsApplication1
{
    public class NBox
    {
        private RectangleF rectangle;
        public NPoint StartPoint = new NPoint();
        public NPoint EndPoint = new NPoint();
        private NPoint centerPoint = new NPoint();
        public NPoint CenterPoint
        {
            get
            {
                centerPoint.X = (StartPoint.X + EndPoint.X) / 2;
                centerPoint.Y = (StartPoint.Y + EndPoint.Y) / 2;
                return centerPoint;
            }
        }
        public float W
        {
            get
            {
                return EndPoint.X - StartPoint.X;
            }
        }
        public float H
        {
            get
            {
                return EndPoint.Y - StartPoint.Y;
            }
        }

        public RectangleF Rectangle
        {
            get
            {
                rectangle.X = StartPoint.X;
                rectangle.Y = StartPoint.Y;
                rectangle.Width = this.W;
                rectangle.Height = this.H;
                rectangle.Inflate(1, 1);
                return rectangle;
            }
        }

        internal void Add(NBox hitBox)
        {
            List<NPoint> points = new List<NPoint>();
            points.Add(this.StartPoint.Clone());
            points.Add(this.EndPoint.Clone());
            points.Add(hitBox.StartPoint.Clone());
            points.Add(hitBox.EndPoint.Clone());

            points.Add(this.StartPoint.Clone().Add(this.W, 0));
            points.Add(this.EndPoint.Clone().Add(-this.W, 0));
            points.Add(hitBox.StartPoint.Clone().Add(hitBox.W, 0));
            points.Add(hitBox.EndPoint.Clone().Add(-hitBox.W, 0));

            this.StartPoint.Set(points.Min(x => x.X), points.Min(x => x.Y));
            this.EndPoint.Set(points.Max(x => x.X), points.Max(x => x.Y));
        }
    }
}
