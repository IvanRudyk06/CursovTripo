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
using System.Windows.Shapes;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for ResultsWorkFactory.xaml
    /// </summary>
    public partial class ResultsWorkFactory : Window
    {
        private List<String> listResults;
        public Factory FactoryResult { get; set; }
        public ResultsWorkFactory(Factory factory)
        {
            listResults = new List<String>();
            FactoryResult = factory;
            InitializeComponent();
            writeResults();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        //public void setUtilization()
        //{
        //    for(int i = 0; i<FactoryResult.MyTechnoLines.Count; i++)
        //    {
        //        for(int j = 0; j< FactoryResult.MyTechnoLines[i].Machines.Length; j++)
        //        {
        //            FactoryResult.MyTechnoLines[i].Machines[j].UtilizationMachine = ((int)FactoryResult.MyTechnoLines[i].Machines[j].SumTimeWork
        //                / (FactoryResult.TimeWork.TimeScheduledMiliSecond/10))/100.0;
        //        }
        //    }
        //}

        public void writeResults()
        {
            listResults.Add("         Підприємство з виготовлення Мікросхем");
            listResults.Add("" + Environment.NewLine);
            listResults.Add("Час роботи : " + FactoryResult.TimeWork.TimeScheduledMiliSecond/ 1000.0+ " год.    Згенеровано мікросхем : "+
                              FactoryResult.CountOfGeneratedDetail+ "    Виготовлено мікросхем : "+ FactoryResult.CountOfDoneDetail);
            foreach(TechnoLine tl in FactoryResult.MyTechnoLines)
            {
                listResults.Add(tl.ToString());
                foreach(Machine m in tl.Machines)
                {
                    listResults.Add(m.ToString());
                }
                listResults.Add("" + Environment.NewLine);
            }

            foreach (String s in listResults)
            {
                textBox.Text += s + Environment.NewLine;
            }
        }


    }
}
