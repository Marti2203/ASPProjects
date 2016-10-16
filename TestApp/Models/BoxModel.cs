using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestApp.Models
{
    public class BoxModel
    {

        public BoxModel()
        {
            Color = "Shitty";
        }

        public String Color { get; set; }
        public String Material { get; set; }

        public int Height { get; set; }
        public int Width { get; set; }
        public int Length { get; set; }
        public int Weight { get; set; }

    }
}