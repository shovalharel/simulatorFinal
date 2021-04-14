
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.Diagnostics;
using OxyPlot;
using SimpleAnomaly;
using System.Reflection;

namespace simulator
{
    class FGmodel : IFGmodel
    {
        // INotifyProertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        IClient myClient;
        private int index_line;
        private int speed;
        public string xml_path;
        public string detect_csv_file;
        public string normal_csv_path;
        //public string dll_path;
        public LinkedList<String> csv_file = new LinkedList<string>(); // lines
        public List<String> chunks = new List<string>(); // names
        Dictionary<int, List<float>> dict;
        Dictionary<int, int> most_cor;
        bool run = true;
        private float rudder;
        private float aileron;
        private float throttle;
        private float rudder_max;
        private float rudder_min;
        private float throttle_max;
        private float throttle_min;
        private float knob_center_x;
        private float knob_center_y;
        private int max_line;
        private float altimeter;
        private float airspeed;
        private float heading;
        private float pitch;
        private float roll;
        private float yaw;
        private Anomaly_det anomaly;
        private List<DataPoint> points = new List<DataPoint>();
        private string chunk_selected;
        bool dict_up = false;
        private string pearson_chunk;
        private List<DataPoint> pearson_points = new List<DataPoint>();
        private List<DataPoint> last_points = new List<DataPoint>();
        private List<DataPoint> line_points = new List<DataPoint>();
        private dynamic dynamicAlg;
        private bool dll_clicked = false;




