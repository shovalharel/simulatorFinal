using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace simulator
{
        public class Joystick_VM :INotifyPropertyChanged
        {

        private IFGmodel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public Joystick_VM(IFGmodel model)
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

        public float VM_Throttle
        {
            get { return this.model.Throttle; }
        }

        public float VM_Rudder
        {
            get { return this.model.Rudder; }
        }

        public float VM_Rudder_Max
        {
            get { return this.model.Rudder_Max; }
        }

        public float VM_Rudder_Min
        {
            get { return this.model.Rudder_Min; }
        }

        public float VM_Throttle_Max
        {
            get { return this.model.Throttle_Max; }
        }

        public float VM_Throttle_Min
        {
            get { return this.model.Throttle_Min; }
        }

        public float VM_KnobCenter_X
        {
            get { return this.model.KnobCenter_X / 2; }
        }

        public float VM_KnobCenter_Y
        {
            get { return this.model.KnobCenter_Y / 2; }
        }
    }
}
