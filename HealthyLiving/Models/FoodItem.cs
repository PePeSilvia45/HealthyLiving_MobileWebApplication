using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HealthyLiving.Models
{
    public class FoodItem
    {
        [Key]
        public int FoodItemId { get; set; }

        [Display(Name ="Food Item")]
        public string FoodItemName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name ="Food Item Information")]
        public string FoodItemInformation { get; set; }

        [Display(Name ="Calorie Count")]
        public int CalorieCount { get; set; }

        [Display(Name ="Image File Name")]
        public string ImageUrl { get; set; }

        //navigation prop 
        [Display(Name ="Food Group")]
        [ForeignKey("FoodGroup")]
        public int FoodGroupId { get; set; }
        public FoodGroup FoodGroup { get; set; }
    }
}