using System;
namespace Product_ReviewsWebAPI.Models
{
	public class ProductDTO
	{
		public string Name { get; set; }
		public double Price { get; set; }
		public ICollection<Review> Reviews { get; set; }
		public double AverageRating { get; set; }
	}
}

