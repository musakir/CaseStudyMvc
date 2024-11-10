using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyMvc.Entities.Model.Tatil
{
    public class TatilSave
    {
        public int Id { get; set; }
        public int TatilTurId { get; set; }
        public DateTime? Tarih { get; set; }
        public string Aciklama { get; set; }
        public int Aktif { get; set; }
    }
}
