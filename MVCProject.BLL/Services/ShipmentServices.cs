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
    public class ShipmentServices : IRepository<ShipmentVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Shipment> _shipmentRepository;

        public ShipmentServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _shipmentRepository = new Repository<Shipment>(context);
        }


        public IEnumerable<ShipmentVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ShipmentVM>>(_shipmentRepository.GetAll());
            return (IEnumerable<ShipmentVM>)data;
        }
      

        public ShipmentVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ShipmentVM>(_shipmentRepository.GetById(id));

        }


        public void Insert(ShipmentVM entity)
        {
            _shipmentRepository.Insert(ProjectMapper.ConvertToEntity<Shipment>(entity));
            uow.SaveChanges();

        }

        public void Update(ShipmentVM entity)
        {

            _shipmentRepository.Update(ProjectMapper.ConvertToEntity<Shipment>(entity));
            uow.SaveChanges();
        }

        public void Delete(ShipmentVM entity)
        {
            _shipmentRepository.Delete(context.Shipments.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
