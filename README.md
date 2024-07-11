# Carpark Rate Calculator

## Overview

The Carpark Rate Calculator is a .NET application designed to calculate parking fees based on various rates. The application includes different types of rates such as early bird, night rate, weekend rate, and standard hourly rates. This solution demonstrates the use of dependency injection, unit testing with Moq and xUnit, and JSON configuration for rates.

## Solution Structure

The solution is organized into several projects:

- **Presentation**: Contains the API that manages the access to Application.
- **Application**: Contains the core business logic and services, including the `RateCalculatorService` that calculates parking fees based on predefined rates.
- **Domain**: Contains the domain entities such as `Rate`, `Price`, and `RateTypes`, as well as the interface `IRatesLoader` for loading rate data.
- **Infrastructure**: Handles data access and configuration, including the `rates.json` file that stores the rate definitions.
- **Web**: UI (front-end) to interact with the API (back-end).
- **Tests**: Contains unit tests for the `RateCalculatorService` using xUnit and Moq.

The solution also includes [.Net Aspire](https://learn.microsoft.com/en-us/dotnet/aspire/get-started/aspire-overview) which is an opinionated, cloud ready stack for building observable, production ready, distributed applications.
The **AppHost** and **ServiceDefaults** projects are only used by Aspire.

## Setup

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 8.0)
- [xUnit](https://xunit.net/) for testing
- [Moq](https://github.com/moq/moq4) for mocking dependencies
- [docker](https://www.docker.com/products/docker-desktop/) for running the redis container (used by Aspire)

### Configuration

1.  **Clone the Repository**

    ```
    git clone <repository-url>
    cd <repository-directory>
    ```

2.  **Configure Rate Data**
    The rate data is configured in rates.json located in the Infrastructure/Data directory. You can modify this file to change the rates used by the application.

### Running the Application

**Visual Studio**: F5 should be enough as the AppHost is already setup as the start up project.
**vscode**: F5 and make sure to configure C# as programming language and then use AppHost [Default Configuration] to start the application and UI.

Use the `webfrontend` project to [endpoint](https://localhost:7146/) to access the UI.
(UI.jpg)
Use the other features to enjoy everything the .Net Aspire has to offer.
(Aspire.jpg)

### Running Tests

To run the unit tests, navigate to the Tests project and execute:

    dotnet test

### Usage

To calculate the rate for parking, create an instance of RateCalculatorService, and call the CalculateRate method with entry and exit timestamps.

    ```bash
    var rateCalculatorService = new RateCalculatorService(ratesLoader);
    DateTime entry = new DateTime(2024, 7, 11, 10, 0, 0);
    DateTime exit = new DateTime(2024, 7, 11, 14, 0, 0);
    var result = rateCalculatorService.CalculateRate(entry, exit);
    ```

### Testing

The unit tests for `RateCalculatorService` ensure that the rate calculation logic is working correctly. Tests cover various scenarios including early bird rates, night rates, weekend rates, and standard hourly rates.

### Sample Test

    ```bash
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
    ```

### License

This project is licensed under the MIT License. See the LICENSE file for more details.