using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;

namespace simulator
{
    public class TimeSeries : ITimeSeries
    {
        private Dictionary<int, List<float>> dict;
        private LinkedList<String> csv_lines = new LinkedList<string>();
        private List<String> features;
        public TimeSeries(string csv_path, List<string> features)
        {
            this.features = features;
            this.dict = new Dictionary<int, List<float>>();
            string line;
            float number;
            StreamReader sr = new StreamReader(csv_path);
            //save all lines in csv file at list
            while ((line = sr.ReadLine()) != null)
            {
                csv_lines.AddLast(line);
            }
            for (int t = 0; t < features.Count(); t++)
            {
                dict.Add(t, new List<float>());
            }
            for (int i = 0; i < csv_lines.Count(); i++)
            {
                string l = csv_lines.ElementAt(i);
                string[] split_line = l.Split(",");
                int size = split_line.Length;
                for (int j = 0; j < size; j++)
                {
                    if (float.TryParse(split_line[j], out number))
                    {
                        dict[j].Add(float.Parse(split_line[j]));
                    }
                }
            }
        }
        //find the vector of "feature" and return the value in the "time" line
        public float find_value(string feature, int time)
        {
            return dict[features.IndexOf(feature)][time];
        }
        //return vector of the features
        public List<string> get_features()
        {
            return features;
        }
        //return the map
        public Dictionary<int, List<float>> get_dict()
        {
            return this.dict;
        }
        //return the vector values of the feature key
        public List<float> get_list(string feature)
        {
            return dict[features.IndexOf(feature)];
        }
        //return the vector size of the the feature
        public int get_size_vector(string feature)
        {
            return dict[features.IndexOf(feature)].Count();
        }
    }
}
