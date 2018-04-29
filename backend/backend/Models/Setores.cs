using backend.WebCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Models
{
    public class Setores : AppDataObject
    {
        [MaxLength(80)]
        public string Descricao { get; set; }
    }
}
