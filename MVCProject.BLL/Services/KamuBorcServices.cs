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
    public class KamuBorcServices : IRepository<KamuBorcVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<KamuBorc> _KamuBorcRepository;

        public KamuBorcServices()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _KamuBorcRepository = new Repository<KamuBorc>(context);
        }


        public IEnumerable<KamuBorcVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<KamuBorcVM>>(_KamuBorcRepository.GetAll());
            return (IEnumerable<KamuBorcVM>)data;
        }
       
        public KamuBorcVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<KamuBorcVM>(_KamuBorcRepository.GetById(id));

        }


        public void Insert(KamuBorcVM entity)
        {
            _KamuBorcRepository.Insert(ProjectMapper.ConvertToEntity<KamuBorc>(entity));
            uow.SaveChanges();

        }

        public void Update(KamuBorcVM entity)
        {

            _KamuBorcRepository.Update(ProjectMapper.ConvertToEntity<KamuBorc>(entity));
            uow.SaveChanges();
        }

        public void Delete(KamuBorcVM entity)
        {
            _KamuBorcRepository.Delete(context.KamuBorc.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
