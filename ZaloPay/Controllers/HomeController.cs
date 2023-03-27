using System.Diagnostics;
using ConsoleApp1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ZaloPay.Models;
using ZaloPayGateway.Models;

namespace ZaloPay.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ZaloPaySDK _paySdk ;
    public HomeController(ILogger<HomeController> logger, ZaloPaySDK paySdk)
    {
        _logger = logger;
        _paySdk = paySdk;
    }

    public IActionResult Index()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Index([FromForm] CreateOrder createOrder)
    {
        var result =  await _paySdk.CreateOrder(createOrder.BankCode);
        
        if (result is { ReturnCode: 1 })
        {
            return Redirect(result.OrderUrl);
        }
        throw new Exception("Tao order loi");
    }

    
    public async Task<IActionResult> Privacy(string appTransId)
    {
        if (appTransId == null) return View();
        var result =  await _paySdk.QueryOrder(appTransId);
        ViewData["result"] = result;
        return View();
    }
    [HttpGet]
    public async Task<IActionResult> Redirect([FromQuery] RedirectOrder redirectOrder)
    {
        if (!_paySdk.ChecksumData(redirectOrder))
        {
            return StatusCode(400, "Bad Request");
        }
        var result =  await _paySdk.QueryOrder(redirectOrder.Apptransid);
        ViewData["result"] = result;
        return View("Privacy");
    }
    [HttpPost]
    public IActionResult Callback([FromBody] CallbackOrder callbackOrder)
    {
        if (!_paySdk.CheckMac(callbackOrder))
        {
            return StatusCode(400, "Bad Request");
        }
        
        // ViewData["result"] = result;
        return View("Privacy");
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}