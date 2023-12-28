using Microsoft.AspNetCore.Mvc;
using homework1.Models;
using homework1.Data.Interfaces;
using homework1.Data.Services;
using homework1.ViewModels;

namespace homework1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly ISubjectService _subjectService;
        private readonly IResultService _resultService;

        public StudentsController(IStudentService studentService, ISubjectService subjectService, IResultService resultService)
        {
            _studentService = studentService;
            _subjectService = subjectService;
            _resultService = resultService;
        }

        // GET: Students
        public async Task<IActionResult> Index(int pageNumber = 1, string searchTerm = null)
        {
            const int PageSize = 10;

            var (students, totalCount) = await _studentService.GetStudentsIndexAsync(pageNumber, PageSize, searchTerm);

            var viewModel = new PaginationViewModel<Student>
            {
                Items = students,
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / PageSize),
                SearchTerm = searchTerm
            };

            return View(viewModel);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var subject = await _subjectService.GetSubjectsAsync();
            var student = await _studentService.GetStudentByIdAsync(id.Value);
            var results = await _resultService.GetResultByStudentIdAsync(student.First().StudentId);

            var studentViewModel = new StudentViewModel
            {
                StudentId = student.First().StudentId,
                StudentName = student.First().Name,
                StudentEmail = student.First().Email,
                StudentDOB = student.First().DOB.ToShortDateString(),
                StudentEnroll = student.First().EnrollmentDate.ToShortDateString(),

                Result = results.Select(r => new ResultViewModel
                {
                    ResultId = r.ResultId,
                    Marks = r.Marks,
                    SubjectId = r.SubjectId,
                    SubjectCode = subject.FirstOrDefault(sub => sub.SubjectId == r.SubjectId)?.Code,
                    SubjectName = subject.FirstOrDefault(sub => sub.SubjectId == r.SubjectId)?.Name
                })
            };

            return View(studentViewModel);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StudentId,Name,Email,DOB,EnrollmentDate,IsDeleted")] Student student)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _studentService.CreateStudentAsync(student);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentByIdAsync(id.Value);

            return View(student.FirstOrDefault());
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StudentId,Name,Email,DOB,EnrollmentDate,IsDeleted")] Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _studentService.UpdateStudentAsync(student);
                }
                catch (Exception)
                {
                    // Handle exceptions or log errors as needed
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _studentService.GetStudentByIdAsync(id.Value);

            return View(student.FirstOrDefault());
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _studentService.DeleteStudentAsync(id);
            }
            catch (Exception)
            {
                // Handle exceptions or log errors as needed
                throw;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
