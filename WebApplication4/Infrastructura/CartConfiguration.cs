using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication4.Infrastructura
{
    public class CartConfiguration
    {
        public ItemLine Processors { get; set; }
        public ItemLine motherboard { get; set; }
        public List<ItemLine> HardDrive { get; set; }
        public List<ItemLine> Ram { get; set; }
        public ItemLine Cooler { get; set; }
        public List<ItemLine> CdRom { get; set; }
        public List<ItemLine> GraphicsCard { get; set; }
        public ItemLine Case { get; set; }
        public ItemLine PowerUnit { get; set; }
        public List<ItemLine> SSD { get; set; }
    }
}