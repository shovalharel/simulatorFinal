using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using OxyPlot;

namespace simulator
{
    public interface IFGmodel : INotifyPropertyChanged
    {
        // coonection to the fg
        void connect();
        void disconnect();
        void start();

        // activate actuators
        void play();
        void stop();
        void pause();
        void forward();
        void back();
        void setspeed05();
        void setspeed075();
        void setspeed1();
        void setspeed15();
        void setspeed2();
        void open_csv();
        void open_xml();
        void update_view(int index);
        Line get_line_reg();

        void open_normal_csv();
        void find_most_corralated();
        List<DataPoint> line_to_points(Line l);
        List<float> get_30_last_points(List<float> list);
       string get_csv_normal_path();
        List<string> get_chunks();

        string get_csv_detect_path();
        //void open_dll();
        //
        int Index_line
        {
            set; get;
        }
        int Max_line
        {
            get; set;
        }
        int Speed
        {
            set; get;
        }

        float Rudder
        {
            get;
            set;
        }

        float Rudder_Max
        {
            get;
            set;
        }
        float Rudder_Min
        {
            get;
            set;
        }

        float Throttle_Max
        {
            get;
            set;
        }

        dynamic DynamicAlg
        {
            get;
            set;
        }
        float Throttle_Min
        {
            get;
            set;
        }
        float Aileron
        {
            get;
            set;
        }

        float Throttle
        {
            get;
            set;
        }

        float KnobCenter_X
        {
            get;
            set;
        }

        float KnobCenter_Y
        {
            get;
            set;
        }
         List<String> Chunks_list
         {
            get;
            set;
         }

       
        List<DataPoint> Points
        {
            get;
            set;
        }
        string Chunk_selected
        {
            get;
            set;
        }
        string Pearson_chunk
        {
            get;
            set;
        }
        List<DataPoint> set_selected_points();
        List<DataPoint> set_pearson_points();
        List<DataPoint> Pearson_points
        {
            get;
            set;
        }
        float Altimeter
        {
            get;
            set;
        }

        float Airspeed
        {
            get;
            set;
        }
        float Heading
        {
            get;
            set;
        }
        float Pitch
        {
            get;
            set;
        }

        float Roll
        {
            get;
            set;
        }
        float Yaw
        {
            get;
            set;
        }

        List<DataPoint> Last_Points
        {
            get;
            set;
        }

        List<DataPoint> Line_Points
        {
            get; 
            set;
        }

        //string get_dll_path();


    }
}
