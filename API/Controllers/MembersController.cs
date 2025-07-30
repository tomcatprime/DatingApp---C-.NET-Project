using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //[Route("api/[controller]")] // Route attribute to define the base URL for this controller - localhost:5001/api/members
    //[ApiController] // Indicates that this class is an API controller
    // This controller will handle requests related to members (users).
    //It is a base class from MVC controller without view rendering capabilities.
    // It provides features like model binding, validation, and automatic response formatting.

    //Changes done to the MembersController to use the new BaseApiController
    public class MembersController(AppDbContext context) : BaseApiController // ControllerBase is used for API controllers that do not require view rendering, derive from ControllerBase
    //we want to return data from out database so we have to inject the AppDbContext into the MembersController
    {
        // Define actions here to handle HTTP requests related to members.
        // For example, you can add methods to get, create, update, or delete members.


        //Creating engine for the MembersController
        // This controller will handle HTTP requests related to members (users).
        
        [Authorize]
        [HttpGet]
        // This action method will handle GET requests to retrieve all members.
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers() // localhost:5001/api/members, async Task is used for asynchronous programming, 
        {
            var members = await context.Users.ToListAsync(); // Retrieve all members from the database using the AppDbContext, asynchronously
            return Ok(members); // Return the list of members with a 200 OK status
        }

        //Endpoint to get a specific member by ID
        [HttpGet("{id}")] //localhost:5001/api/members/{id}
        // This action method will handle GET requests to retrieve a specific member by ID.
        // The {id} in the route indicates that this method expects an ID parameter in the URL.
        public async Task<ActionResult<AppUser>> GetMember(string id) // id is the unique identifier for the member, typically a string or GUID, asynchronous method that returns an ActionResult of type AppUser
        {
            var member = await context.Users.FindAsync(id); // Find a member by ID using the AppDbContext
            if (member == null) // Check if the member exists
            {
                return NotFound(); // Return a 404 Not Found status if the member does not exist
            }

            return Ok(member);
            // Return the member with a 200 OK status if found
        }

    }
    
}

