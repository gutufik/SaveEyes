using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using SaveEyes.Data;

namespace SaveEyes.Pages
{
    /// <summary>
    /// Interaction logic for AgentsListPage.xaml
    /// </summary>
    public partial class AgentsListPage : Page
    {
        public ObservableCollection<Agent> Agents { get; set; }
        public AgentsListPage()
        {
            InitializeComponent();
            Agents = DataAccess.GetAgents();
            DataContext = this;
        }

        private void lvAgents_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //foreach (var agent in Agents)
            //{
            //    if (agent.Logo != null)
            //    {
            //        agent.LogoImage = File.ReadAllBytes(@"C:\Users\GIANOK\Desktop\agents\" + agent.Logo);
            //        DataAccess.SaveAgent(agent);
            //    }
            //}
        }
    }
}
