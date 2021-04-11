using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for Player.xaml
    /// </summary>
    /// 

    public partial class Player : UserControl
    {
        private Player_VM player_vm;
        public Player()
        {
            InitializeComponent();
        }

        public Player_VM VM_Player
        {
            get
            {
                return player_vm;
            }
            set
            {
                player_vm = value;
                this.DataContext = value;
            }
        }



        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }




        private void play_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("play click!!");
            player_vm.play();
        }
        private void stop_Click_1(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("stop click!!");
            player_vm.stop();
        }
        private void pause_Click_1(object sender, RoutedEventArgs e)
        {
            player_vm.pause();
        }
        private void forward_Click_1(object sender, RoutedEventArgs e)
        {
            player_vm.forward();
        }
        private void back_Click_1(object sender, RoutedEventArgs e)
        {
            player_vm.back();
        }
        private bool handle = true;
        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            if (handle) Handle();
            handle = true;
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cmb = sender as ComboBox;
            handle = !cmb.IsDropDownOpen;
            Handle();
        }
        private void Handle()
        {
            switch (cmbSelect.SelectedItem.ToString().Split(new string[] { ": " }, StringSplitOptions.None).Last())
            {
                case "0.5":
                    player_vm.setspeed05();
                    break;
                case "0.75":
                    player_vm.setspeed075();
                    break;
                case "1":
                    player_vm.setspeed1();
                    break;
                case "1.5":
                    player_vm.setspeed15();
                    break;
                case "2":
                    player_vm.setspeed2();
                    break;
            }
        }
    }
}