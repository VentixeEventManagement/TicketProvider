using TicketProvider.Business.Models;
using TicketProvider.Data.Entities;

namespace TicketProvider.Business.Factories
{
    /// <summary>
    /// Provides factory methods for creating Ticket and TicketEntity objects.
    /// </summary>
    public static class TicketFactory
    {
        /// <summary>
        /// Creates a TicketEntity from a TicketRegistrationModel.
        /// </summary>
        /// <param name="form">The registration model containing ticket data.</param>
        /// <returns>A new TicketEntity or null if the input is null.</returns>
        public static TicketEntity? Create(TicketRegistrationModel form) => form == null ? null : new()
        {
            EventId = form.EventId,
            HolderName = form.HolderName,
            HolderEmail = form.HolderEmail,
            Price = form.Price ?? 0m,
        };

        /// <summary>
        /// Creates a Ticket model from a TicketEntity.
        /// </summary>
        /// <param name="entity">The entity containing ticket data.</param>
        /// <returns>A new Ticket model or null if the input is null.</returns>
        public static Ticket? Create(TicketEntity entity)
        {
            if (entity == null)
                return null;

            return new Ticket
            {
                Id = entity.Id,
                EventId = entity.EventId,
                HolderName = entity.HolderName,
                HolderEmail = entity.HolderEmail,
                PurchaseDate = entity.PurchaseDate,
                Price = entity.Price,
            };
        }
    }
}