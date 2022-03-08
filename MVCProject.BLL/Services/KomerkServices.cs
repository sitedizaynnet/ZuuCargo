using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System.Collections.Generic;

namespace MVCProject.BLL.Services
{
    public class KomerkServices : IRepository<KomerkVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Komerk> _ExchangeRepository;

        public KomerkServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ExchangeRepository = new Repository<Komerk>(context);
        }


        public IEnumerable<KomerkVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<KomerkVM>>(_ExchangeRepository.GetAll());
            return (IEnumerable<KomerkVM>)data;
        }



        public KomerkVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<KomerkVM>(_ExchangeRepository.GetById(id));

        }

        public void Insert(KomerkVM entity)
        {
            _ExchangeRepository.Insert(ProjectMapper.ConvertToEntity<Komerk>(entity));
            uow.SaveChanges();

        }

        public void Update(KomerkVM entity)
        {
            _ExchangeRepository.Update(ProjectMapper.ConvertToEntity<Komerk>(entity));
            uow.SaveChanges();
        }

        public void Delete(KomerkVM entity)
        {
            _ExchangeRepository.Delete(context.Komerk.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
