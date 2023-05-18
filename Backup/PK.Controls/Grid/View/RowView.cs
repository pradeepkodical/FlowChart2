using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PK.Grid.View
{
    public class RowView<T> : BoxView
    {
        public T Model { get; set; }
        public virtual void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(Pens.Gray, 
                BoundingBox.Left, 
                BoundingBox.Top, 
                BoundingBox.Width, 
                BoundingBox.Height);
        }
    }
}
