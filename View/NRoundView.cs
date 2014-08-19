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
    public class NRoundView:NBoxView
    {
        private NRound element;
        public NRoundView(NRound element)
            :base(element)
        {
            this.element = element;
        }
       
        public override void Draw(Graphics graphics)
        {
            using (Brush coolBrush = BackgroundBrush())
            {
                graphics.FillEllipse(coolBrush, element.X, element.Y, element.Width, element.Height);
                using (Pen pen = BorderPen())
                {
                    graphics.DrawEllipse(pen, element.X, element.Y, element.Width, element.Height);
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
