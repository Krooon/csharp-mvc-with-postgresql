using exercise.api.Models;
using exercise.api.Repository;
using Microsoft.AspNetCore.Mvc;

namespace exercise.api.EndPoints
{
    public static class EmployeeApi
    {
        public static void ConfigureEmployeeApi(this WebApplication application)
        {
            application.MapGet("/employees", GetEmployees);
            application.MapGet("/employees/{id}", GetEmployee);
            application.MapPost("/employees", AddEmployee);
            application.MapPut("/employees/{id}", UpdateEmployee);
            application.MapDelete("/employees/{id}", DeleteEmployee);

        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetEmployees(IRepository repository)
        {
            return Results.Ok(repository.GetEmployees());
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetEmployee(int id, IRepository repository)
        {
            try
            {
                return await Task.Run(() =>
                {
                    var employee = repository.GetEmployee(id);
                    return repository.AddEmployee(employee) ? Results.Ok(employee) : Results.NotFound();
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }
        [ProducesResponseType(StatusCodes.Status201Created)]
        private static async Task<IResult> UpdateEmployee(int id, Employee employee, IRepository repository)
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (repository.UpdateEmployee(id, employee)) return Results.Created("https://localhost:7174/employees", employee);
                    return Results.NotFound();
                });

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> AddEmployee(Employee employee, IRepository repository)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return repository.AddEmployee(employee) ? Results.Created("https://localhost:7174/employees", employee) : Results.NotFound();
                });
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }


        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        private static async Task<IResult> DeleteEmployee(int id, IRepository repository)
        {
            try
            {
                if (repository.DeleteEmployee(id)) return Results.Ok();
                return Results.NotFound();

            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        }

    }
}
