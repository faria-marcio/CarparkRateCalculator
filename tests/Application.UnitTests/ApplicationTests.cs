using Application.Services;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Moq;

namespace Application.UnitTests;

public class RateCalculatorServiceTests
{
    [Fact]
    public void CalculateRate_ValidEarlyBirdRate_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 11, 6, 30, 0);
        DateTime exit = new DateTime(2024, 7, 11, 16, 30, 0);

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Early Bird", result.RateName);
        Assert.Equal("FlatRate", result.TypeName);
        Assert.Equal(15.0, result.TotalPrice);
    }

    [Fact]
    public void CalculateRate_ValidEarlyBirdFridayRate_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 12, 6, 30, 0); // Friday
        DateTime exit = new DateTime(2024, 7, 12, 16, 30, 0); // Friday

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Weekend Rate", result.RateName);
        Assert.Equal("FlatRate", result.TypeName);
        Assert.Equal(20.0, result.TotalPrice);
    }

    [Fact]
    public void CalculateRate_ValidNightRate_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 11, 18, 30, 0);
        DateTime exit = new DateTime(2024, 7, 12, 5, 30, 0);

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Night Rate", result.RateName);
        Assert.Equal("FlatRate", result.TypeName);
        Assert.Equal(10.0, result.TotalPrice);
    }

    [Fact]
    public void CalculateRate_ValidFridayNightRate_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 12, 18, 30, 0); // Friday
        DateTime exit = new DateTime(2024, 7, 13, 5, 30, 0); // Saturday

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Night Rate", result.RateName);
        Assert.Equal("FlatRate", result.TypeName);
        Assert.Equal(10.0, result.TotalPrice);
    }

    [Fact]
    public void CalculateRate_ValidWeekendRate_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 6, 10, 0, 0); // Saturday
        DateTime exit = new DateTime(2024, 7, 7, 10, 0, 0);  // Sunday

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Weekend Rate", result.RateName);
        Assert.Equal("FlatRate", result.TypeName);
        Assert.Equal(20.0, result.TotalPrice);
    }

    [Fact]
    public void CalculateRate_StandardRateOneHour_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 11, 10, 0, 0);
        DateTime exit = new DateTime(2024, 7, 11, 11, 0, 0);

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Standard Rate", result.RateName);
        Assert.Equal("HourlyRate", result.TypeName);
        Assert.Equal(5.0, result.TotalPrice);
    }

    [Fact]
    public void CalculateRate_StandardRateTwoHours_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 11, 10, 0, 0);
        DateTime exit = new DateTime(2024, 7, 11, 12, 0, 0);

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Standard Rate", result.RateName);
        Assert.Equal("HourlyRate", result.TypeName);
        Assert.Equal(10.0, result.TotalPrice);
    }

    [Fact]
    public void CalculateRate_StandardRateThreeHours_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 11, 10, 0, 0);
        DateTime exit = new DateTime(2024, 7, 11, 13, 0, 0);

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Standard Rate", result.RateName);
        Assert.Equal("HourlyRate", result.TypeName);
        Assert.Equal(15.0, result.TotalPrice);
    }

    [Fact]
    public void CalculateRate_StandardRateMoreThanThreeHours_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 11, 10, 0, 0);
        DateTime exit = new DateTime(2024, 7, 11, 14, 0, 0);

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Standard Rate", result.RateName);
        Assert.Equal("HourlyRate", result.TypeName);
        Assert.Equal(20.0, result.TotalPrice);
    }

    [Fact]
    public void CalculateRate_StandardRateTwoDays_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 10, 10, 0, 0);
        DateTime exit = new DateTime(2024, 7, 11, 14, 0, 0);

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Standard Rate", result.RateName);
        Assert.Equal("HourlyRate", result.TypeName);
        Assert.Equal(40.0, result.TotalPrice);
    }

    [Fact]
    public void CalculateRate_StandardRateOneWeek_ReturnsExpectedRate()
    {
        // Arrange
        var rates = GetSampleRates();
        var mockRatesLoader = new Mock<IRatesLoader>();
        mockRatesLoader.Setup(x => x.LoadRatesData()).Returns(rates);

        var rateCalculatorService = new RateCalculatorService(mockRatesLoader.Object);

        DateTime entry = new DateTime(2024, 7, 4, 10, 0, 0);
        DateTime exit = new DateTime(2024, 7, 11, 14, 0, 0);

        // Act
        var result = rateCalculatorService.CalculateRate(entry, exit);

        // Assert
        Assert.Equal("Standard Rate", result.RateName);
        Assert.Equal("HourlyRate", result.TypeName);
        Assert.Equal(160.0, result.TotalPrice);
    }

    private static List<Rate> GetSampleRates()
    {
        return
            [
                new Rate
                {
                    Id = 1,
                    Name = "Early Bird",
                    TypeId = (int)RateTypes.FlatRate,
                    Prices =
                    [
                        new Price { Id = 1, Value = 15.0, Note = "Flat rate for early birds" }
                    ]
                },
                new Rate
                {
                    Id = 2,
                    Name = "Night Rate",
                    TypeId = (int)RateTypes.FlatRate,
                    Prices =
                    [
                        new Price { Id = 1, Value = 10.0, Note = "Flat rate for night parking" }
                    ]
                },
                new Rate
                {
                    Id = 3,
                    Name = "Weekend Rate",
                    TypeId = (int)RateTypes.FlatRate,
                    Prices =
                    {
                        new Price { Id = 1, Value = 20.0, Note = "Flat rate for weekends" }
                    }
                },
                new Rate
                {
                    Id = 4,
                    Name = "Standard Rate",
                    TypeId = (int)RateTypes.HourlyRate,
                    Prices =
                    {
                        new Price { Id = 1, Value = 5.0, Note = "First hour" },
                        new Price { Id = 2, Value = 10.0, Note = "Second hour" },
                        new Price { Id = 3, Value = 15.0, Note = "Third hour" },
                        new Price { Id = 4, Value = 20.0, Note = "Maximum for the day" }
                    }
                }
            ];
    }
}
