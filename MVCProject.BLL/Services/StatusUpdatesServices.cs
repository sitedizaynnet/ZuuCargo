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
    public class StatusUpdatesServices : IRepository<StatusUpdatesVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<StatusUpdates> _StatusUpdatesRepository;

        public StatusUpdatesServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _StatusUpdatesRepository = new Repository<StatusUpdates>(context);
        }


        public IEnumerable<StatusUpdatesVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<StatusUpdatesVM>>(_StatusUpdatesRepository.GetAll());
            return (IEnumerable<StatusUpdatesVM>)data;
        }


        public StatusUpdatesVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<StatusUpdatesVM>(_StatusUpdatesRepository.GetById(id));

        }


        public void Insert(StatusUpdatesVM entity)
        {
            _StatusUpdatesRepository.Insert(ProjectMapper.ConvertToEntity<StatusUpdates>(entity));
            uow.SaveChanges();

        }

        public void Update(StatusUpdatesVM entity)
        {

            _StatusUpdatesRepository.Update(ProjectMapper.ConvertToEntity<StatusUpdates>(entity));
            uow.SaveChanges();
        }

        public void Delete(StatusUpdatesVM entity)
        {
            _StatusUpdatesRepository.Delete(context.StatusUpdates.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
