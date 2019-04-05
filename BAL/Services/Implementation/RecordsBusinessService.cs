using BAL.Models;
using DAL.Models;
using DAL.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implementation
{
    public class RecordsBusinessService : IRecordsBusinessService
    {
        private readonly IRecordsDataService recordsServices;

        public RecordsBusinessService(IRecordsDataService recordsServices)
        {
            this.recordsServices = recordsServices;
        }

        public async Task<IList<RecordsBusiness>> GetRecordsAsync(string id)
        {
            var recordsDto = await this.recordsServices.GetRecords(id);
            return recordsDto.Select(el => (el.Adapt<RecordsBusiness>())).ToList();
        }

        public Task DeleteRecordsAsync(int id)
        {
            return this.recordsServices.DeleteAsync(id);
        }

        public async Task<RecordsBusiness> GetDetails(int id)
        {
            var recordsDto = await this.recordsServices.GetDetails(id);
            return recordsDto.Adapt<RecordsBusiness>();
        }

        public async Task CreateRecord(RecordsBusiness records)
        {
            var recordsDto = records.Adapt<RecordsData>();
            recordsDto.RecordID = records.RecordID;
            await this.recordsServices.CreateRecord(recordsDto);
        }

        public async Task UpdateRecord(RecordsBusiness records)
        {
            var recordsDto = records.Adapt<RecordsData>();
            recordsDto.RecordID = records.RecordID;
            await this.recordsServices.UpdateRecord(recordsDto);
        }
    }
}
