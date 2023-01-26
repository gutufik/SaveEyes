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
        public IEnumerable<Agent> AgentsForFilters { 
            get 
            {
                var agents = Agents.Where(x => x.Title.ToLower().Contains(Search)
                                                    || x.Email.Contains(Search)
                                                    || x.Phone.Contains(Search))
                            .OrderBy(Sortings[Sort])
                            .Where(x => Type.Title == "Все типы" ? true: x.AgentType == Type)
                            .Skip((PageNumber - 1)* 10).Take(10);
                if (Sort.Contains("возрастанию"))
                    agents = agents.Reverse();


                return agents;
            }
            set 
            {
                Agents = value;
                Paginator.Children.Clear();

                // добавляем переход на предыдущую страницу
                Paginator.Children.Add(new TextBlock { Text = " < " });

                // в цикле добавляем страницы
                for (int i = 1; i < Agents.Count() / 10; i++)
                    Paginator.Children.Add(
                        new TextBlock { Text = " " + i.ToString() + " " });

                // добавляем переход на следующую страницу
                Paginator.Children.Add(new TextBlock { Text = " > " });

                // проходимся в цикле по всем сохданным элементам и задаем им обработчик PreviewMouseDown
                foreach (TextBlock tb in Paginator.Children)
                    tb.PreviewMouseDown += PrevPage_PreviewMouseDown;
            }
        }
        public string Search { get; set; } = "";
        public string Sort { get; set; }
        public AgentType Type { get; set; } = new AgentType { Title = "Все типы" };

        public List<AgentType> AgentTypes { get; set; }
        public Dictionary<string, Func<Agent, object>> Sortings { get; set; }

        private int _pageNumber = 1;

        private int PageNumber { 
            get 
            {
                return _pageNumber;
            }
            set 
            {
                _pageNumber = value;
                InvalidateVisual();
            }
        }
        public AgentsListPage()
        {
            InitializeComponent();
            AgentsForFilters = DataAccess.GetAgents();
            Sortings = new Dictionary<string, Func<Agent, object>>
            {
                { "Наименование по возрастанию", x => x.Title },
                { "Наименование по убыванию", x => x.Title },
                { "Размер скидки по возрастанию", x => x.Discount },
                { "Размер скидки по убыванию", x => x.Discount },
                { "Приоритет по возрастанию", x => x.Priority },
                { "Приоритет по убыванию", x => x.Priority },
            };

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
            switch ((sender as TextBlock).Text)
            {
                case " < ":
                    // переход на предыдущую страницу с проверкой счётчика
                    if (PageNumber > 0) PageNumber--;
                    return;
                case " > ":
                    // переход на следующую страницу с проверкой счётчика
                    if (PageNumber < Agents.Count() / 10) PageNumber++;
                    return;
                default:
                    // в остальных элементах просто номер странцы
                    // учитываем, что надо обрезать пробелы (Trim)
                    PageNumber = Convert.ToInt32(
                        (sender as TextBlock).Text.Trim());
                    return;
            }

        }
    }
}
