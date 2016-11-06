using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using CommonFiles.Resource;
namespace TestApp.Models
{
    public class BoxModel
    {

        public BoxModel()
        {
            ID = Guid.NewGuid().ToString();
        }

        public string ID { get; set; }

        [Display(Name = "Colour", ResourceType = typeof(BoxResources))]
        [MaxLength(30, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string Colour { get; set; }

        [Display(Name = "Height", ResourceType = typeof(BoxResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public int? Height { get; set; }

        [Display(Name = "Width", ResourceType = typeof(BoxResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public int? Width { get; set; }

        [Display(Name = "Length", ResourceType = typeof(BoxResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public int? Length { get; set; }

        [Display(Name = "Material", ResourceType = typeof(BoxResources))]
        [MaxLength(30, ErrorMessageResourceName = "MaxLength", ErrorMessageResourceType = typeof(ErrorResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public string Material { get; set; }

        [Display(Name = "Weight", ResourceType = typeof(BoxResources))]
        [Required(ErrorMessageResourceName = "Required", ErrorMessageResourceType = typeof(ErrorResources))]
        public int? Weight { get; set; }
    }
}