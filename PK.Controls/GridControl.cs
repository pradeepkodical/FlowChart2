using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PK.Grid.Controller;
using PK.Grid.View;
using PK.Grid.Def;
using System.Drawing;

namespace PK.Controls
{
    public class GridControl<T>:UserControl
    {
        private float rowHeight = 22;
        private float scrollWidth = 20;
        private GridController<T> controller;
        private GridView<T> view;

        public event Action<T, ColumnDef<T>> CellClick;

        public GridControl()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);  

            this.view = new GridView<T>();
            this.controller = new GridController<T>(view) {
                RowHeight = rowHeight
            };

            this.Resize += new EventHandler(GridControl_Resize);
            this.controller.Invalidate += new Action(controller_Invalidate);

            this.controller.CellClick += new Action<T, ColumnDef<T>>(controller_CellClick);
        }

        void controller_CellClick(T row, ColumnDef<T> col)
        {
            if(CellClick!=null)
            {
                CellClick(row, col);
            }
        }

        private void controller_Invalidate()
        {
            this.Invalidate();
        }

        private void GridControl_Resize(object sender, EventArgs e)
        {
            DoLayout();
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            controller.OnMouseWheel(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            controller.OnMouseMove(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            this.Focus();            
            controller.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            controller.OnMouseUp(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            controller.OnMouseClick(e);
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            controller.OnMouseDoubleClick(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            controller.OnMouseLeave(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            controller.Draw(e.Graphics);
        }        

        public void SetData(List<ColumnDef<T>> columnDefs, List<T> list)
        {
            controller.ColumnDefs = columnDefs;
            controller.Model = list;
            ReBuildColumns();
            DoLayout();
        }       

        private void ReBuildColumns()
        {
            controller.Columns.Clear();
            float widthSoFar = 0;
            for (int i = 0; i < controller.ColumnDefs.Count; i++)
            {
                controller.Columns.Add(new ColumnController<T>
                {
                    View = new ColumnView<T>
                    {
                        TextFont = new Font(this.Font, FontStyle.Bold),
                        BoundingBox = new RectangleF
                        {
                            X = widthSoFar,
                            Y = 0,
                            Width = controller.ColumnDefs[i].Width,
                            Height = rowHeight
                        }
                    },
                    Model = controller.ColumnDefs[i]
                });
                widthSoFar += controller.ColumnDefs[i].Width;
            }
        }

        #region Layout
        private void DoLayout()
        {
            view.BoundingBox = new RectangleF
            {
                X = 0,
                Y = 0,
                Width = this.Width,
                Height = this.Height
            };

            if (controller.Model != null)
            {
                controller.PageSize = (int)((this.Height - rowHeight) / rowHeight);

                RecomputeColumns();
                RecomputeRows();
                
            }
            this.Invalidate();
        }

        private void RecomputeRows()
        {
            #region Compute Row Width
            controller.Rows.Clear();
            float heightSoFar = rowHeight;
            for (int i = 0; i < controller.PageSize; i++)
            {
                RowController<T> row = new RowController<T>
                {
                    View = new RowView<T>
                    {
                        BoundingBox = new RectangleF
                        {
                            X = 0,
                            Y = heightSoFar,
                            Width = view.BoundingBox.Width - scrollWidth,
                            Height = rowHeight
                        }
                    }
                };

                RecomputeCells(row, heightSoFar);                

                controller.Rows.Add(row);

                heightSoFar += rowHeight;
            }
            #endregion
        }

        private void RecomputeCells(RowController<T> row, float heightSoFar)
        {
            #region Make Cells
            List<CellController<T>> cells = new List<CellController<T>>();
            float widthSoFar = 0;
            for (int j = 0; j < controller.Columns.Count; j++)
            {
                cells.Add(new CellController<T>
                {
                    View = new CellView<T>
                    {
                        TextFont = this.Font,
                        Column = controller.ColumnDefs[j],
                        BoundingBox = new RectangleF
                        {
                            X = widthSoFar,
                            Y = heightSoFar,
                            Width = controller.Columns[j].View.BoundingBox.Width,
                            Height = rowHeight
                        }
                    }
                });
                widthSoFar += controller.Columns[j].View.BoundingBox.Width;
            }
            row.Cells = cells.ToArray();
            #endregion
        }

        private void RecomputeColumns()
        {
            #region Compute Column Width
            float widthSoFar = 0;

            controller.ColumnDefs.FindAll(x=>x.Flex>0).ForEach(x => x.Width = 0);
            float absoluteWidth = controller.ColumnDefs.Sum(x => x.Width);
            float totalFlex = controller.ColumnDefs.Sum(x => x.Flex);
            float distributeWidth = this.Width - scrollWidth - absoluteWidth;

            controller.ColumnDefs.FindAll(x => x.Flex > 0).ForEach(x => {
                x.Width = distributeWidth * x.Flex / totalFlex;
            });

            for (int i = 0; i < controller.ColumnDefs.Count; i++)
            {
                controller.Columns[i].View.BoundingBox = new RectangleF {
                    X = widthSoFar,
                    Y = 0,
                    Width = controller.ColumnDefs[i].Width,
                    Height = rowHeight
                };
                widthSoFar += controller.ColumnDefs[i].Width;
            }            
            #endregion
        }
        #endregion
    }
}
