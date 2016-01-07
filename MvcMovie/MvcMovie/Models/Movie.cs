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
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public string Genre { get; set; }

        [Range(1, 100)]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }

        [Display(Name = "Image Link")]
        public string ImageLink { get; set; }

        [Display(Name = "Banner Link")]
        public string BannerLink { get; set; }

        public string Description { get; set; }
    }
}