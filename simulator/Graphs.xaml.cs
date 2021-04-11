using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
namespace simulator
{
    /// <summary>
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : UserControl
    {
        private Graphs_VM graphs_vm;
        private string path;
        private dynamic dynamicAlg;
        private string dll_path;

        public Graphs()
        {
            InitializeComponent();
        }
        public Graphs_VM VM_Graphs
        {
            get
            {
                return graphs_vm;
            }
            set
            {
                graphs_vm = value;
                this.DataContext = value;
                //Path = graphs_vm.vm_get_dll_path();
                //Path = "C:/Users/Shova/source/repos/SimpleAnomaly/SimpleAnomaly/bin/Debug/SimpleAnomaly.dll";
            }
        }
        private void Listbox_chunks_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            graphs_vm.VM_Chunk_selected = Listbox_chunks_list.SelectedItem.ToString();
        }

        private void Open_dll_Click(object sender, RoutedEventArgs e)
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
            Path = dll_path;
            add_dll();
        }

        public string Path
        {
            set
            {
                dll_path = value;
                
            }
            get
            {
                return dll_path;
            }
        }
        /*        public dynamic DynamicAlg
                {
                    get
                    {
                        return dynamicAlg;
                    }
                    set
                    {
                        dynamicAlg = value;
                       // NotifyPropertyChanged("DynamicAlg");
                    }
                }*/

        public void add_dll()
        {
            try
            {
                Assembly dll = Assembly.LoadFile((dll_path));
                Type[] typesInDll = dll.GetExportedTypes();
                string s = "Dll";
                foreach(Type t in typesInDll)
                {
                    if (t.Name == s)
                    {

                       graphs_vm.VM_DynamicAlg = Activator.CreateInstance(t);

                    }
                }
                // var DllGraphType = typesInDll.Where(t => t.Name == s).FirstOrDefault();
                // var DllGraphInstance = Activator.CreateInstance(DllGraphType);
                dock.Children.Add(graphs_vm.VM_DynamicAlg.create());//?
                graphs_vm.VM_DynamicAlg.updateChoose(graphs_vm.vm_get_csv_normal_path(), graphs_vm.vm_get_csv_detect_path(), graphs_vm.vm_get_chunks(), graphs_vm.VM_Chunk_selected);
               // var updateChoosehMethod = DllGraphType.GetMethod("updateChoose");
               // updateChoosehMethod.Invoke(DllGraphInstance, new object[] { normal_csv_path, detect_csv_file, chunks, Chunk_selected });
                // updateChoose(string csv_learn, string csv_detect, List<string> features, string chosen_feature)
                
            }
            catch (Exception e)
            {
                Console.WriteLine("Error loading Dll File", e);
            }
        }

        public void update(string csv_learn, string csv_detect, List<string> features, string chosen_feature)
        {
            //dynamicAlg.updateChoose(graphs_vm.vm_get_csv_normal_path(), graphs_vm.vm_get_csv_detect_path(), graphs_vm.vm_get_chunks(), graphs_vm.VM_Chunk_selected);
            graphs_vm.VM_DynamicAlg.updateChoose(csv_learn, csv_detect, features, chosen_feature);
        }

        
    }

}