using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart2.ControllerModel;
using System.Windows.Forms;
using FlowChart2.Utility;
using System.Drawing.Drawing2D;

namespace FlowChart2.View
{
    public abstract class NView
    {
        private NComponent component;
        public NView(NComponent component)
        {
            this.component = component;
        }

        public Font TextFont { get; set; }

        public abstract void Draw(Graphics graphics);

        protected void DrawText(Graphics graphics, Font font, string text, Brush brush, float x, float y, float w, float h)
        {
            StringFormat sf = new StringFormat(StringFormat.GenericDefault);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            graphics.DrawString(text, font, brush, new RectangleF(x, y , w, h), sf);
        }        

        protected virtual void DrawSelected(Graphics graphics)
        {
            if (this.component.IsSelected || this.component.IsCloseEnough)
            {
                DrawEdges(graphics);
                if (this.component.SelectedPoint != null)
                {
                    Draw4Arrows(graphics, this.component.SelectedPoint, NConfig.BLOCK_SIZE_2);
                }
            }                
        }

        protected void DrawResizeVertex(Graphics graphics, NPoint point)
        {
            if (point != null)
            {
                NPoint s = point.Clone().Add(-NConfig.BLOCK_SIZE, -NConfig.BLOCK_SIZE);
                graphics.FillRectangle(Brushes.Goldenrod, s.X, s.Y, NConfig.BLOCK_SIZE_2, NConfig.BLOCK_SIZE_2);
                graphics.DrawRectangle(Pens.Red, s.X, s.Y, NConfig.BLOCK_SIZE_2, NConfig.BLOCK_SIZE_2);
            }
        }


        protected void DrawVertex(Graphics graphics, NPoint point)
        {
            if (point != null)
            {
                NPoint s = point.Clone().Add(-NConfig.BLOCK_SIZE, -NConfig.BLOCK_SIZE);
                graphics.FillRectangle(Brushes.CornflowerBlue, s.X, s.Y, NConfig.BLOCK_SIZE_2, NConfig.BLOCK_SIZE_2);
                graphics.DrawRectangle(Pens.Red, s.X, s.Y, NConfig.BLOCK_SIZE_2, NConfig.BLOCK_SIZE_2);
            }
        }

        protected void DrawArrow(Graphics graphics, NPoint src, NPoint dst, Brush brush, Pen pen, float length)
        {
            float angle = (float)Math.Atan2(dst.Y - src.Y, dst.X - src.X);
            float x1 = dst.X - (float)(0.5 * length * Math.Cos(angle + Math.PI / 6.0f));
            float y1 = dst.Y - (float)(0.5 * length * Math.Sin(angle + Math.PI / 6.0f));

            float x2 = dst.X - (float)(0.5 * length * Math.Cos(angle - Math.PI / 6.0f));
            float y2 = dst.Y - (float)(0.5 * length * Math.Sin(angle - Math.PI / 6.0f));

            graphics.FillPolygon(brush, new PointF[] { 
                new PointF(dst.X, dst.Y),
                new PointF(x1, y1),
                new PointF(x2, y2)
            });
            graphics.DrawPolygon(pen, new PointF[] { 
                new PointF(dst.X, dst.Y),
                new PointF(x1, y1),
                new PointF(x2, y2)
            });
        }

        protected void Draw4Arrows(Graphics g, NPoint p, float length)
        {
            /*   ^
             * <-|->
             *   v
             */

            DrawArrow(g, p, p.Clone().Add(0, -length), Brushes.YellowGreen, Pens.Red, length);
            DrawArrow(g, p, p.Clone().Add(0, length),  Brushes.YellowGreen, Pens.Red, length);
            DrawArrow(g, p, p.Clone().Add(-length, 0), Brushes.YellowGreen, Pens.Red, length);
            DrawArrow(g, p, p.Clone().Add(length, 0), Brushes.YellowGreen, Pens.Red, length);
        }

        protected abstract void DrawEdges(Graphics graphics);        
    }
}
