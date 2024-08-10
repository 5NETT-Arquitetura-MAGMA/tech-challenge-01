using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegionalContactsAPI.Core.Entity
{
    public class Cidade : EntityBase
    {
        public string NomeCidade { get; set; }
        public string Estado { get; set; }
        public string DDD { get; set; }
    }
}
