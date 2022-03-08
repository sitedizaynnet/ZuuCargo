using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCProject.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        int SaveChanges();
        ZuuCargoEntities GetContext();
    }
}
