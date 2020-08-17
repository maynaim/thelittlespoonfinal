using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TheLittleSpoon.Models
{
    public class Article
    {
        public int ArticleId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }

        public string Header { get; set; }

        public string Summary { get; set; }

        public string Content { get; set; }

        public string HomeImageUrl { get; set; }

        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public string Location { get; set; }

        public bool IsShowMap { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
