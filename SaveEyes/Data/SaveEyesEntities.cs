using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveEyes.Data
{
    public partial class SaveEyesEntities
    {
        private static SaveEyesEntities context;
        public static SaveEyesEntities GetContext()
        {
            if (context == null)
                context = new SaveEyesEntities();
            return context;
        }
    }
}
