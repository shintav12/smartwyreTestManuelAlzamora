using System;
using Moq;
using Smartwyre.DeveloperTest.Data;
using Smartwyre.DeveloperTest.Factories;
using Smartwyre.DeveloperTest.Services;
using Smartwyre.DeveloperTest.Types;
using Xunit;

namespace Smartwyre.DeveloperTest.Tests;

public class PaymentServiceTests
{
    [Fact]
    public void Calculate_ValidRequest_ReturnsSuccessfulResult()
    {
        // Arrange
        var mockRebateDataStore = new Mock<RebateDataStore>();
        var mockProductDataStore = new Mock<ProductDataStore>();

        var rebateService = new RebateService(mockRebateDataStore.Object, mockProductDataStore.Object);

        var request = new CalculateRebateRequest
        {
            ProductIdentifier = "Product_2",
            RebateIdentifier = "Rebate_2",
            Volume = 10m
        };

        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.True(result.Success);
        Assert.Equal(75m, result.Calculation);
    }

    [Fact]
    public void Calculate_RebateNotFound_ReturnsUnsuccessfulResult()
    {
        // Arrange
        var mockRebateDataStore = new Mock<RebateDataStore>();
        var mockProductDataStore = new Mock<ProductDataStore>();

        var rebateService = new RebateService(mockRebateDataStore.Object, mockProductDataStore.Object);
        var request = new CalculateRebateRequest
        {
            ProductIdentifier = "Product_2",
            RebateIdentifier = "Rebate_4",
            Volume = 10m
        };

        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void Calculate_ProductNotFound_ReturnsUnsuccessfulResult()
    {
        var mockRebateDataStore = new Mock<RebateDataStore>();
        var mockProductDataStore = new Mock<ProductDataStore>();

        var rebateService = new RebateService(mockRebateDataStore.Object, mockProductDataStore.Object);
        var request = new CalculateRebateRequest
        {
            ProductIdentifier = "Product_4",
            RebateIdentifier = "Rebate_2",
            Volume = 10m
        };
        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }

    [Fact]
    public void Calculate_InvalidIncentiveCalculation_ReturnsUnsuccessfulResult()
    {
        var mockRebateDataStore = new Mock<RebateDataStore>();
        var mockProductDataStore = new Mock<ProductDataStore>();

        var rebateService = new RebateService(mockRebateDataStore.Object, mockProductDataStore.Object);
        var request = new CalculateRebateRequest
        {
            ProductIdentifier = "Product_2",
            RebateIdentifier = "Rebate_2",
            Volume = 0
        };
        // Act
        var result = rebateService.Calculate(request);

        // Assert
        Assert.False(result.Success);
    }

}
