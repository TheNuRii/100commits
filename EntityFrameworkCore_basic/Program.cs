using ContosoPizza.Data;
using ContosoPizza.Models;

using ContosoPizzaContex contex = new ContosoPizzaContex();

Product veggieSpecial = new Product()
{
    Name = "Veggie Special Pizza",
    Price = 9.99M
};
contex.Products.Add(veggieSpecial);

Product deluxMeat = new Product()
{
    Name = "Delux Max Pizza",
    Price = 12.99M
};
contex.Products.Add(deluxMeat);

contex.SaveChanges();