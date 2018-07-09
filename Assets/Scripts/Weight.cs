using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    class Weight
    {
        public struct XYWeight
        {
            public int xCoord, yCoord, weightValue;
            public XYWeight(int x, int y, int weight)
            {
                xCoord = x;
                yCoord = y;
                weightValue = weight;
            }
        }
    }
}
