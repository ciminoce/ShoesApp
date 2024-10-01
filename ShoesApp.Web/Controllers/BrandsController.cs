using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using ShoesApp.Web.ViewModels.Brands;
using X.PagedList.Extensions;

namespace ShoesApp.Web.Controllers
{
    public class BrandsController : Controller
    {
        private readonly IBrandsService? _brandServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMapper? _mapper;
        private readonly int pageSize = 5;

        public BrandsController(IBrandsService? brandServices,
            IWebHostEnvironment webHostEnvironment,
            IMapper mapper)
        {
            _brandServices = brandServices??throw new ApplicationException("Dependencies not set");
            _webHostEnvironment= webHostEnvironment;
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");

        }

        public IActionResult Index(int? page)
        {
            var currentPage=page ?? 1;
            var brandList = _brandServices?.GetAll(
                orderBy: o => o.OrderBy(b => b.BrandName));
            var brandListVm=_mapper?.Map<List<BrandListVm>>(brandList);
            return View(brandListVm?.ToPagedList(currentPage,pageSize));
        }

        public IActionResult UpSert(int? id)
        {
            BrandEditVm brandVm;
            if (id == null || id == 0)
            {
                brandVm = new BrandEditVm();
            }
            else
            {
                try
                {
                    string wwwWebRoot=_webHostEnvironment.WebRootPath;
                    Brand? brand = _brandServices?.GetById(filter:b=>b.BrandId==id);
                    if (brand == null)
                    {
                        return NotFound();
                    }
                    brandVm = _mapper!.Map<BrandEditVm>(brand);
                    if (brand.ImageUrl!=null)
                    {
                        string pathFile = Path.Combine(wwwWebRoot, brand.ImageUrl!.TrimStart('/'));
                        ViewData["existingImage"] = System.IO.File.Exists(pathFile);

                    }
                    return View(brandVm);
                }
                catch (Exception)
                {
                    // Log the exception (ex) here as needed
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(brandVm);

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(BrandEditVm brandVm)
        {
            if (!ModelState.IsValid)
            {
                return View(brandVm);
            }

            string wwwWebRoot = _webHostEnvironment.WebRootPath;
            try
            {
                Brand brand = _mapper!.Map<Brand>(brandVm);

                if (_brandServices!.Exist(brand))
                {
                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(brandVm);
                }

                if (brandVm.ImageFile!=null)
                {
                    if (brand.ImageUrl!=null)
                    {
                        string oldFilePath = Path.Combine(wwwWebRoot, brand.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }
                    string fileName=$"{Guid.NewGuid()}{Path.GetExtension(brandVm.ImageFile.FileName)}";
                    string pathFile=Path.Combine(wwwWebRoot,"images","brands",fileName);
                    using (var fileStream=new FileStream(pathFile,FileMode.Create))
                    {
                        brandVm.ImageFile.CopyTo(fileStream);
                    }
                    brand.ImageUrl = $"/images/brands/{fileName}";
                }
                else
                {
                    brand.ImageUrl = null;
                }
                _brandServices.Save(brand);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Log the exception (ex) here as needed
                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(brandVm);
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
            Brand? brand = _brandServices?.GetById(filter:b=>b.BrandId==id);
            if (brand is null)
            {
                return NotFound();
            }
            try
            {
                string wwwWebRoot = _webHostEnvironment.WebRootPath;


                if (_brandServices!.ItsRelated(brand.BrandId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                _brandServices.Remove(brand);
                string oldFilePath = Path.Combine(wwwWebRoot, brand.ImageUrl!.TrimStart('/'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }

                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;

            }
        }


    }
}
