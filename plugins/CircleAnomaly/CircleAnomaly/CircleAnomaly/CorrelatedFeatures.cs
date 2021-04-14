using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleAnomaly
{
    class CorrelatedFeatures
    {
        private string feature1;
        private string feature2;
        private float corrlation;
        private point center;
        //private float radius;
        private float threshold;

        public CorrelatedFeatures(string feature1, string feature2,
            float corrlation, point center, float threshold)
        {
            this.feature1 = feature1;
            this.feature2 = feature2;
            this.corrlation = corrlation;
            this.center = center;
            this.threshold = threshold;
        }

        public point Center
        {
            get
            {
                return this.center;
            }
        }
        public float Threshold
        {
            get
            {
                return this.threshold;
            }
        }
        public float Corrlation
        {
            get
            {
                return this.corrlation;
            }
        }

        public string get_feature1()
        {
            return this.feature1;
        }

        public string get_feature2()
        {
            return this.feature2;
        }
        /*
        public float get_threshold()
        {
            return this.threshold;
        }

        public Line get_line_reg()
        {
            return this.lin_reg;
        }
        */
    }
}
