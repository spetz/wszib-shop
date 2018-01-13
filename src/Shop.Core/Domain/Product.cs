using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Domain
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Category { get; }
        public decimal Price { get; }

        public Product(string name, string category, decimal price)
        {
            Id = Guid.NewGuid();
            Name = name;
            Category = category;
            Price = price;
        }
    }
}
