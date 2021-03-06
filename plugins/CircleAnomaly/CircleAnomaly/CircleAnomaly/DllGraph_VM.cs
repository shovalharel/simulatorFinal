using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CircleAnomaly
{
    public class DllGraph_VM : INotifyPropertyChanged
    {
        private DllGraphModel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public DllGraph_VM(DllGraphModel model)
        {
            this.model = model;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public List<DataPoint> VM_Anomaly_Points
        {
            get
            {
                return this.model.Anomaly_Points;
            }
            set
            {
                this.model.Anomaly_Points = value;
            }
        }

        // need to change to circle
        public List<DataPoint> VM_Circle_Points
        {
            get
            {
                return this.model.Circle_Points;
            }
            set
            {
                this.model.Circle_Points = (List<DataPoint>)value;
            }
        }

        /*public List<DataPoint> VM_Line_Exception_Points
        {
            get
            {
                //return this.line_reg_points;
                return new List<DataPoint>(model.Line_Exception_Points);
            }
            set
            {
                model.Line_Exception_Points = (List<DataPoint>)value;
            }
        }*/
    }
}
