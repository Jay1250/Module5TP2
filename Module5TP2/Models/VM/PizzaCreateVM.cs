using Module5TP2.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module5TP2.Models.VM
{
    public class PizzaCreateVM
    {
        public Pizza Pizza { get; set; }
        public List<SelectListItem> Ingredients { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Pates { get; set; } = new List<SelectListItem>();

        public int IdSelectedPate { get; set; }

        public List<int> IdSelectedIngredient { get; set; } = new List<int>();
    }
}