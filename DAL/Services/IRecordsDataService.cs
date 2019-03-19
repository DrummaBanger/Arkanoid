using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Services
{
    /// <summary>
    /// Интерфейс работы с рекордами
    /// </summary>
    public interface IRecordsDataService
    {
        /// <summary>
        /// Получает список рекордов
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        Task<IList<RecordsData>> GetRecords(string id);

        /// <summary>
        /// Создает рекорд в БД
        /// </summary>
        /// <param name="records">Рекорд</param>
        Task CreateRecord(RecordsData records);

        /// <summary>
        /// Получает рекорд по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        Task<RecordsData> GetDetails(int id);

        /// <summary>
        /// Изменяет рекорд в БД
        /// </summary>
        /// <param name="records">Рекорд</param>
        Task UpdateRecord(RecordsData records);

        /// <summary>
        /// Удаление рекорда из БД
        /// </summary>
        /// <param name="id">Идентификатор рекорда</param>
        Task DeleteAsync(int id);

    }
}
