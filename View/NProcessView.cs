using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.ControllerModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace FlowChart2.View
{
    class NProcessView:NBoxView
    {
        private NProcess element;
        public NProcessView(NProcess element)
            :base(element)
        {
            this.element = element;
        }
       
        public override void Draw(Graphics graphics)
        {
            PointF[] points = new PointF[] { 
                new PointF(this.element.X + this.element.Width*0.1f, this.element.Y),
                new PointF(this.element.X + this.element.Width*1.1f, this.element.Y),                
                new PointF(this.element.X + this.element.Width*0.9f, this.element.Y + this.element.Height),
                new PointF(this.element.X - this.element.Width*0.1f, this.element.Y + this.element.Height)                
            };
            
            using (Brush coolBrush = BackgroundBrush())
            {
                graphics.FillPolygon(coolBrush, points);
            }

            using (Pen pen = BorderPen())
            {
                graphics.DrawPolygon(pen, points);
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
