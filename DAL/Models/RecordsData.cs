using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Models
{
    /// <summary>
    /// Рекорд
    /// </summary>
    public class RecordsData
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public string UserID { get; set; }
        /// <summary>
        /// Идентификатор номера рекорда
        /// </summary>
        public int RecordID { get; set; }
        /// <summary>
        /// Имя игрока
        /// </summary>
        [Display(Name = "Имя игрока")]
        public string UserName { get; set; }
        /// <summary>
        /// Счет игрока
        /// </summary>
        public int UserScore { get; set; }
    }
}