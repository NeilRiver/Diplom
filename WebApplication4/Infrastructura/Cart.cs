using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication4.Models;
namespace WebApplication4.Infrastructura
{
    public class ItemList
    {
        public List<ItemLine> list { get; set; }
    }
    public class ItemLine
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int CategoryId { get; set; }
        public int PodCategoryId { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Brend { get; set; }
        public int Count { get; set; }
        public ItemLine(Item item)
        {
            Id = item.Id;
            Name = item.Name;
            CategoryId = item.CategoryId;
            PodCategoryId = item.PodCategoryId;
            Brend = item.Brend;
            Price = item.Price;
            Description = item.Description;
            Type = item.Type;
            Count = 1;
        }
        public ItemLine(int id, string name, float price,int categoryid,int podcategoryid, string brend, string description, string type)
        {
            Id = id;
            Name = name;
            CategoryId = categoryid;
            PodCategoryId = podcategoryid;
            Brend = brend;
            Price = price;
            Description = description;
            Type = type;
            Count = 1;
        }
    }
}