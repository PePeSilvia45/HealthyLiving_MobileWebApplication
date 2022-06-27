using System;
using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Linq;
using System.Web;

namespace HealthyLiving.Models
{
    public class DatabaseInitializer : DropCreateDatabaseIfModelChanges<HealthyLivingDbContext>
    {
        protected override void Seed(HealthyLivingDbContext context)
        {
            if (!context.Users.Any())
            {
                //create roles and store in database
                //__________________________________

                //create role manager object to create and store roles
                RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                // if no admin role found
                if (!roleManager.RoleExists("Admin"))
                {
                    //create admin role
                    roleManager.Create(new IdentityRole("Admin"));
                }

                // if no member role found
                if (!roleManager.RoleExists("Member"))
                {
                    //create member role
                    roleManager.Create(new IdentityRole("Member"));
                }

                //save
                context.SaveChanges();

                //======================
                // SEED USERS DATABASE =
                //======================

                //Create some users and give them roles

                //create userManager to create users and store them in the database
                UserManager<User> userManager = new UserManager<User>(new UserStore<User>(context));

                //if no user with admin email exist
                if (userManager.FindByName("admin@healthyliving.com") == null)
                {
                    //relaxed password validator
                    userManager.PasswordValidator = new PasswordValidator()
                    {
                        RequireDigit = false,
                        RequiredLength = 1,
                        RequireLowercase = false,
                        RequireNonLetterOrDigit = false,
                        RequireUppercase = false
                    };

                    //create admin user
                    var admin = new User()
                    {
                        UserName = "admin@healthyliving.com",
                        Email = "admin@healthyliving.com",
                        FirstName = "James",
                        LastName = "Park",
                        EmailConfirmed = true,
                        PhoneNumber = "784548654"
                    };

                    //add the password to user
                    userManager.Create(admin, "admin123");

                    //add user to admin role
                    userManager.AddToRole(admin.Id, "Admin");
                }

                //Create some members

                var member1 = new User()
                {
                    UserName = "member1@gmail.com",
                    Email = "member1@gmail.com",
                    FirstName = "John",
                    LastName = "Smith",
                    EmailConfirmed = true,
                    PhoneNumber = "728451245"
                };

                if (userManager.FindByName("member1@gmail.com") == null)
                {
                    userManager.Create(member1, "Password1");
                    userManager.AddToRole(member1.Id, "Member");
                }

                var member2 = new User()
                {
                    UserName = "member2@gmail.com",
                    Email = "member2@gmail.com",
                    FirstName = "Jane",
                    LastName = "Doe",
                    EmailConfirmed = true,
                    PhoneNumber = "7452165312"
                };

                if (userManager.FindByName("member2@gmail.com") == null)
                {
                    userManager.Create(member2, "Password2");
                    userManager.AddToRole(member2.Id, "Member");
                }

                var member3 = new User()
                {
                    UserName = "member3@gmail.com",
                    Email = "member3@gmail.com",
                    FirstName = "Peter",
                    LastName = "Popper",
                    EmailConfirmed = true,
                    PhoneNumber = "7846512342"
                };

                if (userManager.FindByName("member3@gmail.com") == null)
                {
                    userManager.Create(member3, "Password3");
                    userManager.AddToRole(member3.Id, "Member");
                }

                //============================
                // SEED FOOD GROUPS DATABASE =
                //============================

                // create food groups
                var dairy = new FoodGroup { 
                    FoodGroupName = "Dairy", 
                    FoodGroupInformation = "Milk and dairy products, such as cheese and yoghurt, are great sources of protein and calcium. They can form part of a healthy, balanced diet. \n Unsweetened calcium - fortified dairy alternatives like soya milks, soya yoghurts and soya cheeses also count as part of this food group.These can make good alternatives to dairy products. \n To make healthier choices, go for lower fat and lower sugar options.",
                    FoodGroupImageUrl = "Images/FoodGroups/dairy.png" 
                };
                var fruit = new FoodGroup {
                    FoodGroupName = "Fruit",
                    FoodGroupInformation = "Any fruit or 100% fruit juice counts as part of the Fruit Group. <br />Fruits may be fresh, frozen, canned, or dried/dehydrated, and may be whole, cut-up, pureed, or cooked.\nAt least half of the recommended amount of fruit should come from whole fruit, rather than 100% fruit juice.",
                    FoodGroupImageUrl = "Images/FoodGroups/fruit.png" 
                };
                var grain = new FoodGroup { 
                    FoodGroupName = "Grain",
                    FoodGroupInformation = "Grain foods are mostly made from wheat, oats, rice, rye, barley, millet, quinoa and corn. \nThe different grains can be cooked and eaten whole, ground into flour to make a variety of cereal foods like bread, pasta and noodles, or made into ready-to-eat breakfast cereals.",
                    FoodGroupImageUrl = "Images/FoodGroups/grain.png"
                };
                var protein = new FoodGroup { 
                    FoodGroupName = "Protein", 
                    FoodGroupInformation = "All foods made from seafood; meat, poultry, and eggs; beans, peas, and lentils; and nuts, seeds, and soy products are part of the Protein Foods Group.",
                    FoodGroupImageUrl = "Images/FoodGroups/protein.png"
                };
                var vegetables = new FoodGroup { 
                    FoodGroupName = "Vegetables", 
                    FoodGroupInformation = "Any vegetable or 100% vegetable juice counts as part of the Vegetable Group. <br />Vegetables may be raw or cooked; fresh, frozen, canned, or dried/dehydrated; and may be whole, cut-up, or mashed.",
                    FoodGroupImageUrl = "Images/FoodGroups/vegetables.png"
                };

                // add to database
                context.FoodGroups.Add(dairy);
                context.FoodGroups.Add(fruit);
                context.FoodGroups.Add(grain);
                context.FoodGroups.Add(protein);
                context.FoodGroups.Add(vegetables);

                //save context changes
                context.SaveChanges();

                //===========================
                // SEED FOOD ITEMS DATABASE =
                //===========================
                // seed database with dairy foods
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Milk",
                    FoodItemInformation = "Whole milk has the rich, creamy taste you and your family love, with tons of nutrients and just 8 grams of fat per 8-ounce glass.",
                    CalorieCount = 47,
                    ImageUrl = "Images/FoodGroups/Dairy/milk.png",
                    FoodGroup = dairy
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Cheese",
                    FoodItemInformation = "nutritious food consisting primarily of the curd, the semisolid substance formed when milk curdles, or coagulates.",
                    CalorieCount = 402,
                    ImageUrl = "Images/FoodGroups/Dairy/cheese.png",
                    FoodGroup = dairy
                }); 
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Yogurt",
                    FoodItemInformation = "Yogurts can be high in protein, calcium, vitamins, and live culture, or probiotics, which can enhance the gut microbiota.",
                    CalorieCount = 59,
                    ImageUrl = "Images/FoodGroups/Dairy/yogurt.png",
                    FoodGroup = dairy
                });; 
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Milk-Based Desserts",
                    FoodItemInformation = "Dairy desserts (excluding yogurts and ice creams) comprise a diverse range of products containing significant amounts of milk solids. They include creamy and gelled desserts, custards and puddings, sachet dessert mixes, and cheesecakes.",
                    CalorieCount = 130,
                    ImageUrl = "Images/FoodGroups/Dairy/milkbaseddesserts.png",
                    FoodGroup = dairy
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Calcium-Fortified Soy Milk",
                    FoodItemInformation = "Natural soy milk contains only 200 mg calcium per liter, which is 6x less than cow milk. Therefore, most commercial soy milks are fortified with extra calcium up to a level 1200 mg/L, which is the same as that of cow milk.",
                    CalorieCount = 98,
                    ImageUrl = "Images/FoodGroups/Dairy/calciumfortifiedsoymilk.png",
                    FoodGroup = dairy
                });
                context.SaveChanges();

                //seed database with fruit items
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Apple",
                    FoodItemInformation = "Apples are a popular fruit, containing antioxidants, vitamins, dietary fiber, and a range of other nutrients.",
                    CalorieCount = 52,
                    ImageUrl = "Images/FoodGroups/Fruit/apple.png",
                    FoodGroup = fruit
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Banana",
                    FoodItemInformation = "Bananas are one of the most popular fruits worldwide. They contain essential nutrients that can have a protective impact on health.",
                    CalorieCount = 89,
                    ImageUrl = "Images/FoodGroups/Fruit/banana.png",
                    FoodGroup = fruit
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Grapes",
                    FoodItemInformation = "Grapes are small, oval fruits that grow in bunches on vines. Depending on the variety of the grape, they may be eaten fresh, dried to make raisins, or used to make wine, jam, juice or vinegar.",
                    CalorieCount = 67,
                    ImageUrl = "Images/FoodGroups/Fruit/grapes.png",
                    FoodGroup = fruit
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Kiwi",
                    FoodItemInformation = "The kiwifruit, or Chinese gooseberry, originally grew wild in China. Kiwis are a nutrient-dense food — they are rich in in nutrients and low in calories.",
                    CalorieCount = 61,
                    ImageUrl = "Images/FoodGroups/Fruit/kiwi.png",
                    FoodGroup = fruit
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Mango",
                    FoodItemInformation = "A mango is a sweet tropical fruit, and it's also the name of the trees on which the fruit grows. Ripe mangoes are juicy, fleshy, and delicious.",
                    CalorieCount = 60,
                    ImageUrl = "Images/FoodGroups/Fruit/mango.png",
                    FoodGroup = fruit
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Melon",
                    FoodItemInformation = "Both cantaloupe and honeydew melon are good choices, though cantaloupe contains more antioxidants.",
                    CalorieCount = 36,
                    ImageUrl = "Images/FoodGroups/Fruit/melon.png",
                    FoodGroup = fruit
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Orange",
                    FoodItemInformation = "Oranges are a type of low calorie, highly nutritious citrus fruit. As part of a healthful and varied diet, oranges contribute to strong, clear skin and can help lower a person’s risk of many conditions.",
                    CalorieCount = 47,
                    ImageUrl = "Images/FoodGroups/Fruit/orange.png",
                    FoodGroup = fruit
                });
                
                //save changes
                context.SaveChanges();

                //seed database with gran foods
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Biscuits",
                    FoodItemInformation = "A biscuit is a flour-based baked and shaped food product. They are usually sweet and may be made with sugar, chocolate, icing, jam, ginger, or cinnamon.",
                    CalorieCount = 353,
                    ImageUrl = "Images/FoodGroups/Grain/biscuits.png",
                    FoodGroup = grain
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Bread",
                    FoodItemInformation = "Bread is the product of baking a mixture of flour, water, salt, yeast and other ingredients. The basic process involves mixing of ingredients until the flour is converted into a stiff paste or dough, followed by baking the dough into a loaf.",
                    CalorieCount = 265,
                    ImageUrl = "Images/FoodGroups/Grain/bread.png",
                    FoodGroup = grain
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Muffins",
                    FoodItemInformation = "A muffin is a small domed spongy bread– or cake-like baked food. There are two types of muffins: quick bread or English muffins and flatbread or American muffins.",
                    CalorieCount = 377,
                    ImageUrl = "Images/FoodGroups/Grain/muffins.png",
                    FoodGroup = grain
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Pancakes",
                    FoodItemInformation = "Pancakes are an American classic. Breakfast and brunch would simply be incomplete without these fluffy stacks of deliciousness piled high and served with syrup, butter, and all of your topping favorites.",
                    CalorieCount = 227,
                    ImageUrl = "Images/FoodGroups/Grain/pancakes.png",
                    FoodGroup = grain
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Pasta",
                    FoodItemInformation = "Pasta is a type of food typically made from an unleavened dough of wheat flour mixed with water or eggs, and formed into sheets or other shapes, then cooked by boiling or baking.",
                    CalorieCount = 131,
                    ImageUrl = "Images/FoodGroups/Grain/pasta.png",
                    FoodGroup = grain
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Popcorn",
                    FoodItemInformation = "Popcorn is a special type of corn that “pops” when exposed to heat. At the center of each kernel is a small amount of water, which expands when heated and eventually causes the kernel to explode.",
                    CalorieCount = 375,
                    ImageUrl = "Images/FoodGroups/Grain/popcorn.png",
                    FoodGroup = grain
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Rice",
                    FoodItemInformation = "Both white and brown rice contain mainly carbohydrate and some protein, with virtually no fat or sugar.",
                    CalorieCount = 130,
                    ImageUrl = "Images/FoodGroups/Grain/rice.png",
                    FoodGroup = grain
                });

                //save changes
                context.SaveChanges();

                //seed database with Proteins items
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Meats",
                    FoodItemInformation = "Researchers say that red meat contains important nutrients, including protein, vitamin B-12, and iron. However, there is evidence to suggest that eating a lot of red meat can raise a person’s risk of certain cancers, heart disease, and other health concerns.",
                    CalorieCount = 143,
                    ImageUrl = "Images/FoodGroups/Protein/meats.png",
                    FoodGroup = protein
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Poultry",
                    FoodItemInformation = "Poultry provides the body with protein, vitamin B, including thiamin, riboflavin, niacin and pyridoxine, vitamin E, zinc, iron and magnesium.",
                    CalorieCount = 272,
                    ImageUrl = "Images/FoodGroups/Protein/poultry.png",
                    FoodGroup = protein
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Seafood",
                    FoodItemInformation = "Seafood is low in saturated fats, high in protein, and packed full of important nutrients including omega-3 fatty acids, vitamin A, and B vitamins. These nutrients are essential in maintaining your health—particularly your brain, eyes, and immune system.",
                    CalorieCount = 204,
                    ImageUrl = "Images/FoodGroups/Protein/seafood.png",
                    FoodGroup = protein
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Eggs",
                    FoodItemInformation = "One egg has only 75 calories but 7 grams of high-quality protein, 5 grams of fat, and 1.6 grams of saturated fat, along with iron, vitamins, minerals, and carotenoids. The egg is a powerhouse of disease-fighting nutrients like lutein and zeaxanthin.",
                    CalorieCount = 155,
                    ImageUrl = "Images/FoodGroups/Protein/eggs.png",
                    FoodGroup = protein
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Nuts",
                    FoodItemInformation = "Nuts like almonds, pistachios, walnuts, peanuts, and hazelnuts are a great source of nutrients, such as protein, fat, fiber, vitamins, and minerals. When eaten as part of a nutrient-dense diet, nuts may reduce your risk of heart disease and support immune health, among other benefits.",
                    CalorieCount = 607,
                    ImageUrl = "Images/FoodGroups/Protein/nuts.png",
                    FoodGroup = protein
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Beans",
                    FoodItemInformation = "Beans also contain decent amounts of zinc, copper, manganese, selenium, and vitamins B1, B6, E, and K. With only 245 calories per cup (171 grams), pinto beans are one of the most nutrient-dense foods around. Many other varieties are just as impressive.",
                    CalorieCount = 347,
                    ImageUrl = "Images/FoodGroups/Protein/beans.png",
                    FoodGroup = protein
                });

                //save changes
                context.SaveChanges();

                //seed database with vegetables items
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Bell Pepper",
                    FoodItemInformation = "Bell peppers are very high in vitamin C, with a single one providing up to 169% of the RDI. Other vitamins and minerals in bell peppers include vitamin K1, vitamin E, vitamin A, folate, and potassium.",
                    CalorieCount = 26,
                    ImageUrl = "Images/FoodGroups/Vegetables/bellpepper.png",
                    FoodGroup = vegetables
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Broccoli",
                    FoodItemInformation = "Raw broccoli contains almost 90% water, 7% carbs, 3% protein, and almost no fat. Broccoli is very low in calories, providing only 31 calories per cup (91 grams).",
                    CalorieCount = 34,
                    ImageUrl = "Images/FoodGroups/Vegetables/broccoli.png",
                    FoodGroup = vegetables
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Carrots",
                    FoodItemInformation = "Carrots are a particularly good source of beta carotene, fiber, vitamin K1, potassium, and antioxidants. They also have a number of health benefits. They're a weight-loss-friendly food and have been linked to lower cholesterol levels and improved eye health.",
                    CalorieCount = 41,
                    ImageUrl = "Images/FoodGroups/Vegetables/carrots.png",
                    FoodGroup = vegetables
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Cauliflower",
                    FoodItemInformation = "Cauliflower ​​Nutrition Facts. One cup of chopped cauliflower (107g) provides 27 calories, 2.1g of protein, 5.3g of carbohydrates, and 0.3g of fat. Cauliflower is a great source of vitamin C, vitamin B6, and magnesium.",
                    CalorieCount = 25,
                    ImageUrl = "Images/FoodGroups/Vegetables/cauliflower.png",
                    FoodGroup = vegetables
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Green Peas",
                    FoodItemInformation = "Green peas have an impressive nutrition profile. Their calorie content is fairly low, with only 62 calories per 1/2-cup (170-gram) serving. About 70% of those calories come from carbs and the rest are provided by protein and a small amount of fat.",
                    CalorieCount = 81,
                    ImageUrl = "Images/FoodGroups/Vegetables/greenpeas.png",
                    FoodGroup = vegetables
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Potato",
                    FoodItemInformation = "Aside from being high in water when fresh, potatoes are primarily composed of carbs and contain moderate amounts of protein and fiber — but almost no fat.",
                    CalorieCount = 77,
                    ImageUrl = "Images/FoodGroups/Vegetables/potato.png",
                    FoodGroup = vegetables
                });
                context.FoodItems.Add(new FoodItem()
                {
                    FoodItemName = "Tomatoes",
                    FoodItemInformation = "Tomatoes are the major dietary source of the antioxidant lycopene, which has been linked to many health benefits, including reduced risk of heart disease and cancer. They are also a great source of vitamin C, potassium, folate, and vitamin K.",
                    CalorieCount = 18,
                    ImageUrl = "Images/FoodGroups/Vegetables/tomatoes.png",
                    FoodGroup = vegetables
                });

                //save changes
                context.SaveChanges();

                //===============================
                // SEED CALORIE INTAKE DATABASE =
                //===============================

                // seed member 1 data
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-7),
                    TotalDailyCalorieIntake = 2675,
                    TotalDailyWaterIntake = 3200,
                    TotalStepsTaken = 8932,
                    User = member1
                });
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-6),
                    TotalDailyCalorieIntake = 2545,
                    TotalDailyWaterIntake = 2900,
                    TotalStepsTaken = 11216,
                    User = member1
                });
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-5),
                    TotalDailyCalorieIntake = 2400,
                    TotalDailyWaterIntake = 3150,
                    TotalStepsTaken = 5642,
                    User = member1
                });
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-4),
                    TotalDailyCalorieIntake = 2340,
                    TotalDailyWaterIntake = 3155,
                    TotalStepsTaken = 9456,
                    User = member1
                });
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-3),
                    TotalDailyCalorieIntake = 2531,
                    TotalDailyWaterIntake = 2950,
                    TotalStepsTaken = 6542,
                    User = member1
                });
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-2),
                    TotalDailyCalorieIntake = 3012,
                    TotalDailyWaterIntake = 3500,
                    TotalStepsTaken = 7942,
                    User = member1
                });
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-1),
                    TotalDailyCalorieIntake = 2112,
                    TotalDailyWaterIntake = 3350,
                    TotalStepsTaken = 10215,
                    User = member1
                });
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date,
                    TotalDailyCalorieIntake = 2003,
                    TotalDailyWaterIntake = 3240,
                    TotalStepsTaken = 11315,
                    User = member1
                });

                //save changes
                context.SaveChanges();

                // seed member 2 data
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-4),
                    TotalDailyCalorieIntake = 1984,
                    TotalDailyWaterIntake = 2500,
                    TotalStepsTaken = 12045,
                    User = member2
                });
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-3),
                    TotalDailyCalorieIntake = 2130,
                    TotalDailyWaterIntake = 3100,
                    TotalStepsTaken = 11345,
                    User = member2
                });
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-2),
                    TotalDailyCalorieIntake = 2003,
                    TotalDailyWaterIntake = 2100,
                    TotalStepsTaken = 9546,
                    User = member2
                });
                context.Intakes.Add(new Intake()
                {
                    IntakeDate = DateTime.Now.Date.AddDays(-1),
                    TotalDailyCalorieIntake = 1802,
                    TotalDailyWaterIntake = 2800,
                    TotalStepsTaken = 7546,
                    User = member2
                });

                //save changes
                context.SaveChanges();
            }
        }
    }
}