using simulator;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAnomaly
{
    public class SimpleAnomalyDetector
    {
        private Anomaly_det anomaly;
        private List<CorrelatedFeatures> correlated;
        public SimpleAnomalyDetector()
        {
            this.anomaly = new Anomaly_det();
            this.correlated = new List<CorrelatedFeatures>();
        }
        // for array of points and regression line find the max devision and this will be the threshold
        public float find_threshold(List<Point> points, Line reg_line)
        {
            float threshold = 0;
            int size = points.Count;
            for (int i = 0; i < size; i++)
            {
                float d = anomaly.dev(points[i], reg_line);
                if (d > threshold)
                {
                    threshold = d;
                }
            }

            return (float)1.1 * threshold;
        }
        //We will take a file of a normal flight and check for each feature which of the other features
        // is most correlative to it according to the pearson method.
        public void learnNormal(TimeSeries ts)
        {
            float max;
            int max_index = 0;
            float p = 0;
            int size = ts.get_features().Count;
            bool hasCor = false;
            for (int i = 0; i < size; i++)
            {
                max = 0;
                for (int j = i + 1; j < size; j++)
                {
                    p = Math.Abs(anomaly.pearson(ts.get_dict()[i], ts.get_dict()[j]));
                    if (p > max && p > 0.9)
                    {
                        max = p;
                        max_index = j;
                        hasCor = true;
                    }
                }
                if (hasCor)
                {
                    Line line_reg = anomaly.linear_reg(ts.get_dict()[i], ts.get_dict()[max_index]);
                    List<Point> points = anomaly.get_points(ts.get_dict()[i], ts.get_dict()[max_index]);
                    float threshold = find_threshold(points, line_reg);
                    correlated.Add(new CorrelatedFeatures(max, line_reg, threshold, ts.get_features()[i], ts.get_features()[max_index]));
                    hasCor = false;
                }
            }
        }


        //check if pair fetures are exist in the cf vector

        /*
        We will receive the input while flying - that is, line by line. 
        For each two-dimensional point we will measure its distance from the regression line
        we learned for the features that created it. If this distance is large enough than the
        thrashold we saw for these features we will warn of an exception and include in the 
        report the properties involved.
        */
        public List<AnomalyReport> detect(TimeSeries ts)
        {
            List<AnomalyReport> reports = new List<AnomalyReport>();
            int size = correlated.Count;
            int size2 = ts.get_dict()[0].Count;
            for (int time_step = 0; time_step < size2; time_step++)
            {
                for (int i = 0; i < size; ++i)
                {
                    string feature1 = correlated[i].get_feature1();
                    string feature2 = correlated[i].get_feature2();
                    float x_point = ts.find_value(feature1, time_step);
                    float y_point = ts.find_value(feature2, time_step);
                    if ((anomaly.dev(new Point(x_point, y_point), correlated[i].get_line_reg())) > correlated[i].get_threshold())
                    {
                        string description = feature1 + "-" + feature2;
                        reports.Add(new AnomalyReport(description, time_step)); ////// +1??
                    }
                }
            }
            return reports;
        }

        public int Low()
        {
            return 5;
        }
    }
}
