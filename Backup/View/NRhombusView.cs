using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.ControllerModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FlowChart2.View
{
    class NRhombusView:NBoxView
    {
        private NRhombus element;
        public NRhombusView(NRhombus element)
            :base(element)
        {
            this.element = element;
        }
       
        public override void Draw(Graphics graphics)
        {
            using (Brush coolBrush = BackgroundBrush())
            {
                PointF[] points = element.ConnectPoints.Select(x => new PointF(x.X, x.Y)).ToArray();
                graphics.FillPolygon(coolBrush, points);
                using (Pen pen = BorderPen())
                {
                    graphics.DrawPolygon(pen, points);
                }
            }

            DrawText(graphics,
                    this.TextFont,
                    this.element.Model.Text,
                    Brushes.Black,
                    this.element.X,
                    this.element.Y,
                    this.element.Width,
                    this.element.Height);

            DrawSelected(graphics);
        }
    }
}
