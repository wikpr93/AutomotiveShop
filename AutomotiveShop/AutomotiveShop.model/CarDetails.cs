using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AutomotiveShop.model
{
    [Table("CarsDetails")]
    public class CarDetails
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid CarDetailsId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Producent { get; set; }
        [Required(ErrorMessage = "Model is required")]
        public string Model { get; set; }

        [Display(Name = "Year of production")]
        [Required(ErrorMessage = "Year of production is required")]
        public int YearOfProduction { get; set; }

        public virtual List<ProductByCar> ProductsByCar { get; set; }

        public CarDetails()
        {
            ProductsByCar = new List<ProductByCar>();
        }
    }
}