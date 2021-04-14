using simulator;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAnomaly
{
    interface anomaly_detection_util
    {
        float avg(List<float> x);
        float var(List<float> x);
        float cov(List<float> x, List<float> y);
        float pearson(List<float> x, List<float> y);
        Line linear_reg(List<float> x, List<float> y);
        float dev(Point p, Line l);
        List<Point> get_points(List<float> x, List<float> y);
    }
}
