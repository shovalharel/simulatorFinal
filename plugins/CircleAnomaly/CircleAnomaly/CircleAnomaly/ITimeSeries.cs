using System;
using System.Collections.Generic;
using System.Text;

namespace CircleAnomaly
{
    interface ITimeSeries
    {
        //find the vector of "feature" and return the value in the "time" line
        float find_value(string feature, int time);
        //return vector of the features
        List<string> get_features();
        //return the map
        Dictionary<int, List<float>> get_dict();
        //return the vector values of the feature key
        List<float> get_list(string feature);
        //return the vector size of the the feature
        int get_size_vector(string feature);
    }
}
