using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using TaskProject.Core.Entities;
using TaskProject.Infrastructure.Data;
using TaskProject.WebApi.DTOs;

namespace TaskProject.WebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private AppDbContext _db;
        private IMapper _mapper;
        private UserManager<IdentityUser> _userManager;

        public ProductController(AppDbContext appDbContext, IMapper objectMapper, UserManager<IdentityUser> userManager)
        {
            _db = appDbContext; 
            _mapper = objectMapper;
            _userManager = userManager;
        }
         

        [HttpPost()]
        public IActionResult Post([FromBody] ProductDto product)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var prod = _mapper.Map<Product>(product);
            prod.AuthorId = _userManager.GetUserId(User);

            _db.Producst.Add(prod);
            try
            {
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("ProductDate_ManufactureEmail_Unique"))
                {
                    ModelState.AddModelError("dateAndEmail", "a product with the same date and email already exists");
                    return BadRequest(ModelState.ValidationState);
                }
            }
            return Ok();
        }

        [AllowAnonymous]
        [HttpGet("{author?}")]
        public IActionResult GetProducts([FromRoute] string author = null)
        {
            if (author == null)
            {
                return Ok(_db.Producst.Select(p => _mapper.Map<ProductDto>(p)).ToArray());
            }
            else
            {
                var userr = _userManager.FindByNameAsync(author).Result;

                if (userr != null)
                {
                    return Ok(_db.Producst.Where(p => p.AuthorId == userr.Id));
                }
                else
                {
                    return Ok(_db.Producst); 
                }
            }

            return Ok();
        }
    }
}