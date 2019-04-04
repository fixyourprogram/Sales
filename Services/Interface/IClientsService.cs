using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IClientsService
    {
        Clients Get(long id);

        List<Clients> GetAll();

        void Save(Clients entity);

        void Update(Clients entity);
    }
}
