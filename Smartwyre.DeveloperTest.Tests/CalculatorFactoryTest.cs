using System;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class CalculatorFactoryTest
{
    [Fact]
    public void GetCalculator_ForFixedCashAmount_ReturnsFixedCashAmountCalculator()
    {
        // Act
        var calculator = IncentiveCalculatorFactory.GetCalculator(IncentiveType.FixedCashAmount);

        // Assert
        Assert.IsType<CalculatorFactory.FixedCashAmountCalculator>(calculator);
    }

    [Fact]
    public void GetCalculator_ForFixedRateRebate_ReturnsFixedRateRebateCalculator()
    {
        // Act
        var calculator = IncentiveCalculatorFactory.GetCalculator(IncentiveType.FixedRateRebate);

        // Assert
        Assert.IsType<CalculatorFactory.FixedRateRebateCalculator>(calculator);
    }

    [Fact]
    public void GetCalculator_ForAmountPerUom_ReturnsAmountPerUom()
    {
        // Act
        var calculator = IncentiveCalculatorFactory.GetCalculator(IncentiveType.AmountPerUom);

        // Assert
        Assert.IsType<CalculatorFactory.AmountPerUomCalculator>(calculator);
    }

    [Fact]
    public void GetCalculator_ForInvalidIncentiveType_ThrowsNotImplementedException()
    {
        // Act & Assert
        Assert.Throws<NotImplementedException>(() =>
            IncentiveCalculatorFactory.GetCalculator((IncentiveType)999));
    }

}
