using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ECommerce.Api.Products.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TestEnum
    {

        Original,
        Update,
        Delete
    }
}