        // constructor
        public FGmodel(IClient FGclient)
        {
            this.myClient = FGclient;
            this.speed = 100;
            this.index_line = 0;
            this.dict = new Dictionary<int, List<float>>();
            this.most_cor = new Dictionary<int, int>();
            this.anomaly = new Anomaly_det();
        }
        private void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public void connect()
        {
            // Establish the remote endpoint  
            // for the socket. This example uses port 5400 on the local computer.  ??
            IPAddress ipAddr = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 5400);
            myClient.connect(ipEndPoint, xml_path);
        }
        public void open_csv()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Text Files",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            // check if file path is correct
            if (openFileDialog1.ShowDialog() == true)
            {
                detect_csv_file = openFileDialog1.FileName; // initial file path field
                string line;
                StreamReader sr = new StreamReader(detect_csv_file);
                //save all lines in csv file at list
                while ((line = sr.ReadLine()) != null)
                {
                    csv_file.AddLast(line);
                }
                
            }
        }

        public void open_normal_csv()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse CSV File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "csv",
                Filter = "csv files (*.csv)|*.csv",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            // check if file path is correct
            if (openFileDialog1.ShowDialog() == true)
            {
                normal_csv_path = openFileDialog1.FileName; // initial file path field
            }
        }
        public void open_xml()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse XML File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "xml",
                Filter = "xml files (*.xml)|*.xml",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            // check if file path is correct
            if (openFileDialog1.ShowDialog() == true)
            {
                xml_path = openFileDialog1.FileName; // initial file path field
                //StreamReader sr = new StreamReader(file_path);
                readXML((string)xml_path);
                create_dict();
                initial_data();
                find_most_corralated();
                /*TimeSeries ts1 = new TimeSeries((string)file_path_csv, chunks);
                SimpleAnomalyDetector s = new SimpleAnomalyDetector();
                TimeSeries ts = new TimeSeries((string)file_path2, chunks);
                s.learnNormal(ts);
                List<AnomalyReport> a = s.detect(ts1);*/
            }
        }
        public void initial_data()
        {
            Rudder_Max = getMax(dict[chunks.IndexOf("rudder")]);
            Rudder_Min = getMin(dict[chunks.IndexOf("rudder")]);
            Throttle_Max = getMax(dict[chunks.IndexOf("throttle")]);
            Throttle_Min = getMin(dict[chunks.IndexOf("throttle")]);
            KnobCenter_X = 125;
            KnobCenter_Y = 125;
        }
        public void disconnect()
        {
            run = false;
            myClient.disconnect();
        }
        public void start()
        {
            new Thread(delegate ()
            {
                while (index_line < csv_file.Count())
                {
                    if (index_line == csv_file.Count() - 1)
                    {
                        run = false;
                    }
                    if (run)
                    {
                        string r = csv_file.ElementAt(index_line);
                        myClient.write(r);
                        update_view(index_line);
                        Thread.Sleep(speed);
                        Index_line++;
                    }
                }
            }).Start();
        }
        public void update_view(int index)
        {
            Rudder = dict[chunks.IndexOf("rudder")][index];
            Throttle = dict[chunks.IndexOf("throttle")][index]; /// event??
            KnobCenter_X = 60 * dict[chunks.IndexOf("aileron")][index] + 125;
            KnobCenter_Y = 60 * dict[chunks.IndexOf("elevator")][index] + 125;
            Altimeter = dict[chunks.IndexOf("altimeter_indicated-altitude-ft")][index];
            Airspeed = dict[chunks.IndexOf("airspeed-kt")][index];
            Heading = dict[chunks.IndexOf("heading-deg")][index];
            Pitch = dict[chunks.IndexOf("pitch-deg")][index];
            Roll = dict[chunks.IndexOf("roll-deg")][index];
            Yaw = dict[chunks.IndexOf("side-slip-deg")][index];
            Points = set_selected_points();
            NotifyPropertyChanged("Points");
            Pearson_chunk = chunks[most_cor[chunks.IndexOf(Chunk_selected)]];
            Pearson_points = set_pearson_points();
            Last_Points = set_last_points();
            Line_Points = line_to_points(get_line_reg());
            if (dll_clicked)
            {
                dynamicAlg.update(normal_csv_path, detect_csv_file, chunks, chunk_selected);
            }
            //if (dll_chosen)
            //set_selected_points(Pearson_chunk, Pearson_points);
            //NotifyPropertyChanged("Pearson_chunk");
        }
        public void pause()
        {
            run = false;
        }
        public void play()
        {
            run = true;
            //start();
        }
        public void stop()
        {
            Index_line = 0;
            run = false;
        }
        public void readXML(string file_name)
        {
            XDocument xml = XDocument.Load(file_name);
            IEnumerable<string> temp = xml.Descendants("output").Descendants("name").Select(name => (string)name);
            Chunks_list = temp.ToList();
            Chunk_selected = chunks[0];
        }
        public void create_dict()
        {
            float number;
            // add names of chunks to dictionary
            for (int t = 0; t < chunks.Count(); t++)
            {
                dict.Add(t, new List<float>());
            }
            for (int i = 0; i < csv_file.Count(); i++)
            {
                string line = csv_file.ElementAt(i);
                string[] split_line = line.Split(',');
                int size = split_line.Length;
                for (int j = 0; j < size; j++)
                {
                    if (float.TryParse(split_line[j], out number))
                    {
                        dict[j].Add(float.Parse(split_line[j]));
                    }
                }
            }
            Max_line = dict[0].Count();
            dict_up = true;
        }
        public float getMin(List<float> list)
        {
            float min = list[0];
            for (int i = 0; i < list.Count(); i++)
            {
                if (min > list[i])
                    min = list[i];
            }
            return min;
        }
        public float getMax(List<float> list)
        {
            float max = list[0];
            for (int i = 0; i < list.Count(); i++)
            {
                if (max < list[i])
                    max = list[i];
            }
            return max;
        }

        public void find_most_corralated()
        {
            int size = dict.Count;
            float max = 0;
            int max_index = 0;
            for (int i = 0; i < size; i++)
            {
                max = 0;
                max_index = 0;
                for (int j=i+1; j < size; j++)
                {
                    if (Math.Abs(anomaly.pearson(dict[i], dict[j])) > max)
                    {
                        max = Math.Abs(anomaly.pearson(dict[i], dict[j]));
                        max_index = j;
                    }
                }
                //most_cor.Add(i, max_index);

                if (!most_cor.ContainsKey(i))
                {
                    most_cor.Add(i, max_index);
                }
                if (!most_cor.ContainsKey(max_index))
                {
                    most_cor.Add(max_index, i);
                }
            }
        }


        /*public void find_most_corralated()
        {
            int size = dict.Count;
            float max = 0;
            int max_index = 0;
            int j;
            for (int i = 0; i < size; i++)
            {
                if (i != 0)
                {
                    j = 0;
                }
                else
                {
                    j = 1;
                }
                max = 0;
                for (; j < size; j++)
                {
                    if (i != j && Math.Abs(anomaly.pearson(dict[i], dict[j])) > max)
                    {
                        max = Math.Abs(anomaly.pearson(dict[i], dict[j]));
                        max_index = j;
                    }
                }
                most_cor.Add(i, max_index);
                /*
                if (!most_cor.ContainsKey(i))
                {
                    most_cor.Add(i, max_index);
                }
                if (!most_cor.ContainsKey(max_index))
                {
                    most_cor.Add(max_index, i);
                }*/
                //Debug.Write("--------------------------------");
                //Debug.Write(chunks[i]);
                //Debug.Write("---");
                //Debug.Write(chunks[max_index]);
                //Debug.Write("\n");
                //}
                //}
        public Line get_line_reg()
        {
            return anomaly.linear_reg(dict[chunks.IndexOf(Pearson_chunk)], dict[chunks.IndexOf(Chunk_selected)]);
        }
        public List<float> get_30_last_points(List<float> list)
        {
            List<float> last_points = new List<float>();
            int i = 0;
            if (index_line < 300)
            {
                i = 0;
            }
            else
            {
                i = index_line - 299;
            }
            for (; i <= index_line; i++)
            {
                last_points.Add(list[i]);
            }
            return last_points;
        }
        public void forward()
        {
            if (index_line + 100 > csv_file.Count() - 1)
            {
                index_line = csv_file.Count() - 1;
            }
            else
            {
                index_line += 100;
            }
        }
        public void back()
        {
            if (index_line - 100 < 0)
            {
                index_line = 0;
            }
            else
            {
                index_line -= 100;
            }
        }
        public void setspeed05()
        {
            speed = 200;
        }
        public void setspeed075()
        {
            speed = 133;
        }
        public void setspeed1()
        {
            speed = 100;
        }
        public void setspeed15()
        {
            speed = 67;
        }
        public void setspeed2()
        {
            speed = 50;
        }
        // properties
        public float Rudder
        {
            get
            {
                return this.rudder;
            }
            set
            {
                this.rudder = value;
                NotifyPropertyChanged("Rudder");
            }
        }
        public float Rudder_Max
        {
            get
            {
                return this.rudder_max;
            }
            set
            {
                this.rudder_max = value;
                NotifyPropertyChanged("Rudder_Max");
            }
        }
        public float Rudder_Min
        {
            get
            {
                return this.rudder_min;
            }
            set
            {
                this.rudder_min = value;
                NotifyPropertyChanged("Rudder_Min");
            }
        }
        public float Throttle_Max
        {
            get
            {
                return this.throttle_max;
            }
            set
            {
                this.throttle_max = value;
                NotifyPropertyChanged("Throttle_Max");
            }
        }
        public float Throttle_Min
        {
            get
            {
                return this.throttle_min;
            }
            set
            {
                this.throttle_min = value;
                NotifyPropertyChanged("Throttle_Min");
            }
        }
        public float Aileron
        {
            get
            {
                return this.aileron;
            }
            set
            {
                this.aileron = value;
                NotifyPropertyChanged("Aileron");
            }
        }
        public float Throttle
        {
            get
            {
                return this.throttle;
            }
            set
            {
                this.throttle = value;
                NotifyPropertyChanged("Throttle");
            }
        }
        public float KnobCenter_X
        {
            get
            {
                return this.knob_center_x;
            }
            set
            {
                this.knob_center_x = value;
                NotifyPropertyChanged("KnobCenter_X");
            }
        }
        public float KnobCenter_Y
        {
            get
            {
                return this.knob_center_y;
            }
            set
            {
                this.knob_center_y = value;
                NotifyPropertyChanged("KnobCenter_Y");
            }
        }
        public int Max_line
        {
            get
            {
                return this.max_line;
            }
            set
            {
                this.max_line = value;
                NotifyPropertyChanged("Max_line");
            }
        }
        public int Index_line
        {
            get
            {
                return index_line;
            }
            set
            {
                index_line = value;
                NotifyPropertyChanged("Index_line");
            }
        }
        public int Speed
        {
            get
            {
                return speed;
            }
            set
            {
                speed = value;
                NotifyPropertyChanged("Speed");
            }
        }
        public List<String> Chunks_list
        {
            get
            {
                return this.chunks;
            }
            set
            {
                this.chunks = value;
                NotifyPropertyChanged("Chunks_list");
            }
        }
        public float Altimeter
        {
            get
            {
                return this.altimeter;
            }
            set
            {
                this.altimeter = value;
                NotifyPropertyChanged("Altimeter");
            }
        }
        public float Airspeed
        {
            get
            {
                return this.airspeed;
            }
            set
            {
                this.airspeed = value;
                NotifyPropertyChanged("Airspeed");
            }
        }
        public float Heading
        {
            get
            {
                return this.heading;
            }
            set
            {
                this.heading = value;
                NotifyPropertyChanged("Heading");
            }
        }
        public float Pitch
        {
            get
            {
                return this.pitch;

               
            }
            set
            {
                this.pitch = value;
                NotifyPropertyChanged("Pitch");
            }
        }
        public float Roll
        {
            get
            {
                return this.roll;
            }
            set
            {
                this.roll = value;
                NotifyPropertyChanged("Roll");
            }
        }
        public float Yaw
        {
            get
            {
                return this.yaw;
            }
            set
            {
                this.yaw = value;
                NotifyPropertyChanged("Yaw");
            }
        }
        public List<DataPoint> Points
        {
            get
            {
                //return this.points;
                return new List<DataPoint>(points);
            }
            set
            {
                this.points = (List<DataPoint>)value;
                //NotifyPropertyChanged("Points");
            }
        }
        public string Chunk_selected
        {
            get
            {
                return this.chunk_selected;
            }
            set
            {
                this.chunk_selected = value;
                NotifyPropertyChanged("Chunk_selected");
            }
        }
        public List<DataPoint> set_selected_points()
        {
            if (dict_up)
            {
                List<float> col = dict[chunks.IndexOf(Chunk_selected)];
                points = new List<DataPoint>();
                for (int i = 0; i < index_line; i++)
                {
                    points.Add(new DataPoint(i, col[i]));
                }
                Points = points;
                NotifyPropertyChanged("Points");
            }
            return points;
        }
        public List<DataPoint> set_pearson_points()
        {
            if (dict_up)
            {
                List<float> col = dict[chunks.IndexOf(Pearson_chunk)];
                pearson_points = new List<DataPoint>();
                for (int i = 0; i < index_line; i++)
                {
                    pearson_points.Add(new DataPoint(i, col[i]));
                }
                Pearson_points = pearson_points;
                NotifyPropertyChanged("Pearson_points");
            }
            return pearson_points;
        }
        public List<DataPoint> set_last_points()
        {
            List<float> x = get_30_last_points(dict[chunks.IndexOf(Pearson_chunk)]);
            List<float> y = get_30_last_points(dict[chunks.IndexOf(Chunk_selected)]);
            last_points = new List<DataPoint>();
            for (int i = 0; i < x.Count; i++)
            {
                last_points.Add(new DataPoint(x[i], y[i]));
            }
            Last_Points = last_points;
            NotifyPropertyChanged("Last_Points");
            return last_points;
        }
        public List<DataPoint> line_to_points(Line l)
        {
            List<DataPoint> line_p = new List<DataPoint>();
            for (int i = 0; i < Max_line; i++)
            {
                float x = dict[chunks.IndexOf(Pearson_chunk)][i];
                float y = dict[chunks.IndexOf(Chunk_selected)][i];
                line_p.Add(new DataPoint(x, l.f(x)));
                line_p.Add(new DataPoint(l.g(y), y));
            }
            return line_p;
        }
        public string Pearson_chunk
        {
            get
            {
                return this.pearson_chunk;
            }
            set
            {
                this.pearson_chunk = value;
                NotifyPropertyChanged("Pearson_chunk");
            }
        }
        public List<DataPoint> Pearson_points
        {
            get
            {
                //return this.pearson_points;
                return new List<DataPoint>(this.pearson_points);
            }
            set
            {
                this.pearson_points = (List<DataPoint>)value;
                //NotifyPropertyChanged("Pearson_points");
            }
        }
        public List<DataPoint> Line_Points
        {
            get
            {
                //return this.pearson_points;
                return new List<DataPoint>(this.line_points);
            }
            set
            {
                this.line_points = (List<DataPoint>)value;
                NotifyPropertyChanged("Line_Points");
            }
        }
        public List<DataPoint> Last_Points
        {
            get
            {
                //return this.pearson_points;
                return new List<DataPoint>(this.last_points);
            }
            set
            {
                this.last_points = (List<DataPoint>)value;
                NotifyPropertyChanged("Last_Points");
            }
        }
    
        /*public void open_dll()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                InitialDirectory = @"C:\",
                Title = "Browse Dll File",
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = "dll",
                Filter = "dll files (*.dll)|*.dll",
                FilterIndex = 2,
                RestoreDirectory = true,
                ReadOnlyChecked = true,
                ShowReadOnly = true
            };
            // check if file path is correct
            if (openFileDialog1.ShowDialog() == true)
            {
                dll_path = openFileDialog1.FileName; // initial file path field
            }
            DLLPath = dll_path;
        }*/

        /*public Graphs graphs;

        public Graphs Graphs
        {
            get
            {
                return this.graphs;
            }
            set
            {
                this.graphs = value;
                
            }
        }*/
        /*public string get_dll_path()
        {
            return this.dll_path;
        }*/

        public string get_csv_normal_path()
        {
            return this.normal_csv_path;
        }

        public List<string> get_chunks()
        {
            return this.chunks;
        }

        public string get_csv_detect_path()
        {
            return this.detect_csv_file;
        }

        public dynamic DynamicAlg
        {
            get
            {
                return this.dynamicAlg;
            }
            set
            {
                this.dynamicAlg = value;
                dll_clicked = true;
                NotifyPropertyChanged("DynamicAlg");
            }
        }
    }
}