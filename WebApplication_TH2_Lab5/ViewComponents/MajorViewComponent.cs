using Microsoft.AspNetCore.Mvc;
using WebApplication_TH2_Lab5.Data;
using WebApplication_TH2_Lab5.Models;

namespace WebApplication_TH2_Lab5.ViewComponents
{
    public class MajorViewComponent : ViewComponent
    {
        SchoolContext db;
        List<Major> majors;
        public MajorViewComponent(SchoolContext _context)
        {
            db = _context;
            majors = db.Majors.ToList();
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderMajor", majors);
        }
    }
}
