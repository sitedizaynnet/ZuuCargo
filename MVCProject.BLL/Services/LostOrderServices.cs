using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using MVCProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.BLL.Services
{
    public class LostOrderServices : IRepository<LostOrderVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<LostOrder> _LostOrderRepository;

        public LostOrderServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _LostOrderRepository = new Repository<LostOrder>(context);
        }


        public IEnumerable<LostOrderVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<LostOrderVM>>(_LostOrderRepository.GetAll());
            return (IEnumerable<LostOrderVM>)data;
        }
     
       

        public LostOrderVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<LostOrderVM>(_LostOrderRepository.GetById(id));

        }


        public void Insert(LostOrderVM entity)
        {
            _LostOrderRepository.Insert(ProjectMapper.ConvertToEntity<LostOrder>(entity));
            uow.SaveChanges();

        }

        public void Update(LostOrderVM entity)
        {

            _LostOrderRepository.Update(ProjectMapper.ConvertToEntity<LostOrder>(entity));
            uow.SaveChanges();
        }

        public void Delete(LostOrderVM entity)
        {
            _LostOrderRepository.Delete(context.LostOrders.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
