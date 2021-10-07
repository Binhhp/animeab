using System.Collections.Generic;

namespace AnimeAB.ApiIntegration.ServerContainer.Response
{
    public class AnimeVsub
    {
        public List<link> link {  get; set; }
        public string playTech { get; set; }
        public string success { get; set; }

    }

    public class link
    {
        public string file { get; set; }
        public string label { get; set; }
        public string preload {  get; set; }
        public string type {  get; set; }
    }
}
