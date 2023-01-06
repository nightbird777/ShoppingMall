using Microsoft.AspNetCore.Mvc;
using ShoppingMall.Models;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ShoppingMall.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public HomeController(IWebHostEnvironment hostEnvironment)
        {
            this._hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult EmployeeList()
        {

            EmployeeDB employeeDB = new EmployeeDB();
            List<Employee> employeeList = employeeDB.allEmployee();
            
            ViewBag.Employee = employeeList;

            return View();
        }


        public IActionResult editEmployee(int id)
        {

            EmployeeDB employeeDB = new EmployeeDB();
            Employee employee = employeeDB.editEmployeeById(id);
            ViewBag.Employee = employee;
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> saveEmployee(Employee employee)
        {
            List<string> errorMessages = new List<string>();
            EmployeeDB employeeDB = new EmployeeDB();
            DateTime test = new DateTime(0001, 1, 1, 00, 00, 00);
            employee.Image = Request.Form["Image"];


            if (employee.PhotoFile == null && employee.Image == null)
                errorMessages.Add("Photo is required");
            if (employee.Name == null)
                errorMessages.Add("Name is required");
            if (employee.Phone == null)
                errorMessages.Add("Phone Number is required");
            if (employee.Email == null)
                errorMessages.Add("Email is required");
            if (employee.Image == null)
                errorMessages.Add("Image is required");

            if (errorMessages.Count > 0)
            {
                TempData["errorMessage"] = errorMessages;
                //here we are redirecting to the above action and pass the mistaken object to send it back to the HTML page to be edited
                return RedirectToAction("EmployeeList");
            }

            if (employee.PhotoFile != null)
            {
                //Delete the old photo from teh root folder when it is updated
                if ((employee.PhotoFile.FileName != employee.Image) && (employee.Id != 0))
                {
                    string wwwRootPath1 = _hostEnvironment.WebRootPath;
                    string path1 = Path.Combine(wwwRootPath1 + "/UploadedPhoto/", employee.Image);
                    System.IO.File.Delete(path1);
                }
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(employee.PhotoFile.FileName);
                string extension = Path.GetExtension(employee.PhotoFile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfffffff") + extension;
                employee.Image = fileName;
                string path = Path.Combine(wwwRootPath + "/UploadedPhoto/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await employee.PhotoFile.CopyToAsync(fileStream);
                }                
            }

            employeeDB.saveEmployee(employee);

            return RedirectToAction("EmployeeList");


            //employeeDB.saveEmployee(employee);
            //List<Employee> employeeList = employeeDB.allEmployee();
            //ViewBag.Employee = employeeList;
            //return View("EmployeeList");
        }

        public IActionResult NewEmployee()
        {
            return View();
        }

        public IActionResult createEmployee(Employee employee)
        {
            EmployeeDB employeeDB = new EmployeeDB();
            employeeDB.createEmployee(employee);
            List<Employee> employeeList = employeeDB.allEmployee();
            ViewBag.Employee = employeeList;
            return View("EmployeeList");
        }

        public IActionResult deleteEmployee(int id)
        {

            EmployeeDB employeeDB = new EmployeeDB();
            Employee employee = employeeDB.deleteEmployee(id);
            List<Employee> employeeList = employeeDB.allEmployee();
            ViewBag.Employee = employeeList;
            return View("EmployeeList");
        }


        public IActionResult searchEmployee(string search)
        {

            //string search = Request.Form["Name"];
            EmployeeDB employeeDB = new EmployeeDB();
            List<Employee> employeeList = employeeDB.searchEmployee(search);
            ViewBag.Employee = employeeList;
            return View("EmployeeList");
        }


        private IActionResult HiddenConn()
        {

            string connectionString = "server=nyctotampa; initial catalog=personal; user id=raju; password=raju123";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();
            return View();
        }




        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }






    }
}