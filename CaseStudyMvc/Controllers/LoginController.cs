using CaseStudyMvc.Entities.Dto;
using CaseStudyMvc.Entities.Helper;
using CaseStudyMvc.Entities.Model.Result;
using CaseStudyMvc.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CaseStudyMvc.Controllers
{
    public class LoginController : MainController
    {
        public ActionResult Giris()
        {
            model.BASLIK = 1;

            if (!string.IsNullOrWhiteSpace(Request.QueryString["ErrorMessage"]))
                model.ErrorMessage = JsonConvert.DeserializeObject<List<string>>(Request.QueryString["ErrorMessage"].mkToString());

            return View("Giris", model);
        }

        public async Task<ActionResult> GirisYap(string tKullaniciAdi, string tSifre)
        {
            IUserLogin login = new IUserLogin();
            login.UserName = tKullaniciAdi;
            login.Password = tSifre;

            string Data = JsonConvert.SerializeObject(login);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, model.ApiUrl + "/User/Login");
            var content = new StringContent(Data, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            ResultApi<IToken> ResultData = new ResultApi<IToken>();
            ResultData = JsonConvert.DeserializeObject<ResultApi<IToken>>(result);

            if (ResultData == null || ResultData.R == null || ResultData.OperationStatusCode != 200)
            {
                model.ErrorMessage.Add(ResultData.Message);
                return RedirectToAction("Giris", "Login", new { ErrorMessage = JsonConvert.SerializeObject(model.ErrorMessage) });
            }

            CookieOlustur("AccessToken", ResultData.R.AccessToken, 0);
            CookieOlustur("RefreshToken", ResultData.R.RefreshToken, 0);

            return RedirectToAction("TatilTanimla", "Home");
        }

    }
}