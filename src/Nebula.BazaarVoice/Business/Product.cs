using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nebula.BazaarVoice.Business {
    public class Product {

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryId { get; set; }
        public double AverageOverallRating { get; set; }
    }
}
