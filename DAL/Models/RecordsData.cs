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
        [ScaffoldColumn(false)]
        public string UserID { get; set; }
        /// <summary>
        /// Идентификатор номера рекорда
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "№ рекорда")]
        public int RecordID { get; set; }
        /// <summary>
        /// Имя игрока
        /// </summary>
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя игрока должно быть от 3 до 50 символов")]
        [Display(Name = "Имя игрока")]
        public string UserName { get; set; }
        /// <summary>
        /// Счет игрока
        /// </summary>
        [Required]
        [Display(Name = "Счет")]
        public int UserScore { get; set; }
    }
}