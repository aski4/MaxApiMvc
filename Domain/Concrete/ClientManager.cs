using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Enities;
using Domain.EF;

namespace Domain.Concrete
{
    public class ClientManager : IClientManager
    {
        public EF.AppContext Database { get; set; }

        public ClientManager(EF.AppContext db)
        {
            Database = db;
        }

        public void Create(ClientProfile item)
        {
            Database.ClientProfiles.Add(item);
        }
        
        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
