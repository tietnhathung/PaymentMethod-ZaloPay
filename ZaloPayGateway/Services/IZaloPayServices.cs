using System.ComponentModel.DataAnnotations;
using ZaloPayGateway.Models;

namespace ZaloPayGateway.Services;

public interface IZaloPayServices
{
    public ZaloPayOrderCreate BuildZaloPayOrderCreate(string orderId, long amount, string items, string bankCode, string embedData, string callbackUrl);
    public Task<ZaloPayOrderResult?> CreateOrder(ZaloPayOrderCreate zaloPayOrder);
    public Task<OrderQueryResult?> QueryOrder(string appTransId);
    public bool ValidateCallback(CallbackOrder callbackOrder);
    public bool ValidateRedirect(RedirectOrder redirectOrder);
}