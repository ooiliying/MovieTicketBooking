using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Admin.ViewModels
{
    public partial class MovieViewModel
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string PortraitImage { get; set; }
        public string LandscapeImage { get; set; }
        public string Description { get; set; }
        public static List<ReleasedDateTime> ReleasedDateTimeList { get; set; }
        [StringLength(50)]
        public string Genre { get; set; }
        [StringLength(50)]
        public string Duration { get; set; }
        [StringLength(50)]
        public string Language { get; set; }
        [Column(TypeName = "money")]
        public decimal? Price { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTimeOffset CreatedDateTime { get; set; }
        public DateTimeOffset? UpdatedDateTime { get; set; }
        
        public class ReleasedDateTime {
            public Guid Id { get; set; }
            [Required]
            public string Date { get; set; }
            [Required]
            public TimeSpan Time { get; set; }
            [Required]
            public Guid MovieId { get; set; }
            public DateTimeOffset CreatedDateTime { get; set; }
            public DateTimeOffset? UpdatedDateTime { get; set; }
        }
    }
}