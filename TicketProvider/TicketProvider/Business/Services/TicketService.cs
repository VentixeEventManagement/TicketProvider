using System.Linq.Expressions;
using TicketProvider.Business.Factories;
using TicketProvider.Business.Interfaces;
using TicketProvider.Business.Models;
using TicketProvider.Data.Entities;
using TicketProvider.Data.Interfaces;

namespace TicketProvider.Business.Services
{
    /// <summary>
    /// Provides business logic for managing events, including creating, retrieving, updating, and deleting events.
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _eventRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketService"/> class.
        /// </summary>
        /// <param name="eventRepository">The repository used to access event data.</param>
        public TicketService(ITicketRepository eventRepository)
        {
            _eventRepository = eventRepository;
        }

        /// <summary>
        /// Retrieves all events from the data store.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of all events.
        /// </returns>
        public async Task<IEnumerable<Ticket>> GetEventsAsync()
        {
            var entities = await _eventRepository.GetAllAsync();
            var events = entities.Select(TicketFactory.Create);
            return events!;
        }

        /// <summary>
        /// Retrieves a specific event by its unique identifier.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the event if found; otherwise, null.
        /// </returns>
        public async Task<Ticket?> GetEventByIdAsync(int eventId)
        {
            var eventobj = await _eventRepository.GetAsync(p => p.Id == eventId);
            if (eventobj == null)
                return null!;
            return TicketFactory.Create(eventobj)!;
        }

        /// <summary>
        /// Creates a new event using the provided registration data.
        /// </summary>
        /// <param name="eventData">The data for the event to be created.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the event was created successfully; otherwise, false.
        /// </returns>
        public async Task<bool> CreateEventAsync(TicketRegistrationModel eventData)
        {
            var eventEntity = TicketFactory.Create(eventData);

            if (eventEntity == null)
                return false;

            var result = await _eventRepository.AddAsync(eventEntity);
            return result;
        }

        /// <summary>
        /// Updates an existing event with new data.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event to update.</param>
        /// <param name="newEventData">The new data for the event.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the event was updated successfully; otherwise, false.
        /// </returns>
        public async Task<bool> EditEventAsync(int eventId, TicketRegistrationModel newEventData)
        {
            var targetEvent = await _eventRepository.GetAsync(e => e.Id == eventId);
            if (targetEvent == null)
                return false;

            targetEvent.Name = newEventData.Name;
            targetEvent.Description = newEventData.Description;
            targetEvent.StartDate = newEventData.StartDate;
            targetEvent.EndDate = newEventData.EndDate;
            targetEvent.Location = newEventData.Location;
            targetEvent.TicketPrice = newEventData.TicketPrice;
            targetEvent.TicketAmount = newEventData.TicketAmount;

            bool result = await _eventRepository.UpdateAsync(targetEvent);
            return result;
        }

        /// <summary>
        /// Deletes an event with the specified unique identifier.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the event was deleted successfully; otherwise, false.
        /// </returns>
        public async Task<bool> DeleteEventAsync(int eventId)
        {
            var targetEvent = await _eventRepository.GetAsync(e => e.Id == eventId);
            if (targetEvent == null)
                return false;

            bool result = await _eventRepository.RemoveAsync(targetEvent);
            return result;
        }
    }
}