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
        public IEnumerable<Agent> Agents { get; set; }
        public IEnumerable<Agent> AgentsForFilters { get; set; }
        public string Search { get; set; } = "";
        public string Sort { get; set; }
        public AgentType Type { get; set; } = new AgentType { Title = "Все типы" };

        public List<AgentType> AgentTypes { get; set; }
        public Dictionary<string, Func<Agent, object>> Sortings { get; set; }

        private int _pageNumber = 1;

        private int PageNumber { get; set; }
        public AgentsListPage()
        {
            InitializeComponent();
            AgentsForFilters = DataAccess.GetAgents();
            AgentTypes = DataAccess.GetAgentTypes();
            AgentTypes.Insert(0, new AgentType { Title = "Все типы" });


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

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            lvAgents.ItemsSource = AgentsForFilters;
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvAgents.ItemsSource = AgentsForFilters;
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lvAgents.ItemsSource = AgentsForFilters;
        }

        private void PrevPage_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }
    }
}
