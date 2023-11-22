using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Product_ReviewsWebAPI.Data;
using Product_ReviewsWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Product_ReviewsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Reviews
        [HttpGet]
        public IActionResult Get()
        {
            var review = _context.Reviews.ToList();
            return Ok(review);
        }

        // GET: api/Reviews/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            return Ok(review);
        }

        // POST: api/Reviews
        [HttpPost]
        public IActionResult Post([FromBody] Review review)
        {
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return StatusCode(201, review);
        }

        // PUT: api/Reviews/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Review review)
        {
            var updatedReview = _context.Reviews.Find(id);

            if (updatedReview == null)
            {
                return NotFound();
            }
            else
            {
                updatedReview.Text= review.Text;
                updatedReview.Rating = review.Rating;
                _context.Reviews.Update(updatedReview);
                _context.SaveChanges();
                return Ok(updatedReview);
            }

            //var otherVersionSong = _context.Songs.Where(s => s.Id == id).FirstOrDefault();

        }

        // DELETE: api/Reviews/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return NotFound();
            }
            _context.Reviews.Remove(review);
            _context.SaveChanges();
            return NoContent();
        }
        // GET: api/Reviews/5
        [HttpGet("Product/{id}")]
        public IActionResult GetByProductId(int id)
        {
            
            var reviews = _context.Reviews
                .GroupBy(r => r.Id == id);
            if (reviews == null)
            {
                return NotFound();
            }
            return StatusCode(200, reviews);
        }

    }
}

