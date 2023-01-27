using SaveEyes.Data;
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

namespace SaveEyes.Windows
{
    /// <summary>
    /// Interaction logic for ChangePriorityWindow.xaml
    /// </summary>
    public partial class ChangePriorityWindow : Window
    {
        public int Priority { get; set; }
        private List<Agent> agents;
        public ChangePriorityWindow(List<Agent> agents)
        {
            this.agents = agents;
            InitializeComponent();
            Priority = agents.Max(a => a.Priority);
            DataContext = this;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Priority == 0 && MessageBox.Show("Приоритет выбранных агентов не изменится", "Предупреждение", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.Cancel)
            {
                return;
            }
            DialogResult = true;

            foreach (var agent in agents)
            {
                agent.Priority += Priority;
                DataAccess.SaveAgent(agent);
            }
        }
    }
}
