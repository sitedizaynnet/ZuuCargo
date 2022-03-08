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
    public class BoxsVarService : IRepository<BoxsVarVM>
    {
        UnitOfWork uow;
        ZuuCargoEntities context;
        Repository<BoxsVar> _BoxsVarRepository;

        public BoxsVarService()
        {
            context = new ZuuCargoEntities();
            uow = new UnitOfWork(context);
            _BoxsVarRepository = new Repository<BoxsVar>(context);
        }


        public IEnumerable<BoxsVarVM> GetAll()
        {
            var data = ProjectMapper.ConvertToVMList<IEnumerable<BoxsVarVM>>(_BoxsVarRepository.GetAll());
            return (IEnumerable<BoxsVarVM>)data;
        }
       
        public BoxsVarVM GetById(int id)
        {
            return ProjectMapper.ConvertToVM<BoxsVarVM>(_BoxsVarRepository.GetById(id));

        }


        public void Insert(BoxsVarVM entity)
        {
            _BoxsVarRepository.Insert(ProjectMapper.ConvertToEntity<BoxsVar>(entity));
            uow.SaveChanges();

        }

        public void Update(BoxsVarVM entity)
        {

            _BoxsVarRepository.Update(ProjectMapper.ConvertToEntity<BoxsVar>(entity));
            uow.SaveChanges();
        }

        public void Delete(BoxsVarVM entity)
        {
            _BoxsVarRepository.Delete(context.BoxsVar.Find(entity.Id));
            uow.SaveChanges();
        }


    }
}
