namespace net_il_mio_fotoalbum.Models
{
    public class Photo
    {
        public int Id {  get; set; } 
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public bool Visible { get; set; } = true;
        public List<Category>? Categories { get; set; }
        public int? CategoriesId { get; set; }

    }
}
