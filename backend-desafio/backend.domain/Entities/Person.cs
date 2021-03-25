using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.domain.Entities
{
    public class Person
    {
        public int Code { get; set; }
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Uf { get; set; }
        public DateTime BirthDate { get; set; }

        public bool isValid()
        {
            return Cpf.Length == 14 && Uf != null && Name.Length >= 3; 
        }
    }
}
