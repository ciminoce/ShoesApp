using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesApp.Entidades.Dtos.Shoes;
using ShoesApp.Entidades.Entities;
using ShoesApp.Servicios.Interfaces;
using ShoesApp.Web.ViewModels.Shoes;
using X.PagedList.Extensions;

namespace ShoesApp.Web.Controllers
{
    public class ShoesController : Controller
    {
        private readonly IShoesService? _shoeServices;
        private readonly IBrandsService? _brandsService;
        private readonly ISportsService? _sportsService;
        private readonly IGenresService? _genresService;
        private readonly IColoursService? _coloursService;
        private readonly IShoeColoursService? _shoeColoursService;
        private readonly IMapper? _mapper;
        private readonly int pageSize = 5;

        public ShoesController(IShoesService? shoeServices,
            IBrandsService? brandsService,
            ISportsService? sportsService,
            IColoursService coloursService,
            IGenresService genresService,
            IShoeColoursService shoeColoursService,
            IMapper mapper)
        {
            _coloursService = coloursService ?? throw new ApplicationException("Dependencies not set");
            _brandsService = brandsService ?? throw new ApplicationException("Dependencies not set");
            _sportsService = sportsService ?? throw new ApplicationException("Dependencies not set");
            _genresService = genresService ?? throw new ApplicationException("Dependencies not set");
            _shoeServices = shoeServices ?? throw new ApplicationException("Dependencies not set");
            _shoeColoursService = shoeColoursService;
            _mapper = mapper ?? throw new ApplicationException("Dependencies not set");

        }

        public IActionResult Index(int? page)
        {
            var currentPage = page ?? 1;
            var shoeList = _shoeServices?.GetAll(
                orderBy: o => o.OrderBy(b => b.Model),
                propertiesNames: "Brand,Sport,Genre");
            var shoeListVm = _mapper?.Map<List<ShoeListVm>>(shoeList);
            return View(shoeListVm?.ToPagedList(currentPage, pageSize));
        }

        public IActionResult UpSert(int? id)
        {
            ShoeEditVm shoeVm;
            if (id == null || id == 0)
            {
                shoeVm = new ShoeEditVm();
                shoeVm.Brands = GetBrands();
                shoeVm.Sports = GetSports();
                shoeVm.Genres = GetGenres();
            }
            else
            {
                try
                {
                    Shoe? shoe = _shoeServices?.GetById(filter: b => b.ShoeId == id);
                    if (shoe == null)
                    {
                        return NotFound();
                    }
                    shoeVm = _mapper!.Map<ShoeEditVm>(shoe);
                    return View(shoeVm);
                }
                catch (Exception)
                {
                    // Log the exception (ex) here as needed
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }

            }
            return View(shoeVm);

        }

        private List<SelectListItem>? GetGenres()
        {
            return _genresService!.GetAll(orderBy: o => o.OrderBy(g => g.GenreName))!
                .Select(n => new SelectListItem
                {
                    Text = n.GenreName,
                    Value = n.GenreId.ToString()
                }).ToList();
        }

        private List<SelectListItem>? GetColors()
        {
            return _coloursService!.GetAll(orderBy: o => o.OrderBy(g => g.ColourName))!
                .Select(n => new SelectListItem
                {
                    Text = n.ColourName,
                    Value = n.ColourId.ToString()
                }).ToList();
        }

        private List<SelectListItem>? GetSports()
        {
            return _sportsService!.GetAll(orderBy: o => o.OrderBy(g => g.SportName))!
                .Select(n => new SelectListItem
                {
                    Text = n.SportName,
                    Value = n.SportId.ToString()
                }).ToList();
        }

