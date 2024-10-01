using AutoMapper;
using ShoesApp.Entidades.Dtos.Shoes;
using ShoesApp.Entidades.Entities;
using ShoesApp.Web.ViewModels.Brands;
using ShoesApp.Web.ViewModels.Colours;
using ShoesApp.Web.ViewModels.Genres;
using ShoesApp.Web.ViewModels.Shoes;
using ShoesApp.Web.ViewModels.Sizes;
using ShoesApp.Web.ViewModels.Sports;

namespace ShoesApp.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            LoadBrandsMapping();
            LoadColoursMapping();
            LoadGenresMapping();
            LoadSportsMapping();
            LoadSizeMapping();
            LoadShoesMapping();
        }

        private void LoadShoesMapping()
        {
            CreateMap<Shoe, ShoeListVm>()
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Brand.BrandName))
                .ForMember(dest => dest.Genre, opt => opt.MapFrom(src => src.Genre.GenreName))
                .ForMember(dest => dest.Sport, opt => opt.MapFrom(src => src.Sport.SportName));
            CreateMap<Shoe, ShoeEditVm>().ReverseMap();
            CreateMap<Shoe, ShoeAssignColoursVm>();
            CreateMap<ShoeAssignColoursVm, ShoeColourDto>()
                .ForMember(dest => dest.ShoeId, opt => opt.MapFrom(src => src.ShoeId))
                .ForMember(dest => dest.ColourId, opt => opt.MapFrom(src => src.NewColourId))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.NewColourPrice));
                
        }

        private void LoadSizeMapping()
        {
            CreateMap<Size, SizeListVm>();
            CreateMap<Size, SizeEditVm>().ReverseMap();
        }

        private void LoadSportsMapping()
        {
            CreateMap<Sport, SportListVm>();
            CreateMap<Sport, SportEditVm>().ReverseMap();
        }

        private void LoadGenresMapping()
        {
            CreateMap<Genre, GenreListVm>();
            CreateMap<Genre, GenreEditVm>().ReverseMap();
        }

        private void LoadColoursMapping()
        {
            CreateMap<Colour, ColourListVm>();
            CreateMap<Colour, ColourEditVm>().ReverseMap();
        }

        private void LoadBrandsMapping()
        {
            CreateMap<Brand, BrandListVm>();
            CreateMap<Brand, BrandEditVm>().ReverseMap();
        }
    }
}
