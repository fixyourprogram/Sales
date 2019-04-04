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
    public class ClientsService : IClientsService
    {
        private IRepositoryBase<Clients> _repository;

        public ClientsService()
        {
            _repository = new RepositoryBase<Clients>();
        }

        public Clients Get(long id)
        {
            try
            {
                return _repository.Get(l => l.Id == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Clients> GetAll()
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

        public void Save(Clients entity)
        {
            try
            {
                entity.Active = true;

                _repository.Save(entity);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(Clients entity)
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
