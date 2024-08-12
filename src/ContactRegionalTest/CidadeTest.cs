namespace RegionalContactsTest
{
    public class CidadeTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private CidadeController _controller;
        private CidadeService _service;
        private readonly IMemoryCache _memoryCache;

        public CidadeTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddMemoryCache();
                });
            });
            var dbContext = new DatabaseContext();
            var _repository = new CidadeRepository(dbContext.DbContext);
            var _service = new CidadeService(_repository);
            _memoryCache = _factory.Services.GetRequiredService<IMemoryCache>();

            _controller = new CidadeController(_service, _memoryCache);
        }

        [Fact]
        public async Task GetAllCities()
        {
            try
            {
                var response = await _controller.GetAll();
                Assert.NotNull(response);
                var responseModel = response as OkObjectResult;
                Assert.NotNull(responseModel);
                var result = responseModel.Value as IEnumerable<Cidade>;
                Assert.NotNull(result);
                Assert.NotEmpty(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [Fact]
        public async Task CheckCityAreaCode()
        {
            try
            {
                var response = await _controller.GetAll();
                Assert.NotNull(response);
                var responseModel = response as OkObjectResult;
                Assert.NotNull(responseModel);
                var result = responseModel.Value as IEnumerable<Cidade>;
                bool containsCity = result.Any(c => c.NomeCidade == "SÃO PAULO" && c.DDD == "11");
                Assert.True(containsCity);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [Fact]
        public async Task GetCity()
        {
            try
            {
                var responseGetAll = await _controller.GetAll();
                Assert.NotNull(responseGetAll);
                var responseGetAllModel = responseGetAll as OkObjectResult;
                Assert.NotNull(responseGetAllModel);
                var resultGetAll = responseGetAllModel.Value as IEnumerable<Cidade>;
                var random = new Random();
                int count = resultGetAll.Count();
                int randomIndex = random.Next(count);
                Cidade city = resultGetAll.ElementAt(randomIndex);

                var verifyCityResponse = await _controller.Get(city.Id);
                var responseModel = verifyCityResponse as OkObjectResult;
                Assert.NotNull(responseModel);
                var result = responseModel.Value as Cidade;
                Assert.NotNull(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}