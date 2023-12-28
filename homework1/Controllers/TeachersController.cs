using Microsoft.AspNetCore.Mvc;
using homework1.Models;
using homework1.Data.Interfaces;
using homework1.Data.Services;
using homework1.Data.Repositories;
using homework1.ViewModels;

namespace homework1.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ISubjectService _subjectService;

        public TeachersController(ITeacherService teacherService,ISubjectService subjectService)
        {
            _teacherService = teacherService;
            _subjectService = subjectService;
        }

        // GET: Teachers
        public async Task<IActionResult> Index(int pageNumber = 1, string searchTerm = null)
        {
            const int PageSize = 10;

            var (teachers, totalCount) = await _teacherService.GetTeachersIndexAsync(pageNumber, PageSize, searchTerm);

            var viewModel = new PaginationViewModel<Teacher>
            {
                Items = teachers,
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / PageSize),
                SearchTerm = searchTerm
            };

            return View(viewModel);
        }

        // GET: Teachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherService.GetTeacherByIdAsync(id.Value);

            if (teacher == null || !teacher.Any())
            {
                return NotFound();
            }

            // Subjects taught by that teacher
            var subjects = await _subjectService.GetSubjectsByTeacherIdAsync(id.Value);

            ViewData["Subjects"] = subjects;

            return View(teacher.First());
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TeacherId,Name,Email,PhoneNumber,IsDeleted")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _teacherService.CreateTeacherAsync(teacher);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherService.GetTeacherByIdAsync(id.Value);

            return View(teacher.FirstOrDefault());
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TeacherId,Name,Email,PhoneNumber,IsDeleted")] Teacher teacher)
        {
            if (id != teacher.TeacherId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _teacherService.UpdateTeacherAsync(teacher);
                }
                catch (Exception)
                {
                    // Handle exceptions or log errors as needed
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacher = await _teacherService.GetTeacherByIdAsync(id.Value);

            return View(teacher.FirstOrDefault());
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                // Check if there are subjects associated with the teacher
                var subjects = await _subjectService.GetSubjectsByTeacherIdAsync(id);

                if (subjects.Any())
                {
                    // Display an error message indicating that subjects need to be reassigned
                    ModelState.AddModelError(string.Empty, "Please reassign the following subjects before deleting this teacher.");

                    // Fetch subjects that are associated with the teacher
                    var subjectNames = subjects.Select(s => s.Name).ToList();

                    // Add the subject names to ViewData for display in the view
                    ViewData["SubjectNames"] = subjectNames;

                    // Repopulate other data needed for the view
                    var teacher = await _teacherService.GetTeacherByIdAsync(id);

                    // Return the view with the error message, subject names, and teacher data
                    return View("Delete", teacher.FirstOrDefault());
                }

                // If no subjects are associated, proceed with the deletion
                await _teacherService.DeleteTeacherAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception)
            {
                // Handle exceptions as needed
                throw;
            }
        }
    }
}
