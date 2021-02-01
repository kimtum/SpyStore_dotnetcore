using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using SpyStore.Models.Entities;
using SpyStore.Models.Entities.Base;

namespace SpyStore.Dal.Initialization
{
    public static class SampleData
    {
        public static IEnumerable<Category> GetCategories() =>
            new List<Category>
            {
                new Category
                {
                    CategoryName = "Communications",
                    Products = new List<Product>
                    {
                        new Product
                        {
                            Details = new ProductDetails
                            {
                                ProductImage = "product-image.png",
                                ProductImageLarge = "product-image-lg.png",
                                ProductImageThumb = "product-thumb.png",
                                ModelName = "Communications Device",
                                Description = "Subversively stay in touch with this miniaturized" +
                                    "wireless communications device. Speak into the" +
                                    "pointy end and listen with the other end! Voiceactivated" +
                                    "dialing makes calling for backup a" +
                                    "breeze. Excellent for undercover work at schools" +
                                    "rest homes, and most corporate headquarters. Comesin assorted colors.",
                                ModelNumber = "RED1",
                            },
                            UnitCost = 49.99M,
                            CurrentPrice = 49.99M,
                            UnitsInStock = 2,
                            IsFeatured = true
                        },
                        new Product
                        {
                            Details = new ProductDetails
                            {
                                ProductImage = "product-image.png",
                                ProductImageLarge = "product-image-lg.png",
                                ProductImageThumb = "product-thumb.png",
                                ModelName = "Persuasive Pencil",
                                Description = "Persuade anyone to see your point of view!" +
                                    "Captivate your friends and enemies alike!" +
                                    "Draw the crime-scene or map out the chain of events." +
                                    "All you need is several years of training orcopious amounts of natural talent." +
                                    "You’re halfwaythere with the Persuasive Pencil. Purchase this" +
                                    "item with the Retro Pocket Protector Rocket Packfor optimum disguise.",
                                ModelNumber = "LK4TLNT",
                            },
                            UnitCost = 1.99M,
                            CurrentPrice = 1.99M,
                            UnitsInStock = 5,
                        }
                    }
                }
            };

        //public static IEnumerable<Customer> GetAllCustomerRecords(IList<Product> products) =>
        //    new List<Customer>
        //    {
        //        new Customer()
        //        {
        //            EmailAddress = "spy@secrets.com",
        //            Password = "Foo",
        //            FullName = "Super Spy",
        //            Orders = new List<Order>
        //            {
        //                new Order()
        //                {
        //                    OrderDate = DateTime.Now.Subtract(new TimeSpan(20, 0, 0, 0)),
        //                    ShipDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
        //                    OrderDetails = new List<OrderDetail>
        //                    {
        //                        new OrderDetail()
        //                        {
        //                            ProductNavigation = products[0],
        //                            Quantity = 3,
        //                            UnitCost = products[0].CurrentPrice
        //                        },
        //                        new OrderDetail()
        //                        {
        //                            ProductNavigation = products[1],
        //                            Quantity = 2,
        //                            UnitCost = products[1].CurrentPrice
        //                        },
        //                        new OrderDetail()
        //                        {
        //                            ProductNavigation = products[2],
        //                            Quantity = 5,
        //                            UnitCost = products[3].CurrentPrice
        //                        },
        //                    }
        //                }
        //            },
        //            ShoppingCartRecords = new List<ShoppingCartRecord>
        //            {
        //                new ShoppingCartRecord
        //                {
        //                    DateCreated = DateTime.Now,
        //                    ProductNavigation = products[3],
        //                    Quantity = 1,
        //                    LineItemTotal = products[3].CurrentPrice
        //                }
        //            }
        //        }    
        //    };

        public static IEnumerable<Customer> GetAllCustomerRecords(IList<Product> products) =>
            new List<Customer>
            {
                new Customer()
                {
                    EmailAddress = "spy@secrets.com",
                    Password = "Foo",
                    FullName = "Super Spy",
                    Orders = new List<Order>
                    {
                        new Order()
                        {
                            OrderDate = DateTime.Now.Subtract(new TimeSpan(20, 0, 0, 0)),
                            ShipDate = DateTime.Now.Subtract(new TimeSpan(5, 0, 0, 0)),
                            OrderDetails = new List<OrderDetail>
                            {
                                new OrderDetail() 
                                {
                                    ProductNavigation = products[0],
                                    Quantity = 3,
                                    UnitCost = products[0].CurrentPrice
                                },
                                new OrderDetail() 
                                {
                                    ProductNavigation = products[1],
                                    Quantity = 2,
                                    UnitCost = products[1].CurrentPrice
                                },
                                new OrderDetail() 
                                {
                                    ProductNavigation = products[2],
                                    Quantity = 5,
                                    UnitCost = products[2].CurrentPrice//3
                                },
                            }
                        }
                    },

                    ShoppingCartRecords = new List<ShoppingCartRecord>
                    {
                        new ShoppingCartRecord
                        {
                        DateCreated = DateTime.Now,
                        ProductNavigation = products[3],
                        Quantity = 1,
                        LineItemTotal = products[3].CurrentPrice
                        }
                    },
    
                } 
            };
    }
}
