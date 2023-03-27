using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ZaloPayGateway.Helpers;

public class JsonConvertHelper
{
    public static string SerializeObject(object? value)
    {
        var contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };
        return JsonConvert.SerializeObject(value, new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.Indented
        });
    }
    public static T? DeserializeObject<T>(string value)
    {
        var contractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        };
        return JsonConvert.DeserializeObject<T>(value, new JsonSerializerSettings
        {
            ContractResolver = contractResolver,
            Formatting = Formatting.Indented
        });
    }
}