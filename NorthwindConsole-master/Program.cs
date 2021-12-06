﻿using System;
using NLog.Web;
using System.IO;
using System.Linq;
using NorthwindConsole.Model;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace NorthwindConsole
{
    class Program
    {
        // create static instance of Logger
        private static NLog.Logger logger = NLogBuilder.ConfigureNLog(Directory.GetCurrentDirectory() + "\\nlog.config").GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Program started");

            try
            {
                System.Console.WriteLine("would you like to enter \n1) Products \n2) Categories? ");
                string choice = Console.ReadLine();


                if(choice == "1"){

                    var db = new Northwind_88_DBMContext();

                    do
                    {
                        Console.WriteLine("1) Add new record to the Products table ");
                        Console.WriteLine("2) Edit a specified record from the Products table");
                        Console.WriteLine("3) Display all records in the Products table");                   
                        Console.WriteLine("4) Display a specific Product");
                        Console.WriteLine("5) Delete a Product");
                        Console.WriteLine("\"q\" to quit");
                        choice = Console.ReadLine();
                        Console.Clear();
                        logger.Info($"Option {choice} selected");
                        if (choice == "1")
                        {
                            
                            
                            System.Console.Write("Name: ");
                            string name =Console.ReadLine();

                            System.Console.Write("Supplyies ID: ");
                            int suppID = Console.Read();

                            System.Console.Write("Category ID: ");
                            int CateID = Console.Read();

                            System.Console.Write("Quantity Per Unit: ");
                            string quantity = Console.ReadLine();

                            System.Console.Write("Price: ");
                            decimal price = Console.Read();

                            System.Console.Write("Units in Stock: ");
                            int units = Console.Read();

                            System.Console.Write("Units on Order: ");
                            int order = Console.Read();

                            System.Console.Write("Reorder Level: ");
                            int level = Console.Read();

                            System.Console.Write("Disconinued: ");
                            Boolean.TryParse(Console.ReadLine(), out bool discontinued);

                            var product = new Products{
                                ProductName = name, SupplierId = suppID, CategoryId = CateID, QuantityPerUnit = quantity,
                                UnitPrice = price, UnitsInStock = (short) units,
                                UnitsOnOrder = (short) order, ReorderLevel = (short) level,
                                Discontinued = discontinued
                            };

                            db.AddNewProduct(product);
                        }
                        else if (choice == "2")
                        {
                            System.Console.Write("Enter Product ID: ");
                            int productID = Console.Read();

                            System.Console.Write("Name: ");
                            string name =Console.ReadLine();

                            System.Console.Write("Supplyies ID: ");
                            int suppID = Console.Read();

                            System.Console.Write("Category ID: ");
                            int CateID = Console.Read();

                            System.Console.Write("Quantity Per Unit: ");
                            string quantity = Console.ReadLine();

                            System.Console.Write("Price: ");
                            decimal price = Console.Read();

                            System.Console.Write("Units in Stock: ");
                            int units = Console.Read();

                            System.Console.Write("Units on Order: ");
                            int order = Console.Read();

                            System.Console.Write("Reorder Level: ");
                            int level = Console.Read();

                            System.Console.Write("Disconinued: ");
                            Boolean.TryParse(Console.ReadLine(), out bool discontinued);
                                var product = new Products
                                {
                                    ProductName = name, SupplierId = suppID, CategoryId = CateID, QuantityPerUnit = quantity,
                                UnitPrice = price, UnitsInStock = (short) units,
                                UnitsOnOrder = (short) order, ReorderLevel = (short) level,
                                Discontinued = discontinued
                                };

                            Products findProduct = db.GetProductById(productID);

                            findProduct.ToString();

                                findProduct.ProductName = name;
                                findProduct.SupplierId = suppID;
                                findProduct.CategoryId = CateID;
                                findProduct.QuantityPerUnit = quantity;
                                findProduct.UnitPrice = price;
                                findProduct.UnitsInStock = (short) units;
                                findProduct.UnitsOnOrder = (short) order;
                                findProduct.ReorderLevel = (short) level;
                                findProduct.Discontinued = discontinued;

                            db.EditProduct(findProduct);

                            Products output = db.GetProductById(productID);
                            output.ToString();
                        }
                        else if (choice == "3")
                        {
                            System.Console.WriteLine("1) Display all Products through product name\n2) Display all Products through all Fields\n3) Display all disconinued Products\n4) Display Active Products\n5) Display a specific Product");
                            string DisplayChoice = Console.ReadLine();

                            if (DisplayChoice == "1"){
                                foreach(var product in db.GetProduct()){
                                        Console.WriteLine(product.ProductName);
                                }
                            }

                            else if (DisplayChoice == "2"){
                                foreach (var product in db.GetProduct())
                                    {
                                    if (product.Discontinued == true)
                                        {
                                            Console.ForegroundColor = ConsoleColor.Red;
                                        }
                                        Console.WriteLine(product.ToString());
                                }
                            }

                            else if (DisplayChoice == "3"){
                                foreach (var product in db.GetDiscontinued()){
                                    System.Console.WriteLine(product.ProductName);
                                    System.Console.WriteLine(product.Discontinued);
                                }
                            }
                            else if (DisplayChoice == "4"){
                                
                            }
                            else if (DisplayChoice == "5"){
                                System.Console.Write("Product ID: ");
                                int id = Console.Read();
                                var showProduct = db.GetProductById(id);

                                Console.WriteLine($"{showProduct.ToString()}");
                            }
                            else{
                                logger.Warn("No display under choice");
                                Console.WriteLine("The display choice you entered was invalid");
                            }
                        }

                        else if (choice == "4"){
                            Console.Write("Search: ");
                            string search = Console.ReadLine();

                            var SearchedProducts = db.findProducts(search);

                            System.Console.WriteLine($"Found {SearchedProducts.Count}");

                            foreach(var products in SearchedProducts){
                                System.Console.WriteLine($"{products.ToString()}");
                            }

                            System.Console.WriteLine("Process Completed Successfully");
                        }
                        else if (choice == "5"){
                            System.Console.Write("Product Id to Delete: ");
                            int id = Console.Read();

                            var product = db.GetProductById(id);
                            db.DeleteProduct(product);
                            System.Console.WriteLine("The delete was successful");
                        }


                        ///This is where the cluter of category starts
                        else if (choice == "2"){
                            System.Console.WriteLine("1) Add new records to the Categories table");
                            System.Console.WriteLine("2) Edit a specified record from the Categories table");
                            System.Console.WriteLine("3) Display choices");
                            System.Console.WriteLine("4) Display a specific Category and its related active product data (CategoryName, ProductName)");
                            System.Console.WriteLine("5) Delete a specified existing record from the Categories table");

                             if (choice == "1")
                        {
                            
                            
                            System.Console.Write("Name: ");
                            string name =Console.ReadLine();

                            System.Console.Write("Description: ");
                            string Desc = Console.ReadLine();

                            var category = new Categories(){
                                CategoryName = name, Description= Desc
                            };

                            db.AddCategory(category);
                        }
                        else if (choice == "2")
                        {
                            System.Console.Write("Enter Category ID: ");
                            int categoryID = Console.Read();

                             System.Console.Write("Name: ");
                            string name =Console.ReadLine();

                            System.Console.Write("Description: ");
                            string Desc = Console.ReadLine();

                                var category = new Categories
                                {
                                    CategoryName = name, Description= Desc
                                };

                            Categories findCategory = db.GetCategoryById(categoryID);

                            findCategory.ToString();

                                findCategory.CategoryName = name;
                                findCategory.CategoryId = categoryID;
                                findCategory.Description= Desc;

                            db.EditCategory(findCategory);

                            Categories output = db.GetCategoryById(categoryID);
                            output.ToString();
                        }
                        else if (choice == "3")
                        {
                            Console.WriteLine("1) Display All Categories showing Category Name");
                                Console.WriteLine("2) Display All Categories showing All Fields");
                                Console.WriteLine("3) Display a Specific Category");
                                Console.WriteLine("4) Display Categories + Related Product data");
                                Console.WriteLine("5) Display Product Data for Category Id");
                            string DisplayChoice = Console.ReadLine();

                            if (DisplayChoice == "1"){
                                foreach(var category in db.GetCategories()){
                                        Console.WriteLine(category.CategoryName);
                                }
                            }

                            else if (DisplayChoice == "2"){
                                foreach (var cat in db.GetCategories())
                                    {
                                        Console.Write(
                                            $"Id: {cat.CategoryId} Name: {cat.CategoryName} Desc: {cat.Description}\n");
                                    }
                            }
                            else if (DisplayChoice == "3"){
                                System.Console.Write("Category ID: ");
                                int id = Console.Read();
                                var showCategory = db.GetCategoryById(id);

                                Console.WriteLine($"{showCategory.ToString()}");
                            }
                            else if (DisplayChoice == "4")
                                {
                                    db.GetOutputCategoryProductData();
                                }
                                else if (DisplayChoice == "5")
                                {
                                    Console.WriteLine("Product Data for Cat Id of: ");
                                    Int32.TryParse(Console.ReadLine(), out int id);

                                    db.GetCategoryProductNameByCatId(id);
                                }
                            else{
                                logger.Warn("No display under choice");
                                Console.WriteLine("The display choice you entered was invalid");
                            }
                        }

                        else if (choice == "4"){
                            Console.Write("Search: ");
                            string search = Console.ReadLine();

                            var SearchedCategories = db.QueryCategories(search);

                            var qls = new List<Categories>();

                            foreach (var Categories in SearchedCategories){
                                qls.Add(Categories);
                                System.Console.WriteLine($"{Categories.CategoryName}");
                            }

                            System.Console.WriteLine("Process Completed Successfully");
                        }
                        else if (choice == "5"){
                            System.Console.Write("Category Id to Delete: ");
                            int id = Console.Read();

                            var category = db.GetCategoryById(id);
                            db.DeleteCategory(category);
                            System.Console.WriteLine("The delete was successful");
                        }
                        }
                        else{
                            System.Console.WriteLine("invalid choice");
                        }
                        
                    }   while (choice.ToLower() != "q");
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            logger.Info("Program ended");
        }
    }
}
