﻿using BAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public interface IRecordsBusinessService
    {
        /// <summary>
        /// Получает список рекордов
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        Task<IList<RecordsBusiness>> GetRecordsAsync(string id);

        /// <summary>
        /// Создает рекорд в БД
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        /// <param name="operation">Операция (создание/изменение)</param>
        Task CreateRecord(RecordsBusiness records, string operation);

        /// <summary>
        /// Получает рекорд по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        Task<RecordsBusiness> GetDetails(int id);

        /// <summary>
        /// Удаление рекорда из БД
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        Task DeleteRecordsAsync(int id);
    }
}