
using Microsoft.AspNetCore.Mvc;
using ASPEnshu.Data;
using ASPEnshu.Models.ViewModels;
using ASPEnshu.Models.Interfaces;
using ASPEnshu.Models.Services;

namespace ASPEnshu.Controllers
{
    public class EducationEntryController : Controller
    {
        private readonly ASPEnshuContext _context;

        private IService<EducationVM> _service;

        public EducationEntryController(ASPEnshuContext context)
        {
            _context = context;
            _service = new EducationService(_context);
        }

        [HttpGet]
        [Route("/{id}")]
        public async Task<IActionResult> Index(int id) {
            return RedirectToAction("EducationEntrySheet");
        }
        //初期表示（post後の再表示と兼ねる）
        [HttpGet]
        public async Task<IActionResult> EducationEntrySheet(int id) {
            EducationVM viewModel=await _service.SetViewModel();
            TempData["id"] = id.ToString();
            return View(viewModel);
        }

        //post後
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EducationEntrySheet(EducationVM educationVM) {
            ModelState.Clear();
            EducationVM viewModel=await _service.AfterPostExecution(educationVM);

            if (int.Parse(TempData["id"].ToString()) == 1) {
                return View(viewModel);
            }
  
            return RedirectToAction();
        }
    }
}
