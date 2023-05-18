using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

using PK.Grid.Def;

namespace PK.Grid.View
{
    public class CellView<T> : BoxView
    {
        public Font TextFont { get; set; }

        public bool IsMouseOn { get; set; }
        public bool IsMouseDown { get; set; }

        public ColumnDef<T> Column { get; set; }
        public T Model { get; set; }

        public virtual void Draw(Graphics graphics)
        {
            RectangleF rect = BoundingBox;
            rect.Inflate(-1, -1);
            if (IsMouseDown)
            {
                graphics.FillRectangle(Brushes.Yellow, rect);
            }
            else if (IsMouseOn)
            {
                graphics.FillRectangle(Brushes.YellowGreen, rect);
            }
            else
            {
                graphics.FillRectangle(Brushes.WhiteSmoke, rect);
            }

            graphics.DrawRectangle(Pens.Gray,
                BoundingBox.Left,
                BoundingBox.Top,
                BoundingBox.Width,
                BoundingBox.Height);

            //rect.Inflate(-1, -1);
            StringFormat sf = new StringFormat(StringFormat.GenericDefault);
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;
            graphics.DrawString(Column.CellRenderer(Model), TextFont, Brushes.Black, rect, sf);

        }
    }
}
