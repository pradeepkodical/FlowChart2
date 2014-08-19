using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowChart2.Model
{
    public abstract class NModel
    {
        public string ID { get; set; }
        public string ControllerClassName { get; set; }        
        public string ModelClassName { get; set; }
        public NModel()
        {
            this.ID = string.Format("ID_{0}",DateTime.Now.Ticks);
            ModelClassName = this.GetType().FullName;
        }
        
        public virtual FlatModel GetFlatModel()
        {
            return new FlatModel 
            { 
                ID = this.ID,
                Text = this.Text,
                ControllerClassName = this.ControllerClassName,
                ModelClassName = this.ModelClassName
            };
        }

        internal abstract void Accept(FlatModel flatModel);

        public string Text {get; set;}        
    }
}
