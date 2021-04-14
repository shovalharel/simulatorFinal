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
    /// Interaction logic for Graphs.xaml
    /// </summary>
    public partial class Graphs : UserControl
    {
        private Graphs_VM graphs_vm;
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
            }
        }
        private void Listbox_chunks_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            graphs_vm.VM_Chunk_selected = Listbox_chunks_list.SelectedItem.ToString();
        }
    }
}