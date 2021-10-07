using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace AnimeAB.Domain.DTOs
{
    public class CollectionDto
    {
        public string? Key { get; set; }
        public string? Title { get; set; }
        public IFormFile FileUpload { get; set; }
    }
}
