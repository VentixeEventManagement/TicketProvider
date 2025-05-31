﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;
using TicketProvider.Business.Interfaces;
using TicketProvider.Business.Models;
using TicketProvider.SwaggerExamples;

namespace TicketProvider.Controllers
{
    /// <summary>
    /// Handles API requests related to event management, such as retrieving, creating, updating, and deleting events.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _eventService;
        private readonly ILogger<TicketController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="TicketController"/> class.
        /// </summary>
        /// <param name="eventService">The service for event business logic.</param>
        /// <param name="logger">The logger instance for logging errors and information.</param>
        public TicketController(ITicketService eventService, ILogger<TicketController> logger)
        {
            _eventService = eventService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves all events from the system.
        /// </summary>
        /// <returns>A list of all events.</returns>
        [HttpGet]
        [SwaggerResponse(StatusCodes.Status200OK, "A list of all events.", typeof(IEnumerable<Ticket>))]
        [SwaggerResponseExample(StatusCodes.Status200OK, typeof(TicketExample))]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "A server or database error occurred while retrieving events. This may be due to a database outage, data corruption, or an unhandled exception.")]
        public async Task<IActionResult> GetEvents()
        {
            try
            {
                var events = await _eventService.GetEventsAsync();
                return Ok(events);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving events.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while retrieving events. Please try again later.");
            }
        }

        /// <summary>
        /// Retrieves a specific event by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the event.</param>
        /// <returns>The event if found; otherwise, an error response.</returns>
        [HttpGet("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The event was found.", typeof(Ticket))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The event ID provided is invalid. It must be a positive integer.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No event was found with the specified ID.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "A server or database error occurred while retrieving the event.")]
        public async Task<IActionResult> GetEventById(int id)
        {
            if (id <= 0)
                return BadRequest("The event ID must be a positive integer.");

            try
            {
                var eventobj = await _eventService.GetEventByIdAsync(id);
                if (eventobj == null)
                {
                    return NotFound($"No event found with ID {id}.");
                }
                return Ok(eventobj);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving the event with ID {EventId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while retrieving the event. Please try again later.");
            }
        }

        /// <summary>
        /// Creates a new event.
        /// </summary>
        /// <param name="form">The event registration data.</param>
        /// <returns>Created if successful; otherwise, an error response.</returns>
        [HttpPost]
        [SwaggerResponse(StatusCodes.Status201Created, "The event was successfully created.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The event data provided is invalid.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "A server or database error occurred while creating the event.")]
        public async Task<IActionResult> Create(TicketRegistrationModel form)
        {
            if (!ModelState.IsValid)
                return BadRequest("The event data provided is invalid.");

            try
            {
                var result = await _eventService.CreateEventAsync(form);
                if (result)
                    return Created("", null);
                _logger.LogWarning("Failed to create the event due to a server error. Data: {@EventData}", form);
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to create the event due to a server error.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating the event. Data: {@EventData}", form);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while creating the event. Please try again later.");
            }
        }

        /// <summary>
        /// Updates an existing event.
        /// </summary>
        /// <param name="id">The unique identifier of the event to update.</param>
        /// <param name="form">The updated event data.</param>
        /// <returns>Ok if updated; otherwise, an error response.</returns>
        [HttpPut("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The event was successfully updated.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The event ID or data provided is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No event was found with the specified ID.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "A server or database error occurred while updating the event.")]
        public async Task<IActionResult> EditProject(int id, TicketRegistrationModel form)
        {
            if (id <= 0)
                return BadRequest("The event ID must be a positive integer.");
            if (!ModelState.IsValid)
                return BadRequest("The event data provided is invalid.");

            try
            {
                var result = await _eventService.EditEventAsync(id, form);
                if (result)
                    return Ok("Event was updated successfully.");
                _logger.LogWarning("No event found to update with ID {EventId}. Data: {@EventData}", id, form);
                return NotFound($"No event found with ID {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating the event with ID {EventId}. Data: {@EventData}", id, form);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while updating the event. Please try again later.");
            }
        }

        /// <summary>
        /// Deletes an event by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the event to delete.</param>
        /// <returns>Ok if deleted; otherwise, an error response.</returns>
        [HttpDelete("{id}")]
        [SwaggerResponse(StatusCodes.Status200OK, "The event was successfully deleted.")]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "The event ID provided is invalid.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "No event was found with the specified ID.")]
        [SwaggerResponse(StatusCodes.Status500InternalServerError, "A server or database error occurred while deleting the event.")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            if (id <= 0)
                return BadRequest("The event ID must be a positive integer.");

            try
            {
                var result = await _eventService.DeleteEventAsync(id);
                if (result)
                    return Ok("Event was deleted successfully.");
                _logger.LogWarning("No event found to delete with ID {EventId}.", id);
                return NotFound($"No event found with ID {id}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting the event with ID {EventId}.", id);
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while deleting the event. Please try again later.");
            }
        }
    }
}