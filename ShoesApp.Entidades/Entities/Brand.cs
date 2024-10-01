﻿namespace ShoesApp.Entidades.Entities
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; } = null!;
        public string? ImageUrl { get; set; }
        public bool Active { get; set; } = true;
        public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();

    }
}
