using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimeAB.Reponsitories
{
    public class AppSettingFirebase
    {
        public string AuthSecret { get; set; }
        public string ApiKey { get; set; }
        public string AuthDomain { get; set; }
        public string DatabaseURL { get; set; }
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string StorageBucket { get; set; }
        public string MessagingSenderId { get; set; }
        public string AppId { get; set; }
        public string MeasurementId { get; set; }
        public string JwtAuthFirebase { get; set; }
        public string EndpointRefreshToken { get; set; }
    }
}
