using Bulky.DataAccess.Repository.IRepositery;
using Bulky.Models.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
	public class CompanyController : Controller
	{
		private readonly IUnitOfWork unitOfWork;

		public CompanyController(IUnitOfWork unitOfWork)
        {
			this.unitOfWork = unitOfWork;
		}
        public ActionResult Index()
		{
			var CompanyList = unitOfWork.CompanyRepository.GetAll();

			return View(CompanyList);
		}

		// GET: CompanyController/Details/5
		public ActionResult Details(int id)
		{
			return View();
		}

		// GET: CompanyController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: CompanyController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Company company)
		{
			if (ModelState.IsValid)
			{
				unitOfWork.CompanyRepository.Add(company);
				unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View(company);
		}

		// GET: CompanyController/Edit/5
		public ActionResult Edit(int id)
		{
			var company = unitOfWork.CompanyRepository.Get(p => p.Id == id);
			if (company == null)
			{
				return NotFound();
			}
			return View(company);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Company company)
		{
			var Companyindb=unitOfWork.CompanyRepository.Get(c=>c.Id==company.Id);

			if (Companyindb == null) { return NotFound(); }

			if (ModelState.IsValid)
			{
				Companyindb.Id = company.Id;
				Companyindb.Name = company.Name;
				Companyindb.Address = company.Address;
				Companyindb.City = company.City;
				Companyindb.PhoneNumber =company.PhoneNumber;
				Companyindb.State = company.State;
				Companyindb.PostalCode = company.PostalCode;
				unitOfWork.Save();
				return RedirectToAction("Index");
			}
			return View(company);
		}

		

		[HttpDelete]
		public ActionResult Delete(int id)
		{
			var company = unitOfWork.CompanyRepository.Get(c=>c.Id == id);
			if (company ==null) 
			{
				return Json(new { success = false, message = "Error Deleting Product" });


			}
			unitOfWork.CompanyRepository.Remove(company);
			unitOfWork.Save();
			return Json(new { success = true, message = "Company Deleted Successfuly" });

		}
	

		[HttpGet]
		public IActionResult GetAll()
		{
			var productlist=unitOfWork.CompanyRepository.GetAll();

			return Json( new {Data= productlist});
		}
	}
}
