
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

        //初期表示（post後の再表示と兼ねる）
        public async Task<IActionResult> EducationEntrySheet() {
            EducationVM viewModel=await _service.SetViewModel();
            return View(viewModel);
        }

        //post後
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EducationEntrySheet(EducationVM educationVM) {
            ModelState.Clear();
            EducationVM viewModel=await _service.AfterPostExecution(educationVM);
            return View(viewModel);
        }
    }
}
