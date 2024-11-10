using CaseStudyMvc.Entities.Helper;
using CaseStudyMvc.Model;
using Dapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CaseStudyMvc.Controllers
{
    public class MainController : Controller
    {
        public BaseModel model = new BaseModel();

        public void CookieOlustur(string COOKNAME, string VALUE, int sil = 0)
        {
            if (Request.Cookies[COOKNAME] == null)
            {
                HttpCookie myCookie = new HttpCookie(COOKNAME, VALUE.ToString());
                myCookie.Expires = DateTime.Now.AddDays(1);
                HttpContext.Response.Cookies.Add(myCookie);
            }
            else
            {
                HttpCookie myCookie = Request.Cookies[COOKNAME];
                myCookie.Value = VALUE.ToString();
                Response.AppendCookie(myCookie);
            }

            if (sil > 0)
            {
                HttpCookie myCookie = new HttpCookie(COOKNAME);
                myCookie.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(myCookie);
                return;
            }
        }

        public string CookieDegeri(string CookieAdi)
        {
            if (Request.Cookies[CookieAdi] == null)
                return "";

            try
            {
                return Uri.UnescapeDataString(Request.Cookies[CookieAdi].Value).Trim();
            }
            catch (Exception ex)
            {
                MkHelper.LogYaz("Hat Cookie Değeri : " + ex.ToString());
            }

            return "";
        }

    }
}