using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Arkanoid.Models
{
    public class Records
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [ScaffoldColumn(false)]
        public int UserID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "№ рекорда")]
        public int RecordID { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя игрока должно быть от 3 до 50 символов")]
        [Display(Name = "Имя игрока")]
        public string UserName { get; set; }
        [Required]
        [Display(Name = "Счет")]
        public int UserScore { get; set; }
    }
}
