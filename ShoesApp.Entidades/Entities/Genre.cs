namespace ShoesApp.Entidades.Entities;

public class Genre
{
    public int GenreId { get; set; }
    public string GenreName { get; set; } = null!;
    public bool Active { get; set; } = true;
    public ICollection<Shoe> Shoes { get; set; } = new List<Shoe>();

}
