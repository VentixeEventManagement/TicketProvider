using TicketProvider.Business.Models;

namespace TicketProvider.Business.Interfaces
{
    /// <summary>
    /// Defines methods for managing tickets in the system.
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Creates a new ticket using the provided registration data.
        /// </summary>
        /// <param name="ticketData">The data for the ticket to be created.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the ticket was created successfully; otherwise, false.
        /// </returns>
        Task<bool> CreateTicketAsync(TicketRegistrationModel ticketData);

        /// <summary>
        /// Deletes a ticket with the specified unique identifier.
        /// </summary>
        /// <param name="ticketId">The unique identifier of the ticket to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the ticket was deleted successfully; otherwise, false.
        /// </returns>
        Task<bool> DeleteTicketAsync(int ticketId);

        /// <summary>
        /// Updates an existing ticket with new data.
        /// </summary>
        /// <param name="ticketId">The unique identifier of the ticket to update.</param>
        /// <param name="newTicketData">The new data for the ticket.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the ticket was updated successfully; otherwise, false.
        /// </returns>
        Task<bool> EditTicketAsync(int ticketId, TicketRegistrationModel newTicketData);

        /// <summary>
        /// Retrieves a specific ticket by its unique identifier.
        /// </summary>
        /// <param name="ticketId">The unique identifier of the ticket to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the ticket if found; otherwise, null.
        /// </returns>
        Task<Ticket?> GetTicketByIdAsync(int ticketId);

        /// <summary>
        /// Retrieves all tickets in the system.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of all tickets.
        /// </returns>
        Task<IEnumerable<Ticket>> GetTicketsAsync();
    }
}