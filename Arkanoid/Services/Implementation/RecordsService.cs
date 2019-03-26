using Arkanoid.Models;
using BAL.Models;
using BAL.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkanoid.Services.Implementation
{
    internal class RecordsService: IRecordsService
    {
        private readonly IRecordsBusinessService recordsData;

        public RecordsService(IRecordsBusinessService recordsData)
        {
            this.recordsData = recordsData;
        }

        public Task DeleteRecordsAsync(int id)
        {
            return this.recordsData.DeleteRecordsAsync(id);
        }

        public async Task<IEnumerable<Records>> GetRecords(string id)
        {
            return (await this.recordsData.GetRecordsAsync(id)).Adapt<IEnumerable<Records>>();
        }

        public async Task<Records> GetDetails(int id)
        {
            return (await recordsData.GetDetails(id)).Adapt<Records>();
        }

        public async Task<int> CreateRecord(Records records)
        {
            var baseRecords = records.Adapt<RecordsBusiness>();
            await this.recordsData.CreateRecord(baseRecords);
            return baseRecords.RecordID;
        }

        public async Task<int> UpdateRecord(Records records)
        {
            var baseRecords = records.Adapt<RecordsBusiness>();
            await this.recordsData.UpdateRecord(baseRecords);
            return baseRecords.RecordID;
        }
    }
}
