using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System.Collections.Generic;

namespace MVCProject.BLL.Services
{
    public class ArrivedExchangeServices : IRepository<ArrivedExchangeVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<ArrivedExchange> _ArrivedExchangeRepository;

        public ArrivedExchangeServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ArrivedExchangeRepository = new Repository<ArrivedExchange>(context);
        }


        public IEnumerable<ArrivedExchangeVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ArrivedExchangeVM>>(_ArrivedExchangeRepository.GetAll());
            return (IEnumerable<ArrivedExchangeVM>)data;
        }



        public ArrivedExchangeVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ArrivedExchangeVM>(_ArrivedExchangeRepository.GetById(id));

        }

        public void Insert(ArrivedExchangeVM entity)
        {
            _ArrivedExchangeRepository.Insert(ProjectMapper.ConvertToEntity<ArrivedExchange>(entity));
            uow.SaveChanges();

        }

        public void Update(ArrivedExchangeVM entity)
        {
            _ArrivedExchangeRepository.Update(ProjectMapper.ConvertToEntity<ArrivedExchange>(entity));
            uow.SaveChanges();
        }

        public void Delete(ArrivedExchangeVM entity)
        {
            _ArrivedExchangeRepository.Delete(context.ArrivedExchange.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
