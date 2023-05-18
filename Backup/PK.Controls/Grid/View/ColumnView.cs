using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using PK.Grid.Def;

namespace PK.Grid.View
{
    public class ColumnView<T> : BoxView
    {
        public Font TextFont { get; set; }
        public bool IsMouseDown { get; set; }
        public ColumnDef<T> Model { get; set; }
        public virtual void Draw(Graphics graphics)        
        {
            if (IsMouseDown)
            {
                graphics.FillRectangle(Brushes.Yellow, BoundingBox);
            }
            else
            {
                graphics.FillRectangle(Brushes.DarkOrange, BoundingBox);
            }

            graphics.DrawRectangle(Pens.Gray, 
                BoundingBox.Left, 
                BoundingBox.Top, 
                BoundingBox.Width, 
                BoundingBox.Height);
            
            StringFormat sf = new StringFormat(StringFormat.GenericDefault);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            graphics.DrawString(Model.HeaderText, TextFont, Brushes.Black, BoundingBox, sf);




        }
    }
}
