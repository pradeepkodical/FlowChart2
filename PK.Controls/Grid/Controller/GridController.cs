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
    public class GridController<T>
    {
        #region Local Variables
        private int startIndex = 0;
        public int PageSize { get; set;}
        public float RowHeight { get; set; }
        #endregion

        #region View
        protected GridView<T> View { get; set; }
        #endregion

        #region Events
        public event Action Invalidate;
        protected void OnInvalidate()
        {
            if (this.Invalidate != null)
            {
                this.Invalidate();
            }
        }

        public event Action<T, ColumnDef<T>> CellClick;
        protected void OnCellClick(T row, ColumnDef<T> colDef)
        {
            if (this.CellClick != null)
            {
                if (row != null)
                {
                    this.CellClick(row, colDef);
                }
            }
        }
        #endregion

        #region Model
        public List<ColumnDef<T>> ColumnDefs{ get; set; }
        public List<T> Model { get; set; }
        #endregion

        #region Child Controllers
        public List<ColumnController<T>> Columns { get; protected set; }
        public List<RowController<T>> Rows { get; protected set; }        
        #endregion

        public GridController(GridView<T> view)
        {
            this.View = view;
            this.Rows = new List<RowController<T>>();
            this.Columns = new List<ColumnController<T>>();
        }
        
        #region Private Helpers
        private CellController<T> SelectedCell { get; set; }
        private RowController<T> SelectedRow { get; set; }
        private ColumnController<T> SelectedColumn { get; set; }
        private CellController<T> GetCellAt(int x, RowController<T> row)
        {
            if (row != null)
            {
                float left = 0;
                for (int i = 0; i < row.Cells.Length; i++)
                {
                    if (left < x && left + row.Cells[i].View.BoundingBox.Width > x)
                    {
                        return row.Cells[i];
                    }
                    left += row.Cells[i].View.BoundingBox.Width;
                }
            }
            return null;
        }

        private RowController<T> GetRowAt(int y)
        {
            int index = (int)(y / RowHeight);
            index--;
            if (index >= 0 && index < Rows.Count)
            {
                return Rows[index];
            }
            return null;
        }

        private ColumnController<T> GetColumnAt(int x)
        {
            float left = 0;
            for (int i = 0; i < Columns.Count; i++)
            {
                if (left < x && left + Columns[i].View.BoundingBox.Width > x)
                {
                    return Columns[i];
                }
                left += Columns[i].View.BoundingBox.Width;
            }
            return null;
        }
        #endregion

        internal virtual void OnMouseMove(MouseEventArgs e)
        {
            if (e.Y < RowHeight)
            {
                ColumnController<T> currentColumn = GetColumnAt(e.X);
                if (currentColumn != null)
                {
                    if (currentColumn != SelectedColumn)
                    {
                        UnselectAll();
                        SelectedColumn = currentColumn;
                    }
                    SelectedColumn.OnMouseMove(e);
                    this.OnInvalidate();
                    return;
                }
            }

            SelectedRow = GetRowAt(e.Y);
            CellController<T> currentCell = GetCellAt(e.X, SelectedRow);
            if (currentCell != SelectedCell)
            {
                UnselectAll();
                SelectedCell = currentCell;
            }

            if (SelectedCell != null)
            {
                SelectedCell.OnMouseMove(e);
                this.OnInvalidate();
            }
        }        

        internal virtual void OnMouseDown(MouseEventArgs e)
        {
            if (SelectedCell != null)
            {
                SelectedCell.OnMouseDown(e);
                this.OnInvalidate();
            }
            else if (SelectedColumn != null)
            {
                SelectedColumn.OnMouseDown(e);
                this.OnInvalidate();
            }
        }

        internal virtual void OnMouseUp(MouseEventArgs e)
        {
            if (SelectedRow != null && SelectedCell != null)
            {
                SelectedCell.OnMouseUp(e);
                this.OnInvalidate();
                this.OnCellClick(SelectedRow.Model, SelectedCell.View.Column);
            }
            else if (SelectedColumn != null)
            {
                SelectedColumn.OnMouseUp(e);
                this.OnInvalidate();
            }
        }

        internal void OnMouseClick(MouseEventArgs e)
        {
            foreach (ColumnDef<T> colDefs in this.ColumnDefs)
            {
                if (colDefs.Editor != null)
                {
                    colDefs.Editor.Hide();
                }
            }
        }

        internal void OnMouseDoubleClick(MouseEventArgs e)
        {
            if (SelectedCell != null)
            {
                if (SelectedCell.View.Column.Editor != null)
                {
                    SelectedCell.View.Column.Editor.ShowEditorFor(SelectedCell.View);
                }
            }
        }

        internal virtual void OnMouseLeave(EventArgs e)
        {
            UnselectAll();
            this.OnInvalidate();
        }

        private void UnselectAll()
        {
            Columns.ForEach(x =>
            {
                x.Unselect();
            });
            Rows.ForEach(r =>
            {
                for (int i = 0; i < r.Cells.Length; i++)
                {
                    r.Cells[i].Unselect();
                }
            });
            SelectedRow = null;
            SelectedCell = null;
            SelectedColumn = null;
        }

        internal virtual void OnMouseWheel(MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                startIndex -= PageSize;
            }
            else
            {
                startIndex += PageSize;
            }

            if (startIndex < 0) 
            {
                startIndex = 0;
            }

            if (startIndex > Model.Count - PageSize)
            {
                startIndex = Model.Count - PageSize;
            }
            this.OnInvalidate();
        }

        internal virtual void Draw(Graphics graphics)
        {
            if (Model != null)
            {
                View.Model = Model;
                List<T> models = Model.Skip(startIndex).Take(PageSize).ToList();
                
                for (int i = 0; i < models.Count; i++)
                {
                    Rows[i].Model = models[i];
                    Rows[i].Draw(graphics);                    
                }

                Columns.ForEach(x => x.Draw(graphics));
                View.Draw(graphics);
            }
        }        
    }    
}
