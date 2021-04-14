using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using OxyPlot;
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
                return model.Chunk_selected;
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
                return model.Pearson_chunk;
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

       
    }
}