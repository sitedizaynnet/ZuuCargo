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
    public class IlcelerServices : IRepository<IlcelerVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Ilceler> _IlcelerRepository;

        public IlcelerServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _IlcelerRepository = new Repository<Ilceler>(context);
        }


        public IEnumerable<IlcelerVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<IlcelerVM>>(_IlcelerRepository.GetAll());
            return (IEnumerable<IlcelerVM>)data;
        }

        public IlcelerVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<IlcelerVM>(_IlcelerRepository.GetById(id));

        }

        public void Insert(IlcelerVM entity)
        {
            _IlcelerRepository.Insert(ProjectMapper.ConvertToEntity<Ilceler>(entity));
            uow.SaveChanges();

        }

        public void Update(IlcelerVM entity)
        {
            _IlcelerRepository.Update(ProjectMapper.ConvertToEntity<Ilceler>(entity));
            uow.SaveChanges();
        }

        public void Delete(IlcelerVM entity)
        {
            _IlcelerRepository.Delete(ProjectMapper.ConvertToEntity<Ilceler>(entity));
            uow.SaveChanges();
        }


    }
}
