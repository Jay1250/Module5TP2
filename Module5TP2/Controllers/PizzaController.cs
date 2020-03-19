using Module5TP2.Models.BO;
using Module5TP2.Models.VM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Module5TP2.Controllers
{
    public class PizzaController : Controller
    {
        private static List<Pate> pates;
        private static List<Ingredient> ingredients;
        private static List<Pizza> pizzas;
        private static Pate pate;
        public static List<Pizza> PizzasDisponibles => new List<Pizza>
        {};

        public List<Pate> Pates => pates ?? (pates = Pizza.PatesDisponibles);
        public List<Ingredient> Ingredients => ingredients ?? (ingredients = Pizza.IngredientsDisponibles);
        public List<Pizza> Pizzas => pizzas ?? (pizzas = PizzasDisponibles);

        // GET: Pizza
        public ActionResult Index()
        {
            return View(Pizzas);
        }

        // GET: Pizza/Details/5
        public ActionResult Details(int id)
        {
            Pizza pizza = Pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza != null)
            {
                return View(pizza);
            }
            return RedirectToAction("Index");
        }

        // GET: Pizza/Create
        public ActionResult Create()
        {

         //   PizzaCreateVM vm = new PizzaCreateVM();
          //  vm = FakeDb.Instance.PatesDisponlible.Select(x => new SelectListItem { Text = x.Nom, Value = x.Id.ToString() }).ToList();


            List <SelectListItem> listPates = new List<SelectListItem>();
            List<SelectListItem> listIngredients = new List<SelectListItem>();

            foreach (Pate pate in Pates)
            {
                listPates.Add( new SelectListItem { Text = pate.Nom, Value = pate.Id.ToString() });
            }

            foreach (Ingredient ingredient in Ingredients)
            {
                listIngredients.Add(new SelectListItem { Text = ingredient.Nom, Value = ingredient.Id.ToString() });
            }

            PizzaCreateVM pizzaVM = new PizzaCreateVM
            {
                Ingredients = listIngredients,
                Pates = listPates
            };
            
            return View(pizzaVM);
        }

        // POST: Pizza/Create
        [HttpPost]
        public ActionResult Create(PizzaCreateVM pizzaVM)
        {
            try
            {
                foreach (int id in pizzaVM.IdSelectedIngredient){
                    pizzaVM.Pizza.Ingredients.Add(Ingredients.FirstOrDefault(i => i.Id == id));
                }

                Pate pate = Pates.FirstOrDefault(p => p.Id == pizzaVM.IdSelectedPate);
                pizzaVM.Pizza.Pate = pate;
              
                Pizzas.Add(pizzaVM.Pizza);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Edit/5
        public ActionResult Edit(int id)
        {
            Pizza pizza = Pizzas.FirstOrDefault(p => p.Id == id);

            if(pizza != null)
            {
                List<SelectListItem> listPates = new List<SelectListItem>();
                List<SelectListItem> listIngredients = new List<SelectListItem>();

                foreach (Pate pate in Pates)
                {
                    if (pizza.Pate.Equals(pate))
                    {
                        listPates.Add(new SelectListItem { Text = pate.Nom, Value = pate.Id.ToString(), Selected=true });
                    }
                    else
                    {
                        listPates.Add(new SelectListItem { Text = pate.Nom, Value = pate.Id.ToString() });
                    }
                }

                foreach (Ingredient ingredient in Ingredients)
                {
                    if (pizza.Ingredients.Contains(ingredient))
                    {
                        listIngredients.Add(new SelectListItem { Text = ingredient.Nom, Value = ingredient.Id.ToString(), Selected=true });
                    }
                    else
                    {
                        listIngredients.Add(new SelectListItem { Text = ingredient.Nom, Value = ingredient.Id.ToString() });
                    }
                }

                PizzaCreateVM pizzaVM = new PizzaCreateVM
                {
                    Pizza = pizza,
                    Ingredients = listIngredients,
                    Pates = listPates
                };

                return View(pizzaVM);
            }
            return RedirectToAction("Index");
        }

        // POST: Pizza/Edit/5
        [HttpPost]
        public ActionResult Edit(PizzaCreateVM pizzaVM)
        {
            try
            {
                Pizza pizza = Pizzas.FirstOrDefault(p => p.Id == pizzaVM.Pizza.Id);

                pizzaVM.Ingredients.Clear();

                foreach (int id in pizzaVM.IdSelectedIngredient)
                {
                    pizzaVM.Pizza.Ingredients.Add(Ingredients.FirstOrDefault(i => i.Id == id));
                }

                Pate pate = Pates.FirstOrDefault(p => p.Id == pizzaVM.IdSelectedPate);
                pizzaVM.Pizza.Pate = pate;

                int index = Pizzas.FindLastIndex(p => p == pizza);
                Pizzas[index] = pizzaVM.Pizza;

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Pizza/Delete/5
        public ActionResult Delete(int id)
        {
            Pizza pizza = Pizzas.FirstOrDefault(p => p.Id == id);
            if (pizza != null)
            {
                return View(pizza);
            }
            return RedirectToAction("Index");
        }

        // POST: Pizza/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Pizza pizza = Pizzas.FirstOrDefault(p => p.Id == id);
                if (pizza != null)
                    Pizzas.Remove(pizza);
                return RedirectToAction("Index");
            }
            catch
            {
                return RedirectToAction("Index");
            }
        }
    }
}
