using System;

namespace Persistence.Domain
{
    public class DomainType
    {
        private Type type;
        public DomainType(DomainEntity entity)
        {
            type = entity.GetType();
        }

        public DomainType(Type type)
        {
            this.type = type;
        }

        public override bool Equals(object obj)
        {
            if(!(obj is DomainType))
            {
                return false;
            }
            DomainType o = (DomainType)obj;
            return type.Equals(o.type);
        }
        public override int GetHashCode()
        {
            return type.GetHashCode();
        }
    }
}