using System;
using System.Collections.Generic;
using System.Text;
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
    /// Interaction logic for Joystick.xaml
    /// </summary>
    public partial class Joystick : UserControl
    {
        private Joystick_VM joystick_vm; /// <summary>
                                         /// ////////////
                                         /// </summary>
        public Joystick()
        {
            InitializeComponent();
            //centerKnob_Completed(this, new EventArgs());

        }

        public Joystick_VM VM_joystick
        {
            set
            {
                joystick_vm = value;
                this.DataContext = value;
            }
        }

    }
}
