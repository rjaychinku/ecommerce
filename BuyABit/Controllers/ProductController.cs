using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuyABit.Interfaces;
using BuyABit.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuyABit.Controllers
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
    }
}
