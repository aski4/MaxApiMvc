using Domain.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IClientManager : IDisposable
    {
        void Create(ClientProfile item);
        

    }
}
