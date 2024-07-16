using Application.Interfaces;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services;

public class RateCalculatorService(IRatesLoader ratesLoader) : IRateCalculator
{
    private readonly IRatesLoader _ratesLoader = ratesLoader;
    private List<Rate> _rates = [];

    public RateResponse CalculateRate(DateTime entry, DateTime exit)
    {
        _rates = _ratesLoader.LoadRatesData();

        if (entry >= exit)
            return CalculateStandardRate(entry, exit);

        if (IsNightRate(entry, exit))
            return GenerateResponse(Rates.NightRate);

        if (IsWeekendRate(entry, exit))
            return GenerateResponse(Rates.WeekendRate);

        if (IsEarlyBird(entry, exit))
            return GenerateResponse(Rates.EarlyBird);

        return CalculateStandardRate(entry, exit);
    }

    private RateResponse GenerateResponse(Rates rateId = Rates.StandardRate, int priceId = 1, int days = 1)
    {
        var rate = _rates?.Find(r => r.Id == (int)rateId)
            ?? throw new ArgumentException("Rate not found");

        var rateType = Enum.GetName(typeof(RateTypes), rate.TypeId)
            ?? throw new ArgumentException("Rate type not found");

        var price = rate.Prices.Find(p => p.Id == priceId);

        var totalPrice = price != null ? price.Value * days : 0.0;
        var note = price?.Note ?? string.Empty;

        return new RateResponse(rate.Name, rateType, totalPrice, note);
    }

    private static bool IsEarlyBird(DateTime entry, DateTime exit)
    {
        return entry.TimeOfDay >= new TimeSpan(6, 0, 0) &&
               entry.TimeOfDay <= new TimeSpan(9, 0, 0) &&
               exit.TimeOfDay >= new TimeSpan(15, 30, 0) &&
               exit.TimeOfDay <= new TimeSpan(23, 30, 0) &&
               exit.Date == entry.Date;
    }

    private static bool IsNightRate(DateTime entry, DateTime exit)
    {
        return entry.DayOfWeek != DayOfWeek.Saturday &&
               entry.DayOfWeek != DayOfWeek.Sunday &&
               entry.TimeOfDay >= new TimeSpan(18, 0, 0) &&
               entry.TimeOfDay <= new TimeSpan(23, 59, 59) &&
               exit.TimeOfDay <= new TimeSpan(6, 0, 0) &&
               exit.Date == entry.Date.AddDays(1);
    }

    private static bool IsWeekendRate(DateTime entry, DateTime exit)
    {
        return IsWeekend(entry) && IsWeekend(exit);
    }

    private RateResponse CalculateStandardRate(DateTime entry, DateTime exit)
    {
        TimeSpan duration = exit - entry;
        int priceId = duration.TotalHours switch
        {
            <= 0 => 99,
            <= 1 => 1,
            <= 2 => 2,
            <= 3 => 3,
            _ => 4
        };

        int days = priceId != 99 ? (exit.Date - entry.Date).Days + 1 : 0;
        return GenerateResponse(Rates.StandardRate, priceId, days);
    }

    private static bool IsWeekend(DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Friday ||
               date.DayOfWeek == DayOfWeek.Saturday ||
               date.DayOfWeek == DayOfWeek.Sunday;
    }
}
