using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System.Collections.Generic;

namespace MVCProject.BLL.Services
{
    public class ReklamServices : IRepository<ReklamVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Reklam> _ExchangeRepository;

        public ReklamServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ExchangeRepository = new Repository<Reklam>(context);
        }


        public IEnumerable<ReklamVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ReklamVM>>(_ExchangeRepository.GetAll());
            return (IEnumerable<ReklamVM>)data;
        }



        public ReklamVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ReklamVM>(_ExchangeRepository.GetById(id));

        }

        public void Insert(ReklamVM entity)
        {
            _ExchangeRepository.Insert(ProjectMapper.ConvertToEntity<Reklam>(entity));
            uow.SaveChanges();

        }

        public void Update(ReklamVM entity)
        {
            _ExchangeRepository.Update(ProjectMapper.ConvertToEntity<Reklam>(entity));
            uow.SaveChanges();
        }

        public void Delete(ReklamVM entity)
        {
            _ExchangeRepository.Delete(context.Reklam.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
