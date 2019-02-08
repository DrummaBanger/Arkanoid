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

            // Look for any students.
            if (context.Records.Any())
            {
                return;   // DB has been seeded
            }

            var record = new Records[]
            {
                new Records { UserID = 1, RecordID = 1, UserName = "test", UserScore = 100 },
                new Records { UserID = 2, RecordID = 2, UserName = "test1", UserScore = 50 },
            };
            foreach (Records s in record)
            {
                context.Records.Add(s);
            }
            context.SaveChanges();

        }
    }
}