        private List<SelectListItem>? GetBrands()
        {
            return _brandsService!.GetAll(orderBy: o => o.OrderBy(g => g.BrandName))!
                .Select(n => new SelectListItem
                {
                    Text = n.BrandName,
                    Value = n.BrandId.ToString()
                }).ToList();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpSert(ShoeEditVm shoeVm)
        {
            if (!ModelState.IsValid)
            {
                shoeVm.Brands = GetBrands();
                shoeVm.Sports = GetSports();
                shoeVm.Genres = GetGenres();

                return View(shoeVm);
            }


            try
            {
                Shoe shoe = _mapper!.Map<Shoe>(shoeVm);

                if (_shoeServices!.Exist(shoe))
                {
                    shoeVm.Brands = GetBrands();
                    shoeVm.Sports = GetSports();
                    shoeVm.Genres = GetGenres();

                    ModelState.AddModelError(string.Empty, "Record already exist");
                    return View(shoeVm);
                }

                _shoeServices.Save(shoe);
                TempData["success"] = "Record successfully added/edited";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                // Log the exception (ex) here as needed
                shoeVm.Brands = GetBrands();
                shoeVm.Sports = GetSports();
                shoeVm.Genres = GetGenres();

                ModelState.AddModelError(string.Empty, "An error occurred while editing the record.");
                return View(shoeVm);
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
            Shoe? shoe = _shoeServices?.GetById(filter: b => b.ShoeId == id);
            if (shoe is null)
            {
                return NotFound();
            }
            try
            {

                if (_shoeServices!.ItsRelated(shoe.ShoeId))
                {
                    return Json(new { success = false, message = "Related Record... Delete Deny!!" }); ;
                }
                _shoeServices.Remove(shoe);
                return Json(new { success = true, message = "Record successfully deleted" });
            }
            catch (Exception)
            {

                return Json(new { success = false, message = "Couldn't delete record!!! " }); ;

            }
        }

        public IActionResult AssignColors(int? id)
        {
            ShoeAssignColoursVm shoeVm;

            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                try
                {
                    Shoe? shoe = _shoeServices?.GetById(filter: b => b.ShoeId == id);
                    if (shoe == null)
                    {
                        return NotFound();
                    }

                    shoeVm = _mapper!.Map<ShoeAssignColoursVm>(shoe);

                    // Obtener los colores ya asignados con precios
                    shoeVm.AvailableColours = GetColorsWithPrices(shoe.ShoeId);

                    // Obtener los colores disponibles que aún no están asignados a este zapato
                    var assignedColourIds = shoeVm.AvailableColours.Select(c => c.ColourId).ToList();
                    shoeVm.AllColours = GetAllAvailableAndNotAssignedColours(shoeVm, assignedColourIds);
                }
                catch (Exception)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the record.");
                }
            }

            return View(shoeVm);

        }
        /// <summary>
        /// Método para cargar los colores disponibles
        /// y que aún no han sido asignados al shoe
        /// </summary>
        /// <param name="shoeVm">Modelo de vista en cuestión</param>
        /// <param name="assignedColourIds">Lista con los colores ya asignados</param>
        /// <returns>Lista de selección con los colores NO asignados</returns>
        private IEnumerable<SelectListItem> GetAllAvailableAndNotAssignedColours(ShoeAssignColoursVm shoeVm, List<int> assignedColourIds)
        {
            return _coloursService!.GetAll(filter: c => !assignedColourIds.Contains(c.ColourId))!
                .Select(c => new SelectListItem
                {
                    Value = c.ColourId.ToString(),
                    Text = c.ColourName
                }).ToList();
        }

        [HttpPost]
        public IActionResult AssignColors(ShoeAssignColoursVm shoeColorVm)
        {
            if (ModelState.IsValid)
            {
                // Usar AutoMapper para convertir el ViewModel a un DTO
                var shoeColorDto = _mapper!.Map<ShoeColourDto>(shoeColorVm);


                _shoeColoursService!.AssignColorsAndPricesToShoe(shoeColorDto);
                //return RedirectToAction("Details", new { id = shoeColorVm.ShoeId });
                return RedirectToAction("AssignColors");
            }

            // Si algo falla, recargar la vista con los datos necesarios
            shoeColorVm.AvailableColours = GetColorsWithPrices(shoeColorVm.ShoeId);
            return View(shoeColorVm);
        }
        /// <summary>
        /// Método para obtener los colores asignados a un shoe
        /// </summary>
        /// <param name="shoeId">clave del shoe buscado</param>
        /// <returns>retorna la lista de colores asignados a un shoe</returns>
        public List<ShoeColorVm> GetColorsWithPrices(int shoeId)
        {
            // Obtener la lista de colores con precios asignados a este shoe específico
            var assignedColours = _shoeColoursService!.GetAll(filter:sc=>sc.ShoeId==shoeId,propertiesNames:"Colour")!
                .Select(sc => new ShoeColorVm
                {
                    ColourId = sc.ColourId,
                    ColourName = sc.Colour.ColourName, 
                    Price = sc.PriceAdjustment, 
                    IsSelected = true // Marcar como seleccionado porque ya está asignado
                }).ToList();

            return assignedColours;
        }

    }
}
