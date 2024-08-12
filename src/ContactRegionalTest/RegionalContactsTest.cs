using RegionalContactsAPI.Core.DTO;
using RegionalContactsAPI.Core.Service.Interface;

namespace RegionalContactsTest
{
    public class RegionalContactsTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private RegionalContactsController _controller;
        private ContactService _service;
        private readonly ICacheService _cache;

        public RegionalContactsTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddMemoryCache();
                });
            });
            var dbContext = new DatabaseContext();
            var _repository = new ContactRepository(dbContext.DbContext);
            var _service = new ContactService(_repository);
            using (var scope = _factory.Services.CreateScope())
            {
                var _cache = scope.ServiceProvider.GetRequiredService<ICacheService>();

                _controller = new RegionalContactsController(_service, _cache);
            }
        }

        [Fact]
        public async Task GetAllContacts()
        {
            try
            {
                var response = await _controller.GetAll(null);
                Assert.NotNull(response);
                var responseModel = response as OkObjectResult;
                Assert.NotNull(responseModel);
                var result = responseModel.Value as IEnumerable<ContactResponseDTO>;
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
        public async Task GetAllContactsWithFilter()
        {
            try
            {
                var random = new Random();
                int count = Utils.QueryDDDs.Count();
                int randomIndex = random.Next(count);
                var ddd = Utils.QueryDDDs.ElementAt(randomIndex);

                var response = await _controller.GetAll(ddd);
                Assert.NotNull(response);
                var responseModel = response as OkObjectResult;
                Assert.NotNull(responseModel);
                var result = responseModel.Value as IEnumerable<ContactResponseDTO>;
                Assert.NotNull(result);
                Assert.NotEmpty(result);
                var validateDDD = result.Any(x => x.DDD == ddd);
                Assert.True(validateDDD);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        [Fact]
        public async Task ValidateCRUDFlow()
        {
            try
            {
                var response = await _controller.Add(new ContactRequestDTO()
                {
                    Cidade = "São Paulo",
                    DDD = 11,
                    Email = "teste@gmail.com",
                    Estado = "SP",
                    Nome = "Teste Soares",
                    Telefone = 999999999
                });
                Assert.NotNull(response);
                var responseModel = response as CreatedAtActionResult;
                Assert.NotNull(responseModel);
                var result = responseModel.Value as ContactResponseDTO;
                Assert.NotNull(result);

                var id = result.Id;
                var responseGet = await _controller.Get(id);
                Assert.NotNull(responseGet);
                var responseModelGet = responseGet as OkObjectResult;
                Assert.NotNull(responseModelGet);
                var resultGet = responseModelGet.Value as ContactResponseDTO;
                Assert.NotNull(resultGet);

                Assert.Equal(result.Id, resultGet.Id);
                Assert.Equal(result.Nome, resultGet.Nome);
                Assert.Equal(result.Telefone, resultGet.Telefone);
                Assert.Equal(result.DDD, resultGet.DDD);
                Assert.Equal(result.Email, resultGet.Email);
                Assert.Equal(result.Estado, resultGet.Estado);
                Assert.Equal(result.Cidade, resultGet.Cidade);

                var responseUpdate = await _controller.Update(id, new ContactRequestDTO()
                {
                    Cidade = "Fortaleza",
                    DDD = 85,
                    Email = "teste123@gmail.com",
                    Estado = "CE",
                    Nome = "Teste da Silva",
                    Telefone = 988888888
                });

                Assert.NotNull(responseUpdate);
                var responseModelUpdate = responseUpdate as NoContentResult;
                Assert.NotNull(responseModelUpdate);

                var responseGetRetry = await _controller.Get(id);
                Assert.NotNull(responseGetRetry);
                var responseModelGetRetry = responseGetRetry as OkObjectResult;
                Assert.NotNull(responseModelGetRetry);
                var resultGetRetry = responseModelGetRetry.Value as ContactResponseDTO;
                Assert.NotNull(resultGetRetry);

                Assert.Equal(result.Id, resultGetRetry.Id);
                Assert.NotEqual(result.Nome, resultGetRetry.Nome);
                Assert.NotEqual(result.Telefone, resultGetRetry.Telefone);
                Assert.NotEqual(result.DDD, resultGetRetry.DDD);
                Assert.NotEqual(result.Email, resultGetRetry.Email);
                Assert.NotEqual(result.Estado, resultGetRetry.Estado);
                Assert.NotEqual(result.Cidade, resultGetRetry.Cidade);

                var responseDelete = await _controller.Delete(id);
                Assert.NotNull(responseDelete);
                var responseModelDelete = responseDelete as NoContentResult;
                Assert.NotNull(responseModelDelete);

                var responseGetDeleted = await _controller.Get(id);
                Assert.NotNull(responseGetDeleted);
                var responseModelGetDeleted = responseGetDeleted as NotFoundResult;
                Assert.NotNull(responseModelGetDeleted);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}