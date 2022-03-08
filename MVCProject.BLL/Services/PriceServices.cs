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
    public class PriceServices : IRepository<PriceVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Price> _PriceRepository;

        public PriceServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _PriceRepository = new Repository<Price>(context);
        }


        public IEnumerable<PriceVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<PriceVM>>(_PriceRepository.GetAll());
            return (IEnumerable<PriceVM>)data;
        }


        public PriceVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<PriceVM>(_PriceRepository.GetById(id));

        }


        public void Insert(PriceVM entity)
        {
            _PriceRepository.Insert(ProjectMapper.ConvertToEntity<Price>(entity));
            uow.SaveChanges();

        }

        public void Update(PriceVM entity)
        {

            _PriceRepository.Update(ProjectMapper.ConvertToEntity<Price>(entity));
            uow.SaveChanges();
        }

        public void Delete(PriceVM entity)
        {
            _PriceRepository.Delete(context.Prices.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
