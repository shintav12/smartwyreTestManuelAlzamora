using Smartwyre.DeveloperTest.Types;
using System;
using static Smartwyre.DeveloperTest.Factories.CalculatorFactory;

namespace Smartwyre.DeveloperTest.Factories
{
    public interface IIncentiveCalculator
    {
        decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request);
    }
    public static class IncentiveCalculatorFactory
    {
        public static IIncentiveCalculator GetCalculator(IncentiveType incentiveType)
        {
            return incentiveType switch
            {
                IncentiveType.FixedCashAmount => new FixedCashAmountCalculator(),
                IncentiveType.FixedRateRebate => new FixedRateRebateCalculator(),
                IncentiveType.AmountPerUom => new AmountPerUomCalculator(),
                _ => throw new NotImplementedException("Invalid incentive type")
            };
        }
    }
    public static class CalculatorFactory
    {
        public class FixedCashAmountCalculator : IIncentiveCalculator
        {
            public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
            {
                if (rebate.Amount == 0 || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedCashAmount))
                {
                    return 0;
                }
                return rebate.Amount;
            }
        }

        public class FixedRateRebateCalculator : IIncentiveCalculator
        {
            public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
            {
                if (product == null || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.FixedRateRebate))
                {
                    return 0;
                }

                return product.Price * rebate.Percentage * request.Volume;
            }
        }

        public class AmountPerUomCalculator : IIncentiveCalculator
        {
            public decimal CalculateRebate(Rebate rebate, Product product, CalculateRebateRequest request)
            {
                if (product == null || !product.SupportedIncentives.HasFlag(SupportedIncentiveType.AmountPerUom))
                {
                    return 0;
                }

                return rebate.Amount * request.Volume;
            }
        }
    }
}
