using System;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Movie
    {
        [Display(Name = "Movie ID")]
        public int Id { get; set; }

        [Required]
        [MaxLength]
        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [StringLength(5)]
        public string Rating { get; set; }

        [Display(Name = "Image Link")]
        [Url]
        public string ImageLink { get; set; }

    }
}