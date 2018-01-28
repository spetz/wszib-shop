using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Core.Domain
{
    public class Product
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Category { get; private set; }
        public decimal Price { get; private set; }

        public Product(string name, string category, decimal price)
            : this(Guid.NewGuid(), name, category, price)
        {
        }

        public Product(Guid id, string name, string category, decimal price)
        {
            Id = id;
            SetName(name);
            SetCategory(category);
            SetPrice(price);
        }

        public void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Product name is empty.", nameof(name));
            }
            if (name.Length > 100)
            {
                throw new ArgumentException($"Product name is too long: '{name.Length}' chars.", nameof(name));
            }
            Name = name;
        }

        public void SetCategory(string category)
        {
            if (string.IsNullOrWhiteSpace(category))
            {
                throw new ArgumentException("Product category is empty.", nameof(category));
            }
            Category = category;
        }

        public void SetPrice(decimal price)
        {
            if (price < 1 || price > 100000)
            {
                throw new ArgumentException($"Product price is invalid: {price}.", nameof(price));
            }
            Price = price;
        }
    }
}
