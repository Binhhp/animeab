using AnimeAB.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Domain
{
    public class AnimesDomain
    {
        public string? Key { get; set; }
        public string? Image { get; set; }
        public string? FileName { get; set; }
        public string? Title { get; set; }
        public string? TitleVie { get; set; }
        public string? Description { get; set; }
        public int MovieDuration { get; set; }
        public DateTime DateRelease { get; set; }
        public string? Trainer { get; set; }
        public int Episode { get; set; }
        public string? CollectionId { get; set; }
        public Dictionary<string, Categories>? Categories { get; set; }
        public int IsStatus { get; set; }
        public string? Type { get; set; }
        public string? Season { get; set; }
    }
}
