namespace TicketProvider.Business.Models
{
    /// <summary>
    /// Represents a ticket for an event.
    /// </summary>
    public class Ticket
    {
        /// <summary>
        /// The unique identifier for the ticket.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The event this ticket is for.
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// The name of the ticket holder.
        /// </summary>
        public string? HolderName { get; set; }

        /// <summary>
        /// The email of the ticket holder.
        /// </summary>
        public string? HolderEmail { get; set; }

        /// <summary>
        /// The date and time the ticket was purchased.
        /// </summary>
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// The price paid for the ticket, including any applicable taxes or discounts.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The status of the ticket (e.g., Available, Sold, Cancelled).
        /// </summary>
        public string Status { get; set; } = "Available";
    }
}