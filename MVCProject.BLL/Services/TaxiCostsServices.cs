using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System.Collections.Generic;

namespace MVCProject.BLL.Services
{
    public class TaxiCostsServices : IRepository<TaxiCostsVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<TaxiCosts> _TaxiCostsRepository;

        public TaxiCostsServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _TaxiCostsRepository = new Repository<TaxiCosts>(context);
        }


        public IEnumerable<TaxiCostsVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<TaxiCostsVM>>(_TaxiCostsRepository.GetAll());
            return (IEnumerable<TaxiCostsVM>)data;
        }



        public TaxiCostsVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<TaxiCostsVM>(_TaxiCostsRepository.GetById(id));

        }

        public void Insert(TaxiCostsVM entity)
        {
            _TaxiCostsRepository.Insert(ProjectMapper.ConvertToEntity<TaxiCosts>(entity));
            uow.SaveChanges();

        }

        public void Update(TaxiCostsVM entity)
        {
            _TaxiCostsRepository.Update(ProjectMapper.ConvertToEntity<TaxiCosts>(entity));
            uow.SaveChanges();
        }

        public void Delete(TaxiCostsVM entity)
        {
            _TaxiCostsRepository.Delete(context.TaxiCosts.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
