using MVCProject.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MVCProject.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ZuuCargoEntities _context;
        private bool disposed = false;
        public UnitOfWork(ZuuCargoEntities context)
        {
            Database.SetInitializer<ZuuCargoEntities>(null);
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<NotSistemEntities>());

            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        public int SaveChanges()
        {
            try
            {
                if (_context == null)
                    throw new ArgumentNullException("context");

                return _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = "";
                foreach (var validationError in dbEx.EntityValidationErrors)
                {

                    //TI control
                    msg += string.Format("Property:{0} Error:{1}", validationError.Entry.Property("propertyName"), validationError.Entry.Property("ErrorMessage"));

                    Trace.TraceInformation("Property:{0} Error:{1}", validationError.Entry.Property("propertyName"), validationError.Entry.Property("ErrorMessage"));
                    //TI control
                }
                throw new ArgumentException(msg);
            }
        }

        public Entities.ZuuCargoEntities GetContext()
        {
            return _context;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();


            }
            this.disposed = true;

        }
    }
}
