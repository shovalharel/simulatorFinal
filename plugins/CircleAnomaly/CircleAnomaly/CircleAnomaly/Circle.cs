using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleAnomaly
{
    class Circle
    {
        private point center;
        private float radius;

        public Circle(point p, float r)
        {
            this.center = p;
            this.radius = r;
        }

        public float Radius
        {
            get
            {
                return this.radius;
            }
        }
        public point Center
        {
            get
            {
                return this.center;
            }
        }
    }
}