using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulator
{
    public class RunControl_VM : INotifyPropertyChanged
    {
        private IFGmodel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public RunControl_VM(IFGmodel model)
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

        public void open_csv_file()
        {
            model.open_csv();
        }

        public void open_xml_file()
        {
            model.open_xml();
        }

        public void start_running()
        {
            model.connect();
            model.start();
        }
        public void open_csv2_file()
        {
            model.open_normal_csv();
        }
    }
}