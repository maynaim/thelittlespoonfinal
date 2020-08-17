using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheLittleSpoon.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public List<Article> Articles { get; set; }

        public int ColorARGB { get; private set; }

        [NotMapped()]
        public System.Drawing.Color Color
        {
            get { return System.Drawing.Color.FromArgb(this.ColorARGB); }
            set { ColorARGB = value.ToArgb(); }
        }

        [NotMapped()]
        public string HexColor { get { return "#" + Color.R.ToString("X2") + Color.G.ToString("X2") + Color.B.ToString("X2"); } }
    }
}
