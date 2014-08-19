using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart2.Modifiers;
using FlowChart2.Visitor;
using FlowChart2.Utility;
using System.Windows.Forms;
using FlowChart2.Model;

namespace FlowChart2.ControllerModel
{
    public class NConnector : NComponent
    {
        #region Model
        public NConnectorModel Model { get; set; }

        public override NModel GetModel()
        {
            return this.Model;
        }        

        public override void SetFlatModel(FlatModel model)
        {
            Model.Accept(model);
        }
        #endregion

        public NBox StartBox 
        { 
            get; 
            set; 
        }
        public NBox EndBox 
        { 
            get; 
            set; 
        }
                
        public List<NNodeX> TraversedNodes { get; set; }

        public NNodeX EndNode { get; set; }
        public NNodeX StartNode { get; set; }

        public List<NNodeX> PathNodes { get; set; }

        public int Direction { get; set; }

        public NConnector():
            this(10,10,200,200)
        {
            
        }
        public NConnector(float x1, float y1, float x2, float y2)
        {
            this.SortId = 5;
            this.Model = new NConnectorModel
            {            
                ControllerClassName = this.GetType().FullName,                
                StartPoint = new NPoint
                {
                    X = x1,
                    Y = y1
                },
                EndPoint = new NPoint 
                {
                    X = x2,
                    Y = y2
                }
            };
        }        

        public override bool Contains(NPoint point)
        {
            return false;
            //return Contains(EndNode, point);
        }

        public void Reset()
        {
            this.EndNode = null;
            this.StartNode = null;
            this.TraversedNodes = null;
            this.PathNodes = null;
        }

        private bool Contains(NNodeX node, NPoint point)
        {
            if (node != null && node.Parent != null)
            {
                if (node.Point.Equals(point))
                {
                    return true;
                }
                else
                {
                    return Contains(node.Parent, point);
                }
            }
            return false;
        }

        public override bool HitTest(NPoint point)
        {
            this.SelectedPoint = null;
            this.IsSelected = false;
            this.MouseState = NMouseState.NONE;

            if (NMathHelper.Distance(this.Model.StartPoint, point) <= NConfig.HIT_DISTANCE)
            {
                this.SelectedPoint = this.Model.StartPoint;
                this.IsSelected = true;
                this.MouseState = NMouseState.RESIZE;                
            }
            else if (NMathHelper.Distance(this.Model.EndPoint, point) <= NConfig.HIT_DISTANCE)
            {
                this.SelectedPoint = this.Model.EndPoint;
                this.IsSelected = true;
                this.MouseState = NMouseState.RESIZE;                
            }
            else if (HitTestNodeX(EndNode, point))
            {
                this.IsSelected = true;
                this.MouseState = NMouseState.MOVE;                
            }
            return this.IsSelected;
        }

        private bool HitTestNodeX(NNodeX node, NPoint point)
        {
            if (node != null && node.Parent != null)
            {
                if (NMathHelper.Distance(node.Point, point) <= NConfig.HIT_DISTANCE)
                {
                    return true;
                }
                else
                {
                    return HitTestNodeX(node.Parent, point);
                }
            }
            return false;
        }

        public override void MoveBy(NPoint point)
        {
            this.Model.StartPoint.Add(point);
            this.Model.EndPoint.Add(point);
        }

        public override void MouseUp(MouseEventArgs mouseEvent)
        {
            if (this.SelectedPoint != null)
            {
                this.OnVertexMoved(this.SelectedPoint);
            }
        }

        public override void MouseDown(MouseEventArgs mouseEvent)
        {
            if (mouseEvent.Button == MouseButtons.Left)
            {
                lastPoint.Set(((int)(mouseEvent.X / NConfig.BLOCK_SIZE)) * NConfig.BLOCK_SIZE, ((int)(mouseEvent.Y / NConfig.BLOCK_SIZE)) * NConfig.BLOCK_SIZE);
            }
        }

        public override void MouseMove(MouseEventArgs mouseEvent)
        {
            if (mouseEvent.Button == MouseButtons.Left)
            {
                mousePoint.Set(((int)(mouseEvent.X / NConfig.BLOCK_SIZE)) * NConfig.BLOCK_SIZE, ((int)(mouseEvent.Y / NConfig.BLOCK_SIZE)) * NConfig.BLOCK_SIZE);
                tempPoint.Set(mousePoint);
                tempPoint.Subtract(lastPoint);

                this.Reset();

                if (MouseState == NMouseState.MOVE)
                {                    
                    this.MoveBy(tempPoint);
                }
                else if (MouseState == NMouseState.RESIZE)
                {
                    this.SelectedPoint.Add(tempPoint);
                    this.OnVertexMoving(this.SelectedPoint);   
                }
                lastPoint.Set(mousePoint);
            }
        }

        public override void Accept(NVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override void Accept(NViewVisitor visitor)
        {
            visitor.Visit(this);
        }

        public event Action<NConnector, NPoint> VertexMoved;

        protected void OnVertexMoved(NPoint point)
        {
            if (VertexMoved != null)
            {
                VertexMoved(this, point);
            }
        }

        public event Action<NConnector, NPoint> VertexMoving;

        protected void OnVertexMoving(NPoint point)
        {
            if (VertexMoving != null)
            {
                VertexMoving(this, point);
            }
        }

        internal void MakePath(NNodeX node)
        {
            if (node != null)
            {
                this.PathNodes.Add(node);
                MakePath(node.Parent);                
            }
        }
    }
}
