using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimeAB.Domain.DTOs
{
    public class AnimeDto
    {
        public string? Key { get; set; }
        public string? Image { get; set; }
        public IFormFile? FileUpload { get; set; }
        public string? Title { get; set; }
        public string? TitleVie { get; set; }
        public string? Description { get; set; }
        public string? Trainer { get; set; }
        public int Episode { get; set; }
        public int MovieDuration { get; set; }
        public DateTime DateRelease { get; set; }
        public string? Type { get; set; }
        public string[]? Categories { get; set; }
        public string? Season { get; set; }
    }
}
