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
    public class CategoryServices : IRepository<CategoryVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Category> _CategoryRepository;

        public CategoryServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _CategoryRepository = new Repository<Category>(context);
        }


        public IEnumerable<CategoryVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<CategoryVM>>(_CategoryRepository.GetAll());
            return (IEnumerable<CategoryVM>)data;
        }



        public CategoryVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<CategoryVM>(_CategoryRepository.GetById(id));

        }

        public void Insert(CategoryVM entity)
        {
            _CategoryRepository.Insert(ProjectMapper.ConvertToEntity<Category>(entity));
            uow.SaveChanges();

        }

        public void Update(CategoryVM entity)
        {
            _CategoryRepository.Update(ProjectMapper.ConvertToEntity<Category>(entity));
            uow.SaveChanges();
        }

        public void Delete(CategoryVM entity)
        {
            _CategoryRepository.Delete(context.Category.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
