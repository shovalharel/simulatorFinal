using System;
using System.Collections.Generic;
using System.Text;

namespace simulator
{
    public class Line
    {
        public float a;
        public float b;

        public Line()
        {
            this.a = 0;
            this.b = 0;
        }
        public Line(float a, float b)
        {
            this.a = a;
            this.b = b;
        }

        public float f(float x)
        {
            return this.a * x + this.b;
        }

        public float g(float y)
        {
            if (a != 0)
            {
                return (y - b) / a;
            }
            else
            {
                return 0;
            }
        }
    }
}
