using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyHome.Models;

namespace MyHome.ViewModels
{
    public class RoomViewModel
    {
        public Room room { get; set; }

        public IEnumerable<Room> rooms { get; set; }

        public Item item { get; set; }
    }
}