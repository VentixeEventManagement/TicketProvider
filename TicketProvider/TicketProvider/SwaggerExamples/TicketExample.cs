// This document was formatted and refined by AI
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
        /// An <see cref="Ticket"/> instance representing a typical ticket, used for Swagger UI examples.
        /// </returns>
        public Ticket GetExamples()
        {
            return new Ticket
            {
                Id = 1,
                EventId = 101,
                HolderName = "Hans Doe",
                HolderEmail = "jane.doe@example.com",
                PurchaseDate = DateTime.Parse("2025-05-28T14:30:00Z"),
                Price = 59.99m
            };
        }
    }
}