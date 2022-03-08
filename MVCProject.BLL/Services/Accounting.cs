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
    public class AccountingServices : IRepository<AccountingVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Accounting> _AccountingRepository;

        public AccountingServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _AccountingRepository = new Repository<Accounting>(context);
        }


        public IEnumerable<AccountingVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<AccountingVM>>(_AccountingRepository.GetAll());
            return (IEnumerable<AccountingVM>)data;
        }



        public AccountingVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<AccountingVM>(_AccountingRepository.GetById(id));

        }

        public void Insert(AccountingVM entity)
        {
            _AccountingRepository.Insert(ProjectMapper.ConvertToEntity<Accounting>(entity));
            uow.SaveChanges();

        }

        public void Update(AccountingVM entity)
        {
            _AccountingRepository.Update(ProjectMapper.ConvertToEntity<Accounting>(entity));
            uow.SaveChanges();
        }

        public void Delete(AccountingVM entity)
        {
            _AccountingRepository.Delete(context.Accountings.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
