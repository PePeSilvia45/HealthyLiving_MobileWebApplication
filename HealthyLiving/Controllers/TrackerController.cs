using HealthyLiving.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HealthyLiving.Controllers
{
    public class TrackerController : Controller
    {
        private HealthyLivingDbContext context = new HealthyLivingDbContext();

        // GET: Tracker
        [Authorize(Roles = "Member")]
        public ActionResult Tracker()
        {
            string userId = User.Identity.GetUserId();

            if (userId != null)
            {
                Intake dailyIntake = context.Intakes
                        .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                        .SingleOrDefault(d => d.UserId.Equals(userId));

                ViewBag.DailyIntake = dailyIntake;
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddCalories(int? getCalories)
        {
            if (getCalories != null)
            {
                if (getCalories < 0)
                {
                    TempData["AlertMessage"] = "Your calories input was unsuccessful...";
                    TempData["AlertSuccess"] = "fail";

                    return RedirectToAction("Tracker");
                }
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

                //get the current user's ID
                string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;

                Intake dailyIntake = context.Intakes
                            .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                            .SingleOrDefault(d => d.UserId.Equals(userId));
                dailyIntake.AddToTotalDailyCalorieIntake(getCalories.Value);

                context.Entry(dailyIntake).State = EntityState.Modified;
                context.SaveChanges();

                TempData["AlertMessage"] = "Your calories have been successfully updated... ";
                TempData["AlertSuccess"] = "success";

                return RedirectToAction("Tracker");
            }
            return HttpNotFound();

        }

        public ActionResult ResetCalories()
        {
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

            //get current users userId
            string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;

            //get that users current days intake
            Intake dailyIntake = context.Intakes
                            .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                            .SingleOrDefault(d => d.UserId.Equals(userId));
            dailyIntake.ResetTotalCalorieIntake();

            context.Entry(dailyIntake);
            context.SaveChanges();

            TempData["AlertMessage"] = "Calorie Intake has been successfully reset... ";
            TempData["AlertSuccess"] = "success";

            return RedirectToAction("Tracker");
        }



        [HttpPost]
        public ActionResult AddWater(int? getWater)
        {
            if (getWater != null)
            {
                if (getWater < 0)
                {
                    TempData["AlertMessage"] = "Your water input was unsuccessful...";
                    TempData["AlertSuccess"] = "fail";

                    return RedirectToAction("Tracker");
                }
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

                //get the current user's ID
                string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;

                Intake dailyIntake = context.Intakes
                            .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                            .SingleOrDefault(d => d.UserId.Equals(userId));
                dailyIntake.AddToTotalDailyWaterIntake(getWater.Value);

                context.Entry(dailyIntake).State = EntityState.Modified;
                context.SaveChanges();

                TempData["AlertMessage"] = "Your water intake has been successfully updated... ";
                TempData["AlertSuccess"] = "success";

                return RedirectToAction("Tracker");
            }
            return HttpNotFound();
        }
        public ActionResult ResetWater()
        {
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

            //get current users userId
            string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;

            //get that users current days intake
            Intake dailyIntake = context.Intakes
                            .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                            .SingleOrDefault(d => d.UserId.Equals(userId));
            dailyIntake.ResetTotalWaterIntake();

            context.Entry(dailyIntake);
            context.SaveChanges();

            TempData["AlertMessage"] = "Water Intake has been successfully reset... ";
            TempData["AlertSuccess"] = "success";

            return RedirectToAction("Tracker");
        }

        [HttpPost]
        public ActionResult AddSteps(int? getSteps)
        {
            if (getSteps != null)
            {
                if (getSteps < 0)
                {
                    TempData["AlertMessage"] = "Your steps input was unsuccessful...";
                    TempData["AlertSuccess"] = "fail";

                    return RedirectToAction("Tracker");
                }
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

                //get the current user's ID
                string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;

                Intake dailyIntake = context.Intakes
                            .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                            .SingleOrDefault(d => d.UserId.Equals(userId));
                dailyIntake.AddToTotalStepsRecorded(getSteps.Value);

                context.Entry(dailyIntake).State = EntityState.Modified;
                context.SaveChanges();

                TempData["AlertMessage"] = "Your steps have been successfully updated... ";
                TempData["AlertSuccess"] = "success";

                return RedirectToAction("Tracker");
            }
            return HttpNotFound();
        }

        public ActionResult ResetSteps()
        {
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

            //get current users userId
            string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;

            //get that users current days intake
            Intake dailyIntake = context.Intakes
                            .Where(d => DbFunctions.TruncateTime(d.IntakeDate) == DateTime.Today)
                            .SingleOrDefault(d => d.UserId.Equals(userId));
            dailyIntake.ResetTotalStepsTaken();

            context.Entry(dailyIntake);
            context.SaveChanges();

            TempData["AlertMessage"] = "Steps taken has been successfully reset... ";
            TempData["AlertSuccess"] = "success";

            return RedirectToAction("Tracker");
        }
        public ActionResult History()
        {
            UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

            string userId = userManager.FindByEmail(User.Identity.GetUserName()).Id;

            var Intakes = context.Intakes.Where(d => d.UserId.Equals(userId)).ToList();

            ViewBag.Intakes = Intakes;

            return View();
        }
    }
}