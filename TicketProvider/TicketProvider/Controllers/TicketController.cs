using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using TicketProvider.Business.Interfaces;
using TicketProvider.Business.Models;
using TicketProvider.SwaggerExamples;

namespace TicketProvider.Controllers
{
    /// <summary>
    /// Handles API requests related to ticket management, such as retrieving, creating, updating, and deleting tickets.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ILogger<TicketController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketController"/> class.
        /// </summary>
        /// <param name="ticketService">The service for ticket business logic.</param>
        /// <param name="logger">The logger instance for logging errors and information.</param>
        public TicketController(ITicketService ticketService, ILogger<TicketController> logger)
        {
            _ticketService = ticketService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all tickets from the system.
        /// </summary>
        /// <returns>A list of all tickets.</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "A list of all tickets.", typeof(IEnumerable<Ticket>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TicketExample))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "A server or database error occurred while retrieving tickets.")]
        public async Task<IActionResult> GetTickets()
        {
            try
            {
                var tickets = await _ticketService.GetTicketsAsync();
                return Ok(tickets);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving tickets.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while retrieving tickets. Please try again later.");
            }
        }

        /// <summary>
        /// Retrieves a specific ticket by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ticket.</param>
        /// <returns>The ticket if found; otherwise, an error response.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The ticket was found.", typeof(Ticket))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The ticket ID provided is invalid. It must be a positive integer.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No ticket was found with the specified ID.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "A server or database error occurred while retrieving the ticket.")]
        public async Task<IActionResult> GetTicketById(int id)
        {
            if (id <= 0)
                return BadRequest("The ticket ID must be a positive integer.");

            try
            {
                var ticket = await _ticketService.GetTicketByIdAsync(id);
                if (ticket == null)
                {
                    return NotFound($"No ticket found with ID {id}.");
                }
                return Ok(ticket);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving the ticket with ID {TicketId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while retrieving the ticket. Please try again later.");
            }
        }

        /// <summary>
        /// Creates a new ticket.
        /// </summary>
        /// <param name="form">The ticket registration data.</param>
        /// <returns>Created if successful; otherwise, an error response.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "The ticket was successfully created.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The ticket data provided is invalid.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "A server or database error occurred while creating the ticket.")]
        public async Task<IActionResult> Create(TicketRegistrationModel form)
        {
            if (!ModelState.IsValid)
                return BadRequest("The ticket data provided is invalid.");

            try
            {
                var result = await _ticketService.CreateTicketAsync(form);
                if (result)
                    return Created("", null);
                _logger.LogWarning("Failed to create the ticket due to a server error. Data: {@TicketData}", form);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create the ticket due to a server error.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating the ticket. Data: {@TicketData}", form);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while creating the ticket. Please try again later.");
            }
        }

        /// <summary>
        /// Updates an existing ticket.
        /// </summary>
        /// <param name="id">The unique identifier of the ticket to update.</param>
        /// <param name="form">The updated ticket data.</param>
        /// <returns>Ok if updated; otherwise, an error response.</returns>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The ticket was successfully updated.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The ticket ID or data provided is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No ticket was found with the specified ID.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "A server or database error occurred while updating the ticket.")]
        public async Task<IActionResult> EditTicket(int id, TicketRegistrationModel form)
        {
            if (id <= 0)
                return BadRequest("The ticket ID must be a positive integer.");
            if (!ModelState.IsValid)
                return BadRequest("The ticket data provided is invalid.");

            try
            {
                var result = await _ticketService.EditTicketAsync(id, form);
                if (result)
                    return Ok("Ticket was updated successfully.");
                _logger.LogWarning("No ticket found to update with ID {TicketId}. Data: {@TicketData}", id, form);
                return NotFound($"No ticket found with ID {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating the ticket with ID {TicketId}. Data: {@TicketData}", id, form);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while updating the ticket. Please try again later.");
            }
        }

        /// <summary>
        /// Deletes a ticket by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the ticket to delete.</param>
        /// <returns>Ok if deleted; otherwise, an error response.</returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The ticket was successfully deleted.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The ticket ID provided is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No ticket was found with the specified ID.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "A server or database error occurred while deleting the ticket.")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (id <= 0)
                return BadRequest("The ticket ID must be a positive integer.");

            try
            {
                var result = await _ticketService.DeleteTicketAsync(id);
                if (result)
                    return Ok("Ticket was deleted successfully.");
                _logger.LogWarning("No ticket found to delete with ID {TicketId}.", id);
                return NotFound($"No ticket found with ID {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting the ticket with ID {TicketId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while deleting the ticket. Please try again later.");
            }
        }
    }
}
