using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System.Collections.Generic;

namespace MVCProject.BLL.Services
{
    public class RemainingCostServices : IRepository<RemainingCostVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<RemainingCost> _ExchangeRepository;

        public RemainingCostServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ExchangeRepository = new Repository<RemainingCost>(context);
        }


        public IEnumerable<RemainingCostVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<RemainingCostVM>>(_ExchangeRepository.GetAll());
            return (IEnumerable<RemainingCostVM>)data;
        }



        public RemainingCostVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<RemainingCostVM>(_ExchangeRepository.GetById(id));

        }

        public void Insert(RemainingCostVM entity)
        {
            _ExchangeRepository.Insert(ProjectMapper.ConvertToEntity<RemainingCost>(entity));
            uow.SaveChanges();

        }

        public void Update(RemainingCostVM entity)
        {
            _ExchangeRepository.Update(ProjectMapper.ConvertToEntity<RemainingCost>(entity));
            uow.SaveChanges();
        }

        public void Delete(RemainingCostVM entity)
        {
            _ExchangeRepository.Delete(context.RemainingCost.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
