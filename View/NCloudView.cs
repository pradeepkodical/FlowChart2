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
    public class NCloudView:NBoxView
    {
        private NCloud element;

        public NCloudView(NCloud element)
            :base(element)
        {
            this.element = element;
            
        }

        private RectangleF MakeRect(float x, float y, float w, float h)
        { 
            float max_width = 486;
            float max_height = 326;

            return new RectangleF(                
                element.X + (x * element.Width / max_width),
                element.Y + (y * element.Height / max_height),
                w * element.Width / max_width,
                h * element.Height / max_height
            );
        }

        private List<RectangleF> GetElements()
        {            
            List<RectangleF> rects = new List<RectangleF> 
            {
                MakeRect( 0, 110, 99, 87),
                MakeRect( 15, 183, 85, 84),
                MakeRect( 42, 136, 178, 172),
                MakeRect( 50, 30, 145, 136),
                MakeRect( 152, 27, 237, 228),
                MakeRect( 162, 9, 102, 99),
                MakeRect( 175, 172, 157, 154),
                MakeRect( 249, 0, 95, 88),
                MakeRect( 296, 158, 128, 126),
                MakeRect( 322, 0, 113, 111),
                MakeRect( 359, 95, 127, 129),
                MakeRect( 372, 43, 104, 105)
            };            

            return rects;
        }
       
        public override void Draw(Graphics graphics)
        {
            List<RectangleF> rectangles = GetElements();

            using (Pen pen = BorderPen())
            {
                rectangles.ForEach(x => graphics.DrawEllipse(pen, x));
            }

            using (Brush coolBrush = BackgroundBrush())
            {
                rectangles.ForEach(x =>
                {
                    x.Inflate(-1, -1);
                    graphics.FillEllipse(coolBrush, x);
                });                            
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
