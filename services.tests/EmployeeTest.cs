using core.tests;
using entities;
using entities.config;
using entities.entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace services.tests
{
    [TestCaseOrderer("core.tests.PriorityOrderer", "core.tests")]
    public class PlanejamentoSafraTests
    {
        readonly Employee employee = new Employee
        {
            Id = Guid.NewGuid(),
            Name = "New Employee"
        };


        [Fact, TestPriority(0)]
        public void TEST_MOCK_REPOSITORY_INSERT()
        {
            var options = new DbContextOptionsBuilder<EFAppContext>()
                             //database server host
                             .UseInMemoryDatabase("localhost")
                             .Options;

            using (var context = new EFAppContext(options))
            {
                context.Employees.Add(employee);
                context.SaveChanges();
            }

            using (var context = new EFAppContext(options))
            {
                var service = new EFRepository<Employee>(context);
                Assert.Equal(1, context.Employees.Count());

            }
        }

        [Fact, TestPriority(1)]
        public void TEST_MOCK_REPOSITORY_GETALL()
        {
            var options = new DbContextOptionsBuilder<EFAppContext>()
                             .UseInMemoryDatabase("localhost")
                             .Options;
            using (var context = new EFAppContext(options))
            {
                var service = new EFRepository<Employee>(context);
                var result = service.GetAll().ToList();
                Assert.NotNull(result);
            }
        }



        [Fact, TestPriority(2)]
        public void TEST_MOCK_REPOSITORY_UPDATE()
        {
            var options = new DbContextOptionsBuilder<EFAppContext>()
                             .UseInMemoryDatabase("localhost")
                             .Options;

            using (var context = new EFAppContext(options))
            {
                var service = new EFRepository<Employee>(context);
                var result = service.GetById(employee.Id);

                result.UpdatedAt = DateTime.Today;
                service.Update(result);
                context.SaveChanges();

                Assert.NotNull(result);
            }
        }

        [Fact, TestPriority(3)]
        public void TEST_MOCK_REPOSITORY_DELETE()
        {
            var options = new DbContextOptionsBuilder<EFAppContext>()
                            .UseInMemoryDatabase("localhost")
                            .Options;

            using (var context = new EFAppContext(options))
            {
                var service = new EFRepository<Employee>(context);

                var item = service.GetById(employee.Id); ;

                service.DeleteRange(employee => employee.Id == item.Id);
                context.SaveChanges();

                Assert.Equal(0, context.Employees.Count());
            }
        }

    }
}
