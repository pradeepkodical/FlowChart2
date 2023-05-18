using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace PK.Grid.View
{
    public class GridView<T> : BoxView
    {
        public List<T> Model { get; set; }
        public virtual void Draw(Graphics graphics)
        {
            graphics.DrawRectangle(Pens.DarkGray, 
                BoundingBox.Left, 
                BoundingBox.Top, 
                BoundingBox.Width-1, 
                BoundingBox.Height-1);
        }
    }    
}
