using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleAnomaly
{
    class MinCircle
    {
        public float max(float a, float b)
        {
            return a > b ? a : b;
        }
        public float min(float a, float b)
        {
            return a < b ? a : b;
        }


        public Circle findMinCircle(List<point> points)
        {
            float x_max, x_min, y_max, y_min, x_center, y_center, x_dist, y_dist, radius;
            x_max = points[0].x;
            x_min = points[0].x;
            y_max = points[0].y;
            y_min = points[0].y;
            for (int i = 0; i < points.Count(); i++)
            {
                x_max = max(points[i].x, x_max);
                x_min = min(points[i].x, x_min);
                y_max = max(points[i].y, y_max);
                y_min = max(points[i].y, y_min);
            }
            x_center = (x_max + x_min) / 2;
            y_center = (y_max + y_min) / 2;

            x_dist = x_max - x_min;
            y_dist = y_max - y_min;

            radius = max(x_dist, y_dist) / 2;

            return (new Circle(new point(x_center, y_center), radius));
        }
    }
}
