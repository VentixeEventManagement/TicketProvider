using System;
using Swashbuckle.AspNetCore.Filters;
using TicketProvider.Business.Models;

namespace TicketProvider.SwaggerExamples
{
    /// <summary>
    /// Provides a sample <see cref="Ticket"/> instance for Swagger documentation.
    /// </summary>
    public class TicketExample : IExamplesProvider<Ticket>
    {
        /// <summary>
        /// Returns an example <see cref="Ticket"/> object with realistic sample data.
        /// </summary>
        /// <returns>
        /// An <see cref="Ticket"/> instance representing a typical event, used for Swagger UI examples.
        /// </returns>
        public Ticket GetExamples()
        {
            return new Ticket
            {
                Id = 1,
                EventId = "comicon",
                Description = "Annual comic and pop culture convention.",
                StartDate = DateTime.Parse("2025-05-28T12:12:03.663Z"),
                EndDate = DateTime.Parse("2025-05-29T18:00:00.000Z"),
                Location = "Convention Center, Cityville",
                TicketPrice = 49,
                TicketAmount = "500"
            };
        }
    }
}