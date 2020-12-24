using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using WebApp.Areas.Admin_0306181083.Data;
using WebApp.Areas.Admin_0306181083.Models;
using static WebApp.Helper;

namespace WebApp.Areas.Admin_0306181083.Controllers
{
    [Area("Admin_0306181083")]
    public class PostController : Controller
    {
        private readonly DPContext _context;

        public PostController(DPContext context)
        {
            _context = context;
        }

        // GET: Admin_0306181083/Post
            //public async Task<IActionResult> Index()
            //{
            //    var dPContext = _context.Post.Include(p => p.Loai);
            //    return View(await dPContext.ToListAsync());
            //}
        public ActionResult Index(int? page)
        {

            // 1. Tham số int? dùng để thể hiện null và kiểu int
            // page có thể có giá trị là null và kiểu int.

            // 2. Nếu page = null thì đặt lại là 1.
            if (page == null) page = 1;

            // 3. Tạo truy vấn, lưu ý phải sắp xếp theo trường nào đó, ví dụ OrderBy
            // theo LinkID mới có thể phân trang.
            var links = (from l in _context.Post
                         .Include(p => p.Loai)
                         select l).OrderBy(x => x.ID);

            // 4. Tạo kích thước trang (pageSize) hay là số Link hiển thị trên 1 trang
            int pageSize = 3;

            // 4.1 Toán tử ?? trong C# mô tả nếu page khác null thì lấy giá trị page, còn
            // nếu page = null thì lấy giá trị 1 cho biến pageNumber.
            int pageNumber = (page ?? 1);

            // 5. Trả về các Link được phân trang theo kích thước và số trang.
            return View(links.ToPagedList(pageNumber, pageSize));
        }

        // GET: Transaction/AddOrEdit(Insert)
        // GET: Transaction/AddOrEdit/5(Update)
        [NoDirectAccess]
        public async  Task<IActionResult> AddOrEdit(int id = 0)
        {
            ViewBag.ListCategory = _context.Category.ToList();
            if (id == 0)
            {
                return View(new Post());

            }
          else
            {
                var post = await _context.Post.FindAsync(id);
                if (post == null)
                {
                    return NotFound();
                }
              
                return View(post);
            }
            
           
        }


       
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("ID,Title,Img,Content,CategoryPost")] Post post, IFormFile ful)
        {
           
            if (ModelState.IsValid)
            {
                if(id == 0)
                {
                    _context.Add(post);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    try
                    {
                        if (ful != null)
                        {
                            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/post", post.ID + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1]);
                            using (var stream = new FileStream(path, FileMode.Create))
                            {
                                await ful.CopyToAsync(stream);
                            }
                            post.Img = post.ID + "." + ful.FileName.Split(".")[ful.FileName.Split(".").Length - 1];

                        }
                        _context.Update(post);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PostExists(post.ID))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }

                return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this,"_ViewAll",_context.Post.ToList()) });
            }

            return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", post) });

        }

        // GET: Admin_0306181083/Post/Delete/5 
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.Loai)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        // POST: Admin_0306181083/Post/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.ID == id);
        }
    }
}
