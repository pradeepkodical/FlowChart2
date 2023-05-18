using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.Utility;
using FlowChart2.ControllerModel;

namespace FlowChart2.Model
{
    public class NBoxModel: NModel
    {
        public NPoint StartPoint {get; set;}
        public NPoint EndPoint{get; set;}
                
        public override FlatModel GetFlatModel()
        {
            FlatModel model = base.GetFlatModel();
            model.StartPoint = this.StartPoint;
            model.EndPoint = this.EndPoint;
            model.SortOrder = 10;
            return model;
        }

        internal override void Accept(FlatModel flatModel)
        {
            this.ID = flatModel.ID;
            this.Text = flatModel.Text;
            this.StartPoint = flatModel.StartPoint;
            this.EndPoint = flatModel.EndPoint;
        }
    }
}
