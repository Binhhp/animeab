using Microsoft.AspNetCore.Http;

namespace AnimeAB.AppAdmin.AnimeEndpoints
{
    public class AnimeDetailRequest
    {
        public string Key { get; set; }
        public string Image { get; set; }
        public IFormFile FileUpload { get; set; }
        public string Title { get; set; }
        public string Link { get; set; }
        //Link server hoat hinh 247
        public string LinkHH247 { get; set; } = "";
        //Link server vuighe
        public string LinkVuighe { get; set; } = "";
        public int Episode { get; set; }
        public bool Iframe { get; set; }
    }
}
