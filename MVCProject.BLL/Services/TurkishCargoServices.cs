using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System.Collections.Generic;

namespace MVCProject.BLL.Services
{
    public class TurkishCargoServices : IRepository<TurkishCargoVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<TurkishCargo> _TurkishCargoRepository;

        public TurkishCargoServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _TurkishCargoRepository = new Repository<TurkishCargo>(context);
        }


        public IEnumerable<TurkishCargoVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<TurkishCargoVM>>(_TurkishCargoRepository.GetAll());
            return (IEnumerable<TurkishCargoVM>)data;
        }



        public TurkishCargoVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<TurkishCargoVM>(_TurkishCargoRepository.GetById(id));

        }

        public void Insert(TurkishCargoVM entity)
        {
            _TurkishCargoRepository.Insert(ProjectMapper.ConvertToEntity<TurkishCargo>(entity));
            uow.SaveChanges();

        }

        public void Update(TurkishCargoVM entity)
        {
            _TurkishCargoRepository.Update(ProjectMapper.ConvertToEntity<TurkishCargo>(entity));
            uow.SaveChanges();
        }

        public void Delete(TurkishCargoVM entity)
        {
            _TurkishCargoRepository.Delete(context.TurkishCargo.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
