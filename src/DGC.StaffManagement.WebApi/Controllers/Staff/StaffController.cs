using DGC.StaffManagement.Application.Features.Staff.Command;
using DGC.StaffManagement.Application.Features.Staff.Commands;
using DGC.StaffManagement.Application.Features.Staff.Queries;
using DGC.StaffManagement.Application.Interfaces;
using DGC.StaffManagement.Application.Services;
using DGC.StaffManagement.Shared.Commons;
using DGC.StaffManagement.Shared.Paging;
using DGC.StaffManagement.WebApi.Controllers.BaseControllers;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Net.Mime;

namespace DGC.StaffManagement.WebApi.Controllers.Staff
{

    public class StaffController(IStaffService staffService) : AControllerBase
    {
        /// <summary>
        /// Get paginated list of staff members
        /// </summary>
        /// <param name="query">Filter and pagination parameters</param>
        /// <returns>Paginated list of staff members</returns>
        [SwaggerOperation("Get Staff", Description = "Retrieve paginated list of staff members with filtering options")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Staff retrieved successfully", typeof(Result<IPaginate<GetStaffViewModel>>), MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid request parameters", typeof(Result<string>), MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Server error", typeof(Result<string>), MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpGet]
        public async Task<IActionResult> GetStaffAsync([FromQuery] GetStaffQuery query)
        {

            var result = await staffService.GetStaffAsync(query);
            return Ok(result);

        }

        /// <summary>
        /// Get staff member by ID
        /// </summary>
        /// <param name="id">Staff ID</param>
        /// <returns>Staff member details</returns>
        [SwaggerOperation("Get Staff by ID", Description = "Retrieve a specific staff member by their ID")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Staff retrieved successfully", typeof(Result<GetStaffViewModel>), MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Staff not found", typeof(Result<string>), MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID", typeof(Result<string>), MediaTypeNames.Application.Json)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStaffByIdAsync(int id)
        {

            var result = await staffService.GetStaffByIdAsync(id);

            return Ok(result);

        }

        /// <summary>
        /// Create a new staff member
        /// </summary>
        /// <param name="command">Staff creation data</param>
        /// <returns>Created staff member details</returns>
        [SwaggerOperation("Create Staff", Description = "Create a new staff member")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Staff created successfully", typeof(Result<GetStaffViewModel>), MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid input data", typeof(Result<string>), MediaTypeNames.Application.Json)]
        [HttpPost]
        public async Task<IActionResult> CreateStaffAsync([FromBody] CreateStaffCommand command)
        {
            var result = await staffService.CreateStaffAsync(command);
            return Ok(result);

        }

        /// <summary>
        /// Update an existing staff member
        /// </summary>
        /// <param name="id">Staff ID to update</param>
        /// <param name="command">Staff update data</param>
        /// <returns>Updated staff member details</returns>
        [SwaggerOperation("Update Staff", Description = "Update an existing staff member")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Staff updated successfully", typeof(Result<GetStaffViewModel>), MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Staff not found", typeof(Result<string>), MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid input data", typeof(Result<string>), MediaTypeNames.Application.Json)]
        [HttpPut("{id}")] // ID in path
        public async Task<IActionResult> UpdateStaffAsync(int id, [FromBody] UpdateStaffCommand command)
        {

                var result = await staffService.UpdateStaffAsync(id, command);

                return Ok(result);
        }
        /// <summary>
        /// Delete a staff member
        /// </summary>
        /// <param name="id">Staff ID to delete</param>
        /// <returns>Success status</returns>
        [SwaggerOperation("Delete Staff", Description = "Delete a staff member")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Staff deleted successfully", typeof(Result<bool>), MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Staff not found", typeof(Result<string>), MediaTypeNames.Application.Json)]
        [SwaggerResponse((int)HttpStatusCode.BadRequest, "Invalid ID", typeof(Result<string>), MediaTypeNames.Application.Json)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStaffAsync(int id)
        {

            var result = await staffService.DeleteStaffAsync(id);
            return Ok(result);

        }

    }
}
