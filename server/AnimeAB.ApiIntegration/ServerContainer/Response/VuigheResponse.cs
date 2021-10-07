using System.Collections.Generic;

namespace AnimeAB.ApiIntegration.ServerContainer.Response
{
    public class VuigheResponse
    {
        public SourcesVm sources { get; set; }
    }

    public class SourcesVm
    {
        public List<FileVm> vip { get; set; }
        public List<FileVm> gd {  get; set;}
        public List<FileVm> pt {  get; set; }
        public List<FileVm> yt {  get; set;}
        public List<FileVm> fb {  get; set; }

        public string embed { get; set; }
        public string mp4 { get;set;  }
        public HlsVm m3u8 { get; set; }
    }
    public class FileVm
    {
        public string src {  get; set; }
        public string type {  get; set; }
        public string quality { get; set; }
    }

    public class HlsVm
    {
        public string hls { get; set; }
    }
}
