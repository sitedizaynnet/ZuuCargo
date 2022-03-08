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
    public class RefundService : IRepository<RefundVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Refund> _RefundRepository;

        public RefundService()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _RefundRepository = new Repository<Refund>(context);
        }


        public IEnumerable<RefundVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<RefundVM>>(_RefundRepository.GetAll());
            return (IEnumerable<RefundVM>)data;
        }
       
        public RefundVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<RefundVM>(_RefundRepository.GetById(id));

        }


        public void Insert(RefundVM entity)
        {
            _RefundRepository.Insert(ProjectMapper.ConvertToEntity<Refund>(entity));
            uow.SaveChanges();

        }

        public void Update(RefundVM entity)
        {

            _RefundRepository.Update(ProjectMapper.ConvertToEntity<Refund>(entity));
            uow.SaveChanges();
        }

        public void Delete(RefundVM entity)
        {
            _RefundRepository.Delete(context.Refunds.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
