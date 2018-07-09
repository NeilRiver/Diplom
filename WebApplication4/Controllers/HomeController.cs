using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication4.Models;
using WebApplication4.Infrastructura;
namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            dbContext db = new dbContext();
            var a = db.CategorySet.ToList();
            return View(a);
        }
        public ActionResult PodCat(int Id)
        {
            dbContext db = new dbContext();
            var a = (from u in db.PodCategorySet where u.CategoryId == Id select u).ToList();
            return PartialView(a);
        }
        public ActionResult ListItem(int Id)
        {
            dbContext db = new dbContext();
            var a = (from u in db.ItemSet where u.PodCategoryId == Id select u).ToList();
            return PartialView(a);
        }
        public ActionResult CartItemList()
        {
            dbContext db = new dbContext();
            CartConfiguration list = new CartConfiguration();
           

            if (Session["Cart"] == null)
            {
                list = new CartConfiguration();
                return PartialView(list);
            }
            else
            {
            
                list = (CartConfiguration)Session["Cart"];
            }
            long allPrice = 0;
            if (list.Processors != null)
            {
                allPrice += (int)list.Processors.Price;
            }
            if (list.motherboard != null)
            {
                allPrice += (int)list.motherboard.Price;
            }
            if (list.HardDrive != null)
            {
                foreach (var a in list.HardDrive)
                    allPrice += (int)a.Price;
            }
            if (list.Ram != null)
            {
                foreach (var a in list.Ram)
                    allPrice += (int)a.Price;
            }
            if (list.Cooler != null)
            {
                allPrice += (int)list.Cooler.Price;
            }
            if (list.CdRom != null)
            {
                foreach (var a in list.CdRom)
                    allPrice += (int)a.Price;
            }
            if (list.GraphicsCard != null)
            {
                foreach (var a in list.GraphicsCard)
                    allPrice += (int)a.Price;
            }
            if (list.Case != null)
            {
                allPrice += (int)list.Case.Price;
            }
            if (list.PowerUnit != null)
            {
                allPrice += (int)list.PowerUnit.Price;
            }
            if (list.SSD != null)
            {
                foreach (var a in list.SSD)
                    allPrice += (int)a.Price;
            }
            ViewBag.allPrice = allPrice;
            return PartialView(list);
        }
        public string AddItemCart(int id)
        {
            string message = "";
            CartConfiguration a = new CartConfiguration();
            dbContext db = new dbContext();
            if (Session["Cart"] == null)
            {
               
                a = new CartConfiguration();
            }
            else
            {
                a = (CartConfiguration)Session["Cart"];
            }

            ItemLine b = new ItemLine(db.ItemSet.Find(id));
            switch (b.CategoryId)
            {
                case 1:
                    if (a.motherboard == null)
                    {
                        a.Processors = b;
                        message = "Процессор " + a.Processors.Name + " добавлен";
                        a.Cooler = null;
                        break;
                    }
                    else
                    {
                        if (a.motherboard.Description.Contains(b.Type))
                        {
                            a.Processors = b;
                            message = "Процессор " + a.Processors.Name + " добавлен";
                            a.Cooler = null;
                            break;
                        }
                        else
                        {
                            message = "Процессор " + b.Name.ToString() + " не походит к материнской плате " + a.motherboard.Name;
                            break;
                        }
                            
                    }
                    

                case 2:
                    if(a.Processors!=null)
                    {
                        if (b.Description.Contains(a.Processors.Type))
                        {
                            a.motherboard = b;
                            message = "Матринска плата "+b.Name.ToString()+ " добавлена";
                            if (a.Ram != null)
                            {
                                foreach(var it in a.Ram.ToList())
                                {
                                    if (b.Description.Contains(it.Type) == false)
                                    {
                                        a.Ram.Remove(it);
                                    }
                                }
                            }
                            a.Cooler = null;
                            break;
                        }
                        else
                        {
                            message = "Матринска плата " + b.Name.ToString() + " не походит к процессору "+a.Processors.Name;
                            break;
                        }
                       
                    }
                    else
                    {
                        a.motherboard = b;
                        if (a.Ram != null)
                        {
                            foreach (var it in a.Ram.ToList())
                            {
                                if (b.Description.Contains(it.Type) == false)
                                {
                                    a.Ram.Remove(it);
                                }
                            }
                        }
                        a.Cooler = null;
                    }
                    message = "";
                    break;

                case 3:
                    if (a.HardDrive == null)
                    {
                        a.HardDrive = new List<ItemLine>();
                      
                    }
                    if (a.HardDrive.Count < 2)
                    {
                        a.HardDrive.Add(b);
                        message = "Жесткий диск " + b.Name.ToString() + " добавлена";
                    }
                    else
                    {
                        message = "Достигнуто максимальное количество жестких дисков";
                    }

                    break;
                case 4:
                    if (a.Ram == null)
                    {
                        a.Ram = new List<ItemLine>();

                    }
                    int CountRam = 1;
                    if (a.motherboard != null&&b.Type!=null)
                    {
                        if (a.motherboard.Description.Contains(b.Type))
                        {
                            int position = a.motherboard.Description.IndexOf(b.Type) - 2;
                            CountRam = int.Parse(a.motherboard.Description[position].ToString());
                            if (a.Ram.Count < CountRam)
                            {
                                a.Ram.Add(b);
                                message = "ОЗУ " + b.Name.ToString() + " добавлена";
                            }
                            else
                            {
                                message = "Достигнуто максимальное количество ОЗУ";
                            }
                        }
                        else
                        {
                            if (a.motherboard.Description.Contains("память")==false)
                            {
                                a.Ram = new List<ItemLine>();
                                a.Ram.Add(b);
                                message = "ОЗУ " + b.Name.ToString() + " добавлена";
                            }
                            else {
                                message = "ОЗУ " + b.Name.ToString() + "не подходит к материнской плате "+a.motherboard.Name; }
                        }
                    }
                    else
                    {
                        a.Ram = new List<ItemLine>();
                        a.Ram.Add(b);
                        message = "ОЗУ " + b.Name.ToString() + " добавлена";
                    }

                   
                    break;
                case 5:
                    if (a.motherboard != null)
                    {
                        if (a.motherboard.Description.Contains(b.Type))
                        {
                            a.Cooler = b;
                            message = "Кулер " + b.Name.ToString() + " добавлен";
                            break;
                        }
                        else
                        {
                            message = "Кулер " + b.Name.ToString() + " не подходит к материнской плате " + a.motherboard.Name;
                            break;
                        }
                    }
                    else
                    {
                        a.Cooler = b;
                        message = "Кулер " + b.Name.ToString() + " добавлен";
                        break;
                    }
                   
                case 6:
                    if (a.CdRom == null)
                    {
                        a.CdRom = new List<ItemLine>();

                    }
                    if (a.CdRom.Count < 2)
                    {
                        a.CdRom.Add(b);
                        message = "Привод " + b.Name.ToString() + " добавлен";
                        break;
                    } else
                    {
                        message = "Достигнуто максимальное количество приводов";
                        break;
                    }
                      
                case 7:
                    if (a.GraphicsCard == null)
                    {
                        a.GraphicsCard = new List<ItemLine>();

                    }
                    if (a.GraphicsCard.Count < 2) { 
                        a.GraphicsCard.Add(b);
                    message = "Видеокарта " + b.Name.ToString() + " добавлена";
                    break;
            } else
                    {
                message = "Достигнуто максимальное количество видеокарт";
                break;
            }
                case 8:
                    a.Case = b;
                    message = "";
                    break;
                case 9:
                    a.PowerUnit = b;
                    message = "Корпус " + b.Name.ToString() + " добавлен";
                    break;
                case 10:
                    if (a.SSD == null)
                    {
                        a.SSD= new List<ItemLine>();

                    }
                    if (a.SSD.Count < 2) { 
                        a.SSD.Add(b);
                    message = "SSD " + b.Name.ToString() + " добавлена";
                    break;
            } else
                    {
                message = "Достигнуто максимальное количество SSD";
                break;
            }

        }
            Session["Cart"] = a;
            return message;
        }
        public ActionResult Clear()
        {
            Session["Cart"] = null;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult SaveOrder(Order ord)
        {
            try {
                dbContext db = new dbContext();
                db.OrderSet.Add(ord);
                
                db.SaveChanges();
                var id = (from u in db.OrderSet select u).ToList().Last();
                CartConfiguration list = new CartConfiguration();


                if (Session["Cart"] == null)
                {
                    list = new CartConfiguration();
                    return PartialView(list);
                }
                else
                {

                    list = (CartConfiguration)Session["Cart"];
                }
                long allPrice = 0;

                
                if (list.Processors != null)
                {
                    ItemOrder ito = new ItemOrder() { ItemId = list.Processors.Id, Name = list.Processors.Name, Brend = list.Processors.Brend, CategoryId = list.Processors.CategoryId, Count = list.Processors.Count, PodCategoryId = list.Processors.PodCategoryId, Price = list.Processors.Price, OrderId = id.Id };
               
                    db.ItemOrderSet.Add(ito);
                    allPrice += (int)list.Processors.Price;
                }
                if (list.motherboard != null)
                {
                    ItemOrder ito = new ItemOrder() { ItemId = list.motherboard.Id, Name = list.motherboard.Name, Brend = list.motherboard.Brend, CategoryId = list.motherboard.CategoryId, Count = list.motherboard.Count, PodCategoryId = list.motherboard.PodCategoryId, Price = list.motherboard.Price, OrderId = id.Id };

                    db.ItemOrderSet.Add(ito);
                    allPrice += (int)list.Processors.Price;
                }
                if (list.HardDrive != null)
                {
                    foreach (var a in list.HardDrive)
                    {
                        ItemOrder ito = new ItemOrder() { ItemId = a.Id, Name = a.Name, Brend = a.Brend, CategoryId = a.CategoryId, Count = a.Count, PodCategoryId = a.PodCategoryId, Price = a.Price, OrderId = id.Id };
                        allPrice += (int)a.Price;
                        db.ItemOrderSet.Add(ito);
                      
                    }
                }
                if (list.Ram != null)
                {
                    foreach (var a in list.Ram)
                    {
                        ItemOrder ito = new ItemOrder() { ItemId = a.Id, Name = a.Name, Brend = a.Brend, CategoryId = a.CategoryId, Count = a.Count, PodCategoryId = a.PodCategoryId, Price = a.Price, OrderId = id.Id };
                        allPrice += (int)a.Price;
                        db.ItemOrderSet.Add(ito);

                    }
                }
                if (list.Cooler != null)
                {
                    ItemOrder ito = new ItemOrder() { ItemId = list.Cooler.Id, Name = list.Cooler.Name, Brend = list.Cooler.Brend, CategoryId = list.motherboard.CategoryId, Count = list.Cooler.Count, PodCategoryId = list.Cooler.PodCategoryId, Price = list.Cooler.Price, OrderId = id.Id };

                    db.ItemOrderSet.Add(ito);
                    allPrice += (int)list.Cooler.Price;
                }
                if (list.CdRom != null)
                {
                    foreach (var a in list.CdRom)
                    {
                        ItemOrder ito = new ItemOrder() { ItemId = a.Id, Name = a.Name, Brend = a.Brend, CategoryId = a.CategoryId, Count = a.Count, PodCategoryId = a.PodCategoryId, Price = a.Price, OrderId = id.Id };
                        allPrice += (int)a.Price;
                        db.ItemOrderSet.Add(ito);

                    }
                }
                if (list.GraphicsCard != null)
                {
                    foreach (var a in list.GraphicsCard)
                    {
                        ItemOrder ito = new ItemOrder() { ItemId = a.Id, Name = a.Name, Brend = a.Brend, CategoryId = a.CategoryId, Count = a.Count, PodCategoryId = a.PodCategoryId, Price = a.Price, OrderId = id.Id };
                        allPrice += (int)a.Price;
                        db.ItemOrderSet.Add(ito);

                    }
                }
                if (list.Case != null)
                {
                    allPrice += (int)list.Case.Price;
                    ItemOrder ito = new ItemOrder() { ItemId = list.Case.Id, Name = list.Case.Name, Brend = list.Case.Brend, CategoryId = list.Case.CategoryId, Count = list.Case.Count, PodCategoryId = list.Case.PodCategoryId, Price = list.Case.Price, OrderId = id.Id };

                    db.ItemOrderSet.Add(ito);
                }
                if (list.PowerUnit != null)
                {
                    allPrice += (int)list.PowerUnit.Price;
                    ItemOrder ito = new ItemOrder() { ItemId = list.PowerUnit.Id, Name = list.PowerUnit.Name, Brend = list.PowerUnit.Brend, CategoryId = list.PowerUnit.CategoryId, Count = list.PowerUnit.Count, PodCategoryId = list.PowerUnit.PodCategoryId, Price = list.PowerUnit.Price, OrderId = id.Id };

                    db.ItemOrderSet.Add(ito);
                }
                if (list.SSD != null)
                {
                    foreach (var a in list.SSD)
                    {
                        ItemOrder ito = new ItemOrder() { ItemId = a.Id, Name = a.Name, Brend = a.Brend, CategoryId = a.CategoryId, Count = a.Count, PodCategoryId = a.PodCategoryId, Price = a.Price, OrderId = id.Id };
                        allPrice += (int)a.Price;
                        db.ItemOrderSet.Add(ito);

                    }
                }
              
                Order ord2 = db.OrderSet.Find(id.Id);
                ord2.Price = allPrice;
                db.Entry(ord2).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                Session["Cart"] = null;
            }
            catch
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Contact()
        {
            if (Session["Auth"] == null) {
                return RedirectToAction("Index");
            } else {
                if((bool)Session["Auth"] == true)
                return View((new dbContext()).OrderSet.ToList());
            }
            return RedirectToAction("Index");
        }
        public ActionResult Details(int id)
        {
            dbContext db = new dbContext();
            var ord = db.OrderSet.Find(id);
            ord.Status = 1;
            db.Entry(ord).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            ViewBag.FIO = db.OrderSet.Find(id).FIO;
            ViewBag.Price = db.OrderSet.Find(id).Price;
            ViewBag.Telephone = db.OrderSet.Find(id).Telephone;
            var a = (from u in db.ItemOrderSet where u.OrderId == id select u).ToList();
            return PartialView(a);
        }
        public void Remove(int code,int id = 0)
        {
            CartConfiguration a = new CartConfiguration();
            dbContext db = new dbContext();
            if (Session["Cart"] == null)
            {

                a = new CartConfiguration();
            }
            else
            {
                a = (CartConfiguration)Session["Cart"];
            }

            switch (code)
            {
                case 1:
                    a.Processors = null;
                    break;
                case 2:
                    a.motherboard = null;
                    break;
                case 3:
                    if (a.HardDrive != null)
                    {
                        foreach (var h in a.HardDrive)
                        {
                            if (h.Id == id)
                            {
                                a.HardDrive.Remove(h);
                                break;
                            }
                        }
                    }
                    break;
                case 4:
                    if (a.Ram != null)
                    {
                        foreach (var h in a.Ram)
                        {
                            if (h.Id == id)
                            {
                                a.Ram.Remove(h);
                                break;
                            }
                        }
                    }
                    break;
                case 5:
                    a.Cooler = null;
                    break;
                case 6:
                    if (a.CdRom != null)
                    {
                        foreach (var h in a.CdRom)
                        {
                            if (h.Id == id)
                            {
                                a.CdRom.Remove(h);
                                break;
                            }
                        }
                    }
                    break;
                case 7:
                    if (a.GraphicsCard != null)
                    {
                        foreach (var h in a.GraphicsCard)
                        {
                            if (h.Id == id)
                            {
                                a.GraphicsCard.Remove(h);
                                break;
                            }
                        }
                    }
                    break;
                case 8:
                    a.Case = null;
                    break;
                case 9:
                    a.PowerUnit = null;
                    break;
                case 10:
                    if (a.SSD != null)
                    {
                        foreach (var h in a.SSD)
                        {
                            if (h.Id == id)
                            {
                                a.SSD.Remove(h);
                                break;
                            }
                        }
                    }
                    break;

            }
        }
        [HttpPost]
        public ActionResult LogOn(string login, string password)
        {
            if (login == "admin" && password == "admin")
            {
                Session["Auth"] = true;
                return RedirectToAction("Contact");
            }
            else
                return RedirectToAction("Index");
        }
        public ActionResult LogOff()
        {
            Session["Auth"] = null;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public void Delete (int id)
        {
            dbContext db = new dbContext();
           var ord =  db.OrderSet.Find(id);
            db.Entry(ord).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
    }
}