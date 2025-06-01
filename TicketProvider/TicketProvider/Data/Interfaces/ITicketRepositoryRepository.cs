// This document was formatted and refined by AI
using System.Linq.Expressions;
using TicketProvider.Data.Entities;

namespace TicketProvider.Data.Interfaces
{
    /// <summary>
    /// Provides data access methods specific to <see cref="TicketEntity"/> objects.
    /// Inherits basic CRUD operations from <see cref="IBaseRepository{EventEntity}"/>.
    /// </summary>
    public interface ITicketRepository : IBaseRepository<TicketEntity>
    {

        /// <summary>
        /// Retrieves all ticket entities for a specific event by its unique identifier.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of ticket entities for the specified event.
        /// </returns>
        Task<IEnumerable<TicketEntity>> GetAllByEventIdAsync(int eventId);
    }
}