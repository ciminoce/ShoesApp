using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using ShoesApp.Web.ViewModels.Genres;
using X.PagedList.Extensions;

namespace ShoesApp.Web.Controllers
{
    public class GenresController : Controller
    {
        private readonly IGenresService? _genreServices;
        private readonly IMapper? _mapper;
        private readonly int pageSize = 5;

        public GenresController(IGenresService? genreServices,
            IMapper mapper)
        {
            _genreServices = genreServices??throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");

        }

        public IActionResult Index(int? page)
        {
            var currentPage=page ?? 1;
            var genreList = _genreServices?.GetAll(
                orderBy: o => o.OrderBy(b => b.GenreName));
            var genreListVm=_mapper?.Map<List<GenreListVm>>(genreList);
            return View(genreListVm?.ToPagedList(currentPage,pageSize));
        }

        public IActionResult UpSert(int? id)
        {
            GenreEditVm genreVm;
            if (id == null || id == 0)
            {
                genreVm = new GenreEditVm();
            }
            else
            {
                try
                {
                    Genre? genre = _genreServices?.GetById(filter:b=>b.GenreId==id);
                    if (genre == null)
                    {
                        return NotFound();
                    }
                    genreVm = _mapper!.Map<GenreEditVm>(genre);
                    return View(genreVm);
                }
                catch (Exception)
                {
                    // Log the exception (ex) here as needed
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(genreVm);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(GenreEditVm genreVm)
        {
            if (!ModelState.IsValid)
            {
                return View(genreVm);
            }


            try
            {
                Genre genre = _mapper!.Map<Genre>(genreVm);

                if (_genreServices!.Exist(genre))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(genreVm);
                }

                _genreServices.Save(genre);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Log the exception (ex) here as needed
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(genreVm);
            }
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }
            Genre? genre = _genreServices?.GetById(filter:b=>b.GenreId==id);
            if (genre is null)
            {
                return NotFound();
            }
            try
            {

                if (_genreServices!.ItsRelated(genre.GenreId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                _genreServices.Remove(genre);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;

            }
        }


    }
}
