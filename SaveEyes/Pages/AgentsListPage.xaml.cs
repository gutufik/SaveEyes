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
        public List<AgentType> AgentTypes { get; set; }
        public Dictionary<string, Func<Agent, object>> Sortings { get; set; }

        private int PageNumber = 1;
        public AgentsListPage()
        {
            InitializeComponent();
            Agents = DataAccess.GetAgents();
            Sortings = new Dictionary<string, Func<Agent, object>>
            {
                { "Наименование по возрастанию", x => x.Title },
                { "Наименование по убыванию", x => x.Title },
                //{ "Размер скидки по возрастанию", x => x },
                //{ "Размер скидки по убыванию", x => x },
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

        private void ApplyFilters()
        {
            if (cbSort.SelectedItem != null && cbFilter.SelectedItem != null)
            {
                var searchText = tbSearch.Text.ToLower();
                var selectedType = cbFilter.SelectedItem as AgentType;
                var selectedSorting = cbSort.SelectedItem as string;
                AgentsForFilters = Agents.Where(x => x.Title.ToLower().Contains(searchText) 
                                                    || x.Email.Contains(searchText)
                                                    || x.Phone.Contains(searchText));
                if (selectedType.Title != "Все типы")
                    AgentsForFilters = AgentsForFilters.Where(x => x.AgentType == selectedType);

                if (selectedSorting != null)
                {
                    if (selectedSorting.Contains("по убыванию"))
                        AgentsForFilters = AgentsForFilters.OrderByDescending(Sortings[selectedSorting]);
                    else
                        AgentsForFilters = AgentsForFilters.OrderBy(Sortings[selectedSorting]);
                }

                lvAgents.ItemsSource = new ObservableCollection<Agent>(AgentsForFilters.Skip((PageNumber - 1) * 10).Take(10));
                SetPageNumbers();
            }
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApplyFilters();
        }
        private void SetPageNumbers()
        {
            PageNumbersPanel.Children.Clear();
            int pagesCount = AgentsForFilters.Count() % 10 == 0 ? AgentsForFilters.Count() / 10 : AgentsForFilters.Count() / 10 + 1;
            for (int i = 0; i < pagesCount; i++)
            {
                var hyperlink = new Hyperlink()
                {
                    Foreground = new SolidColorBrush(Colors.Black),
                    FontSize = 25,
                    TextDecorations = null
                };
                hyperlink.Inlines.Add($"{i + 1}");
                hyperlink.Click += NavigateToPage;

                var textBlock = new TextBlock() { Margin = new Thickness(5, 0, 5, 0) };

                if (i == PageNumber - 1)
                    hyperlink.TextDecorations = TextDecorations.Underline;

                textBlock.Inlines.Add(hyperlink);

                PageNumbersPanel.Children.Add(textBlock);
            }
        }
        private void NavigateToPage(object sender, RoutedEventArgs e)
        {
            PageNumber = int.Parse(((sender as Hyperlink).Inlines.FirstOrDefault() as Run).Text);
            (sender as Hyperlink).TextDecorations = TextDecorations.Underline;
            ApplyFilters();
        }
        private void PreviousPageButton_Click(object sender, RoutedEventArgs e)
        {
            if (PageNumber > 1)
            {
                PageNumber--;
                ApplyFilters();
            }
        }

        private void NextPageButton_Click(object sender, RoutedEventArgs e)
        {
            int pagesCount = AgentsForFilters.Count() % 10 == 0 ? AgentsForFilters.Count() / 10 : AgentsForFilters.Count() / 10 + 1;
            if (PageNumber < pagesCount)
            {
                PageNumber++;
                ApplyFilters();
            }
        }
    }
}
