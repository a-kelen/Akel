using Akel;
using Akel.Domain.Core;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTest
{
    public class ResultControllerTest :IClassFixture<TestClientProvider<Startup>>
    {
        bool GetByIdPass = false;
        [Fact]
        public async Task GetTest()
        {
            var client = new TestClientProvider<Startup>().Client;

            var res = await client.GetAsync("api/Results");
            res.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async Task PostTest()
        {
            var client = new TestClientProvider<Startup>().Client;
            Result result = new Result()
            {
                Id = new Guid("ec62beab-e20b-48de-ba07-8eb98dd3da03"),
                TestId = new Guid("F9ED9EF0-C26A-4BCB-2221-08D7766D8482"),
                UserProfileId = new Guid("E71A978C-FAC1-45C9-D98D-08D775C04CED"),
                Value = 0.7
            };
            var res = await client.PostAsync("api/Results", new StringContent(
                    JsonConvert.SerializeObject(result),
                    Encoding.UTF8,
                    "application/json"
                ));
            res.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.Created, res.StatusCode);
        }
        [Fact]
        public async Task GetByIdTest()
        {
            var client = new TestClientProvider<Startup>().Client;

            var res = await client.GetAsync("api/Results/993341A0-FA69-4FE9-0B94-08D77EF2A0E3");
            res.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        [Fact]
        public async Task GetByUserTest()
        {
            var client = new TestClientProvider<Startup>().Client;

            var res = await client.GetAsync("api/Results/byuser/E71A978C-FAC1-45C9-D98D-08D775C04CED");
            res.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
        
        [Fact]
        public async Task PutTest()
        {
            var client = new TestClientProvider<Startup>().Client;
            Result result = new Result()
            {
                Id = new Guid("ec62beab-e20b-48de-ba07-8eb98dd3da03"),
                TestId = new Guid("F9ED9EF0-C26A-4BCB-2221-08D7766D8482"),
                UserProfileId = new Guid("E71A978C-FAC1-45C9-D98D-08D775C04CED"),
                Value = 0.85
            };
            var res = await client.PutAsync("api/Results/ec62beab-e20b-48de-ba07-8eb98dd3da03", new StringContent(
                    JsonConvert.SerializeObject(result),
                    Encoding.UTF8,
                    "application/json"
                ));
            res.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }

       
        [Fact]
        public async Task DeleteTest()
        {
            var client = new TestClientProvider<Startup>().Client;

            var res = await client.DeleteAsync("api/Results/ec62beab-e20b-48de-ba07-8eb98dd3da03");
            res.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, res.StatusCode);
        }
    }
}
