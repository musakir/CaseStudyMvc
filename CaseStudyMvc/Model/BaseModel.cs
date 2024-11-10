using CaseStudyMvc.Entities.Dto;
using CaseStudyMvc.Entities.Model;
using CaseStudyMvc.Entities.Model.EyaletTatil;
using CaseStudyMvc.Entities.Model.Parametre;
using CaseStudyMvc.Entities.Model.Tatil;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CaseStudyMvc.Model
{
    public class BaseModel
    {
        public List<string> ErrorMessage = new List<string>();

        public int FISID = 0;
        public int BASLIK = 0;
        public string KRITER = "";
        public int USERID = 0;
        public string USERNAME = "";

        public List<EyaletTatilList> EyaletTatilListe;
        public List<EyaletList> EyaletListe;
        public List<TatilList> TatilListe;
        public List<TatilList> TatilData;
        public List<TatilTurList> TatilTurListe;
        public List<EyaletTatilSecimList> EyaletTatilSecimListe;

        public int EyaletId = 0, UlkeId = 0, TatilId = 0, Yil = 0, Ay = 0, TatilTurId = 0;
        public string ApiUrl = "http://localhost:8223/api";
        public string AccessToken = "";
    }
}