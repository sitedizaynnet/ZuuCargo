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
    public class CustomerLocationServices : IRepository<CustomerLocationVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<CustomerLocation> _CustomerLocationRepository;

        public CustomerLocationServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _CustomerLocationRepository = new Repository<CustomerLocation>(context);
        }


        public IEnumerable<CustomerLocationVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<CustomerLocationVM>>(_CustomerLocationRepository.GetAll());
            return (IEnumerable<CustomerLocationVM>)data;
        }
       
        public CustomerLocationVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<CustomerLocationVM>(_CustomerLocationRepository.GetById(id));

        }


        public void Insert(CustomerLocationVM entity)
        {
            _CustomerLocationRepository.Insert(ProjectMapper.ConvertToEntity<CustomerLocation>(entity));
            uow.SaveChanges();

        }

        public void Update(CustomerLocationVM entity)
        {

            _CustomerLocationRepository.Update(ProjectMapper.ConvertToEntity<CustomerLocation>(entity));
            uow.SaveChanges();
        }

        public void Delete(CustomerLocationVM entity)
        {
            _CustomerLocationRepository.Delete(context.CustomerLocation.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
