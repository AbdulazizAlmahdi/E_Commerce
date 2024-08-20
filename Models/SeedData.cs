using E_commerce.Infersructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
            if (!webContext.Governorates.Any())
            {

                webContext.AddRange(StaticData.yemenGovernorates);
                webContext.SaveChanges();
            }
            if (!webContext.Directorates.Any())
            {

                webContext.AddRange(StaticData.allDirectorates);
                webContext.SaveChanges();
            }
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
            if (!webContext.Phones.Any())
            {
                for (int i = 0; i < 3; i++)
                {

                    webContext.AddRange(
                        new Phone
                        {
                            Number = "77711111" + i.ToString(),
                            CreatedAt = DateTime.UtcNow,
                            UpdatedAt = DateTime.UtcNow
                        }
                        );

                    webContext.SaveChanges();
                }
            }
            if (!webContext.Users.Any())
            {

                for (int i = 0; i < 3; i++)
                {
                    Phone phone = webContext.Phones.Single(r => r.Number == "77711111" + i.ToString());
                    Place place = webContext.Places.Single(r => r.Name == "taiz");

                    webContext.AddRange(
                    new User
                    {
                        Name = RandomString(20),
                        Address = "sana'a asbahi",
                        Password = Hashpassword.Hashedpassword("123456"),
                        Phone = phone,
                        Place = place,
                        Status = "فعال",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        JobName = StaticData.Roles[i].Name,
                        DirectorateId = 1
                    }
                    );

                    webContext.SaveChanges();
                }
            }
            if (!webContext.Helps.Any())
            {

                for (int i = 0; i < 3; i++)
                {

                    webContext.AddRange(
                    new Help
                    {
                        Subject = RandomString(20),
                        Details = RandomString(20),
                        Phone = "77711111" + i.ToString(),
                        Status = "Pending",
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    }
                    );

                    webContext.SaveChanges();
                }
            }
            if (!webContext.Categories.Any())
            {
                for (int i = 0; i < 3; i++)
                {
                    webContext.AddRange(
                        new Category
                        {
                            Name = RandomString(10),
                            UpdatedAt = DateTime.UtcNow,
                            DeletedAt = DateTime.UtcNow,
                            UserId = i + 1
                        });

                    webContext.SaveChanges();
                }
            }
            if (!webContext.Products.Any())
            {
                IList<Category> categories = webContext.Categories.ToList();
                foreach (Category category in categories)
                {

                    for (int i = 0; i < 3; i++)
                    {
                        webContext.Add(
                            new Product
                            {
                                NameAr = RandomString(15),
                                CategoryId = category.Id,
                                Price = 200,
                                DetailsAr = RandomString(20),
                                DetailsEn = RandomString(20),
                                Duration = 3,
                                Status = "فعال",
                                Quantity = 20,
                                Address = "صنعاء سوق علي محسن",
                                Unit = "سلة",
                                CreatedAt = DateTime.UtcNow,
                                DirectorateId = 250
                            }
                            );

                        webContext.SaveChanges();
                    }
                }
            }

            if (!webContext.Products.Any())
            {
                IList<Category> categories = webContext.Categories.ToList();
                foreach (Category category in categories)
                {

                    for (int i = 0; i < 3; i++)
                    {
                        webContext.AddRange(
                            new Product
                            {
                                NameAr = RandomString(15),
                                CategoryId = category.Id,
                                Price = 200,
                                DetailsAr = RandomString(20),
                                DetailsEn = RandomString(20),
                                Duration = 3,
                                Status = "فعال",
                                Quantity = 20,
                                Address = " سوق علي محسن",
                                Unit = "سلة",
                                CreatedAt = DateTime.UtcNow,
                                UserId = category.Id,
                                DirectorateId = 250
                            }
                            );

                        webContext.SaveChanges();
                    }
                }
            }

            /*if (!webContext.Auctions.Any())
            {
                IList<Product> products = webContext.Products.ToList();
                foreach (Product product in products)
                {

                    for (int i = 0; i < 3; i++)
                    {
                        webContext.AddRange(
                            new Auction
                            {
                                StartDate = DateTime.UtcNow,
                                EndDate = DateTime.UtcNow.AddDays(3),
                                ProductId = product.Id,
                                StartPrice = 200,
                            }
                            );

                        webContext.SaveChanges();
                    }
                }
            }*/

            if (!webContext.Purchases.Any())
            {
                IList<Product> products = webContext.Products.ToList();
                foreach (Product product in products)
                {

                    webContext.Add(
                        new Purchase
                        {
                            Status = true,
                            Amount = 50,
                            ExtraAmount = 0,
                            Address = "sana'a",
                            Phone = "777339975",
                            Detials = "رقم الحوالة 123456789",
                            CreatedAt = DateTime.UtcNow,
                            UserId = 1,

                        }
                        );

                    webContext.SaveChanges();

                }
            }
        }
    }
}