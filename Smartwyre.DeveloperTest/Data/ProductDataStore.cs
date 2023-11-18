using System.Collections.Generic;
using System.Linq;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class ProductDataStore: IProductDataStore
{
    public List<Product> ProductList = new()
    {
        new Product { Identifier = "Product_1", Price = 200m, SupportedIncentives = SupportedIncentiveType.FixedCashAmount },
        new Product { Identifier = "Product_2", Price = 150m, SupportedIncentives = SupportedIncentiveType.FixedRateRebate },
        new Product { Identifier = "Product_3", Price = 300m, SupportedIncentives = SupportedIncentiveType.AmountPerUom }
    };
    public Product GetProduct(string productIdentifier)
    {
        return ProductList.FirstOrDefault(x => x.Identifier.ToLower().Equals(productIdentifier.ToLower()));
    }
}
