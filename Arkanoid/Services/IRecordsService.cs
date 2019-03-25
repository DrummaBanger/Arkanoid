using Arkanoid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Arkanoid.Services
{
    /// <summary>
    /// Интерфейс работы с рекордами
    /// </summary>
    public interface IRecordsService
    {
        /// <summary>
        /// Удаление рекорда из БД
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        Task DeleteRecordsAsync(int id);

        /// <summary>
        /// Получает список рекордов
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        Task<IEnumerable<Records>> GetRecords(string id);

        /// <summary>
        /// Получает рекорд по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        Task<Records> GetDetails(int id);

        /// <summary>
        /// Создает рекорд в БД
        /// </summary>
        /// <param name="records">Рекорд</param>
        /// <param name="operation">Операция (создание/изменение)</param>
        Task<int> CreateRecord(Records records, string operation);
    }
}
