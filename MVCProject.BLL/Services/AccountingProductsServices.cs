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
    public class AccountingProductsServices : IRepository<AccountingProductsVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<AccountingProducts> _AccountingProductsRepository;

        public AccountingProductsServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _AccountingProductsRepository = new Repository<AccountingProducts>(context);
        }


        public IEnumerable<AccountingProductsVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<AccountingProductsVM>>(_AccountingProductsRepository.GetAll());
            return (IEnumerable<AccountingProductsVM>)data;
        }



        public AccountingProductsVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<AccountingProductsVM>(_AccountingProductsRepository.GetById(id));

        }

        public void Insert(AccountingProductsVM entity)
        {
            _AccountingProductsRepository.Insert(ProjectMapper.ConvertToEntity<AccountingProducts>(entity));
            uow.SaveChanges();

        }

        public void Update(AccountingProductsVM entity)
        {
            _AccountingProductsRepository.Update(ProjectMapper.ConvertToEntity<AccountingProducts>(entity));
            uow.SaveChanges();
        }

        public void Delete(AccountingProductsVM entity)
        {
            _AccountingProductsRepository.Delete(context.AccountingProducts.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
