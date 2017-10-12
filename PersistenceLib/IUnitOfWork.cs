using PersistenceLib.Domain;
using System;
using System.Collections.Generic;

namespace Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Dictionary<DomainType, IRepository<DomainEntity>> Repositories { get; }
        
        void AddRepository(IRepository<DomainEntity> repository);
        void CreateRepository(DomainEntity repositoryType);
        void RemoveRepository(DomainType repositoryType);
        void Save();
    }

}
