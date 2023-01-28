using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveEyes.Data
{
    public static class DataAccess
    {
        public  delegate void RefreshListDelegate();
        public static event RefreshListDelegate RefreshList;

        public static ObservableCollection<Agent> GetAgents() => new ObservableCollection<Agent>(SaveEyesEntities.GetContext().Agents);

        public static void SaveAgent(Agent agent)
        {
            if(agent.ID == 0)
                SaveEyesEntities.GetContext().Agents.Add(agent);
            SaveEyesEntities.GetContext().SaveChanges();
            RefreshList?.Invoke();
        }

        public static List<AgentType> GetAgentTypes()
        {
            return SaveEyesEntities.GetContext().AgentTypes.ToList();
        }

        public static void DeleteAgent(Agent agent)
        {
            SaveEyesEntities.GetContext().Agents.Remove(agent);
            SaveEyesEntities.GetContext().SaveChanges();
            RefreshList?.Invoke();
        }

        public static List<Product> GetProducts()
        {
            return SaveEyesEntities.GetContext().Products.ToList();
        }

        public static void DeleteProductSale(ProductSale product)
        {
            SaveEyesEntities.GetContext().ProductSales.Remove(product);
            SaveEyesEntities.GetContext().SaveChanges();
            RefreshList?.Invoke();
        }
    }
}
