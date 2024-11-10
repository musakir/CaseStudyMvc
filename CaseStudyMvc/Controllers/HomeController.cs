using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CaseStudyMvc.Entities.Helper;
using CaseStudyMvc.Entities.Model.EyaletTatil;
using CaseStudyMvc.Entities.Model.Parametre;
using CaseStudyMvc.Entities.Model.Result;
using CaseStudyMvc.Entities.Model.Tatil;
using CaseStudyMvc.Model;
using Newtonsoft.Json;
using RawPrint;

namespace CaseStudyMvc.Controllers
{
    public class HomeController : MainController
    {
        public ActionResult Index(string id)
        {
            return RedirectToAction("EyaletTatilListe", "Home");
        }

        public async Task<ActionResult> EyaletTatilListe(string id)
        {
            string RequestFilter = "";

            EyaletTatilFilter filter = new EyaletTatilFilter();

            if (Request.QueryString["KriterEyaletId"] != null)
            {
                model.EyaletId = Request.QueryString["KriterEyaletId"].mkToInt();
                RequestFilter += "&EyaletId=" + model.EyaletId;
            }

            if (Request.QueryString["KriterUlke"] != null)
            {
                model.UlkeId = Request.QueryString["KriterUlke"].mkToInt();
                RequestFilter += "&UlkeId=" + model.UlkeId;
            }

            if (Request.QueryString["KriterYil"] != null)
            {
                model.Yil = Request.QueryString["KriterYil"].mkToInt();
                RequestFilter += "&Yil=" + model.Yil;
            }

            if (Request.QueryString["KriterYil"] != null)
            {
                model.Ay = Request.QueryString["KriterAy"].mkToInt();
                RequestFilter += "&Ay=" + model.Ay;
            }

            if (Request.QueryString["KriterTatilTur"] != null)
            {
                model.TatilTurId = Request.QueryString["KriterTatilTur"].mkToInt();
                RequestFilter += "&TatilTurId=" + model.TatilTurId;
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, model.ApiUrl + "/EyaletTatil/EyaletTatilList?1=1" + RequestFilter);
            var response = await client.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            ResultApi<List<EyaletTatilList>> ResultData = new ResultApi<List<EyaletTatilList>>();
            ResultData = JsonConvert.DeserializeObject<ResultApi<List<EyaletTatilList>>>(result);

            if (ResultData == null || ResultData.R == null || ResultData.OperationStatusCode != 200)
            {
                model.ErrorMessage.Add(ResultData.Message);
                return View("EyaletTatilListe", model);
            }

            var requestEyaletList = new HttpRequestMessage(HttpMethod.Get, model.ApiUrl + "/Parametre/EyaletList");
            var responseEyaletList = await client.SendAsync(requestEyaletList);

            string resultEyaletList = await responseEyaletList.Content.ReadAsStringAsync();

            ResultApi<List<EyaletList>> ResultDataEyaletList = new ResultApi<List<EyaletList>>();
            ResultDataEyaletList = JsonConvert.DeserializeObject<ResultApi<List<EyaletList>>>(resultEyaletList);

            var requestTatilTurList = new HttpRequestMessage(HttpMethod.Get, model.ApiUrl + "/Parametre/TatilTurList");
            var responseTatilTurList = await client.SendAsync(requestTatilTurList);

            string resultTatilTurList = await responseTatilTurList.Content.ReadAsStringAsync();

            ResultApi<List<TatilTurList>> ResultDataTatilTurList = new ResultApi<List<TatilTurList>>();
            ResultDataTatilTurList = JsonConvert.DeserializeObject<ResultApi<List<TatilTurList>>>(resultTatilTurList);

            model.TatilTurListe = ResultDataTatilTurList.R;
            model.EyaletListe = ResultDataEyaletList.R;
            model.EyaletTatilListe = ResultData.R;

            if (!string.IsNullOrWhiteSpace(Request.QueryString["ErrorMessage"]))
                model.ErrorMessage = JsonConvert.DeserializeObject<List<string>>(Request.QueryString["ErrorMessage"].mkToString());

            return View("EyaletTatilListe", model);
        }

