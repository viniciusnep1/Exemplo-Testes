using core.tests;
using entities.entities;
using FluentAssertions;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using web.tests.core;
using Xunit;

namespace web.tests.scenarios
{
    public class EmployeeControllerTests : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        private readonly Employee employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "New Employee"
        };

        public EmployeeControllerTests(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }

        [Fact, TestPriority(1)]
        public async Task ReturnOkResponseCreate()
        {
            var postRequest = new
            {
                Url = "/api/Employee",
                Body = new
                {
                    employee
                }
            };
            var response = await Client.PostAsync(postRequest.Url, ContentHelper.GetStringContent(postRequest.Body));
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact, TestPriority(2)]
        public async Task ReturnOkResponseGetById()
        {
            var response = await Client.GetAsync($"/api/Employee/{employee.Id}");
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
