using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using FlowChart2.Utility;
using FlowChart2.ControllerModel;

namespace FlowChart2.Model
{
    public class NConnectorModel:NModel
    {
        public string StartBoxID { get; set; }
        public string EndBoxID { get; set; }

        public int StartBoxIndex { get; set; }
        public int EndBoxIndex { get; set; }

        public NPoint StartPoint { get; set; }

        public NPoint EndPoint { get; set; }
        
        public override FlatModel GetFlatModel()
        {
            FlatModel model = base.GetFlatModel();
            model.StartBoxID = this.StartBoxID;
            model.EndBoxID = this.EndBoxID;

            model.StartPoint = this.StartPoint;
            model.EndPoint = this.EndPoint;

            model.StartBoxIndex = this.StartBoxIndex;
            model.EndBoxIndex = this.EndBoxIndex;

            model.SortOrder = 10000;
            return model;
        }

        internal override void Accept(FlatModel flatModel)
        {
            this.ID = flatModel.ID;
            this.Text = flatModel.Text;

            this.StartBoxID = flatModel.StartBoxID;
            this.EndBoxID = flatModel.EndBoxID;

            this.StartPoint = flatModel.StartPoint;
            this.EndPoint = flatModel.EndPoint;

            this.StartBoxIndex = flatModel.StartBoxIndex;
            this.EndBoxIndex = flatModel.EndBoxIndex;
        }
    }
}
