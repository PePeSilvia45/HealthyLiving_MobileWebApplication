using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HealthyLiving.Models
{
    public class Intake
    {
        [Key]
        public int CalorieIntakeId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime IntakeDate { get; set; }


        //CALORIE INTAKE
        public int TotalDailyCalorieIntake { get; set; }

        //calc total calorie intake
        public void AddToTotalDailyCalorieIntake(int intakeAmount)
        {
            TotalDailyCalorieIntake += intakeAmount;
        }

        //set calorie intake
        public void SetCalorieIntake(int intakeAmount)
        {
            TotalDailyCalorieIntake = intakeAmount;
        }

        //reset calorie intake
        public void ResetTotalCalorieIntake()
        {
            TotalDailyCalorieIntake = 0;
        }


        //WATER INTAKE
        public int TotalDailyWaterIntake { get; set; }

        //calc total water intake
        public void AddToTotalDailyWaterIntake(int amountIn)
        {
            TotalDailyWaterIntake += amountIn;
        }

        //set water intake
        public void SetWaterIntake(int setAmount)
        {
            TotalDailyWaterIntake = setAmount;
        }

        //reset water intake
        public void ResetTotalWaterIntake()
        {
            TotalDailyWaterIntake = 0;
        }


        //STEPS TAKEN
        public int TotalStepsTaken { get; set; }

        //calc total steps taken
        public void AddToTotalStepsRecorded(int amountIn)
        {
            TotalStepsTaken += amountIn;
        }

        //set steps taken
        public void SetStepTaken(int setAmount)
        {
            TotalStepsTaken = setAmount;
        }

        //reset steps taken
        public void ResetTotalStepsTaken()
        {
            TotalStepsTaken = 0;
        }

        //naviational property
        [ForeignKey("User")]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}