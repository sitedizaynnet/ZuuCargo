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
    public class ArrivedMoneyServices : IRepository<ArrivedMoneyVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<ArrivedMoney> _ArrivedMoneyRepository;

        public ArrivedMoneyServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _ArrivedMoneyRepository = new Repository<ArrivedMoney>(context);
        }


        public IEnumerable<ArrivedMoneyVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<ArrivedMoneyVM>>(_ArrivedMoneyRepository.GetAll());
            return (IEnumerable<ArrivedMoneyVM>)data;
        }



        public ArrivedMoneyVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<ArrivedMoneyVM>(_ArrivedMoneyRepository.GetById(id));

        }

        public void Insert(ArrivedMoneyVM entity)
        {
            _ArrivedMoneyRepository.Insert(ProjectMapper.ConvertToEntity<ArrivedMoney>(entity));
            uow.SaveChanges();

        }

        public void Update(ArrivedMoneyVM entity)
        {
            _ArrivedMoneyRepository.Update(ProjectMapper.ConvertToEntity<ArrivedMoney>(entity));
            uow.SaveChanges();
        }

        public void Delete(ArrivedMoneyVM entity)
        {
            _ArrivedMoneyRepository.Delete(context.ArrivedMoney.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
