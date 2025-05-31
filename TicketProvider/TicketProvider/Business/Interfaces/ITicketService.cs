using TicketProvider.Business.Models;

namespace TicketProvider.Business.Interfaces
{
    /// <summary>
    /// Defines methods for managing events in the system.
    /// </summary>
    public interface ITicketService
    {
        /// <summary>
        /// Creates a new event using the provided registration data.
        /// </summary>
        /// <param name="eventData">The data for the event to be created.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the event was created successfully; otherwise, false.
        /// </returns>
        Task<bool> CreateEventAsync(TicketRegistrationModel eventData);

        /// <summary>
        /// Deletes an event with the specified unique identifier.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the event was deleted successfully; otherwise, false.
        /// </returns>
        Task<bool> DeleteEventAsync(int eventId);

        /// <summary>
        /// Updates an existing event with new data.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event to update.</param>
        /// <param name="newEventData">The new data for the event.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the event was updated successfully; otherwise, false.
        /// </returns>
        Task<bool> EditEventAsync(int eventId, TicketRegistrationModel newEventData);

        /// <summary>
        /// Retrieves a specific event by its unique identifier.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the event if found; otherwise, null.
        /// </returns>
        Task<Ticket?> GetEventByIdAsync(int eventId);

        /// <summary>
        /// Retrieves all events in the system.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of all events.
        /// </returns>
        Task<IEnumerable<Ticket>> GetEventsAsync();
    }
}