using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart2.Visitor;
using FlowChart2.View;
using System.Windows.Forms;
using FlowChart2.Model;

namespace FlowChart2.ControllerModel
{
    public abstract class NComponent
    {
        public abstract bool Contains(NPoint point);
        public abstract bool HitTest(NPoint point);
        public abstract void MoveBy(NPoint point);

        public abstract void Accept(NVisitor visitor);
        public abstract void Accept(NViewVisitor visitor);

        public NPoint SelectedPoint { get; set; }

        public NView View { get; set; }

        public bool IsSelected { get; set; }
        public bool IsCloseEnough { get; set; }

        protected NMouseState MouseState { get; set; }

        protected NPoint lastPoint = new NPoint();
        protected NPoint mousePoint = new NPoint();
        protected NPoint tempPoint = new NPoint();

        public abstract void MouseUp(MouseEventArgs mouseEvent);
        public abstract void MouseDown(MouseEventArgs mouseEvent);
        public abstract void MouseMove(MouseEventArgs mouseEvent);

        public int SortId { get; set; }

        public virtual FlatModel GetFlatModel()
        {
            return this.GetModel().GetFlatModel();
        }

        public abstract void SetFlatModel(FlatModel model);

        public abstract NModel GetModel();        
    }
}
