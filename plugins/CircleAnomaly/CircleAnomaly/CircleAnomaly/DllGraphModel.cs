using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleAnomaly
{
    public class DllGraphModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        //private UserControl1 uc;
        private SimpleAnomalyDetector sd;
        private List<DataPoint> reg_points;
        private List<DataPoint> circle_points;
        //private List<DataPoint> line_exception_points;
        private TimeSeries ts_normal;
        private List<AnomalyReport> anomaly_reports;

        public DllGraphModel()
        {
            this.circle_points = new List<DataPoint>(); //change
            this.reg_points = new List<DataPoint>(); // save


        }

        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void updateChoose(string csv_learn, string csv_detect, List<string> features, string chosen_feature)
        {
            Anomaly_detector anomaly = new Anomaly_detector();
            SimpleAnomalyDetector s = new SimpleAnomalyDetector();
            this.sd = s;
            ts_normal = new TimeSeries((string)csv_learn, features);
            s.learnNormal(ts_normal);
            TimeSeries ts_detect = new TimeSeries((string)csv_detect, features);
            anomaly_reports = s.detect(ts_detect);
            CorrelatedFeatures c = s.get_correlated(chosen_feature);
            if (c != null)
            {
                string second_cor;
                if (c.get_feature1() == chosen_feature)
                {
                    second_cor = c.get_feature2();
                }
                else
                {
                    second_cor = c.get_feature1();
                }
                // size vector
                int size = ts_normal.get_dict()[features.IndexOf(chosen_feature)].Count;
                // create circle ****************************************************************
                List<DataPoint> learn_points = circle_to_points(c.Center.x, c.Center.y, c.Threshold);
                Circle_Points = learn_points;
                NotifyPropertyChanged("Circle_Points");
                //List<DataPoint> learn_points = line_to_points(c.get_line_reg(), size, ts_detect.get_dict()[features.IndexOf(chosen_feature)], ts_detect.get_dict()[features.IndexOf(second_cor)]);
                //Line_Reg_Points = learn_points;
                //NotifyPropertyChanged("Line_Reg_Points");
                // the point of the detect
                List<DataPoint> detected_points = new List<DataPoint>();
                for (int i = 0; i < anomaly_reports.Count; i++)
                {
                    if (anomaly_reports[i].get_feature1() == chosen_feature)
                    {
                        float x = ts_detect.get_dict()[features.IndexOf(chosen_feature)][(int)anomaly_reports[i].timeStep];
                        float y = ts_detect.get_dict()[features.IndexOf(second_cor)][(int)anomaly_reports[i].timeStep];
                        detected_points.Add(new DataPoint(x, y));

                    }
                    else
                    {
                        float x = ts_detect.get_dict()[features.IndexOf(second_cor)][(int)anomaly_reports[i].timeStep];
                        float y = ts_detect.get_dict()[features.IndexOf(chosen_feature)][(int)anomaly_reports[i].timeStep];
                        detected_points.Add(new DataPoint(x, y));
                    }
                }
                Anomaly_Points = detected_points;

                NotifyPropertyChanged("Anomaly_Points");
            }

            // anomaly.get_points(ts_normal.get_dict()[features.IndexOf(chosen_feature)], ts_normal.get_dict()[ts_normal.get]

        }


        public void show_exceptions_on_slider()
        {
            List<DataPoint> points = new List<DataPoint>();
            List<int> time_exceptions = new List<int>();
            for (int i = 0; i < anomaly_reports.Count; i++)
            {
                time_exceptions.Add(anomaly_reports[i].get_timestep());
                points.Add(new DataPoint(anomaly_reports[i].get_timestep(), 0));
            }

        }
        /**
        // create the line i need to do this circle
        public List<DataPoint> line_to_points(line l, int size, List<float> x, List<float> y)
        {
            List<DataPoint> line_p = new List<DataPoint>();
            for (int i = 0; i < size; i++)
            {
                line_p.Add(new DataPoint(x[i], l.f(x[i])));
                line_p.Add(new DataPoint(l.g(y[i]), y[i]));
            }
            return line_p;
        }
        */
        // create the circle
        public List<DataPoint> circle_to_points(double a, double b, double r)
        {
            List<DataPoint> list = new List<DataPoint>();
            for (double i = 0; i < Math.PI * 2; i += 0.001)
            {
                double x = a + (r * Math.Cos(i));
                double y = b + (r * Math.Sin(i));
                list.Add(new DataPoint(x, y));
            }
            return list;
        }


        // the anomaly points 
        public List<DataPoint> Anomaly_Points
        {
            get
            {
                return this.reg_points;
            }
            set
            {
                this.reg_points = (List<DataPoint>)value;
                NotifyPropertyChanged("Anomaly_Points");
            }
        }

        public List<DataPoint> Circle_Points
        {
            get
            {
                //return this.line_reg_points;
                return new List<DataPoint>(this.circle_points);
            }
            set
            {
                this.circle_points = (List<DataPoint>)value;
                NotifyPropertyChanged("Circle_Points");
            }
        }

        /*public List<DataPoint> Line_Exception_Points
        {
            get
            {
                //return this.line_reg_points;
                return new List<DataPoint>(this.line_exception_points);
            }
            set
            {
                this.line_exception_points = (List<DataPoint>)value;
                NotifyPropertyChanged("Line_Exception_Points");
            }
        }*/
    }
}
