using Book_Swap_Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Book_Swap_UI_Design.Controllers
{
    public class UserController : Controller
    {
        private readonly HttpClient client;
        private readonly string apiUrl;
        private List<User> userList1;
        private User userDetails;

        public UserController()
        {
            client = new HttpClient();
            apiUrl = "https://localhost:7177/api/User";
            userList1 = new List<User>();
            userDetails = new User();
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userlist = client.GetAsync(apiUrl + string.Format("/UserList")).Result;
                    if (userlist.IsSuccessStatusCode)
                    {
                        string apiResponse = await userlist.Content.ReadAsStringAsync();
                        userList1 = JsonConvert.DeserializeObject<List<User>>(apiResponse)!;
                        return View(userList1);
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddUser(User userdetails)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var getEmployee = client.PostAsJsonAsync(apiUrl + "api/AddUser", userdetails).Result;
                    if (getEmployee.IsSuccessStatusCode)
                    {
                        return View();
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                return View(ex.Message);
            }
        }
        public async Task<ActionResult> Edit(int id = 0)
        {
            var bookList = client.GetAsync(apiUrl + string.Format("/GetUserDetails?user=" + id)).Result;
            if (bookList.IsSuccessStatusCode)
            {
                string apiResponse = await bookList.Content.ReadAsStringAsync();
                userDetails = JsonConvert.DeserializeObject<User>(apiResponse);
                return View(userDetails);
            }
            return View(userDetails);
        }
        [HttpPost]
        public ActionResult Edit(User userdetails)
        {
            var getEmployee = client.PutAsJsonAsync(apiUrl + "/UpdateUser", userdetails).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Details(int id = 0)
        {
            var userdetailView = client.PostAsJsonAsync(apiUrl + "/GetUserDetails", id).Result;
            if (userdetailView.IsSuccessStatusCode)
            {
                string apiResponse = await userdetailView.Content.ReadAsStringAsync();
                userDetails = JsonConvert.DeserializeObject<User>(apiResponse);
                return View(userDetails);
            }
            return View(userDetails);
        }
        public async Task<ActionResult> Delete(int id = 0)
        {
            var userList = client.GetAsync(apiUrl + string.Format("/DeleteUser" + id)).Result;
            if (userList.IsSuccessStatusCode)
            {
                string apiResponse = await userList.Content.ReadAsStringAsync();
                userDetails = JsonConvert.DeserializeObject<User>(apiResponse);
                return View(userDetails);
            }
            return View(userDetails);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var getEmployee = client.PostAsJsonAsync(apiUrl + "/DeleteUser", id).Result;
            if (getEmployee.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
