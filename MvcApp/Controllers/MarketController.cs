using System.Net;
using Domain.DTOs.MarketDTOs;
using Domain.Filters;
using Infrastructure.Services.MarketService;
using Microsoft.AspNetCore.Mvc;

namespace MvcApp.Controllers;

public class MarketController : Controller
{

    private readonly IMarketService _marketService;

    public MarketController(IMarketService marketService)
    {
        _marketService = marketService;
    }

    public IActionResult CreateMarketAsync()
    {
        return View();
    }

    public IActionResult UpdateMarketAsync()
    {
        return View();
    }
    public IActionResult GetMarketAsync()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> GetMarketAsync(MarketFilter filter)
    {
        try
        {
            var response = await _marketService.GetMarketsAsync(filter);
            if (response.StatusCode != (int)HttpStatusCode.OK)
            {
                return StatusCode((int)response.StatusCode!, response.Errors);
            }

            var market = response.Data;
            return View(market);
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
        }
    }

    // [HttpGet("{MarketId}")]
    // public async Task<IActionResult> GetMarketByIdAsync(int marketId)
    // {
    //     try
    //     {
    //         var response = await _marketService.GetMarketByIdAsync(marketId);
    //         return Ok(response.Data);
    //     }
    //     catch (Exception ex)
    //     {
    //         return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
    //     }
    // }


    [HttpPost]
    public async Task<IActionResult> CreateMarketAsync(CreateMarketDto market)
    {
        try
        {
            var response = await _marketService.CreateMarketAsync(market);
            return RedirectToAction("GetMarket");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateMarketAsync(UpdateMarketDto market)
    {
        try
        {
            var response = await _marketService.UpdateMarketAsync(market);
            return RedirectToAction("GetMarket");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
        }
    }

    [HttpPost]
    public async Task<IActionResult> RemoveMarketAsync(int MarketId)
    {
        try
        {
            var response = await _marketService.RemoveMarketAsync(MarketId);
            return RedirectToAction("GetMarket");
        }
        catch (Exception ex)
        {
            return StatusCode((int)HttpStatusCode.InternalServerError, $"Error: {ex.Message}");
        }
    }

}