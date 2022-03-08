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
    public class CargosService : IRepository<CargosVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Cargos> _CargosRepository;

        public CargosService()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _CargosRepository = new Repository<Cargos>(context);
        }


        public IEnumerable<CargosVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<CargosVM>>(_CargosRepository.GetAll());
            return (IEnumerable<CargosVM>)data;
        }
       
        public CargosVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<CargosVM>(_CargosRepository.GetById(id));

        }


        public void Insert(CargosVM entity)
        {
            _CargosRepository.Insert(ProjectMapper.ConvertToEntity<Cargos>(entity));
            uow.SaveChanges();

        }

        public void Update(CargosVM entity)
        {

            _CargosRepository.Update(ProjectMapper.ConvertToEntity<Cargos>(entity));
            uow.SaveChanges();
        }

        public void Delete(CargosVM entity)
        {
            _CargosRepository.Delete(context.Cargos.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
