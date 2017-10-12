using Persistence.Domain;
using System;
using System.Collections.Generic;

namespace Persistence
{
    public class DFUnitOfWork 
    {
        private DFContext context;
        public DFRepository<Offer> Offers { get; }

        private bool disposed = false;

        public DFUnitOfWork()
        {
            context = new DFContext();
            Offers = new DFRepository<Offer>(context);
        }

        public DFUnitOfWork(DFContext dfContext)
        {
            context = dfContext;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
