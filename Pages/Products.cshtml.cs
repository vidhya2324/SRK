using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace SRKFruitsWeb.Pages
{
    public class ProductItem
    {
        public string Name { get; set; } = string.Empty;
        public string Note { get; set; } = string.Empty;
        public string? ImageUrl { get; set; }

    }

    public class ProductsModel : PageModel
    {
        public List<ProductItem> ExoticFruits { get; set; } = new()
{
    new ProductItem
    {
        Name = "White Dragon Fruit",
        ImageUrl = "/images/products/white-dragon-fruit.png"
    },

    new ProductItem
    {
        Name = "Apples",
        Note = "Washington, Fuji & more",
        ImageUrl = "/images/products/apples.png"
    },

    new ProductItem
    {
        Name = "Oranges",
        Note = "Egyptian / South African",
        ImageUrl = "/images/products/oranges.png"
    },

    new ProductItem
    {
        Name = "Kiwi",
        ImageUrl = "/images/products/kiwi.png"
    },

    new ProductItem
    {
        Name = "Pears",
        ImageUrl = "/images/products/pears.png"
    },

    new ProductItem
    {
        Name = "Grapes",
        ImageUrl = "/images/products/grapes.png"
    },

    new ProductItem
    {
        Name = "Avocado",
        ImageUrl = "/images/products/avocado.png"
    },

    new ProductItem
    {
        Name = "Longan",
        ImageUrl = "/images/products/longan.png"
    }
};

        public List<ProductItem> SeasonalFruits { get; set; } = new()
{
    new ProductItem
    {
        Name = "Rambutan",
        ImageUrl = "/images/products/rambutan.png"
    },

    new ProductItem
    {
        Name = "Mangosteen",
        ImageUrl = "/images/products/mangosteen.png"
    },

    new ProductItem
    {
        Name = "Lychee",
        ImageUrl = "/images/products/lychee.png"
    },

    new ProductItem
    {
        Name = "Gooseberries",
        ImageUrl = "/images/products/gooseberries.png"
    },

    new ProductItem
    {
        Name = "Mango",
        Note = "Seasonal varieties",
        ImageUrl = "/images/products/mango.png"
    },

    new ProductItem
    {
        Name = "Pomegranate",
        ImageUrl = "/images/products/pomegranate.png"
    },

    new ProductItem
    {
        Name = "Guava & More",
        ImageUrl = "/images/products/guava-and-more.png"
    }
};

        public void OnGet()
        {
        }
    }
}
