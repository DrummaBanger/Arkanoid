using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Data
{
    public class RecordInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            if (context.Records.Any())
            {
                return; 
            }
        }
    }
}
