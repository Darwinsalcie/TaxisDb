﻿

namespace TaxisDb.Domain.Base
{
    public abstract class BaseEntity<TType>
    {
        public abstract TType Id { get; set; }

    }
}
