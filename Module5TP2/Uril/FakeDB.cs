using Module5TP2.Models.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Module5TP2.Uril
{
    public class FakeDB {



        private List<Pizza> pizzas;

        public List<Pizza> PizzasDisponibles
        {
            get { return pizzas; }
            private set { this.pizzas = value; }
        }


        private static Singleton _instance;
    static readonly object instanceLock = new object();


      public Pate Pate { get; set; }
      public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        private FakeDB()
    {
            this.IngredientDisponlible = this.InitIngredientDisponlibles();

    }


        public List<Pate> PateDisponible
        {
            get { return pates; }
            private set { this.pates = value; }
        }

    public static FakeDB Instance
    {
        get
        {
            if (_instance == null) //Les locks prennent du temps, il est préférable de vérifier d'abord la nullité de l'instance.
            {
                lock (instanceLock)
                {
                    if (_instance == null) //on vérifie encore, au cas où l'instance aurait été créée entretemps.
                        _instance = new FakeDB();
                }
            }
            return _instance;
        }
    }
}

// Autre implémentation possible, depuis le .Net 4, sans lock
public sealed class Singleton
{
    private static readonly Lazy<Singleton> _lazy = new Lazy<Singleton>(() => new Singleton());

    public static Singleton Instance { get { return _lazy.Value; } }

    private Singleton()
    {
    }
}
}