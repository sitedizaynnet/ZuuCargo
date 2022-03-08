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
    public class SubMenuServices : IRepository<SubMenusVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<SubMenus> _subMenusRepository;

        public SubMenuServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _subMenusRepository = new Repository<SubMenus>(context);
        }


        public IEnumerable<SubMenusVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<SubMenusVM>>(_subMenusRepository.GetAll());
            return (IEnumerable<SubMenusVM>)data;
        }

        public SubMenusVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<SubMenusVM>(_subMenusRepository.GetById(id));

        }

        public void Insert(SubMenusVM entity)
        {
            _subMenusRepository.Insert(ProjectMapper.ConvertToEntity<SubMenus>(entity));
            uow.SaveChanges();

        }

        public void Update(SubMenusVM entity)
        {
            _subMenusRepository.Update(ProjectMapper.ConvertToEntity<SubMenus>(entity));
            uow.SaveChanges();
        }

        public void Delete(SubMenusVM entity)
        {
            _subMenusRepository.Delete(ProjectMapper.ConvertToEntity<SubMenus>(entity));
            uow.SaveChanges();
        }

        IEnumerable<SubMenusVM> IRepository<SubMenusVM>.GetAll()
        {
            throw new NotImplementedException();
        }

        SubMenusVM IRepository<SubMenusVM>.GetById(int id)
        {
            throw new NotImplementedException();
        }

        void IRepository<SubMenusVM>.Insert(SubMenusVM entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<SubMenusVM>.Update(SubMenusVM entity)
        {
            throw new NotImplementedException();
        }

        void IRepository<SubMenusVM>.Delete(SubMenusVM entity)
        {
            throw new NotImplementedException();
        }
    }
}
