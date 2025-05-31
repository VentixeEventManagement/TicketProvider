using Microsoft.EntityFrameworkCore;
using TicketProvider.Data.Entities;

namespace TicketProvider.Data.Contexts
{
    /// <summary>
    /// Represents the Entity Framework database context for the application.
    /// Provides access to the database tables and manages entity objects during runtime.
    /// </summary>
    public class TicketContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketContext"/> class with the specified options.
        /// </summary>
        /// <param name="options">The options to be used by the DbContext.</param>
        public TicketContext(DbContextOptions<TicketContext> options) : base(options)
        {
        }

        /// <summary>
        /// Gets or sets the events table in the database.
        /// </summary>
        public DbSet<TicketEntity> Tickets { get; set; }
    }
}
