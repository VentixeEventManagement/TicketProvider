// This document was formatted and refined by AI
using TicketProvider.Business.Factories;
using TicketProvider.Business.Interfaces;
using TicketProvider.Business.Models;
using TicketProvider.Data.Entities;
using TicketProvider.Data.Interfaces;

namespace TicketProvider.Business.Services
{
    /// <summary>
    /// Provides business logic for managing tickets, including creating, retrieving, updating, and deleting tickets.
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketService"/> class.
        /// </summary>
        /// <param name="ticketRepository">The repository used to access ticket data.</param>
        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// Retrieves all tickets from the data store.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of all tickets.
        /// </returns>
        public async Task<IEnumerable<Ticket>> GetTicketsAsync()
        {
            var entities = await _ticketRepository.GetAllAsync();
            var tickets = entities.Select(TicketFactory.Create);
            return tickets!;
        }

        /// <summary>
        /// Retrieves a specific ticket by its unique identifier.
        /// </summary>
        /// <param name="ticketId">The unique identifier of the ticket to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains the ticket if found; otherwise, null.
        /// </returns>
        public async Task<Ticket?> GetTicketByIdAsync(int ticketId)
        {
            var ticketEntity = await _ticketRepository.GetAsync(p => p.Id == ticketId);
            if (ticketEntity == null)
                return null;
            return TicketFactory.Create(ticketEntity);
        }

        /// <summary>
        /// Retrieves all tickets for a specific event by its unique identifier.
        /// </summary>
        /// <param name="eventId">The unique identifier of the event whose tickets are to be retrieved.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains a collection of tickets for the specified event.
        /// </returns>
        public async Task<IEnumerable<Ticket>> GetTicketsByEventIdAsync(int eventId)
        {
            var entities = await _ticketRepository.GetAllByEventIdAsync(eventId);
            return entities.Select(TicketFactory.Create)!;
        }

        /// <summary>
        /// Creates a new ticket using the provided registration data.
        /// </summary>
        /// <param name="ticketData">The data for the ticket to be created.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the ticket was created successfully; otherwise, false.
        /// </returns>
        public async Task<bool> CreateTicketAsync(TicketRegistrationModel ticketData)
        {
            var ticketEntity = TicketFactory.Create(ticketData);

            if (ticketEntity == null)
                return false;

            var result = await _ticketRepository.AddAsync(ticketEntity);
            return result;
        }

        /// <summary>
        /// Updates an existing ticket with new data.
        /// </summary>
        /// <param name="ticketId">The unique identifier of the ticket to update.</param>
        /// <param name="newTicketData">The new data for the ticket.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the ticket was updated successfully; otherwise, false.
        /// </returns>
        public async Task<bool> EditTicketAsync(int ticketId, TicketRegistrationModel newTicketData)
        {
            var targetTicket = await _ticketRepository.GetAsync(t => t.Id == ticketId);
            if (targetTicket == null)
                return false;

            targetTicket.EventId = newTicketData.EventId;
            targetTicket.HolderName = newTicketData.HolderName;
            targetTicket.HolderEmail = newTicketData.HolderEmail;
            targetTicket.Price = newTicketData.Price ?? targetTicket.Price;

            bool result = await _ticketRepository.UpdateAsync(targetTicket);
            return result;
        }

        /// <summary>
        /// Deletes a ticket with the specified unique identifier.
        /// </summary>
        /// <param name="ticketId">The unique identifier of the ticket to delete.</param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result contains true if the ticket was deleted successfully; otherwise, false.
        /// </returns>
        public async Task<bool> DeleteTicketAsync(int ticketId)
        {
            var targetTicket = await _ticketRepository.GetAsync(t => t.Id == ticketId);
            if (targetTicket == null)
                return false;

            bool result = await _ticketRepository.RemoveAsync(targetTicket);
            return result;
        }
    }
}