using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using PK.Grid.View;
using System.Drawing;

namespace PK.Controls.Grid.Editors
{
    public class Editor<T>
    {
        private T model;
        public event Action<T, CancelEventArgs> BeforeShow;
        public event Action<T, CancelEventArgs> BeforeHide;

        private Action<T> Set { get; set; }
        private Action<T> Get { get; set; }
        private Control EditorControl { get; set; }

        public Editor(
            Control control,
            Action<T> setter,
            Action<T> getter)
        {
            this.EditorControl = control;
            this.Set = setter;
            this.Get = getter;

            control.KeyDown += new KeyEventHandler(control_KeyDown);
        }

        private void control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                HideInternal();
            }
        }

        private void SetBox(RectangleF rect)
        {
            EditorControl.Left = (int)rect.X+1;
            EditorControl.Top = (int)rect.Y+1;
            EditorControl.Width = (int)rect.Width;
            EditorControl.Height = (int)rect.Height-2;
        }

        private bool ShowInternal()
        {
            CancelEventArgs arg = new CancelEventArgs(false);
            if (this.BeforeShow != null)
            {
                this.BeforeShow(this.model, arg);
            }
            if (!arg.Cancel)
            {
                if (this.model != null)
                {
                    Set(this.model);
                }
                EditorControl.Show();
            }
            return !arg.Cancel;
        }

        private bool HideInternal()
        {
            CancelEventArgs arg = new CancelEventArgs(false);
            if (this.BeforeHide != null)
            {
                this.BeforeHide(this.model, arg);
            }
            if (!arg.Cancel)
            {
                EditorControl.Hide();
                if (this.model != null)
                {
                    Get(this.model);
                }
            }
            return !arg.Cancel;
        }

        internal void ShowEditorFor(CellView<T> cellView)
        {
            if (EditorControl.Visible && !HideInternal())
            {
                return;
            }
            SetBox(cellView.BoundingBox);
            this.model = cellView.Model;
            ShowInternal();
        }

        internal void Hide()
        {
            HideInternal();
        }
    }

}
