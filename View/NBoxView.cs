using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using FlowChart2.ControllerModel;
using System.Windows.Forms;
using FlowChart2.Utility;

namespace FlowChart2.View
{
    public class NBoxView : NView
    {
        private NBox element;

        public NBoxView(NBox element):base(element)
        {
            this.element = element;
        }

        protected Pen BorderPen()
        {
            return new Pen(Brushes.CornflowerBlue, 2);
        }

        protected Brush BackgroundBrush()
        {
            return new LinearGradientBrush(
                new RectangleF(
                    this.element.X,
                    this.element.Y,
                    this.element.Width,
                    this.element.Height),
                Color.FromArgb(255, Color.Green), Color.Yellow, -120f);
        }

        protected override void DrawEdges(Graphics graphics)
        {
            NPoint point = new NPoint 
            { 
                X = this.element.X, 
                Y = this.element.Y
            };

            DrawResizeVertex(graphics, point);            
            point.Add(this.element.Width, this.element.Height);
            DrawResizeVertex(graphics, point);            

            for (int i = 0; i < this.element.ConnectPoints.Length; i++)
            {
                DrawVertex(graphics, this.element.ConnectPoints[i]);
            }
        }

        public override void Draw(Graphics graphics)
        {
            using (Brush coolBrush = BackgroundBrush())
            {
                graphics.FillRectangle(coolBrush,
                       this.element.X, 
                       this.element.Y,
                       this.element.Width, 
                       this.element.Height);                
            }

            using (Pen pen = BorderPen())
            {
                graphics.DrawRectangle(pen,
                    this.element.X,
                    this.element.Y,
                    this.element.Width,
                    this.element.Height);
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
