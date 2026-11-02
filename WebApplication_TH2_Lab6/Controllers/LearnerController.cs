using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_TH2_Lab6.Data;
using WebApplication_TH2_Lab6.Models;

namespace WebApplication_TH2_Lab6.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;
        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        // Số phần tử dữ liệu trên 1 trang
        private int pageSize = 3;

        // R - danh sách (đồng bộ): nhận mid để lọc theo MajorID, hiển thị trang đầu
        public IActionResult Index(int? mid)
        {
            var result = GetPage(mid, null, 1);
            return View(result);
        }

        // Lọc + tìm kiếm + phân trang không đồng bộ (AJAX): trả về PartialView chứa bảng và thanh phân trang
        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            var result = GetPage(mid, keyword, pageIndex);
            return PartialView("LearnerTable", result);
        }

        // Dùng chung cho Index và LearnerFilter: lọc theo mid, tìm theo keyword, cắt theo trang
        private List<Learner> GetPage(int? mid, string? keyword, int? pageIndex)
        {
            // lấy toàn bộ learners trong DbSet, chuyển về IQueryable để query
            var learners = (IQueryable<Learner>)db.Learners;

            // mid = 0 (mục "All") hoặc null: không lọc theo chuyên ngành
            if (mid != null && mid != 0)
            {
                learners = learners.Where(l => l.MajorID == mid);
                ViewBag.mid = mid; // gửi mid về view để ghi lại trên nav-phân trang
            }

            // có keyword thì tìm theo tên
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.Trim();
                learners = learners.Where(l => l.FirstMidName.ToLower().Contains(keyword.ToLower()));
                ViewBag.keyword = keyword; // gửi keyword về view để ghi lại trên nav-phân trang
            }

            // tính số trang
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;

            // chỉ số trang, mặc định 1, không vượt quá số trang
            int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);
            if (pageNum > 0 && page > pageNum) page = pageNum;
            ViewBag.pageIndex = page;

            // chọn dữ liệu trong trang hiện tại (OrderBy để thứ tự ổn định khi Skip/Take)
            return learners.OrderBy(l => l.LearnerID)
                           .Skip(pageSize * (page - 1))
                           .Take(pageSize)
                           .Include(m => m.Major)
                           .ToList();
        }

        // C - thêm
        public IActionResult Create()
        {
            //cách 1: tạo List<SelectListItem> thủ công
            var majors = new List<SelectListItem>();
            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = item.MajorName,
                    Value = item.MajorID.ToString()
                });
            }
            ViewBag.MajorID = majors;
            //cách 2: dùng SelectList
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View(learner);
        }

        // U - sửa
        public IActionResult Edit(int id)
        {
            if (db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }

        // D - xóa
        public IActionResult Delete(int id)
        {
            var learner = db.Learners.Include(l => l.Major)
                .Include(e => e.Enrollments)
                .FirstOrDefault(m => m.LearnerID == id);
            if (learner == null)
            {
                return NotFound();
            }
            if (learner.Enrollments.Count() > 0)
            {
                return Content("This learner has some enrollments, can't delete!");
            }
            return View(learner);
        }

        // POST: Learner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var learner = db.Learners.Find(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool LearnerExists(int id)
        {
            return db.Learners.Any(e => e.LearnerID == id);
        }
    }
}
