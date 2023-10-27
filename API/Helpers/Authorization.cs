using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Helpers
{
    public class Authorization
    {
        public enum Rol{
            Administrator,
            Manager,
            Employee,
            Person
        }
        public const Rol rol_default = Rol.Person;
    }
}