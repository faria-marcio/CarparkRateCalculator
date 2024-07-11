namespace Web;

public class CalculateRateApiClient(HttpClient httpClient)
{
    public async Task<RateResponse> GetRateAsync(string entry, string exit, CancellationToken cancellationToken = default)
    {
        var rate = await httpClient.GetFromJsonAsync<RateResponse>($"/calculaterate?entry={entry}&exit={exit}", cancellationToken);

        return rate ?? new RateResponse("", "", 0.0, "");
    }
}

public record RateResponse(string RateName, string TypeName, double TotalPrice, string Note);
