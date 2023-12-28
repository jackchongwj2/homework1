using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using homework1.Models;
using homework1.Data.Interfaces;
using homework1.ViewModels;
using homework1.Data.Services;

namespace homework1.Controllers
{
    public class SubjectsController : Controller
    {
        private readonly ISubjectService _subjectService;
        private readonly ITeacherService _teacherService;

        public SubjectsController(ISubjectService subjectService, ITeacherService teacherService)
        {
            _subjectService = subjectService;
            _teacherService = teacherService;
        }

        private async Task<SubjectViewModel> GetSingleSubjectViewModelAsync(int id)
        {
            var subject = await _subjectService.GetSubjectByIdAsync(id);
            var teacher = await _teacherService.GetTeacherByIdAsync(subject.First().TeacherId);

            var singleSubjectViewModel = new SubjectViewModel
            {
                SubjectId = subject.First().SubjectId,
                SubjectName = subject.First().Name,
                SubjectCode = subject.First().Code,
                TeacherId = subject.First().TeacherId,
                TeacherName = teacher.First().Name
            };

            return singleSubjectViewModel;
        }

        // GET: Subjects
        public async Task<IActionResult> Index(int pageNumber = 1, string searchTerm = null)
        {
            const int PageSize = 10;

            var teachers = await _teacherService.GetTeachersAsync();
            var (subjects, totalCount) = await _subjectService.GetSubjectsIndexAsync(pageNumber, PageSize, searchTerm);

            var viewModel = new PaginationViewModel<SubjectViewModel>
            {
                Items = subjects.Select(s => new SubjectViewModel
                {
                    SubjectId = s.SubjectId,
                    SubjectName = s.Name,
                    SubjectCode = s.Code,
                    TeacherId = s.TeacherId,
                    TeacherName = s.Teacher?.Name
                }),
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / PageSize),
                SearchTerm = searchTerm
            };

            return View(viewModel);
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var singleSubjectViewModel = await GetSingleSubjectViewModelAsync(id.Value);

            return View(singleSubjectViewModel);
        }

        // GET: Subjects/Create
        public async Task<IActionResult> Create()
        {
            var teachers = await _teacherService.GetTeachersAsync();

            ViewBag.TeacherId = new SelectList(teachers, "TeacherId", "Name");

            return View();
        }

        // POST: Subjects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubjectId,Name,Code,TeacherId,IsDeleted")] Subject subject)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _subjectService.CreateSubjectAsync(subject);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(subject);
        }

        // GET: Subjects/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var teachers = await _teacherService.GetTeachersAsync();

            ViewBag.TeacherId = new SelectList(teachers, "TeacherId", "Name");

            if (id == null)
            {
                return NotFound();
            }

            var subject = await _subjectService.GetSubjectByIdAsync(id.Value);

            return View(subject.FirstOrDefault());
        }

        // POST: Subjects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubjectId,Name,Code,TeacherId,IsDeleted")] Subject subject)
        {
            if (id != subject.SubjectId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _subjectService.UpdateSubjectAsync(subject);
                }
                catch (Exception)
                {
                    // Handle exceptions or log errors as needed
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var singleSubjectViewModel = await GetSingleSubjectViewModelAsync(id.Value);

            return View(singleSubjectViewModel);
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _subjectService.DeleteSubjectAsync(id);
            }
            catch (Exception)
            {
                throw;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
