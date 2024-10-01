using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using ShoesApp.Web.ViewModels.Sports;
using X.PagedList.Extensions;

namespace ShoesApp.Web.Controllers
{
    public class SportsController : Controller
    {
        private readonly ISportsService? _sportServices;
        private readonly IMapper? _mapper;
        private readonly int pageSize = 5;

        public SportsController(ISportsService? sportServices,
            IMapper mapper)
        {
            _sportServices = sportServices??throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");

        }

        public IActionResult Index(int? page)
        {
            var currentPage=page ?? 1;
            var sportList = _sportServices?.GetAll(
                orderBy: o => o.OrderBy(b => b.SportName));
            var sportListVm=_mapper?.Map<List<SportListVm>>(sportList);
            return View(sportListVm?.ToPagedList(currentPage,pageSize));
        }

        public IActionResult UpSert(int? id)
        {
            SportEditVm sportVm;
            if (id == null || id == 0)
            {
                sportVm = new SportEditVm();
            }
            else
            {
                try
                {
                    Sport? sport = _sportServices?.GetById(filter:b=>b.SportId==id);
                    if (sport == null)
                    {
                        return NotFound();
                    }
                    sportVm = _mapper!.Map<SportEditVm>(sport);
                    return View(sportVm);
                }
                catch (Exception)
                {
                    // Log the exception (ex) here as needed
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(sportVm);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(SportEditVm sportVm)
        {
            if (!ModelState.IsValid)
            {
                return View(sportVm);
            }


            try
            {
                Sport sport = _mapper!.Map<Sport>(sportVm);

                if (_sportServices!.Exist(sport))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(sportVm);
                }

                _sportServices.Save(sport);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Log the exception (ex) here as needed
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(sportVm);
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
            Sport? sport = _sportServices?.GetById(filter:b=>b.SportId==id);
            if (sport is null)
            {
                return NotFound();
            }
            try
            {

                if (_sportServices!.ItsRelated(sport.SportId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                _sportServices.Remove(sport);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;

            }
        }


    }
}
