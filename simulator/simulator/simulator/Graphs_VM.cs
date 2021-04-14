using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using OxyPlot;
using System.Windows.Media;

namespace simulator
{
    public class Graphs_VM : INotifyPropertyChanged
    {
        private IFGmodel model;
        public event PropertyChangedEventHandler PropertyChanged;
        public Graphs_VM(IFGmodel model)
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
        public List<String> VM_Chunks_list
        {
            get
            {
                return this.model.Chunks_list;
            }
            set
            {
                this.model.Chunks_list = value;
            }
        }
        public List<DataPoint> VM_Points
        {
            get
            {
                return model.Points;
            }
            set
            {
                model.Points = (List<DataPoint>)value;
            }
        }
       
        public string VM_Chunk_selected
        {
            get
            {
                string myStr = model.Chunk_selected;
                if (string.IsNullOrEmpty(myStr)) return myStr;
                if (myStr.Length < 15) return myStr;
                return myStr.Substring(0, 12) + "...";
                //return model.Chunk_selected;
            }
            set
            {
                model.Chunk_selected = value;
                //model.set_selected_points();
            }
        }
        

        public string VM_Pearson_chunk
        {
            get
            {
                string myStr = model.Pearson_chunk;
                if (string.IsNullOrEmpty(myStr)) return myStr;
                if (myStr.Length < 15) return myStr;
                return myStr.Substring(0, 12) + "...";
                //return model.Pearson_chunk;
            }
            set
            {
                model.Pearson_chunk = value;
                //model.set_selected_points();
            }
        }
        public List<DataPoint> VM_Pearson_points
        {
            get
            {
                return model.Pearson_points;
            }
            set
            {
                model.Pearson_points = (List<DataPoint>)value;
            }
        }

        public List<DataPoint> VM_Line_Points
        {
            get
            {
                //return this.pearson_points;
                return model.Line_Points; 
            }
            set
            {
                this.model.Line_Points = (List<DataPoint>)value;
            }
        }

        public List<DataPoint> VM_Last_Points
        {
            get
            {
                return model.Last_Points;
            }
            set
            {
                model.Last_Points = (List<DataPoint>)value;
            }
        }

        /*public string vm_get_dll_path()
        {
            return this.model.get_dll_path();
        }*/

        public string vm_get_csv_normal_path()
        {
            return this.model.get_csv_normal_path();
        }
        public string vm_get_csv_detect_path()
        {
            return this.model.get_csv_detect_path();
        }
        public List<string> vm_get_chunks()
        {
            return this.model.get_chunks();
        }


        public dynamic VM_DynamicAlg
        {
            get
            {
                return model.DynamicAlg;
            }
            set
            {
                model.DynamicAlg = value;
            }
        }
        public int VM_Index_line
        {
            get
            {
                //VM_DynamicAlg.Change(Brushes.Blue);
                return model.Index_line;
            }
            set
            {
                //VM_DynamicAlg.Change(Brushes.Blue);
                model.Index_line = value;
            }
        }


        /////////////////////////////////////
        ///






    }
}