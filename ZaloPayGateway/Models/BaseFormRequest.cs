namespace ZaloPayGateway.Models;

public interface IBaseFormRequest
{
    public Dictionary<string, string> ToDictionary();
}