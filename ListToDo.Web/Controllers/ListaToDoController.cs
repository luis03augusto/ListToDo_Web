using ListToDo.Web.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ListToDo.Web.Controllers
{
    public class ListaToDoController : Controller
    {
        ListaDeItemViewModel _model = new ListaDeItemViewModel();
        string BaseUrl = "http://localhost:63078/";
        // GET: ListaToDo

        public async Task<ActionResult> Index()
        {
            List<Models.ListToDo> listToDos = new List<Models.ListToDo>();

            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = await client.GetAsync("api/ListToDo");

                if (Res.IsSuccessStatusCode)
                {
                    var ListItens = Res.Content.ReadAsStringAsync().Result;
                    listToDos = JsonConvert.DeserializeObject<List<Models.ListToDo>>(ListItens, settings);
                    _model.ListToDo_Pendetens = listToDos.Where(x => x.Status == "Pendente").ToList();
                    _model.ListToDo_Concluidas = listToDos.Where(x => x.Status == "Concluido").ToList();
                }
            }

            return View(_model);
        }
        public ActionResult Finalizar(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res = client.GetAsync("api/Finalizar/" + id).Result;
                return RedirectToAction("Index");
            }
        }
        public ActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Form(Models.ListToDo listToDo)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res =  client.PostAsJsonAsync("api/ListToDo",listToDo).Result;
                return Json(Res.StatusCode);
            }            
        }
        public ActionResult FindOne(int id)
        {
            Models.ListToDo listToDo = new Models.ListToDo();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(BaseUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage Res =  client.GetAsync("api/ListToDo/"+ id).Result;

                if (Res.IsSuccessStatusCode)
                {
                    return View(Res.Content.ReadAsAsync<Models.ListToDo>().Result);
                }
                return View("Index");
            }
        }

        public ActionResult Update(Models.ListToDo listToDo)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage Res = client.PutAsJsonAsync("api/ListToDo/"+listToDo.Id, listToDo).Result;
                }
                return View();
            }
            catch (Exception)
            {

                return View();
            }           
        }

        public ActionResult Delete(int id)
        {
            try
            {
                Models.ListToDo listaToDo = new Models.ListToDo();
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(BaseUrl);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("Application/json"));
                    HttpResponseMessage Res = client.DeleteAsync("api/ListToDo" + id).Result;
                }
                return View("Index");
            }
            catch (Exception)
            {

                return View("Error");
            }
        }
    }
}