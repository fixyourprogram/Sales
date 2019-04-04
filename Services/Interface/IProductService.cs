using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IProductService
    {
        Products Get(long id);

        List<Products> GetAll();

        void Save(Products entity);

        void Update(Products entity);

    }
}
