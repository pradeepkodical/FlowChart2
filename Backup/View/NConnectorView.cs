using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using FlowChart2.ControllerModel;
using FlowChart2.Utility;

namespace FlowChart2.View
{
    public class NConnectorView:NView
    {
        private NConnector connector;

        public NConnectorView(NConnector connector)
            : base(connector)
        {
            this.connector = connector;
        }

        protected override void DrawEdges(Graphics graphics)
        {
            DrawResizeVertex(graphics, this.connector.Model.StartPoint);
            DrawResizeVertex(graphics, this.connector.Model.EndPoint); 
            
            /*NPoint s = this.connector.Model.StartPoint.Clone().Add(-NConfig.BLOCK_SIZE, -NConfig.BLOCK_SIZE);
            graphics.DrawRectangle(Pens.GreenYellow, s.X, s.Y, NConfig.BLOCK_SIZE_2, NConfig.BLOCK_SIZE_2);
            s = this.connector.Model.EndPoint.Clone().Add(-NConfig.BLOCK_SIZE, -NConfig.BLOCK_SIZE);
            graphics.DrawRectangle(Pens.GreenYellow, s.X, s.Y, NConfig.BLOCK_SIZE_2, NConfig.BLOCK_SIZE_2);
             */ 
        }

        public override void Draw(Graphics graphics)
        {
            DrawSelected(graphics);
            if (this.connector.EndNode != null)
            {
                Draw(
                    graphics, 
                    this.connector.EndNode, 
                    new Pen(Brushes.IndianRed, 2.0f));

                DrawArrow(
                    graphics, 
                    this.connector.EndNode.Parent.Point, 
                    this.connector.EndNode.Point, 
                    Brushes.YellowGreen, 
                    Pens.SaddleBrown, 
                    1.5f * NConfig.BLOCK_SIZE);
            }
            else 
            {
                graphics.DrawLine(
                    new Pen(Brushes.IndianRed, 2.0f), 
                    connector.Model.StartPoint.X, 
                    connector.Model.StartPoint.Y, 
                    connector.Model.EndPoint.X, 
                    connector.Model.EndPoint.Y);
                
                DrawArrow(graphics, 
                    connector.Model.StartPoint, 
                    connector.Model.EndPoint, 
                    Brushes.YellowGreen, 
                    Pens.SaddleBrown, 
                    1.5f * NConfig.BLOCK_SIZE);
            }            
        }        

        private void Draw(Graphics graphics, NNodeX node, Pen pen)
        {
            if (node != null)
            {
                if (node.Point != null)
                {
                    if (node.Parent != null)
                    {
                        graphics.DrawLine(
                            pen,
                            node.Point.X,
                            node.Point.Y,
                            node.Parent.Point.X,
                            node.Parent.Point.Y);
                        Draw(graphics, node.Parent, pen);
                    }
                }
            }            
        }        
    }
}
