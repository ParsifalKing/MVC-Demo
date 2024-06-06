using System.Net;
using Domain.DTOs.ProductDTOs;
using Domain.Filters;
using Infrastructure.Services.ProductService;
using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    public IActionResult CreateProductAsync()
    {
        return View();
    }

    public IActionResult UpdateProductAsync()
    {
        return View();
    }
    public IActionResult GetProductsAsync()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetProductsAsync(ProductFilter filter)
    {
        try
        {
            var response = await _productService.GetProductsAsync(filter);
            if (response.StatusCode != (int)HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode!, response.Errors);
            }

            var products = response.Data;
            return View(products);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
        }
    }

    // [HttpGet("{ProductId}")]
    // public async Task<IActionResult> GetProductByIdAsync(int productId)
    // {
    //     try
    //     {
    //         var response = await _productService.GetProductByIdAsync(productId);
    //         return Ok(response.Data);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
    //     }
    // }


    [HttpPost]
    public async Task<IActionResult> CreateProductAsync(CreateProductDto Product)
    {
        try
        {
            var response = await _productService.CreateProductAsync(Product);
            return RedirectToAction("GetProducts");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateProductAsync(UpdateProductDto Product)
    {
        try
        {
            var response = await _productService.UpdateProductAsync(Product);
            return RedirectToAction("GetProducts");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveProductAsync(int productId)
    {
        try
        {
            var response = await _productService.RemoveProductAsync(productId);
            return RedirectToAction("GetProducts");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
        }
    }
}

