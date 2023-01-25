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
        public static ObservableCollection<Agent> GetAgents() => new ObservableCollection<Agent>(SaveEyesEntities.GetContext().Agents);

        public static void SaveAgent(Agent agent)
        {
            if(agent.ID == 0)
                SaveEyesEntities.GetContext().Agents.Add(agent);
            SaveEyesEntities.GetContext().SaveChanges();
        }
    }
}
