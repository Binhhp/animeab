using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories.Entities
{
    public class Notification
    {
        public string Key { get; set; }
        public string UserRevice { get; set; }
        public string Message { get; set; }
        public string LinkNotify { get; set; }
        public DateTime When { get; set; } = DateTime.Now;
        public bool IsRead { get; set; } = false;
        public string Title { get; set; } = "Bình luận";
    }
}
