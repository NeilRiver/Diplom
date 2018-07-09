using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication4.Models
{
    public class dbContext:DbContext
    {
        public dbContext() : base("Connect")
        {

        }
        public DbSet<Category> CategorySet { get; set; }
        public DbSet<PodCategory> PodCategorySet { get; set; }
        public DbSet<Item> ItemSet { get; set; }
        public DbSet<Order> OrderSet { get; set; }
        public DbSet<ItemOrder> ItemOrderSet { get; set; }

    }
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UrlImage { get; set; }
    }
    public class PodCategory
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
    
    }
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public int PodCategoryId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Brend { get; set; }
    }

    public class Order
    {
        public int Id { get; set; }
        public string FIO { get; set; }
        public string Telephone { get; set; }
        public long Price { get; set; }
        public int Status { get; set; }
    }
    public class ItemOrder
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public int PodCategoryId { get; set; }
        public string Brend { get; set; }
        public int Count { get; set; }
    }
   
}