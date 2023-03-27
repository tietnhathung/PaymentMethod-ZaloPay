using System.Security.Cryptography;
using Newtonsoft.Json;
using ZaloPay.Models;
using ZaloPay.ZaloPayHelper.Crypto;
using ZaloPayGateway.Models;
using ZaloPayGateway.Services;

namespace ConsoleApp1;

public class ZaloPaySDK
{
    private string callback_url = "https://localhost:7072/Home/Callback";
    private string redirect_url = "https://localhost:7072/Home/Redirect";
    private readonly IZaloPayServices _zaloPayServices;
    private readonly IConfiguration _configuration;

    public ZaloPaySDK(IZaloPayServices zaloPayServices, IConfiguration configuration)
    {
        _zaloPayServices = zaloPayServices;
        _configuration = configuration;
    }

    public async Task<ZaloPayOrderResult?> CreateOrder(string bankCode)
    {
        var rnd = new Random();
        var embedData = new
        {
            redirecturl = redirect_url
        };
        var items = new[] { new {} };
        var orderId = rnd.Next(1000000);
        var zaloPayOrderCreate = _zaloPayServices.BuildZaloPayOrderCreate(orderId.ToString(),50000, JsonConvert.SerializeObject(items), bankCode, JsonConvert.SerializeObject(embedData), callback_url);
        var result = await _zaloPayServices.CreateOrder(zaloPayOrderCreate);
        return result;
    }

    public async Task<OrderQueryResult?> QueryOrder(string appTransId)
    {
        OrderQueryResult? result = await _zaloPayServices.QueryOrder(appTransId);
        
        return result;
    }

    public bool ChecksumData(RedirectOrder redirectOrder)
    {
        return _zaloPayServices.ValidateRedirect(redirectOrder);
    }

    public bool CheckMac(CallbackOrder callbackOrder)
    {
        return _zaloPayServices.ValidateCallback(callbackOrder);
    }
}