using Sakila.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila
{
    public abstract class EntityBase
    {
        public bool IsDeleted { get; set; }
    }
}
