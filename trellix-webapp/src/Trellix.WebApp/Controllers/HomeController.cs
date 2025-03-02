namespace Trellix.WebApp.Controllers;

using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Trellix.Services.Container;
using Trellix.WebApp.Models;

public class HomeController
(
    IResponseHeaderService responseHeaderService,
    ILogger<HomeController> logger
) : Controller
{
    private readonly IResponseHeaderService _responseHeaderService = responseHeaderService ?? throw new ArgumentNullException(nameof(responseHeaderService));
    private readonly ILogger<HomeController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task<IActionResult> Index()
    {
        _responseHeaderService.SetCspHeader(ViewBag, Response);

        return await Task.FromResult(View());
    }

    public async Task<IActionResult> Privacy()
    {
        _responseHeaderService.SetCspHeader(ViewBag, Response);

        return await Task.FromResult(View());
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public async Task<IActionResult> Error()
    {
        _responseHeaderService.SetCspHeader(ViewBag, Response);

        return await Task.FromResult(View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier }));
    }
}
