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
        [Display(Name = "Имя игрока")]
        public string UserName { get; set; }
        [Display(Name = "Счет")]
        public int UserScore { get; set; }
    }
}
