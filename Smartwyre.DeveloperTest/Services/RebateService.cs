using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Types;

namespace Smartwyre.DeveloperTest.Services;

public class RebateService : IRebateService
{
    private readonly RebateDataStore _rebateDataStore;
    private readonly ProductDataStore _productDataStore;

    public RebateService(RebateDataStore rebateDataStore, ProductDataStore productDataStore)
    {
        _rebateDataStore = rebateDataStore;
        _productDataStore = productDataStore;
    }


    public CalculateRebateResult Calculate(CalculateRebateRequest request)
    {
        var rebate = _rebateDataStore.GetRebate(request.RebateIdentifier);
        var product = _productDataStore.GetProduct(request.ProductIdentifier);
        var result = new CalculateRebateResult();

        if (rebate == null) return result;
       
        var calculator = IncentiveCalculatorFactory.GetCalculator(rebate.Incentive);

        var rebateAmount = calculator.CalculateRebate(rebate, product, request);
        result.Success = rebateAmount > 0;
        result.Calculation = rebateAmount;

        if (result.Success)
        {
            _rebateDataStore.StoreCalculationResult(rebate, rebateAmount);
        }

        return result;
    }

}
