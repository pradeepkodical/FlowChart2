using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.View;
using FlowChart2.ControllerModel;
using System.Drawing;

namespace FlowChart2.Visitor
{
    public class NViewVisitor
    {
        private Font textFont;

        public NViewVisitor(Font textFont)
        {
            this.textFont = textFont;
        }

        private NView PostProcessView(NView view)
        {
            view.TextFont = textFont;
            return view;
        }
        public void Visit(NBox component)
        {
            component.View = PostProcessView(new NBoxView(component));            
        }

        public void Visit(NProcess component)
        {
            component.View = PostProcessView(new NProcessView(component));            
        }
        
        public void Visit(NDatabase component)
        {
            component.View = PostProcessView(new NDatabaseView(component));            
        }

        public void Visit(NRhombus component)
        {
            component.View = PostProcessView(new NRhombusView(component));            
        }

        public void Visit(NRound component)
        {
            component.View = PostProcessView(new NRoundView(component));            
        }

        public  void Visit(NCloud component)
        {
            component.View = PostProcessView(new NCloudView(component));            
        }

        public  void Visit(NConnector component)
        {
            component.View = PostProcessView(new NConnectorView(component));            
        }
    }
}
