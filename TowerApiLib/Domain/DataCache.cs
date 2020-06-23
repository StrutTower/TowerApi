using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TowerApiLib.Domain {
    public class DataCache {
        [Key]
        public string EntityID { get; set; }

        [Key]
        public CacheType CacheTypeID { get; set; } 

        public DateTime StoredOn { get; set; }

        public string JsonData { get; set; }


        public T Deserialize<T>() {
            return JsonConvert.DeserializeObject<T>(JsonData);
        }
    }
}
