using MVCProject.Common.Mappers;
using MVCProject.Common.ViewModels;
using MVCProject.DAL.Repository;
using MVCProject.DAL.UnitOfWork;
using MVCProject.Entities;
using System.Collections.Generic;

namespace MVCProject.BLL.Services
{
    public class MyBalanceServices : IRepository<MyBalanceVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<MyBalance> _MyBalanceRepository;

        public MyBalanceServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _MyBalanceRepository = new Repository<MyBalance>(context);
        }


        public IEnumerable<MyBalanceVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<MyBalanceVM>>(_MyBalanceRepository.GetAll());
            return (IEnumerable<MyBalanceVM>)data;
        }



        public MyBalanceVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<MyBalanceVM>(_MyBalanceRepository.GetById(id));

        }

        public void Insert(MyBalanceVM entity)
        {
            _MyBalanceRepository.Insert(ProjectMapper.ConvertToEntity<MyBalance>(entity));
            uow.SaveChanges();

        }

        public void Update(MyBalanceVM entity)
        {
            _MyBalanceRepository.Update(ProjectMapper.ConvertToEntity<MyBalance>(entity));
            uow.SaveChanges();
        }

        public void Delete(MyBalanceVM entity)
        {
            _MyBalanceRepository.Delete(context.MyBalance.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
