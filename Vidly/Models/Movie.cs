using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Models
{ 
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public DateTime AddedDate { get; set; }

        [Range(1, 20)]
        [Required]
        public int Number { get; set; }
    }
}