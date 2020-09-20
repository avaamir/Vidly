using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class MoviesFormViewModel
    {

        public MoviesFormViewModel()
        {
            Id = 0;
        }

        public MoviesFormViewModel(Movie movie, IEnumerable<Genre> genres)
        {
            Genres = genres;
            Id = movie.Id;
            Name = movie.Name;
            GenreId = movie.GenreId;
            ReleaseDate = movie.ReleaseDate;
            Number = movie.Number;
        }


        public string Title => Id != 0 ? "Edit Movie" : "New Movie";
        public IEnumerable<Genre> Genres { get; set; }


        public int? Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Genre is Required")]
        public byte? GenreId { get; set; }

        [Required]
        public DateTime? ReleaseDate { get; set; }

        [Range(1, 20)]
        [Required]
        [Display(Name = "Number in Stock")]
        public int? Number { get; set; }

    }
}