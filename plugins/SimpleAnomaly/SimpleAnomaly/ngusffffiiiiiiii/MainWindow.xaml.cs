using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using simulator;
using System.Reflection;

namespace simulator
{

    public partial class MainWindow : Window
    {

        FGViewModel vm;
        Player_VM player_vm;
        Joystick_VM joystick_vm;
        Graphs_VM graphs_vm;
        DataFly_VM dataFly_vm;
        dynamic c;
        dynamic graph;


        public MainWindow()
        {
            InitializeComponent();
            FGmodel model = new FGmodel(new MyClient());
            vm = new FGViewModel(model);
            joystick_vm = new Joystick_VM(model);
            joystick.VM_joystick = joystick_vm;
            player_vm = new Player_VM(model);
            player.VM_Player = player_vm;
            graphs_vm = new Graphs_VM(model);
            graphs.VM_Graphs = graphs_vm;
            dataFly_vm = new DataFly_VM(model);
            dataFly.VM_DataFly = dataFly_vm;
            
            /////////////////*
            /*string p3 = @"C:\Users\Shova\source\repos\SimpleAnomaly\SimpleAnomaly\obj\Debug\SimpleAnomaly.dll";
            Assembly dll = Assembly.LoadFile(p3);
            Type[] typesInDll = dll.GetExportedTypes();
            string s = "SimpleAnomalyDetector";
            var SimpleAnomalyDetectorType = typesInDll.Where(t => t.Name==s).FirstOrDefault();
            var SimpleAnomalyDetectorInstance = Activator.CreateInstance(SimpleAnomalyDetectorType);*/
            /*var learnNormalMethod = SimpleAnomalyDetectorType.GetMethod("learnNormal");
            learnNormalMethod.Invoke(SimpleAnomalyDetectorInstance, null);
            string l = "Low";
            var LowType = typesInDll.Where(t => t.Name == l).FirstOrDefault();
            var LowInstance = Activator.CreateInstance(LowType);
            var lowMethod = SimpleAnomalyDetectorType.GetMethod("Low");
            Array[] a = new Array[5];
            lowMethod.Invoke(SimpleAnomalyDetectorInstance, a);
            int f = 0;*/
            /////////////////////////////////*/
            DataContext = vm;
            main_window.Show(); // open main window
        }

        private void doit(object sender, RoutedEventArgs e)
        {
            c.update(Brushes.Red);
        }

    }
}


