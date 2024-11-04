using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Web.Mvc;
using _23DH112875_MyStore.Models;
using System.Data.SqlClient;
namespace _23DH112875_MyStore.Models.ViewModel;

namespace _23DH112875_MyStore.Areas.Admin.Controllers;
{
    public class ProductController : Controller
{
    // GET: Admin/Product
    private MyStoreEntities db = new MyStoreEntities();
    public ActionResult Index(string searchTerm, decimal? minPrice, decimal? maxPrice, string sortOrder)
    {
        var model = new ProductSearchVM();
        var products = db.Products.AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            model.SearchTerm = searchTerm;
            products = products.Where(p =>
                       p.ProductName.Contains(searchTerm) ||
                       p.ProductDescription.Contains(searchTerm) ||
                       p.Category.CategoryName.Contains(searchTerm));
        if (minPrice.HasValue)
        {
            products = products.Where(p => p.ProductPrice >= minPrice.Value);
        }

        if (maxPrice.HasValue)
        {
            products = products.Where(p => p.ProductPrice <= maxPrice.Value);
        }
        model.Products = products.ToList();
        return View(model);
    }
    switch (sortOrder) 
    {
            case "name_asc": products = products.OrderBy(p => p.ProductName);
                break;
            case "name_desc":products = products.OrderByDescending(p => p.ProductName);
                break;
            case "price_asc":products = products.OrderBy(p => p.ProductPrice);
                break;
            case "price_desc":products = products.OrderByDescending(p => p.ProductPrice);
                break;
            default:
                products = products.OrderBy(p => p.ProductName);
                break;
        }
        model.Products = products.ToList();
         return View(model);
        }
    }
}
