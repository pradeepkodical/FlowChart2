using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Diagnostics;
using FlowChart2.ControllerModel;
using FlowChart2.Utility;

namespace FlowChart2
{
    public class NMouseTool
    {
        public event Action Change;
        public event Action<NComponent> ComponentSelected;
        public event Action ReDraw;

        public NComponent SelectedComponent { get; set; }
        protected List<NComponent> components;

        public virtual void Move(MouseEventArgs e) { }
        public virtual void Down(MouseEventArgs e) { }
        public virtual void Up(MouseEventArgs e) { }

        protected void OnComponentSelected(NComponent component)
        {
            if (this.ComponentSelected != null)
            {
                this.ComponentSelected(component);
            }
        }

        protected void OnChange()
        {
            if (this.Change != null)
            {
                this.Change();
            }
        }

        protected void OnReDraw()
        {
            if (this.ReDraw != null)
            {
                this.ReDraw();
            }
        }
    }

    public class NBoxTool : NMouseTool
    {
        private NPoint mousePoint = new NPoint();
        private NPoint lastPoint = new NPoint();
        private NPoint tempPoint = new NPoint();
        
        public NBoxTool(List<NComponent> components)            
        {
            this.components = components;
            this.ComponentSelected += new Action<NComponent>(NBoxTool_ComponentSelected);
        }

        private void NBoxTool_ComponentSelected(NComponent obj)
        {
            this.SelectedComponent = obj;
        }

        public override void Down(MouseEventArgs e)
        {
            mousePoint.Set(e.X, e.Y);

            if (this.SelectedComponent != null)
            {
                if (this.SelectedComponent.HitTest(mousePoint))
                {
                    this.SelectedComponent.MouseDown(e);
                    this.OnReDraw();
                    return;
                }
            }

            this.OnComponentSelected(null);

            this.components.ForEach(x => {
                x.IsSelected = false;
                x.IsCloseEnough = false;
                x.SelectedPoint = null;
            });

            foreach(NComponent component in this.components.OrderBy(x => x.SortId))
            {
                if (component.HitTest(mousePoint))
                {
                    this.OnComponentSelected(component);
                    component.MouseDown(e);
                    break;
                }
            }
            this.OnReDraw();
        }

        public override void Up(MouseEventArgs e)
        {
            if (this.SelectedComponent != null)
            {
                this.SelectedComponent.MouseUp(e);
                this.OnReDraw();
                this.OnChange();
            }
        }

        public override void Move(MouseEventArgs e)
        {
            if (this.SelectedComponent != null)
            {
                this.SelectedComponent.MouseMove(e);
                this.OnReDraw();
            }
        }
    }    
}
