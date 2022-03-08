using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System.Collections.Generic;

namespace MVCProject.BLL.Services
{
    public class ExpensiveServices : IRepository<ExpensiveVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Expensive> _ExchangeRepository;

        public ExpensiveServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ExchangeRepository = new Repository<Expensive>(context);
        }


        public IEnumerable<ExpensiveVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ExpensiveVM>>(_ExchangeRepository.GetAll());
            return (IEnumerable<ExpensiveVM>)data;
        }



        public ExpensiveVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ExpensiveVM>(_ExchangeRepository.GetById(id));

        }

        public void Insert(ExpensiveVM entity)
        {
            _ExchangeRepository.Insert(ProjectMapper.ConvertToEntity<Expensive>(entity));
            uow.SaveChanges();

        }

        public void Update(ExpensiveVM entity)
        {
            _ExchangeRepository.Update(ProjectMapper.ConvertToEntity<Expensive>(entity));
            uow.SaveChanges();
        }

        public void Delete(ExpensiveVM entity)
        {
            _ExchangeRepository.Delete(context.Expensive.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
