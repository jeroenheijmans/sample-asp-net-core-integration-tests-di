using System.Threading.Tasks;
using Xunit;

namespace Foo.Web.Tests
{
    public class ValuesControllerTests : IClassFixture<IntegrationTestsFixture>
    {
        private readonly IntegrationTestsFixture fixture;

        public ValuesControllerTests(IntegrationTestsFixture fixture)
        {
            this.fixture = fixture;
        }

        [Fact]
        public async Task Integration_test_uses_decorator()
        {
            var client = fixture.CreateClient();
            var result = await client.GetAsync("/api/values");
            var data = await result.Content.ReadAsStringAsync();
            result.EnsureSuccessStatusCode();
            Assert.Equal("Service Value (decorated)", data);
        }
    }
}
