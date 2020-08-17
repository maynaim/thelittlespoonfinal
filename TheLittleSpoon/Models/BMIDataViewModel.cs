using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using ServiceReference1;

namespace TheLittleSpoon.Models
{
    public class BMIFormData
    {
        public double UserWeight { get; set; }

        public double UserHieght { get; set; }

        public BmiReport BMIReport { get; set; }
    }
}
