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

                            System.Console.Write("Enter New Name: ");
                            string newName = Console.ReadLine();

                            Products findProduct = db.GetProductById(productID);

                            findProduct.ToString();
                            findProduct.ProductName=newName;

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
                            else{
                                logger.Warn("No display under choice");
                                Console.WriteLine("The display choice you entered was invalid");
                            }
                        }
                        Console.WriteLine();
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
