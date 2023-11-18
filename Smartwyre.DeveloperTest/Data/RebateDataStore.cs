using System.Collections.Generic;
using System.Linq;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Data;

public class RebateDataStore: IRebateDataStore
{
    public List<Rebate> RebateList = new()
    {
        new Rebate { Identifier = "Rebate_1", Amount = 50, Percentage = 0.10m, Incentive = IncentiveType.FixedCashAmount },
        new Rebate { Identifier = "Rebate_2", Amount = 30, Percentage = 0.05m, Incentive = IncentiveType.FixedRateRebate },
        new Rebate { Identifier = "Rebate_3", Amount = 20, Percentage = 0.15m, Incentive = IncentiveType.AmountPerUom }
    };

    public Rebate GetRebate(string rebateIdentifier)
    {
        return RebateList.FirstOrDefault(x => x.Identifier.ToLower().Equals(rebateIdentifier.ToLower()));
    }

    public void StoreCalculationResult(Rebate account, decimal rebateAmount)
    {
        RebateList.Add(account);
    }
}
