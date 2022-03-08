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
    public class SemtServices : IRepository<SemtVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Semt> _SemtRepository;

        public SemtServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _SemtRepository = new Repository<Semt>(context);
        }


        public IEnumerable<SemtVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<SemtVM>>(_SemtRepository.GetAll());
            return (IEnumerable<SemtVM>)data;
        }

        public SemtVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<SemtVM>(_SemtRepository.GetById(id));

        }

        public void Insert(SemtVM entity)
        {
            _SemtRepository.Insert(ProjectMapper.ConvertToEntity<Semt>(entity));
            uow.SaveChanges();

        }

        public void Update(SemtVM entity)
        {
            _SemtRepository.Update(ProjectMapper.ConvertToEntity<Semt>(entity));
            uow.SaveChanges();
        }

        public void Delete(SemtVM entity)
        {
            _SemtRepository.Delete(ProjectMapper.ConvertToEntity<Semt>(entity));
            uow.SaveChanges();
        }


    }
}
