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
    public class DepositServices : IRepository<DepositVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Deposit> _DepositRepository;

        public DepositServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _DepositRepository = new Repository<Deposit>(context);
        }


        public IEnumerable<DepositVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<DepositVM>>(_DepositRepository.GetAll());
            return (IEnumerable<DepositVM>)data;
        }
       
        public DepositVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<DepositVM>(_DepositRepository.GetById(id));

        }


        public void Insert(DepositVM entity)
        {
            _DepositRepository.Insert(ProjectMapper.ConvertToEntity<Deposit>(entity));
            uow.SaveChanges();

        }

        public void Update(DepositVM entity)
        {

            _DepositRepository.Update(ProjectMapper.ConvertToEntity<Deposit>(entity));
            uow.SaveChanges();
        }

        public void Delete(DepositVM entity)
        {
            _DepositRepository.Delete(context.Deposit.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
