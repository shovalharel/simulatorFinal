using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAnomaly
{
    public class Anomaly_detector : IAnomaly_detector
    {
        public float avg(List<float> x)
        {
            float sum =0;
            for(int i=0; i<x.Count; i++)
            {
                sum += x[i];
            }
            return sum / x.Count;
        }

        public float var(List<float> x)
        {
            float av = avg(x);
            float sum = 0;
            for (int i=0; i< x.Count; i++)
            {
                sum += x[i] * x[i];
            }
            return (sum / x.Count) - av * av;
        }

        public float cov(List<float> x, List<float> y)
        {
            float sum = 0;
            for (int i = 0; i < x.Count; i++)
            {
                sum += x[i] * y[i];
            }
            sum /= x.Count;
            return sum - avg(x) * avg(y);
        }

        public float pearson(List<float> x, List<float> y)
        {
            float c = var(x);
            float b = var(y);
            double n = Math.Sqrt(Math.Abs(c)) * Math.Sqrt(Math.Abs(b));

            if (var(x) ==0 || var(y) == 0)
            {
                return 0;
            }
            return (float)(cov(x, y) / ((Math.Sqrt(var(x))) * (Math.Sqrt(var(y)))));
        }

        public line linear_reg(List<float> x, List<float> y)
        {
            int size = x.Count;
            float a = cov(x, y) / var(x);
            float b = avg(y) - a * (avg(x));
            return new line(a, b);
        }

        /*public float dev(Point p , List<Point> points)
        {
            Line l = linear_reg(points);
            return dev(p, l);
        }*/

        public float dev(point p, line l)
        {
            return Math.Abs(p.y - l.f(p.x));
        }
        public List<point> get_points(List<float> x, List<float> y)
        {
            int size = x.Count;
            List<point> points = new List<point>(size);
            for (int i = 0; i < size; i++)
            {
                points.Add(new point(x[i], y[i]));
            }
            return points;
        }
    }
}
