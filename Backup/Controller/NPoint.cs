﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FlowChart2.ControllerModel
{
    public class NPoint
    {
        public float X {get; set;}
        public float Y{get; set;}        

        public NPoint()
        { 
        }
        
        public void Set(NPoint p)
        {
            Set(p.X, p.Y);
        }
        public void Set(float x, float y)
        {
            this.X = x;
            this.Y = y;            
        }
        public NPoint Subtract(NPoint p)
        {
            Add(-p.X, -p.Y);
            return this;
        }
        public NPoint Add(NPoint p)
        {
            Add(p.X, p.Y);
            return this;
        }
        public NPoint Add(float x, float y)
        {
            this.X += x;
            this.Y += y;
            return this;
        }

        public NPoint Clone()
        {
            return new NPoint { X = this.X, Y = this.Y };
        }

        public override bool Equals(object obj)
        {
            NPoint p = obj as NPoint;
            return p != null && p.X == this.X && p.Y == this.Y;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }        
    }
}
