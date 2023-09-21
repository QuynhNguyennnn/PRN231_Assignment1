using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObject;
using DataAccess.repository;
using AutoMapper;
using eStoreAPI.DTOs;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private IProductRepository repository = new ProductRepository();


        public ProductsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<List<Product>> GetProducts() => repository.GetProducts();


        [HttpGet("id")]
        public ActionResult<Product> GetProductDetail(int id)
        {
            return repository.GetProductByID(id);
        }

        [HttpPost]
        public IActionResult PostProduct(AddProductDtos p)
        {
            // Use mapper when post
            var productDto = _mapper.Map<Product>(p);
            repository.InsertProduct(productDto);
            return NoContent();
        }

        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var p = repository.GetProductByID(id);
            if (p == null)
            {
                return NotFound();
            }
            repository.DeleteProduct(p);
            return Ok(repository.GetProducts());
        }

        [HttpPut("id")]
        public IActionResult UpdateProduct(int id, Product p)
        {
            var pImp = repository.GetProductByID(id);
            if (p == null)
                return NotFound();
            var productDto = _mapper.Map<Product>(p);
            repository.UpdateProduct(productDto);
            return NoContent();
        }

        [HttpGet("keyWord")]
        public ActionResult<List<Product>> FindProductsByName(string productName) => repository.FindProductsByName(productName);
    }
}
