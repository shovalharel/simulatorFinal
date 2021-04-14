using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace simulator
{
    public class Player_VM : INotifyPropertyChanged
    {
        private IFGmodel model;
        public event PropertyChangedEventHandler PropertyChanged;

        public Player_VM(IFGmodel model)
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

        public double VM_Index_line
        {
            get { return this.model.Index_line; }

            set
            {
                this.model.Index_line = (int)value;

            }

        }
        public int VM_Max_line
        {
            get
            {
                return model.Max_line;
            }
            set
            {
                this.model.Max_line = value;

            }
        }

        public int VM_Speed
        {
            get
            {
                return model.Speed;
            }
            set
            {
                model.Speed = (int)value;
                //NotifyPropertyChanged("Speed");   
            }
        }
        public void play()
        {
            model.play();
        }
        public void stop()
        {
            model.stop();
        }
        public void pause()
        {
            model.pause();
        }
        public void forward()
        {
            model.forward();
        }
        public void back()
        {
            model.back();
        }
        public void setspeed05()
        {
            model.setspeed05();
        }
        public void setspeed075()
        {
            model.setspeed075();
        }
        public void setspeed1()
        {
            model.setspeed1();
        }
        public void setspeed15()
        {
            model.setspeed15();
        }
        public void setspeed2()
        {
            model.setspeed2();
        }

        public void open_csv_file()
        {
            model.open_csv();
        }

        public void open_csv2_file()
        {
            model.open_normal_csv();
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

    }
}
