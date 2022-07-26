using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_commerce.Models
{
    public static class SeedData
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static void Seeddb(WebContext webContext)
        {
            webContext.Database.Migrate();

            if (!webContext.Permissions.Any())
            {
                webContext.AddRange(
                    new Permission
                    {
                        Name = "User",
                    },
                    new Permission
                    {
                        Name = "producr",
                    },
                    new Permission
                    {
                        Name = "Auction",
                    },
                    new Permission
                    {
                        Name = "Category",
                    },
                    new Permission
                    {
                        Name = "Help",
                    },
                    new Permission
                    {
                        Name = "Payment",
                    },
                    new Permission
                    {
                        Name = "Payment",
                    },
                    new Permission
                    {
                        Name = "Purchase",
                    }
                    );

                webContext.SaveChanges();
            }
            if (!webContext.Places.Any())
            {
                webContext.AddRange(
                    new Place
                    {
                        Name = "sana'a",

                    },
                    new Place
                    {
                        Name = "taiz",

                    },
                    new Place
                    {
                        Name = "ibb",

                    }
                    );

                webContext.SaveChanges();
            }
            if (!webContext.UserStatuses.Any())
            {
                webContext.AddRange(
                    new UserStatus
                    {
                        Name = "active",

                    },
                    new UserStatus
                    {
                        Name = "not-active",

                    }
                    );

                webContext.SaveChanges();
            }
            if (!webContext.Phones.Any())
            {
                for(int i = 0; i < 500; i++)
                {

                    webContext.AddRange(
                        new Phone
                        {
                            Number = "+96777711111" + i.ToString(),
                            CreatedAt=DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        }
                        );

                    webContext.SaveChanges();
                }
            }
            if (!webContext.Users.Any())
            {
                
                for (int i = 0; i < 500; i++)
                {
                    Phone phone = webContext.Phones.Single(r => r.Number == "+96777711111"+i.ToString());
                    Place place = webContext.Places.Single(r => r.Name == "taiz");
                    UserStatus userStatusOne = webContext.UserStatuses.Single(r => r.Name == "active");

                    webContext.AddRange(
                    new User
                    {
                        Name = RandomString(20),
                        Address = "sana'a asbahi",
                        Password = "abc123",
                        Phone = phone,
                        Place = place,
                        UserStatus = userStatusOne,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                    );

                    webContext.SaveChanges();
                }
            }
            if (!webContext.Helps.Any())
            {
                
                for (int i = 0; i < 500; i++)
                {
                    Phone phone = webContext.Phones.Single(r => r.Number == "+96777711111"+i.ToString());

                    webContext.AddRange(
                    new Help
                    {
                        Subject = RandomString(20),
                        Details = RandomString(20),
                        Phone = phone,
                        Status= "Pending",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                    );

                    webContext.SaveChanges();
                }
            }
            if (!webContext.Categories.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    webContext.AddRange(
                        new Category
                        {
                            Name = RandomString(10),
                            UpdatedAt = DateTime.UtcNow,
                            DeletedAt = DateTime.UtcNow,
                        });

                    webContext.SaveChanges();
                }
            }
            if (!webContext.Products.Any())
            {
                IList<Category> categories = webContext.Categories.ToList();
                foreach (Category category in categories)
                {

                    for (int i = 0; i < 50; i++)
                    {
                        webContext.AddRange(
                            new Product
                            {
                                NameAr = RandomString(15),
                                NameEn = RandomString(15),
                                CategoryId = category.Id,
                                Price = 200,
                                DetailsAr = RandomString(20),
                                DetailsEn = RandomString(20),
                                Duration = 3,
                                Image = "no image",
                                Status = true,
                                Quantity = 20,
                                Unit = "سلة",
                                Views = 2,
                                Discount = 20,
                                Evaluation = 5,
                                CreatedAt = DateTime.UtcNow,
                                UpdatedAt = DateTime.UtcNow,
                                DeletedAt = DateTime.UtcNow
                            }
                            );

                        webContext.SaveChanges();
                    }
                }
            }          
        }
    }
}