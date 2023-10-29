using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Repository
{
    public interface IEntity<K>
    {
        public K Id {get; set; }
    }
}