using API.DTOs.ProductDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.Entities;
using Service.Services.Interfaces;

namespace API.Controllers;

[ApiController]
[Route("product")]
public class ProductController(IProductService productService, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetAll()
    {
        try
        {
            return await productService.GetAll();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }    
    [HttpGet("recents")]
    public async Task<ActionResult<List<Product>>> GetRecentlyViewedProducts()
    {
        try
        {
            return await productService.GetRecentlyViewedProducts();
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetById(Guid id)
    {
        try
        {
            return Ok(await productService.GetById(id));
        }
        catch (Exception exception)
        {
            return NotFound(exception.Message);
        }
    }

    [HttpPost]
    public async Task<ActionResult> Add(ProductDto dto)
    {
        Product newProduct = mapper.Map<Product>(dto);
        try
        {
            await productService.Add(newProduct);
            return Ok("Product created!");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> Update(ProductDto dto, Guid id)
    {
        Product newProduct = mapper.Map<Product>(dto);
        try
        {
            await productService.Update(id, newProduct);
            return Ok("Product Updated!");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        try
        {
            await productService.Delete(id);
            return Ok("Product Deleted!");
        }
        catch (Exception exception)
        {
            return BadRequest(exception.Message);
        }
    }
}