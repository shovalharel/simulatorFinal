using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace simulator
{

    public class DataFly_VM : INotifyPropertyChanged
    {

        private IFGmodel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public DataFly_VM(IFGmodel model)
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

        // properties
         public float VM_Altimeter
        {
            get { return this.model.Altimeter; }
        }

        public float VM_Airspeed
        {
            get { return this.model.Airspeed; }
        }
        public float VM_Heading
        {
            get { return this.model.Heading; }
        }
        public float VM_Pitch
        {
            get { return this.model.Pitch; }
        }
        public float VM_Roll
        {
            get { return this.model.Roll; }
        }
        public float VM_Yaw
        {
            get { return this.model.Yaw; }
        }
        

    }

}