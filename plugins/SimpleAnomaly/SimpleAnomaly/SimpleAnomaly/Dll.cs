using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace SimpleAnomaly
{
    public class Dll
    {
        private UserControl1 uc;
        private DllGraph_VM vm;
        private DllGraphModel model;

        public UserControl1 create()
        {
            model = new DllGraphModel();
            vm = new DllGraph_VM(model);
            uc = new UserControl1(vm);
            return uc;
        }

        public void update(string csv_learn, string csv_detect, List<string> features, string chosen_feature)
        {
            model.updateChoose(csv_learn, csv_detect, features, chosen_feature);
        }

        

        
    }
}
