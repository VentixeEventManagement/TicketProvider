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
        // Add event-specific data access methods here if needed in the future.
    }
}