using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using ShoesApp.Web.ViewModels.Sizes;
using X.PagedList.Extensions;

namespace ShoesApp.Web.Controllers
{
    public class SizesController : Controller
    {
        private readonly ISizesService? _sizeServices;
        private readonly IMapper? _mapper;
        private readonly int pageSize = 5;

        public SizesController(ISizesService? sizeServices,
            IMapper mapper)
        {
            _sizeServices = sizeServices??throw new ApplicationException("Dependencies not set");
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");

        }

        public IActionResult Index(int? page)
        {
            var currentPage=page ?? 1;
            var sizeList = _sizeServices?.GetAll(
                orderBy: o => o.OrderBy(b => b.SizeNumber));
            var sizeListVm=_mapper?.Map<List<SizeListVm>>(sizeList);
            return View(sizeListVm?.ToPagedList(currentPage,pageSize));
        }

        public IActionResult UpSert(int? id)
        {
            SizeEditVm sizeVm;
            if (id == null || id == 0)
            {
                sizeVm = new SizeEditVm();
            }
            else
            {
                try
                {
                    Size? size = _sizeServices?.GetById(filter:b=>b.SizeId==id);
                    if (size == null)
                    {
                        return NotFound();
                    }
                    sizeVm = _mapper!.Map<SizeEditVm>(size);
                    return View(sizeVm);
                }
                catch (Exception)
                {
                    // Log the exception (ex) here as needed
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(sizeVm);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(SizeEditVm sizeVm)
        {
            if (!ModelState.IsValid)
            {
                return View(sizeVm);
            }


            try
            {
                Size size = _mapper!.Map<Size>(sizeVm);

                if (_sizeServices!.Exist(size))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(sizeVm);
                }

                _sizeServices.Save(size);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Log the exception (ex) here as needed
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(sizeVm);
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
            Size? size = _sizeServices?.GetById(filter:b=>b.SizeId==id);
            if (size is null)
            {
                return NotFound();
            }
            try
            {

                if (_sizeServices!.ItsRelated(size.SizeId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                _sizeServices.Remove(size);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;

            }
        }


    }
}
