using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BuyABit.Interfaces;
using BuyABit.Models;
using Microsoft.AspNetCore.Mvc;
using Serilog;

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

        // GET: /Product/GetAll
        [HttpGet]
        [Route(nameof(GetAll))]
        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return await _dbcontext.GetAllProductsAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to GetAll products.");
                return null;
            }
        }

        // GET: /Product/GetAllSizes
        [HttpGet]
        [Route(nameof(GetAllSizes))]
        public async Task<IEnumerable<ProductSize>> GetAllSizes()
        {
            try
            {
                return await _dbcontext.GetProductSizesAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to GetAllSizes.");
                return null;
            }
        }

        // GET: /Product/GetAllColours
        [HttpGet]
        [Route(nameof(GetAllColours))]
        public async Task<IEnumerable<ProductColour>> GetAllColours()
        {
            try
            {
                return await _dbcontext.GetProductColoursAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "The application failed to GetAllColours.");
                return null;
            } 
        }
    }
}
