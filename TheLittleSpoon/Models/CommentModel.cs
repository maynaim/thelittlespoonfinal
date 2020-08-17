using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheLittleSpoon.Models
{
    public class Comment
    {
        public int CommentId { get; set; }

        public string Author { get; set; }

        public string Content { get; set; }

        public int Sequence { get; set; }

        public DateTime DatePosted { get; set; }

        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }
    }
}
