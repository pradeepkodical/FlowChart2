using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using PK.Grid.View;


namespace PK.Grid.Controller
{
    public class RowController<T>
    {
        public RowView<T> View { get; set; }
        public T Model { get; set; }

        public CellController<T>[] Cells { get; set; }

        internal virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        internal virtual void OnMouseDown(MouseEventArgs e)
        {
        }

        internal virtual void OnMouseUp(MouseEventArgs e)
        {
        }

        internal void Draw(Graphics graphics)
        {
            View.Model = Model;            
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i].Model = Model;
                Cells[i].Draw(graphics);                
            }
            View.Draw(graphics);
        }
    }
}
