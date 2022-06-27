using HealthyLiving.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthyLiving.Controllers
{
    public class HomeController : Controller
    {
        private HealthyLivingDbContext context = new HealthyLivingDbContext();

        public ActionResult SplashScreen()
        {
            return View();
        }

        public ActionResult Index()
        {
            if (User.IsInRole("Member"))
            {
                var foodItems = context.FoodItems.ToList();
                foreach (var item in foodItems)
                {
                    context.Entry(item).Reload();
                }


                string userId = User.Identity.GetUserId();

                if (userId != null)
                {
                    if (context.Intakes
                        .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                        .SingleOrDefault(d => d.UserId.Equals(userId)) != null)
                    {
                        Intake dailyIntakes = context.Intakes
                            .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                            .SingleOrDefault(d => d.UserId.Equals(userId));


                        ViewBag.DailyIntakes = dailyIntakes;
                    }
                    else
                    {
                        return RedirectToAction("newDay");
                    }

                }
            }

            return View();
        }


        public ActionResult newDay()
        {
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

            string userId = User.Identity.GetUserId();

            User user = context.Users.Find(userId);

            Intake newCal = new Intake
            {
                IntakeDate = DateTime.Today,
                TotalDailyCalorieIntake = 0,
                TotalDailyWaterIntake = 0,
                TotalStepsTaken = 0,
                User = user,

            };

            context.Intakes.Add(newCal);
            context.SaveChanges();

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult Contact()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Contact(Message message)
        {
            if (ModelState.IsValid)
            {
                message.DateOfMessage = DateTime.Now.Date;
                context.Messages.Add(message);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(message);
        }

        [HttpGet]
        public ActionResult Podcast()
        {
            //create a list for the podcasts to be passed to the view
            List<Podcast> podcasts = new List<Podcast>();

            //bring podcasts into the list
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep.1 Introduction", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_1_Introducing_5_2_December_2014.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep.2 Planning", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_2_Planning_for_your_first_fast.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep.3 Your fast day", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_3_Your_fast_day_survival_guide.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep.4 What to Eat on a Fast Day", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_4_What_to_Eat_on_a_Fast_Day_-_a_menu_of_food_ideas_Jan_2015.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep.5 How to love your food without overdoing it", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_5_-_how_to_love_your_food_on_non-fast_days_without_overdoing_it.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep.6 Your flexible", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_6_Your_flexible_5_2_diet.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep.7 Pippa story", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_7_Pippa_story_plus_advice_on_weighing_in.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep 8 Mr and Mrs and their weight loss", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_8_Mr_and_Mrs_5_2_and_their_57_kilo_weight_loss.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep 9 Foodie fasting in France", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_9_Foodie_fasting_in_France_Belinda_Berry.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep 10 Simones guide to losing weight with a disability", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_10_Simones_guide_to_losing_weight_with_a_disability.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep 11 Sharons quest", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_11_Sharons_quest_to_lose_11_stone_or_70kg.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep 12 Trudies success", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_12_Trudies_success_after_27_years_of_yoyo_dieting.mp3" });
            podcasts.Add(new Podcast { Name = "Diet Podcast Ep 16 QA", FilePath = "~/Podcasts/5_2_Diet_Podcast_Ep_16_QA" });

            return View(podcasts);
        }

        public ActionResult UnderstandingCalories()
        {
            return View();
        }

        public ActionResult FoodGroups()
        {
            ViewBag.FoodGroups = context.FoodGroups.ToList();
            return View();
        }


        public ActionResult ViewItems(int? id)
        {
            if (id != null)
            {
                //get all food items in selected catagory
                var foodItems = context.FoodItems.Where(p => p.FoodGroupId == id).ToList();
                if (foodItems != null)
                {
                    ViewBag.FoodItems = foodItems;
                    //get the last food item in the list
                    ViewBag.LastItem = foodItems[foodItems.Count() - 1];
                }

                //send foodgroup to the view
                FoodGroup foodGroup = context.FoodGroups.Find(id);
                if (foodGroup != null) ViewBag.FoodGroup = foodGroup;
            }

            return View();
        }
    }
}