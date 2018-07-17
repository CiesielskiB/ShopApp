using ShopApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.DataAccess.InMemory
{
	public class ProductCategoryRepository
	{
		ObjectCache cache = MemoryCache.Default;
		List<ProductCategory> productsCategories;

		public ProductCategoryRepository()
		{
			productsCategories = cache["productsCategories"] as List<ProductCategory>;
			if (productsCategories == null) productsCategories = new List<ProductCategory>();
		}

		public void Commit()
		{
			cache["productsCategories"] = productsCategories;
		}

		public void Insert(ProductCategory productCategory)
		{
			productsCategories.Add(productCategory);
		}

		public void Update(ProductCategory productCategory)
		{
			ProductCategory productCategoryToUpdate = productsCategories.Find(p => p.Id == productCategory.Id);
			if (productCategoryToUpdate != null) productCategoryToUpdate = productCategory;
			else throw new Exception("Product category not found");
		}

		public ProductCategory Find(string Id)
		{
			ProductCategory productCategoryToFind = productsCategories.Find(p => p.Id == Id);
			if (productCategoryToFind != null) return productCategoryToFind;
			else throw new Exception("Product category not found");
		}

		public IQueryable<ProductCategory> Collection()
		{
			return productsCategories.AsQueryable();
		}

		public void Delete(string Id)
		{
			ProductCategory productCategoryToDelete = productsCategories.Find(p => p.Id == Id);
			if (productCategoryToDelete != null) productsCategories.Remove(productCategoryToDelete);
			else throw new Exception("Product category not found");
		}
	}
}
