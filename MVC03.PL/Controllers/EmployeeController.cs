using Microsoft.AspNetCore.Mvc;
using MVC03.BLL.Interfaces;
using MVC03.DAL.Models;
using MVC03.PL.Dtos;

namespace MVC03.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepo;
        public EmployeeController(IEmployeeRepository employeeRepository)
        {
            _employeeRepo = employeeRepository;
        }
        public IActionResult Index()
        {
            var employees = _employeeRepo.GetAll();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employee()
                {
                    Name = model.Name,
                    Salary = model.Salary,
                    Address = model.Address,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Age = model.Age,

                    HiringDate = model.HiringDate,
                    Phone = model.Phone,
                    CreateAt = model.CreateAt,
                    Email = model.Email,

                };
                var count = _employeeRepo.Add(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View(model);
        }


        [HttpGet]
        public IActionResult Details(int? id, string viewname = "Details")
        {
            if (id is null) return BadRequest("Invalid Id");
            var employee = _employeeRepo.Get(id.Value);

            if (employee == null) return NotFound(new { statusCode = 404, messege = $"Employee With Id:{id} is Not Found" });
            return View(viewname, employee);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {

            if (id is null) return BadRequest("Invalid Id");
            var employee = _employeeRepo.Get(id.Value);
            if (employee == null) return NotFound(new { statusCode = 404, messege = $"Employee With Id:{id} is Not Found" });
            var employeeDto = new EmployeeDto()
            {
                
                Name = employee.Name,
                Salary = employee.Salary,
                Address = employee.Address,
                IsActive = employee.IsActive,
                IsDeleted = employee.IsDeleted,
                Age = employee.Age,

                HiringDate = employee.HiringDate,
                Phone = employee.Phone,
                CreateAt = employee.CreateAt,
                Email = employee.Email,

            };
            return View(employeeDto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                // if (id != model.Id) return BadRequest();
                var employee = new Employee()
                {
                    Id = id,
                    Name = model.Name,
                    Salary = model.Salary,
                    Address = model.Address,
                    IsActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    Age = model.Age,

                    HiringDate = model.HiringDate,
                    Phone = model.Phone,
                    CreateAt = model.CreateAt,
                    Email = model.Email,

                };
                {
                    var count = _employeeRepo.Update(employee);
                    if (count > 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }

                }
            }
            return View(model);
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Edit([FromRoute] int id, EmployeeDto model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var employee = _employeeRepo.Get(id);

        //        if (employee == null) return NotFound(new { statusCode = 400, messege = $"Employee With Id:{id} is Not Found" });


        //        employee.Email = model.Email;
        //        employee.Phone = model.Phone;
        //        employee.Address = model.Address;
        //        employee.Age = model.Age;
        //        employee.HiringDate = model.HiringDate;
        //        employee.Name = model.Name;
        //        employee.IsActive = model.IsActive;
        //        employee.IsDeleted = model.IsDeleted;
        //        employee.CreateAt = model.CreateAt;
        //        employee.Salary = model.Salary;

        //        var count = _employeeRepo.Update(employee);
        //        if (count > 0)
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }

        //    }
        //    return View(model);
        //}


        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete([FromRoute] int id, EmployeeDto model)
        {
            if (ModelState.IsValid)
            {
                var employee = _employeeRepo.Get(id);

                if (employee == null) return NotFound(new { statusCode = 400, messege = $"Employee With Id:{id} is Not Found" });


                var count = _employeeRepo.Delete(employee);
                if (count > 0)
                {
                    return RedirectToAction(nameof(Index));
                }


            }
            return View(model);
        }



    }
}
