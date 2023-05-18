using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FlowChart2.ControllerModel;

namespace FlowChart2.Utility
{
    internal class NMathHelper
    {
        internal static double Distance(NPoint startPoint, NPoint endPoint)
        {
            NPoint p = endPoint.Clone().Subtract(startPoint);
            return Math.Sqrt(p.Y * p.Y + p.X * p.X);
        }

        internal static float DistanceToLine(NPoint StartPoint, NPoint EndPoint, int x, int y)
        {
            float A = x - StartPoint.X;
            float B = y - StartPoint.Y;
            float C = EndPoint.X - StartPoint.X;
            float D = EndPoint.Y - StartPoint.Y;

            return (float)(Math.Abs(A * D - C * B) / Math.Sqrt(C * C + D * D));
        }

        internal static float MRound(float value, int factor)
        {
            return (float)(Math.Round(value / factor)) * factor;
        }
    }
}
