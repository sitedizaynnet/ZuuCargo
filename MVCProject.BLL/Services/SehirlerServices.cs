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
    public class SehirlerServices : IRepository<SehirlerVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Sehirler> _SehirlerRepository;

        public SehirlerServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _SehirlerRepository = new Repository<Sehirler>(context);
        }


        public IEnumerable<SehirlerVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<SehirlerVM>>(_SehirlerRepository.GetAll());
            return (IEnumerable<SehirlerVM>)data;
        }

        public SehirlerVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<SehirlerVM>(_SehirlerRepository.GetById(id));

        }

        public void Insert(SehirlerVM entity)
        {
            _SehirlerRepository.Insert(ProjectMapper.ConvertToEntity<Sehirler>(entity));
            uow.SaveChanges();

        }

        public void Update(SehirlerVM entity)
        {
            _SehirlerRepository.Update(ProjectMapper.ConvertToEntity<Sehirler>(entity));
            uow.SaveChanges();
        }

        public void Delete(SehirlerVM entity)
        {
            _SehirlerRepository.Delete(ProjectMapper.ConvertToEntity<Sehirler>(entity));
            uow.SaveChanges();
        }


    }
}
