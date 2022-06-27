using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HealthyLiving.Models
{
    public class FoodGroup
    {
        [Key]
        public int FoodGroupId { get; set; }

        [Display(Name ="Food Group")]
        public string FoodGroupName { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "Food Group Information")]
        public string FoodGroupInformation { get; set; }

        [Display(Name = "Food Group Image")]
        public string FoodGroupImageUrl { get; set; }
        
        
        //navigation prop
        public List<FoodItem> FoodItems { get; set; }
    }
}