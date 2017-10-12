using Persistence.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new DFContext();
            context.SaveChanges();
        }
    }
}
