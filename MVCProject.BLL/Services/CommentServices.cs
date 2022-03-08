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
    public class CommentServices : IRepository<CommentVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Comment> _CommentRepository;

        public CommentServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _CommentRepository = new Repository<Comment>(context);
        }


        public IEnumerable<CommentVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<CommentVM>>(_CommentRepository.GetAll());
            return (IEnumerable<CommentVM>)data;
        }
        public IEnumerable<CommentVM> GetAllByProviderId(int id)
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<CommentVM>>(_CommentRepository.GetAll().Where(x=>x.ProviderId == id));
            return (IEnumerable<CommentVM>)data;
        }


        public CommentVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<CommentVM>(_CommentRepository.GetById(id));

        }

        public void Insert(CommentVM entity)
        {
            _CommentRepository.Insert(ProjectMapper.ConvertToEntity<Comment>(entity));
            uow.SaveChanges();

        }

        public void Update(CommentVM entity)
        {
            _CommentRepository.Update(ProjectMapper.ConvertToEntity<Comment>(entity));
            uow.SaveChanges();
        }

        public void Delete(CommentVM entity)
        {
            _CommentRepository.Delete(context.Comment.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
