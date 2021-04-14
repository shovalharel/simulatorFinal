using System;
using System.Collections.Generic;
using System.Text;

namespace simulator
{
    public class CorrelatedFeatures
    {
        private string feature1;
        private string feature2;
        private float corrlation;
        private Line lin_reg;
        private float threshold;

        public CorrelatedFeatures(float corrlation, Line lin_reg, float threshold,
            string feature1,string feature2)
        {
            this.corrlation = corrlation;
            this.feature1 = feature1;
            this.feature2 = feature2;
            this.lin_reg = lin_reg;
            this.threshold = threshold;
        }

        public string get_feature1()
        {
            return this.feature1;
        }

        public string get_feature2()
        {
            return this.feature2;
        }

        public float get_threshold()
        {
            return this.threshold;
        }

        public Line get_line_reg()
        {
            return this.lin_reg;
        }



    }
}
