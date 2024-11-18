using System.ComponentModel.DataAnnotations;

namespace LibraryApi.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Book>? Books { get; set; }
        public AuthorDetails? AuthorDetails { get; set; }
    }
}
