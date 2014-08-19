using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.ControllerModel;

namespace FlowChart2.Model
{
    public class FlatModel
    {
        public NPoint StartPoint { get; set; }
        public NPoint EndPoint { get; set; }

        public string StartBoxID { get; set; }
        public string EndBoxID { get; set; }

        public int StartBoxIndex { get; set; }
        public int EndBoxIndex { get; set; }

        public string ID { get; set; }
        public string Text { get; set; }
        public string ControllerClassName { get; set; }
        public string ModelClassName { get; set; }

        public int SortOrder { get; set; }

        public NModel GetModel()
        {
            NModel model = (NModel)Activator.CreateInstance(Type.GetType(this.ModelClassName));
            model.Accept(this);
            return model;
        }
    }
}
