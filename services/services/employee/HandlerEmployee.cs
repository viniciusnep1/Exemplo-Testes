using core.config;
using entities.entities;
using entities.repository;
using MediatR;
using services.services.employee.commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace services.services.employee
{
    public class HandlerEmployee : IRequestHandler<ReadEmployeeCommand, Response>,
        IRequestHandler<CreateEmployeeCommand, Response>,
        IRequestHandler<UpdateEmployeeCommand, Response>,
        IRequestHandler<DeleteEmployeeCommand, Response>
    {
        private readonly IRepository<Employee> repository;

        public HandlerEmployee(IRepository<Employee> repository)
        {
            this.repository = repository;
        }

        public async Task<Response> Handle(ReadEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Way to get all entities
            //var employees = repository.GetAll(x => x.Active && !x.DeletedAt.HasValue);


            var employees = new List<Employee>();

            employees.Add(new Employee
            {
                Name = "Employee 1",
            });

            employees.Add(new Employee
            {
                Name = "Employee 2",
            });

            return await Task.FromResult(new Response(employees));
        }

        public async Task<Response> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = new Employee
            {
                Name = request.Name,
                Description = request.Description
            };

            // Way to create an entity
            //await repository.Create(employee);
            //await repository.SaveChanges();

            return await Task.FromResult(new Response(employee));
        }

        public async Task<Response> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = repository.GetById(request.Id);

            employee.Name = request.Name;
            employee.Description = request.Description;

            // Way to update an entity
            //repository.Update(employee);
            //await repository.SaveChanges();

            return await Task.FromResult(new Response(employee));
        }

        public async Task<Response> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = repository.GetById(request.Id);

            // Way to delete an entity
            //repository.Delete(employee);
            //await repository.SaveChanges();

            return await Task.FromResult(new Response(employee));
        }
    }
}
