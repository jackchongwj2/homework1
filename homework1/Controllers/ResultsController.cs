using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using homework1.Data;
using homework1.Models;
using homework1.Data.Interfaces;
using homework1.Data.Services;
using homework1.ViewModels;
using System.Data.Entity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace homework1.Controllers
{
    public class ResultsController : Controller
    {
        private readonly IResultService _resultService;
        private readonly ISubjectService _subjectService;
        private readonly IStudentService _studentService;

        public ResultsController(IResultService resultService, ISubjectService subjectService, IStudentService studentService)
        {
            _resultService = resultService;
            _subjectService = subjectService;
            _studentService = studentService;
        }

        // GET: Results
        public async Task<IActionResult> Index(int pageNumber = 1, string searchTerm = null)
        {
            const int PageSize = 10;

            // Retrieve results
            var (results, totalCount) = await _resultService.GetResultsIndexAsync(pageNumber, PageSize, searchTerm);

            if (!results.Any())
            {
                // If there are no results, return empty view
                return View(new PaginationViewModel<ResultViewModel>
                {
                    Items = Enumerable.Empty<ResultViewModel>(),
                    PageNumber = pageNumber,
                    TotalPages = 0,
                    SearchTerm = searchTerm
                });
            }

            // Retrieve subjects and students only if there are results
            var subjects = await _subjectService.GetSubjectsAsync();
            var students = await _studentService.GetStudentsAsync();

            // Create ViewModel
            var viewModel = new PaginationViewModel<ResultViewModel>
            {
                Items = results.Select(r => new ResultViewModel
                {
                    ResultId = r.ResultId,
                    Marks = r.Marks,
                    StudentId = r.StudentId,
                    StudentName = students.FirstOrDefault(s => s.StudentId == r.StudentId)?.Name,
                    StudentEmail = students.FirstOrDefault(s => s.StudentId == r.StudentId)?.Email,
                    SubjectId = r.SubjectId,
                    SubjectCode = subjects.FirstOrDefault(sub => sub.SubjectId == r.SubjectId)?.Code,
                    SubjectName = subjects.FirstOrDefault(sub => sub.SubjectId == r.SubjectId)?.Name
                }),
                PageNumber = pageNumber,
                TotalPages = (int)Math.Ceiling((double)totalCount / PageSize),
                SearchTerm = searchTerm
            };

            return View(viewModel);
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var singleResultViewModel = await GetSingleResultViewModelAsync(id.Value);

            return View(singleResultViewModel);
        }

        // GET: Results/Create
        public async Task<IActionResult> Create()
        {
            var students = await _studentService.GetStudentsAsync();

            ViewBag.StudentId = new SelectList(students, "StudentId", "Name");

            return View();
        }

        // POST: Results/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ResultId,StudentId,SubjectId,Marks")] Result result)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _resultService.CreateResultAsync(result);
                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            var students = await _studentService.GetStudentsAsync();

            ViewBag.StudentId = new SelectList(students, "StudentId", "Name");

            return View(result);
        }

        // GET: Results/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var singleResultViewModel = await GetSingleResultViewModelAsync(id.Value);

            return View(singleResultViewModel);
        }

        // POST: Results/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ResultViewModel resultViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Use the resultViewModel to update the result data
                    var resultModel = new Result
                    {
                        ResultId = resultViewModel.ResultId,
                        StudentId = resultViewModel.StudentId,
                        SubjectId = resultViewModel.SubjectId,
                        Marks = resultViewModel.Marks
                    };

                    await _resultService.UpdateResultAsync(resultModel);

                    return RedirectToAction(nameof(Index));
                }
                catch (InvalidOperationException ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                }
            }

            // If the ModelState is not valid, redisplay the form
            return View(resultViewModel);
        }

        // GET: Results/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var singleResultViewModel = await GetSingleResultViewModelAsync(id.Value);

            return View(singleResultViewModel);
        }

        // POST: Results/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _resultService.DeleteResultAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<ResultViewModel> GetSingleResultViewModelAsync(int id)
        {
            var result = await _resultService.GetResultByIdAsync(id);
            var subject = await _subjectService.GetSubjectByIdAsync(result.First().SubjectId);
            var student = await _studentService.GetStudentByIdAsync(result.First().StudentId);

            var singleResultViewModel = new ResultViewModel
            {
                ResultId = result.First().ResultId,
                Marks = result.First().Marks,
                StudentId = result.First().StudentId,
                StudentName = student.First().Name,
                StudentEmail = student.First().Email,
                SubjectId = result.First().SubjectId,
                SubjectCode = subject.First().Code,
                SubjectName = subject.First().Name
            };

            return singleResultViewModel;
        }

        [HttpGet]
        public async Task<IActionResult> GetAvailableSubjectsByStudentId(int studentId)
        {
            try
            {
                // Retrieve subjects not taken by the selected student asynchronously
                var subjects = await _resultService.GetAvailableSubjectsByStudentId(studentId);

                // Return JSON result
                return Json(subjects);
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return BadRequest("Error retrieving subjects: " + ex.Message);
            }
        }
    }
}
