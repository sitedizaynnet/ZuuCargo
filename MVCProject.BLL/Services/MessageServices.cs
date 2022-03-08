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
    public class MessageServices : IRepository<MessageVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Message> _MessageRepository;

        public MessageServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _MessageRepository = new Repository<Message>(context);
        }

        public IEnumerable<MessageVM> GetAllByUserId(string userId)
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<MessageVM>>(_MessageRepository.GetAll().Where(x => x.OwnerId == userId));
            return (IEnumerable<MessageVM>)data;
        }
        public IEnumerable<MessageVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<MessageVM>>(_MessageRepository.GetAll());
            return (IEnumerable<MessageVM>)data;
        }

        public MessageVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<MessageVM>(_MessageRepository.GetById(id));

        }

        public void Insert(MessageVM entity)
        {
            _MessageRepository.Insert(ProjectMapper.ConvertToEntity<Message>(entity));
            uow.SaveChanges();

        }

        public void Update(MessageVM entity)
        {
            _MessageRepository.Update(ProjectMapper.ConvertToEntity<Message>(entity));
            uow.SaveChanges();
        }

        public void Delete(MessageVM entity)
        {
            _MessageRepository.Delete(context.Messages.Find(entity.Id));

            uow.SaveChanges();
        }


    }
}
