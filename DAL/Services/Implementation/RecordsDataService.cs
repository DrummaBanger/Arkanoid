using DAL.Data;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Services.Implementation
{
    public class RecordsDataService: IRecordsDataService
    {
        private ApplicationDbContext db;
        public RecordsDataService(ApplicationDbContext context)
        {
            db = context;
        }

        public async Task<IList<RecordsData>> GetRecords(string id)
        {
            return await db.Records.ToListAsync();
        }

        public async Task<RecordsData> GetDetails(int id)
        {
            var records = db.Records
                .FirstOrDefaultAsync(m => m.RecordID == id);

            return await records;
        }

        public async Task CreateRecord(RecordsData records)
        {
            this.db.Records.Add(records);
            await this.db.SaveChangesAsync();
        }

        public async Task UpdateRecord(RecordsData records)
        {
            this.db.Records.Update(records);
            await this.db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var records = await db.Records.FindAsync(id);
            db.Records.Remove(records);
            await db.SaveChangesAsync();
        }

        private bool RecordsExists(int id)
        {
            return db.Records.Any(e => e.RecordID == id);
        }
    }
}
