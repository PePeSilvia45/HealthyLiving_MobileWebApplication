using HealthyLiving.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthyLiving.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private HealthyLivingDbContext context = new HealthyLivingDbContext();

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var foodGroups = context.FoodGroups.ToList();
                ViewBag.FoodGroups = foodGroups;
                ViewBag.LastItem = foodGroups[foodGroups.Count() - 1];

            }
            return View("Index");
        }
        //==================================================================================================================
        //                                      FOOD GROUPS
        //==================================================================================================================
        public ActionResult DetailsGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            //find food group
            FoodGroup foodGroup = context.FoodGroups.Find(id);

            if (foodGroup == null)
            {
                return HttpNotFound();
            }

            //send food group to view
            return View(foodGroup);
        }

        public ActionResult EditGroup(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            //find food group
            FoodGroup foodGroup = context.FoodGroups.Find(id);

            if (foodGroup == null)
            {
                return HttpNotFound();
            }

            //send food group to view
            return View(foodGroup);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditGroup([Bind(Include = "FoodGroupId, FoodGroupName, FoodGroupInformation, FoodGroupImageUrl")] FoodGroup foodGroup)
        {
            if (ModelState.IsValid)
            {
                context.Entry(foodGroup).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(foodGroup);
        }

        public ActionResult DeleteGroup(int? id)
        {
            //get food group to be removed
            FoodGroup foodGroup = context.FoodGroups.Find(id);
            //also get all items from that group to be removed
            var foodItems = context.FoodItems.Where(p => p.FoodGroupId == id).ToList();
            foreach (var item in foodItems)
            {
                //remove each item
                context.FoodItems.Remove(item);
            }
            //remove group
            context.FoodGroups.Remove(foodGroup);
            //save
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult ViewItems(int? id)
        {
            if (id != null)
            {
                //get all food items in a category
                var foodItems = context.FoodItems.Where(p => p.FoodGroupId == id).ToList();
                //get food group
                FoodGroup foodGroup = context.FoodGroups.Find(id);

                if (foodGroup != null)
                {
                    //send the food group in viewbag
                    ViewBag.FoodGroup = foodGroup;
                }
                //put food items in view bag
                ViewBag.FoodItems = foodItems;
                //put the last item in the list in a viewbag.
                ViewBag.lastItem = foodItems[foodItems.Count() - 1];
            }

            return View();
        }

        //==================================================================================================================
        //                                      FOOD ITEMS
        //==================================================================================================================
        public ActionResult DetailsItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            //get all food items in a category
            FoodItem foodItem = context.FoodItems.Find(id);
            FoodGroup foodGroup = context.FoodGroups.Find(foodItem.FoodGroupId);

            ViewBag.FoodGroup = foodGroup;

            if (foodItem == null)
            {
                return HttpNotFound();
            }

            //send food group to view
            return View(foodItem);
        }

        public ActionResult EditItem(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }

            //get all food items in a category
            FoodItem foodItem = context.FoodItems.Find(id);

            if (foodItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodGroupId = new SelectList(context.FoodGroups, "FoodGroupId", "FoodGroupName");

            //send food group to view
            return View(foodItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditItem(FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                context.Entry(foodItem).State = EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("ViewItems", new {id = foodItem.FoodGroupId});
            }
            return View(foodItem);
        }

        public ActionResult DeleteItem(int? id)
        {
            //get food group to be removed
            FoodItem foodItem = context.FoodItems.Find(id);

            //remove item
            context.FoodItems.Remove(foodItem);
            //save
            context.SaveChanges();

            return RedirectToAction("ViewItems", new {id = foodItem.FoodGroupId});
        }


        public ActionResult CreateFoodItem()
        {
            ViewBag.FoodGroupId = new SelectList(context.FoodGroups, "FoodGroupId", "FoodGroupName");
            return View();
        }

        [HttpPost]
        public ActionResult CreateFoodItem(FoodItem foodItem)
        {

            if (ModelState.IsValid)
            {
                context.FoodItems.Add(foodItem);
                context.SaveChanges();

                return View("DetailsItem", foodItem);
            }

            return View(foodItem);
        }

    }
}