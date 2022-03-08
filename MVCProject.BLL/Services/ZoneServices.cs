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
    public class ZoneServices : IRepository<ZoneVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Zone> _ZoneRepository;

        public ZoneServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ZoneRepository = new Repository<Zone>(context);
        }


        public IEnumerable<ZoneVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ZoneVM>>(_ZoneRepository.GetAll());
            return (IEnumerable<ZoneVM>)data;
        }


        public ZoneVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ZoneVM>(_ZoneRepository.GetById(id));

        }


        public void Insert(ZoneVM entity)
        {
            _ZoneRepository.Insert(ProjectMapper.ConvertToEntity<Zone>(entity));
            uow.SaveChanges();

        }

        public void Update(ZoneVM entity)
        {

            _ZoneRepository.Update(ProjectMapper.ConvertToEntity<Zone>(entity));
            uow.SaveChanges();
        }

        public void Delete(ZoneVM entity)
        {
            _ZoneRepository.Delete(context.Zones.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
