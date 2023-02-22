namespace HotelListing.API.Test;

public abstract class HotelsTest
{
    private const string Url = "https://localhost:7076/api/v2.2/Hotels";
    private readonly RestClient _client;
    private readonly RestRequest _request;
    private readonly ITestOutputHelper _testOutputHelper;

    private HotelsTest(ITestOutputHelper testOutputHelper, RestRequest request)
    {
        _testOutputHelper = testOutputHelper;
        _request = request;
        _client = new RestClient(Url);
    }

    public class GetHotels : HotelsTest
    {
        public GetHotels(ITestOutputHelper testOutputHelper)
            : base(testOutputHelper, new RestRequest(Url))
        {
        }

        [Fact]
        public async Task Returns_a_list_of_hotels()
        {
            var response = await _client.GetAsync<List<GetHotel>>(_request);
            response?.Count.Should().BePositive();
            _testOutputHelper.WriteLine("hi");
        }
    }
}