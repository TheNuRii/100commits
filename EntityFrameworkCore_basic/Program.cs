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

var products =  from product in contex.Products
                where product.Price > 10.00M
                orderby product.Name
                select product;

foreach (Product p in products)
{
    Console.WriteLine($"Id: {p.Id}");
    Console.WriteLine($"Name: {p.Name}");
    Console.WriteLine($"Price: {p.Price}");
    Console.WriteLine(new string('_', 20));
}

var veggieSpecialDelte = contex.Products
    .Where(p => p.Name == "Veggie Special Pizza")
    .FirstOrDefault();

if (veggieSpecial is Product)
{
    contex.Remove(veggieSpecial);
}

               