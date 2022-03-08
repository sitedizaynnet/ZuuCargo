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
    public class OrderServices : IRepository<OrderVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<Order> _OrderRepository;

        public OrderServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _OrderRepository = new Repository<Order>(context);
        }


        public IEnumerable<OrderVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<OrderVM>>(_OrderRepository.GetAll());
            return (IEnumerable<OrderVM>)data;
        }
       
       

        public OrderVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<OrderVM>(_OrderRepository.GetById(id));

        }


        public void Insert(OrderVM entity)
        {
            _OrderRepository.Insert(ProjectMapper.ConvertToEntity<Order>(entity));
            uow.SaveChanges();

        }

        public void Update(OrderVM entity)
        {

            _OrderRepository.Update(ProjectMapper.ConvertToEntity<Order>(entity));
            uow.SaveChanges();
        }

        public void Delete(OrderVM entity)
        {
            _OrderRepository.Delete(context.Order.Find(entity.Id));
            uow.SaveChanges();
        }
        public  List<OrderVM> GetAllByUserId(string userId)
        {

            var data = ProjectMapper.ConvertToVMList<IEnumerable<MessageVM>>(_OrderRepository.GetAll().Where(x => x.UsersId == userId));

            return (List<OrderVM>)data;
        }


    }
}
