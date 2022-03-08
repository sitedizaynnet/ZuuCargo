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
    public class TotalCargoService : IRepository<TotalCargoVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<TotalCargo> _TotalCargoRepository;

        public TotalCargoService()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _TotalCargoRepository = new Repository<TotalCargo>(context);
        }


        public IEnumerable<TotalCargoVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<TotalCargoVM>>(_TotalCargoRepository.GetAll());
            return (IEnumerable<TotalCargoVM>)data;
        }
       
        public TotalCargoVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<TotalCargoVM>(_TotalCargoRepository.GetById(id));

        }


        public void Insert(TotalCargoVM entity)
        {
            _TotalCargoRepository.Insert(ProjectMapper.ConvertToEntity<TotalCargo>(entity));
            uow.SaveChanges();

        }

        public void Update(TotalCargoVM entity)
        {

            _TotalCargoRepository.Update(ProjectMapper.ConvertToEntity<TotalCargo>(entity));
            uow.SaveChanges();
        }

        public void Delete(TotalCargoVM entity)
        {
            _TotalCargoRepository.Delete(context.TotalCargo.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
