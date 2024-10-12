using Newtonsoft.Json;
using RegionalContactsAPI.Core.DTO;
using System.Net.Http.Json;
using System.Text;

namespace RegionalContactsTest
{
    public class IntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        private CidadeController _controller;
        private CidadeService _service;
        private readonly IMemoryCache _memoryCache;

        public IntegrationTest(WebApplicationFactory<Program> factory)
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

        #region Cidade

        [Fact]
        public async Task GetAllCities()
        {
            try
            {
                HttpClient _client = _factory.CreateClient();
                var response = await _client.GetAsync("/Cidade");
                Assert.True((int)response.StatusCode == 200);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<Cidade>>();
                    Assert.NotNull(result);
                    Assert.NotEmpty(result);
                }
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
                HttpClient _client = _factory.CreateClient();
                var response = await _client.GetAsync("/Cidade");
                Assert.True((int)response.StatusCode == 200);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<Cidade>>();
                    Assert.NotNull(result);
                    Assert.NotEmpty(result);
                    bool containsCity = result.Any(c => c.NomeCidade == "SÃO PAULO" && c.DDD == "11");
                    Assert.True(containsCity);
                }
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
                HttpClient _client = _factory.CreateClient();
                var responseGetAll = await _client.GetAsync("/Cidade");
                Assert.True((int)responseGetAll.StatusCode == 200);
                if (responseGetAll.IsSuccessStatusCode)
                {
                    var resultGetAll = await responseGetAll.Content.ReadFromJsonAsync<IEnumerable<Cidade>>();
                    Assert.NotNull(resultGetAll);
                    Assert.NotEmpty(resultGetAll);
                    var random = new Random();
                    int count = resultGetAll.Count();
                    int randomIndex = random.Next(count);
                    Cidade city = resultGetAll.ElementAt(randomIndex);
                    var response = await _client.GetAsync($"/Cidade/{city.Id}");
                    Assert.True((int)response.StatusCode == 200);
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadFromJsonAsync<Cidade>();
                        Assert.NotNull(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #endregion Cidade

        #region RegionalContacts

        [Fact]
        public async Task GetAllContacts()
        {
            try
            {
                HttpClient _client = _factory.CreateClient();
                var response = await _client.GetAsync("/RegionalContacts");
                Assert.True((int)response.StatusCode == 200);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<ContactResponseDTO>>();
                    Assert.NotNull(result);
                    Assert.NotEmpty(result);
                }
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

                HttpClient _client = _factory.CreateClient();
                var response = await _client.GetAsync($"/RegionalContacts?ddd={ddd}");
                Assert.True((int)response.StatusCode == 200);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<IEnumerable<ContactResponseDTO>>();
                    Assert.NotNull(result);
                    Assert.NotEmpty(result);
                    var validateDDD = result.Any(x => x.DDD == ddd);
                    Assert.True(validateDDD);
                }
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
                HttpClient _client = _factory.CreateClient();
                var dto = new ContactRequestDTO()
                {
                    Cidade = "São Paulo",
                    DDD = 11,
                    Email = "teste@gmail.com",
                    Estado = "SP",
                    Nome = "Teste Soares",
                    Telefone = 999999999
                };
                var jsonContent = JsonConvert.SerializeObject(dto);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("/RegionalContacts", content);
                Assert.True((int)response.StatusCode == 201);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<ContactResponseDTO>();
                    Assert.NotNull(result);

                    var id = result.Id;
                    var responseGet = await _client.GetAsync($"/RegionalContacts/{id}");
                    Assert.True((int)responseGet.StatusCode == 200);
                    if (responseGet.IsSuccessStatusCode)
                    {
                        var resultGet = await responseGet.Content.ReadFromJsonAsync<ContactResponseDTO>();
                        Assert.NotNull(resultGet);
                        Assert.Equal(result.Id, resultGet.Id);
                        Assert.Equal(result.Nome, resultGet.Nome);
                        Assert.Equal(result.Telefone, resultGet.Telefone);
                        Assert.Equal(result.DDD, resultGet.DDD);
                        Assert.Equal(result.Email, resultGet.Email);
                        Assert.Equal(result.Estado, resultGet.Estado);
                        Assert.Equal(result.Cidade, resultGet.Cidade);

                        var updateDto = new ContactRequestDTO()
                        {
                            Cidade = "Fortaleza",
                            DDD = 85,
                            Email = "teste123@gmail.com",
                            Estado = "CE",
                            Nome = "Teste da Silva",
                            Telefone = 988888888
                        };

                        var jsonUpdate = JsonConvert.SerializeObject(updateDto);
                        var contentUpdate = new StringContent(jsonUpdate, Encoding.UTF8, "application/json");
                        var responseUpdate = await _client.PutAsync($"/RegionalContacts/{id}", contentUpdate);
                        Assert.True((int)responseUpdate.StatusCode == 204);
                        if (responseUpdate.IsSuccessStatusCode)
                        {
                            var responseGetRetry = await _client.GetAsync($"/RegionalContacts/{id}");
                            Assert.True((int)responseGetRetry.StatusCode == 200);
                            if (responseGetRetry.IsSuccessStatusCode)
                            {
                                var resultGetRetry = await responseGetRetry.Content.ReadFromJsonAsync<ContactResponseDTO>();

                                Assert.NotNull(resultGetRetry);
                                Assert.Equal(result.Id, resultGetRetry.Id);
                                Assert.NotEqual(result.Nome, resultGetRetry.Nome);
                                Assert.NotEqual(result.Telefone, resultGetRetry.Telefone);
                                Assert.NotEqual(result.DDD, resultGetRetry.DDD);
                                Assert.NotEqual(result.Email, resultGetRetry.Email);
                                Assert.NotEqual(result.Estado, resultGetRetry.Estado);
                                Assert.NotEqual(result.Cidade, resultGetRetry.Cidade);

                                var responseDelete = await _client.DeleteAsync($"/RegionalContacts/{id}");
                                Assert.True((int)responseDelete.StatusCode == 204);
                                if (responseDelete.IsSuccessStatusCode)
                                {
                                    var responseGetDeleted = await _client.GetAsync($"/RegionalContacts/{id}");
                                    Assert.True((int)responseGetDeleted.StatusCode == 404);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        #endregion RegionalContacts
    }
}