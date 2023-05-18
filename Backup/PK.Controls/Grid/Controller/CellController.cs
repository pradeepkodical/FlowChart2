using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using PK.Grid.View;

namespace PK.Grid.Controller
{
    public class CellController<T>
    {   
        public CellView<T> View { get; set; }
        public T Model { get; set; }

        internal virtual void OnMouseMove(MouseEventArgs e)
        {
            View.IsMouseOn = true;
        }

        internal virtual void OnMouseDown(MouseEventArgs e)
        {
            View.IsMouseDown = true;
        }

        internal virtual void OnMouseUp(MouseEventArgs e)
        {
            View.IsMouseDown = false;
        }

        internal void Draw(Graphics graphics)
        {
            View.Model = Model;
            View.Draw(graphics);
        }

        internal void Unselect()
        {
            View.IsMouseDown = false;
            View.IsMouseOn = false;
        }
    }
}
