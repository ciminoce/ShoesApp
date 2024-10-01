using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using ShoesApp.Web.ViewModels.Colours;
using X.PagedList.Extensions;

namespace ShoesApp.Web.Controllers
{
    public class ColoursController : Controller
    {
        private readonly IColoursService? _colourServices;
        private readonly IMapper? _mapper;
        private readonly int pageSize = 5;

        public ColoursController(IColoursService? colourServices,
            IMapper mapper)
        {
            _colourServices = colourServices??throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");

        }

        public IActionResult Index(int? page)
        {
            var currentPage=page ?? 1;
            var colourList = _colourServices?.GetAll(
                orderBy: o => o.OrderBy(b => b.ColourName));
            var colourListVm=_mapper?.Map<List<ColourListVm>>(colourList);
            return View(colourListVm?.ToPagedList(currentPage,pageSize));
        }

        public IActionResult UpSert(int? id)
        {
            ColourEditVm colourVm;
            if (id == null || id == 0)
            {
                colourVm = new ColourEditVm();
            }
            else
            {
                try
                {
                    Colour? colour = _colourServices?.GetById(filter:b=>b.ColourId==id);
                    if (colour == null)
                    {
                        return NotFound();
                    }
                    colourVm = _mapper!.Map<ColourEditVm>(colour);
                    return View(colourVm);
                }
                catch (Exception)
                {
                    // Log the exception (ex) here as needed
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(colourVm);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(ColourEditVm colourVm)
        {
            if (!ModelState.IsValid)
            {
                return View(colourVm);
            }


            try
            {
                Colour colour = _mapper!.Map<Colour>(colourVm);

                if (_colourServices!.Exist(colour))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(colourVm);
                }

                _colourServices.Save(colour);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Log the exception (ex) here as needed
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(colourVm);
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
            Colour? colour = _colourServices?.GetById(filter:b=>b.ColourId==id);
            if (colour is null)
            {
                return NotFound();
            }
            try
            {

                if (_colourServices!.ItsRelated(colour.ColourId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                _colourServices.Remove(colour);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;

            }
        }


    }
}
