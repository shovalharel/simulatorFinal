using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAnomaly
{
    public class AnomalyReport
    {

        public string description;
        public long timeStep;
        public AnomalyReport(string description, long timeStep)
        {
            this.description = description;
            this.timeStep = timeStep;
        }
    }
}
