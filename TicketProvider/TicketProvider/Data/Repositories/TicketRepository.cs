using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TicketProvider.Data.Contexts;
using TicketProvider.Data.Entities;
using TicketProvider.Data.Interfaces;

namespace TicketProvider.Data.Repositories
{
    /// <summary>
    /// Provides data access methods specific to event entities, using Entity Framework Core.
    /// </summary>
    public class TicketRepository : BaseRepository<TicketEntity>, ITicketRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketRepository"/> class with the specified data context.
        /// </summary>
        /// <param name="context">The database context to use for event data operations.</param>
        public TicketRepository(TicketContext context) : base(context)
        {
        }

        /// <summary>
        /// Retrieves all event entities from the database asynchronously.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of all event entities.
        /// </returns>
        public override async Task<IEnumerable<TicketEntity>> GetAllAsync()
        {
            var entities = await _db.ToListAsync();
            return entities;
        }

        /// <summary>
        /// Retrieves a single event entity that matches the specified condition.
        /// </summary>
        /// <param name="expression">The condition to match the event entity against.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the event entity if found; otherwise, null.
        /// </returns>
        public override async Task<TicketEntity?> GetAsync(Expression<Func<TicketEntity, bool>> expression)
        {
            var entity = await _db.FirstOrDefaultAsync(expression);
            return entity;
        }

        /// <summary>
        /// Retrieves all ticket entities for a specific event by its unique identifier.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of ticket entities for the specified event.
        /// </returns>
        public async Task<IEnumerable<TicketEntity>> GetAllByEventIdAsync(int eventId)
        {
            return await _db.Where(t => t.EventId == eventId).ToListAsync();
        }

        // Add more advanced methods here like filtering by date, location, etc.
    }
}