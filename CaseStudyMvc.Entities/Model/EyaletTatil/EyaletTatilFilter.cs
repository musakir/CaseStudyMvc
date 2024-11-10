using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudyMvc.Entities.Model.EyaletTatil
{
    public class EyaletTatilFilter
    {
        public int Id { get; set; }
        public DateTime? Tarih { get; set; }
        public int EyaletId { get; set; }
    }
}
