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
    public class ZuuCargoServices : IRepository<ZuuCargoVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<ZuuCargo> _ZuuCargoRepository;

        public ZuuCargoServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ZuuCargoRepository = new Repository<ZuuCargo>(context);
        }


        public IEnumerable<ZuuCargoVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ZuuCargoVM>>(_ZuuCargoRepository.GetAll());
            return (IEnumerable<ZuuCargoVM>)data;
        }
       
        public ZuuCargoVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ZuuCargoVM>(_ZuuCargoRepository.GetById(id));

        }


        public void Insert(ZuuCargoVM entity)
        {
            _ZuuCargoRepository.Insert(ProjectMapper.ConvertToEntity<ZuuCargo>(entity));
            uow.SaveChanges();

        }

        public void Update(ZuuCargoVM entity)
        {

            _ZuuCargoRepository.Update(ProjectMapper.ConvertToEntity<ZuuCargo>(entity));
            uow.SaveChanges();
        }

        public void Delete(ZuuCargoVM entity)
        {
            _ZuuCargoRepository.Delete(context.ZuuCargo.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
