using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System.Collections.Generic;

namespace MVCProject.BLL.Services
{
    public class ShipmentTurpexBarcodesServices : IRepository<ShipmentTurpexBarcodesVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<ShipmentTurpexBarcodes> _ExchangeRepository;

        public ShipmentTurpexBarcodesServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ExchangeRepository = new Repository<ShipmentTurpexBarcodes>(context);
        }


        public IEnumerable<ShipmentTurpexBarcodesVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ShipmentTurpexBarcodesVM>>(_ExchangeRepository.GetAll());
            return (IEnumerable<ShipmentTurpexBarcodesVM>)data;
        }



        public ShipmentTurpexBarcodesVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ShipmentTurpexBarcodesVM>(_ExchangeRepository.GetById(id));

        }

        public void Insert(ShipmentTurpexBarcodesVM entity)
        {
            _ExchangeRepository.Insert(ProjectMapper.ConvertToEntity<ShipmentTurpexBarcodes>(entity));
            uow.SaveChanges();

        }

        public void Update(ShipmentTurpexBarcodesVM entity)
        {
            _ExchangeRepository.Update(ProjectMapper.ConvertToEntity<ShipmentTurpexBarcodes>(entity));
            uow.SaveChanges();
        }

        public void Delete(ShipmentTurpexBarcodesVM entity)
        {
            _ExchangeRepository.Delete(context.ShipmentTurpexBarcodes.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
