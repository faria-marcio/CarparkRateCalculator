namespace Domain.ValueObjects;

public record RateResponse(string RateName, string TypeName, double TotalPrice, string Note);