using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart2.Visitor;
using FlowChart2.Utility;
using System.Windows.Forms;
using FlowChart2.Model;

namespace FlowChart2.ControllerModel
{
    public class NBox:NComponent
    {
        #region Model
        public NBoxModel Model { get; private set; }
        
        public override NModel GetModel()
        {
            return this.Model;
        }
        
        public override void SetFlatModel(FlatModel model)
        {
            Model.Accept(model);
        }
        #endregion

        public NBox()
        {
            this.SortId = 10;
            this.Model = new NBoxModel 
            { 
                ControllerClassName = this.GetType().FullName,                
                StartPoint = new NPoint(),
                EndPoint = new NPoint()
            };            

            this.ConnectPoints = new NPoint[] { 
                new NPoint(),
                new NPoint(),
                new NPoint(),
                new NPoint()
            };
        }

        public NPoint[] ConnectPoints { get; private set; }

        public float X
        {
            get
            {
                return this.Model.StartPoint.X;
            }
            set
            {
                this.Model.StartPoint.X = value;
            }
        }

        public float Y
        {
            get
            {
                return this.Model.StartPoint.Y;
            }
            set
            {
                this.Model.StartPoint.Y = value;                
            }
        }

        public float Width
        {
            get
            {
                return this.Model.EndPoint.X - this.Model.StartPoint.X;
            }
            set
            {
                if (this.Model.StartPoint.X + value > NConfig.BLOCK_SIZE_2)
                {
                    this.Model.EndPoint.X = this.Model.StartPoint.X + value;
                }
            }
        }

        public float Height
        {
            get
            {
                return this.Model.EndPoint.Y - this.Model.StartPoint.Y;
            }
            set
            {
                if (this.Model.StartPoint.Y + value > NConfig.BLOCK_SIZE_2)
                {
                    this.Model.EndPoint.Y = this.Model.StartPoint.Y + value;
                }
            }
        }

        public virtual void RecomputeConnectPoints()
        {
            this.ConnectPoints[0].Set(this.Model.StartPoint.X + Width / 2, this.Model.StartPoint.Y);
            this.ConnectPoints[1].Set(this.Model.EndPoint.X, this.Model.StartPoint.Y + Height / 2);
            this.ConnectPoints[2].Set(this.Model.StartPoint.X + Width / 2, this.Model.EndPoint.Y);
            this.ConnectPoints[3].Set(this.Model.StartPoint.X, this.Model.StartPoint.Y + Height / 2);
        }

        public override void MoveBy(NPoint point)
        {
            this.Model.StartPoint.Add(point);
            this.Model.EndPoint.Add(point);            
        }

        public override bool Contains(NPoint point)
        {
            return this.Model.StartPoint.X <= point.X
                &&
                point.X <= this.Model.EndPoint.X
                &&
                this.Model.StartPoint.Y <= point.Y
                &&
                point.Y <= this.Model.EndPoint.Y;
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
                return true;
            }
            else
            {
                this.SelectedPoint = null;
                if (this.Contains(point))
                {
                    this.IsSelected = true;
                    this.MouseState = NMouseState.MOVE;                    
                }
            }
            return this.IsSelected;
        }

        public override void MouseUp(MouseEventArgs mouseEvent)
        {
            float w = NMathHelper.MRound(this.Width, NConfig.BLOCK_SIZE_2);
            float h = NMathHelper.MRound(this.Height, NConfig.BLOCK_SIZE_2);

            this.X = NMathHelper.MRound(this.X, NConfig.BLOCK_SIZE_2);
            this.Y = NMathHelper.MRound(this.Y, NConfig.BLOCK_SIZE_2);

            this.Width = w;
            this.Height = h;

            this.RecomputeConnectPoints();

            this.OnVertexMoved(this.SelectedPoint);
        }

        public override void MouseDown(MouseEventArgs mouseEvent)
        {
            if (mouseEvent.Button == MouseButtons.Left)
            {
                lastPoint.Set(mouseEvent.X, mouseEvent.Y);
            }
        }

        public override void MouseMove(MouseEventArgs mouseEvent)
        {
            if (mouseEvent.Button == MouseButtons.Left)
            {
                mousePoint.Set(mouseEvent.X, mouseEvent.Y);
                tempPoint.Set(mousePoint);
                tempPoint.Subtract(lastPoint);

                if (MouseState == NMouseState.MOVE)
                {
                    this.MoveBy(tempPoint);
                    this.RecomputeConnectPoints();
                    this.OnVertexMoving(this.SelectedPoint);
                }
                else if (MouseState == NMouseState.RESIZE)
                {
                    this.SelectedPoint.Add(tempPoint);
                    if (this.Width < NConfig.BLOCK_SIZE_2 || this.Height < NConfig.BLOCK_SIZE_2)
                    {
                        this.SelectedPoint.Subtract(tempPoint);
                    }
                    this.RecomputeConnectPoints();
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

        public event Action<NBox, NPoint> VertexMoved;

        protected void OnVertexMoved(NPoint point)
        {
            if (VertexMoved != null)
            {
                VertexMoved(this, point);
            }
        }

        public event Action<NBox, NPoint> VertexMoving;

        protected void OnVertexMoving(NPoint point)
        {
            if (VertexMoving != null)
            {
                VertexMoving(this, point);
            }
        }
    }
}
