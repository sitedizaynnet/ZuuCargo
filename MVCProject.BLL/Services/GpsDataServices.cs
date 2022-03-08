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
    public class GpsDataServices : IRepository<GpsDataVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<GpsData> _GpsDataRepository;

        public GpsDataServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _GpsDataRepository = new Repository<GpsData>(context);
        }


        public IEnumerable<GpsDataVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<GpsDataVM>>(_GpsDataRepository.GetAll());
            return (IEnumerable<GpsDataVM>)data;
        }
       
        public GpsDataVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<GpsDataVM>(_GpsDataRepository.GetById(id));

        }


        public void Insert(GpsDataVM entity)
        {
            _GpsDataRepository.Insert(ProjectMapper.ConvertToEntity<GpsData>(entity));
            uow.SaveChanges();

        }

        public void Update(GpsDataVM entity)
        {

            _GpsDataRepository.Update(ProjectMapper.ConvertToEntity<GpsData>(entity));
            uow.SaveChanges();
        }

        public void Delete(GpsDataVM entity)
        {
            _GpsDataRepository.Delete(context.GpsData.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
