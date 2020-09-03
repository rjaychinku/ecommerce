﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using myShop.Interfaces;
using myShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace myShop.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IDatabaseService _dbcontext;
        public ProductController(IDatabaseService dbcontext)
        {
            _dbcontext = dbcontext;
        }

        // GET: api/Product
        [HttpGet]
        [Route(nameof(GetAll))]
        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _dbcontext.GetAllProductsAsync();
        }

        // GET: api/Product
        [HttpGet]
        [Route(nameof(GetAllSizes))]
        public async Task<IEnumerable<ProductSize>> GetAllSizes()
        {
            return await _dbcontext.GetProductSizesAsync();
        }

        // GET: api/Product
        [HttpGet]
        [Route(nameof(GetAllColours))]
        public async Task<IEnumerable<ProductColour>> GetAllColours()
        {
            return await _dbcontext.GetProductColoursAsync();
        }

        // GET: api/Product/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Product
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
