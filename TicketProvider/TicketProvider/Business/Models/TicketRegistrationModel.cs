// This document was formatted and refined by AI
namespace TicketProvider.Business.Models
{
    /// <summary>
    /// Represents the data required to register (purchase) a ticket for an event.
    /// </summary>
    public class TicketRegistrationModel
    {
        /// <summary>
        /// The event this ticket is for.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// The name of the ticket holder.
        /// </summary>
        public string HolderName { get; set; } = null!;

        /// <summary>
        /// The email of the ticket holder.
        /// </summary>
        public string HolderEmail { get; set; } = null!;

        /// <summary>
        /// The price paid for the ticket, if applicable.
        /// </summary>
        public decimal? Price { get; set; }
    }
}