using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System.Collections.Generic;

namespace MVCProject.BLL.Services
{
    public class ShipmentBarcodesServices : IRepository<ShipmentBarcodesVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<ShipmentBarcodes> _ExchangeRepository;

        public ShipmentBarcodesServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ExchangeRepository = new Repository<ShipmentBarcodes>(context);
        }


        public IEnumerable<ShipmentBarcodesVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ShipmentBarcodesVM>>(_ExchangeRepository.GetAll());
            return (IEnumerable<ShipmentBarcodesVM>)data;
        }



        public ShipmentBarcodesVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ShipmentBarcodesVM>(_ExchangeRepository.GetById(id));

        }

        public void Insert(ShipmentBarcodesVM entity)
        {
            _ExchangeRepository.Insert(ProjectMapper.ConvertToEntity<ShipmentBarcodes>(entity));
            uow.SaveChanges();

        }

        public void Update(ShipmentBarcodesVM entity)
        {
            _ExchangeRepository.Update(ProjectMapper.ConvertToEntity<ShipmentBarcodes>(entity));
            uow.SaveChanges();
        }

        public void Delete(ShipmentBarcodesVM entity)
        {
            _ExchangeRepository.Delete(context.ShipmentBarcodes.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
