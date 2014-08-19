using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using FlowChart2.ControllerModel;
using FlowChart2.Utility;

namespace FlowChart2.View
{
    public class NDatabaseView:NBoxView
    {
        private NDatabase element;
        public NDatabaseView(NDatabase element)
            :base(element)
        {
            this.element = element;
        }
       
        public override void Draw(Graphics graphics)
        {
            using (Pen pen = BorderPen())
            {
                float factor = element.Height * 0.2f;
                graphics.DrawEllipse(pen, element.X, element.Y + element.Height - factor, element.Width, factor);
                graphics.DrawRectangle(pen, element.X, element.Y + factor/2, element.Width, element.Height - factor);
            
                using (Brush coolBrush = BackgroundBrush())
                {
                    graphics.FillEllipse(coolBrush, element.X, element.Y, element.Width, factor);
                    graphics.FillEllipse(coolBrush, element.X, element.Y + element.Height - factor, element.Width, factor);
                    graphics.FillRectangle(coolBrush, element.X, element.Y + factor/2, element.Width, element.Height - factor);                
                }
                graphics.DrawEllipse(pen, element.X, element.Y, element.Width, factor);

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
}
