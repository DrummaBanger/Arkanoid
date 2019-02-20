using Arkanoid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkanoid.Data
{
    public class RecordInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // context.Database.EnsureCreated();

            if (context.Records.Any())
            {
                return;   // DB has been seeded
            }

           
        }
    }
}
