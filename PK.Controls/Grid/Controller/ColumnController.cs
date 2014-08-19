using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PK.Grid.View;
using PK.Grid.Def;

namespace PK.Grid.Controller
{
    public class ColumnController<T>
    {
        public ColumnView<T> View { get; set; }
        public ColumnDef<T> Model { get; set; }
        internal virtual void OnMouseMove(MouseEventArgs e)
        {
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
        }
    }
}
