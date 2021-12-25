using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EvrakYonetimSistemi.Models
{
    public class EvrakTipi
    {
        [Key]
        public int TipID { get; set; }
        public string TipAdi { get; set; }

        public IList<Evrak> Evraks { get; set; }
    }
}
