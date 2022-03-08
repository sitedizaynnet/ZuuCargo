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
    public class MenuServices : IRepository<MenusVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Menus> _MenusRepository;

        public MenuServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _MenusRepository = new Repository<Menus>(context);
        }


        public IEnumerable<MenusVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<MenusVM>>(_MenusRepository.GetAll());
            return (IEnumerable<MenusVM>)data;
        }

        public MenusVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<MenusVM>(_MenusRepository.GetById(id));

        }

        public void Insert(MenusVM entity)
        {
            _MenusRepository.Insert(ProjectMapper.ConvertToEntity<Menus>(entity));
            uow.SaveChanges();

        }

        public void Update(MenusVM entity)
        {
            _MenusRepository.Update(ProjectMapper.ConvertToEntity<Menus>(entity));
            uow.SaveChanges();
        }

        public void Delete(MenusVM entity)
        {
            _MenusRepository.Delete(ProjectMapper.ConvertToEntity<Menus>(entity));
            uow.SaveChanges();
        }


    }
}
