using BAL.Models;
using DAL.Models;
using DAL.Services;
using Mapster;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services.Implementation
{
    internal class RecordsBusinessService : IRecordsBusinessService
    {
        private readonly IRecordsDataService recordsServices;

        public RecordsBusinessService(IRecordsDataService recordsServices)
        {
            this.recordsServices = recordsServices;
        }

        public async Task<IList<RecordsBusiness>> GetRecordsAsync(string id)
        {
            var recordsDto = await this.recordsServices.GetRecords(id);
            var records = new List<RecordsBusiness>();
            foreach (var el in recordsDto)
            {
                var record = el.Adapt<RecordsBusiness>();
                records.Add(record);
            }
            return records;
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

        public async Task CreateRecord(RecordsBusiness records, string operation)
        {
            var recordsDto = records.Adapt<RecordsData>();
            recordsDto.RecordID = records.RecordID;
            switch (operation)
            {
                case "add":
                    await this.recordsServices.CreateRecord(recordsDto);
                    break;
                case "update":
                    await this.recordsServices.UpdateRecord(recordsDto);
                    break;
            }
        }
    }
}