        public async Task<ActionResult> TatilTanimla(string id)
        {
            model.AccessToken = CookieDegeri("AccessToken").mkToString();

            if (string.IsNullOrWhiteSpace(model.AccessToken))
                return RedirectToAction("Giris", "Login");

            string RequestFilter = "";
            if (Request.QueryString["KriterTatilId"] != null)
            {
                model.TatilId = Request.QueryString["KriterTatilId"].mkToInt();
                RequestFilter = "&Id=" + model.TatilId;
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, model.ApiUrl + "/Tatil/TatilList?1=1");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", model.AccessToken);
            var response = await client.SendAsync(request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Giris", "Login");

            string result = await response.Content.ReadAsStringAsync();

            ResultApi<List<TatilList>> ResultData = new ResultApi<List<TatilList>>();
            ResultData = JsonConvert.DeserializeObject<ResultApi<List<TatilList>>>(result);

            var requestData = new HttpRequestMessage(HttpMethod.Get, model.ApiUrl + "/Tatil/TatilList?1=1" + RequestFilter);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", model.AccessToken);
            var responseData = await client.SendAsync(requestData);

            if (responseData.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Giris", "Login");

            string resultData = await responseData.Content.ReadAsStringAsync();

            ResultApi<List<TatilList>> ResultDataData = new ResultApi<List<TatilList>>();
            ResultDataData = JsonConvert.DeserializeObject<ResultApi<List<TatilList>>>(resultData);

            var requestTatilTurList = new HttpRequestMessage(HttpMethod.Get, model.ApiUrl + "/Parametre/TatilTurList");
            var responseTatilTurList = await client.SendAsync(requestTatilTurList);

            string resultTatilTurList = await responseTatilTurList.Content.ReadAsStringAsync();

            ResultApi<List<TatilTurList>> ResultDataTatilTurList = new ResultApi<List<TatilTurList>>();
            ResultDataTatilTurList = JsonConvert.DeserializeObject<ResultApi<List<TatilTurList>>>(resultTatilTurList);

            var requestDataSecim = new HttpRequestMessage(HttpMethod.Get, model.ApiUrl + "/Tatil/EyaletTatilSecimList?1=1&TatilId=" + model.TatilId);
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", model.AccessToken);
            var responseDataSecim = await client.SendAsync(requestDataSecim);

            if (responseDataSecim.StatusCode == HttpStatusCode.Unauthorized)
                return RedirectToAction("Giris", "Login");

            string resultDataSecim = await responseDataSecim.Content.ReadAsStringAsync();

            ResultApi<List<EyaletTatilSecimList>> ResultDataDataSecim = new ResultApi<List<EyaletTatilSecimList>>();
            ResultDataDataSecim = JsonConvert.DeserializeObject<ResultApi<List<EyaletTatilSecimList>>>(resultDataSecim);

            model.TatilTurListe = ResultDataTatilTurList.R;
            model.TatilListe = ResultData.R;
            model.TatilData = ResultDataData.R;
            model.EyaletTatilSecimListe = ResultDataDataSecim.R;

            if (!string.IsNullOrWhiteSpace(Request.QueryString["ErrorMessage"]))
                model.ErrorMessage = JsonConvert.DeserializeObject<List<string>>(Request.QueryString["ErrorMessage"].mkToString());

            return View("TatilTanimla", model);
        }

        [HttpPost]
        public async Task<JsonResult> TatilSave(int TatilId, string Tarih, int TatilTur, string TatilAciklama, int Aktif)
        {
            model.AccessToken = CookieDegeri("AccessToken").mkToString();

            TatilSave save = new TatilSave();
            save.Id = TatilId;
            save.TatilTurId = TatilTur;
            save.Aciklama = TatilAciklama;
            save.Aktif = Aktif;

            if (!string.IsNullOrWhiteSpace(Tarih))
                save.Tarih = DateTime.Parse(Tarih);

            string body = JsonConvert.SerializeObject(save);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, model.ApiUrl + "/Tatil/TatilSave");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", model.AccessToken);
            var content = new StringContent(body, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            ResultApi<int> ResultData = new ResultApi<int>();
            ResultData = JsonConvert.DeserializeObject<ResultApi<int>>(result);

            string JSON = "";
            JSON = JsonConvert.SerializeObject(ResultData);

            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> EyaletTatilSave(int TatilId, int EyaletId, int EyaletTatilSecim)
        {
            model.AccessToken = CookieDegeri("AccessToken").mkToString();

            EyaletTatilSave save = new EyaletTatilSave();
            save.Id = 0;
            save.EyaletId = EyaletId;
            save.TatilId = TatilId;
            save.EyaletTatilSecim = EyaletTatilSecim;

            string body = JsonConvert.SerializeObject(save);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, model.ApiUrl + "/EyaletTatil/EyaletTatilSave");
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", model.AccessToken);
            var content = new StringContent(body, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);

            string result = await response.Content.ReadAsStringAsync();

            ResultApi<int> ResultData = new ResultApi<int>();
            ResultData = JsonConvert.DeserializeObject<ResultApi<int>>(result);

            string JSON = "";
            JSON = JsonConvert.SerializeObject(ResultData);

            return Json(JSON, JsonRequestBehavior.AllowGet);
        }

    }
}