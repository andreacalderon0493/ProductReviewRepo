using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_ReviewsWebAPI;
using Product_ReviewsWebAPI.Data;
using Product_ReviewsWebAPI.Models;

namespace Product_ReviewsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Products
        [HttpGet]
        public IActionResult GetProduct([FromQuery] double? maxPrice)
        {
            var product = _context.Products.ToList();
            if (maxPrice != null)
            {
                product = product.Where(p => p.Price < maxPrice).ToList();
            }
            return Ok(product);
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            
            var productReview = _context.Products
                .Include(r => r.Reviews)
                .Select( r => new ProductDTO
                {
                    Name = r.Name,
                    Price = r.Price,
                    AverageRating = r.Reviews.Average(r => r.Rating),
                    Reviews = r.Reviews.Select(p => new ReviewDTO
                    {
                        Text = p.Text,
                        Rating = p.Rating
                        
                    }).ToList()
                })
                .FirstOrDefault(r => r.Id == id);
            return StatusCode(200, productReview);
        }
    
        // POST: api/Products
        [HttpPost]
        public IActionResult Post([FromBody] Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return StatusCode(201, product);
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Product product)
        {
            var updatedProduct = _context.Products.Find(id);

            if (updatedProduct == null)
            {
                return NotFound();
            }
            else
            {
                updatedProduct.Name = product.Name;
                updatedProduct.Price = product.Price;
                updatedProduct.Reviews = product.Reviews;
                _context.Products.Update(updatedProduct);
                _context.SaveChanges();
                return Ok(updatedProduct);
            }

           

        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return NoContent();
        }
  
    }
}
