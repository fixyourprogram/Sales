using Domain.Entities;
using Infrastructure.Concrete;
using Infrastructure.Interfaces;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Concrete
{
    public class ProductService : IProductService
    {
        private IRepositoryBase<Products> _repository;

        public ProductService()
        {
            _repository = new RepositoryBase<Products>();
        }

        public Products Get(long id)
        {
            try
            {
                return _repository.Get(l => l.Id == id, f => f.Clients);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Products> GetAll()
        {
            try
            {
                return _repository.GetAll().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Save(Products entity)
        {
            try
            {
                _repository.Save(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Products entity)
        {
            try
            {

                _repository.Update(entity, entity.Id);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
