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
    public class ReplyServices : IRepository<ReplyVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Reply> _ReplyRepository;

        public ReplyServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ReplyRepository = new Repository<Reply>(context);
        }


        public IEnumerable<ReplyVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ReplyVM>>(_ReplyRepository.GetAll());
            return (IEnumerable<ReplyVM>)data;
        }

        public ReplyVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ReplyVM>(_ReplyRepository.GetById(id));

        }

        public void Insert(ReplyVM entity)
        {
            _ReplyRepository.Insert(ProjectMapper.ConvertToEntity<Reply>(entity));
            uow.SaveChanges();

        }

        public void Update(ReplyVM entity)
        {
            _ReplyRepository.Update(ProjectMapper.ConvertToEntity<Reply>(entity));
            uow.SaveChanges();
        }

        public void Delete(ReplyVM entity)
        {
            _ReplyRepository.Delete(context.Replies.Find(entity.Id));

            uow.SaveChanges();
        }


    }
}
