using System;
using System.Collections.Generic;
using System.Text;

namespace CircleAnomaly
{
    interface IAnomaly_detector
    {
        float avg(List<float> x);
        float var(List<float> x);
        float cov(List<float> x, List<float> y);
        float pearson(List<float> x, List<float> y);
        line linear_reg(List<float> x, List<float> y);
        float dev(point p, line l);
        List<point> get_points(List<float> x, List<float> y);
    }
}
