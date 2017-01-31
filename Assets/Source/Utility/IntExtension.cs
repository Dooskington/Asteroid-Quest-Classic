using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Source.Utility
{
    public static class IntExtension
    {
        public static float Map(this int value, float min, float max, float otherMin, float otherMax)
        {
            return (value - min) / (max - min) * (otherMax - otherMin) + otherMin;
        }
    }
}
