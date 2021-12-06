using System;
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
                System.Console.WriteLine("would you like to enter \n1) Products \n2) Categories ");
                string choice = Console.ReadLine();


                if(choice == "1"){

                    var db = new Northwind_88_DBMContext();

                        Console.WriteLine("1) Add new record to the Products table ");
                        Console.WriteLine("2) Edit a specified record from the Products table");
                        Console.WriteLine("3) Display all records in the Products table");                   
                        Console.WriteLine("4) Display a specific Product");
                        Console.WriteLine("5) Delete a Product");
                        choice = Console.ReadLine();
                        Console.Clear();
                        logger.Info($"Option {choice} selected");
                        if (choice == "1")
                        {
                            
                            
                            System.Console.Write("Name: ");
                            String name = Console.ReadLine();

                            System.Console.Write("Supplier ID: ");
                            Int32.TryParse(Console.ReadLine(), out int suppID);

                            System.Console.Write("Category ID: ");
                            Int32.TryParse(Console.ReadLine(), out int cateID);

                            System.Console.Write("Quantity Per Unit: ");
                            string quantity = Console.ReadLine();

                            System.Console.Write("Price: ");
                            Decimal.TryParse(Console.ReadLine(), out decimal price);

                            System.Console.Write("Units in Stock: ");
                            Int32.TryParse(Console.ReadLine(), out int units);

                            System.Console.Write("Units on Order: ");
                            Int32.TryParse(Console.ReadLine(), out int order);

                            System.Console.Write("Reorder Level: ");
                            Int32.TryParse(Console.ReadLine(), out int level);

                            System.Console.Write("Disconinued: ");
                            Boolean.TryParse(Console.ReadLine(), out bool discontinued);

                            var product = new Products{
                                ProductName = name, SupplierId = suppID, CategoryId = cateID, QuantityPerUnit = quantity,
                                UnitPrice = price, UnitsInStock = (short) units,
                                UnitsOnOrder = (short) order, ReorderLevel = (short) level,
                                Discontinued = discontinued
                            };

                            db.AddNewProduct(product);
                        }
                        else if (choice == "2")
                        {
                            System.Console.Write("Enter Product ID: ");
                            Int32.TryParse(Console.ReadLine(), out int productID);

                            System.Console.Write("Name: ");
                            String name = Console.ReadLine();

                            System.Console.Write("Supplier ID: ");
                            Int32.TryParse(Console.ReadLine(), out int suppID);

                            System.Console.Write("Category ID: ");
                            Int32.TryParse(Console.ReadLine(), out int cateID);

                            System.Console.Write("Quantity Per Unit: ");
                            string quantity = Console.ReadLine();

                            System.Console.Write("Price: ");
                            Decimal.TryParse(Console.ReadLine(), out decimal price);

                            System.Console.Write("Units in Stock: ");
                            Int32.TryParse(Console.ReadLine(), out int units);

                            System.Console.Write("Units on Order: ");
                            Int32.TryParse(Console.ReadLine(), out int order);

                            System.Console.Write("Reorder Level: ");
                            Int32.TryParse(Console.ReadLine(), out int level);

                            System.Console.Write("Disconinued: ");
                            Boolean.TryParse(Console.ReadLine(), out bool discontinued);
                                var product = new Products
                                {
                                    ProductName = name, SupplierId = suppID, CategoryId = cateID, QuantityPerUnit = quantity,
                                UnitPrice = price, UnitsInStock = (short) units,
                                UnitsOnOrder = (short) order, ReorderLevel = (short) level,
                                Discontinued = discontinued
                                };

                            Products findProduct = db.GetProductById(productID);

                            findProduct.ToString();

                                findProduct.ProductName = name;
                                findProduct.SupplierId = suppID;
                                findProduct.CategoryId = cateID;
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
                            System.Console.WriteLine("1) Display all Products through product name\n2) Display all Products through all Fields\n3) Display all disconinued Products\n4) Display Active Products");
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
                                        Console.WriteLine(product.ProductName);

                                    }
                                    Console.ForegroundColor = ConsoleColor.White;
                                }

                            else if (DisplayChoice == "3") {
                                    foreach (var prod in db.GetDiscontinuedProds())
                                    {
                                        Console.Write(
                                            $"Id: {prod.ProductId} Name: {prod.ProductName} Discont: {prod.Discontinued}\n");
                                    }
                                }
                            else if (DisplayChoice == "4"){
                                    foreach (var prod in db.GetActiveProds())
                                    {
                                        Console.Write(
                                            $"Id: {prod.ProductId} Name: {prod.ProductName} Discont: {prod.Discontinued}\n");
                                    }
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

                            var yhs = new List<Categories>();

                            foreach(var yh in SearchedProducts){
                                System.Console.WriteLine($"Id: {yh.ProductId} Name: {yh.ProductName}\n");
                            }

                            System.Console.WriteLine("Process Completed Successfully");
                        }
                        else if (choice == "5"){
                                Console.WriteLine("Enter Product ID :");
                                Int32.TryParse(Console.ReadLine(), out int id);

                                var prod = db.GetProductById(id);
                                db.DeleteProduct(prod);
                                Console.WriteLine("Delete successful");
                            }
                    }


                        ///This is where the cluter of category starts
                        else if (choice == "2"){
                            System.Console.WriteLine("1) Add new records to the Categories table");
                            System.Console.WriteLine("2) Edit a specified record from the Categories table");
                            System.Console.WriteLine("3) Display choices");
                            System.Console.WriteLine("4) Display a specific Category and its related active product data (CategoryName, ProductName)");
                            System.Console.WriteLine("5) Delete a specified existing record from the Categories table");

                            choice = Console.ReadLine();
                            var db = new Northwind_88_DBMContext();

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
                            Int32.TryParse(Console.ReadLine(), out int categoryID);

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
                                Console.WriteLine("3) Display Categories + Related Product data");
                                Console.WriteLine("4) Display Product Data for Category Id");
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
                            else if (DisplayChoice == "3")
                                {
                                    db.GetOutputCategoryProductData();
                                }
                                else if (DisplayChoice == "4")
                                {
                                    Console.WriteLine("Category Id: ");
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
                            System.Console.Write("Category Id: ");
                            Int32.TryParse(Console.ReadLine(), out int id);

                            var category = db.GetCategoryById(id);
                            db.DeleteCategory(category);
                            System.Console.WriteLine("The delete was successful");
                        }
                        }
                        else{
                            System.Console.WriteLine("invalid choice");
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
