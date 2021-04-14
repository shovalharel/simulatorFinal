using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAnomaly
{
    public class AnomalyReport
    {

        public string description;
        public long timeStep;
        private string feature1;
        private string feature2;

        public AnomalyReport(string feature1, string feature2, long timeStep)
        {

            this.feature1 = feature1;
            this.feature2 = feature2;
            this.timeStep = timeStep;
        }

        public string get_feature1()
        {
            return this.feature1;
        }

        public string get_feature2()
        {
            return this.feature2;
        }

        public int get_timestep()
        {
            return (int)this.timeStep;
        }
    }
}
