
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

        [TempData]
        public int Id { get; set; }

        public EducationVM EducationVM { get; set; }

        public EducationEntryController(ASPEnshuContext context)
        {
            _context = context;
            _service = new EducationService(_context);
        }
         
        //初期表示（post後の再表示と兼ねる）
        [HttpGet]
        public async Task<IActionResult> EducationEntrySheet(int id) {
            //ここはセッション管理のテストなので、演習とは関係ない
            if (HttpContext.Session.GetString("now") == null) {
                HttpContext.Session.SetString("now", DateTime.Now.ToString());
            }

            EducationVM = await _service.SetViewModel();
            this.Id = id;
            ViewBag.Session=HttpContext.Session.Id;      //ここはセッション管理のテストなので、演習とは関係ない
            return View(EducationVM);
        }

        //post後
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EducationEntrySheet(EducationVM educationVM) {

            EducationVM = await _service.AfterPostExecution(educationVM);

            if (Id == 1) {   //起動urlのid部分が1の場合、RPG対応しない
                ModelState.Clear();
                ViewBag.Session = HttpContext.Session.Id;    //ここはセッション管理のテストなので、演習とは関係ない
                return View(EducationVM);
            }
            return RedirectToAction();  //こちらがPRG(Post-Redirect-Get)対応したもの
        }
    }
}
