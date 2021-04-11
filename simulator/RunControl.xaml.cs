using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for runControl.xaml
    /// </summary>
    public partial class RunControl : UserControl
    {
        private RunControl_VM runControl_vm;
        public RunControl()
        {
            InitializeComponent();
        }
        public RunControl_VM VM_RunControl
        {
            get
            {
                return runControl_vm;
            }
            set
            {
                runControl_vm = value;
                this.DataContext = value;
            }
        }
        // open file browser

        private void Open_csv_Click(object sender, RoutedEventArgs e)
        {
            runControl_vm.open_csv_file();
        }

        private void Open_xml_Click(object sender, RoutedEventArgs e)
        {
            runControl_vm.open_xml_file();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            runControl_vm.start_running();
        }

        private void Open_csv2_file_click(object sender, RoutedEventArgs e)
        {
            runControl_vm.open_csv2_file();
        }
    }
}