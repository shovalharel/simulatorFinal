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
    /// Interaction logic for DataFly.xaml
    /// </summary>
    public partial class DataFly : UserControl
    {
        private DataFly_VM dataFly_vm;
        public DataFly()
        {
            InitializeComponent();
        }
        public DataFly_VM VM_DataFly
        {
            set
            {
                this.dataFly_vm = value;
                this.DataContext = value;
            }

        }
    }
}