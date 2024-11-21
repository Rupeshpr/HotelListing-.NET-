/*using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;
using HotelListing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAuthManager _authManager;

        public AccountController(
            UserManager<ApiUser> userManager,
            SignInManager<ApiUser> signInManager,
            ILogger<AccountController> logger,
            IMapper mapper,
            RoleManager<IdentityRole> roleManager, IAuthManager authManager) // Inject RoleManager
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _roleManager = roleManager; // Initialize RoleManager
            _authManager = authManager;
        }

        [HttpPost]
        [Route("register")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration Attempt for {userDTO.Email}");

            // Check if model state is valid and log errors if invalid
            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Model state is invalid: {Errors}", ModelState.Values.SelectMany(v => v.Errors));
                return BadRequest(ModelState);
            }

            try
            {
                // Map the UserDTO to ApiUser
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;  // Ensure the UserName is set to Email

                // Create the user
                var results = await _userManager.CreateAsync(user, userDTO.Password);

                if (!results.Succeeded)
                {
                    // Log each error and add to ModelState for better diagnostics
                    foreach (var error in results.Errors)
                    {
                        _logger.LogWarning($"Error during registration: {error.Code} - {error.Description}");
                        ModelState.AddModelError(error.Code, error.Description);
                    }

                    // Return detailed BadRequest with error messages
                    return BadRequest(new { message = "User Registration Attempt Failed", errors = results.Errors });
                }

                // Assign the roles to the user
                foreach (var role in userDTO.Roles)
                {
                    var roleExist = await _roleManager.RoleExistsAsync(role);
                    if (!roleExist)
                    {
                        _logger.LogWarning($"Role {role} does not exist.");
                        return BadRequest(new { message = $"Role {role} does not exist" });
                    }

                    var addRoleResult = await _userManager.AddToRoleAsync(user, role);
                    if (!addRoleResult.Succeeded)
                    {
                        // Log and return failure if role assignment fails
                        foreach (var error in addRoleResult.Errors)
                        {
                            _logger.LogWarning($"Error assigning role {role}: {error.Code} - {error.Description}");
                        }
                        return BadRequest(new { message = $"Failed to assign role {role}", errors = addRoleResult.Errors });
                    }
                }

                // Return success response with email confirmation
                return Accepted(new { message = "Registration successful", user = userDTO.Email });
            }
            catch (Exception ex)
            {
                // Log and return generic error with status code 500
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Register)}");
                return Problem(detail: ex.Message, title: $"Error in {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(statusCode: 200)]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 500)]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login Attempt for {userDTO.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 Bad Request if ModelState is invalid
            }

            try
            {
                // Attempt to sign in the user with the provided email and password
               *//* var result = await _signInManager.PasswordSignInAsync(userDTO.Email, userDTO.Password, false, false);

                if (!result.Succeeded)
                {
                    // If login fails, return 401 Unauthorized
                    return Unauthorized(new { message = "Invalid login attempt", user = userDTO.Email });
                }

                // If login is successful, return 200 OK
                return Ok(new { message = "Login successful", user = userDTO.Email });*//*


                if(!await _authManager.ValidateUser(userDTO))
                {
                    return Unauthorized();
                }
                return Accepted(new {Token = await _authManager.CreateToken()});
            }
            catch (Exception ex)
            {
                // Log the error and return 500 Internal Server Error if an exception occurs
                _logger.LogError(ex, $"Something Went Wrong in the {nameof(Login)}");
                return Problem(detail: ex.Message, title: $"Error in {nameof(Login)}", statusCode: 500);
            }
        }
    }
}*/








using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;
using HotelListing.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace HotelListing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly SignInManager<ApiUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IMapper _mapper;
        private readonly IAuthManager _authManager;

        public AccountController(
            UserManager<ApiUser> userManager,
            SignInManager<ApiUser> signInManager,
            ILogger<AccountController> logger,
            IMapper mapper,
            IAuthManager authManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _mapper = mapper;
            _authManager = authManager;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDTO)
        {
            _logger.LogInformation($"Registration attempt for {userDTO.Email}");

            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Role validation: Check if the role is valid (either "User" or "Administrator")
            var validRoles = new List<string> { "User", "Administrator" };
            if (string.IsNullOrEmpty(userDTO.Role) || !validRoles.Contains(userDTO.Role))
            {
                return BadRequest(new { message = "Invalid role. Please specify a valid role (User or Administrator)." });
            }

            try
            {
                // Map the DTO to the user model
                var user = _mapper.Map<ApiUser>(userDTO);
                user.UserName = userDTO.Email;  // Set UserName to Email

                // Create the user
                var result = await _userManager.CreateAsync(user, userDTO.Password);
                if (!result.Succeeded)
                {
                    // If registration fails, log the errors and return them
                    foreach (var error in result.Errors)
                    {
                        _logger.LogWarning($"Error during registration: {error.Code} - {error.Description}");
                    }
                    return BadRequest(new { message = "User registration failed", errors = result.Errors });
                }

                // Add the specified role to the user (it will only be added once)
                var roles = new List<string> { userDTO.Role }; // Add the role from DTO
                var roleResult = await _userManager.AddToRolesAsync(user, roles); // Add the role to user
                if (!roleResult.Succeeded)
                {
                    // If role assignment fails, return an error
                    _logger.LogWarning($"Error assigning role to {user.UserName}");
                    return BadRequest(new { message = "User registration successful, but role assignment failed." });
                }

                // Return success response
                return Accepted(new { message = "Registration successful", user = userDTO.Email });
            }
            catch (Exception ex)
            {
                // Log any exception and return a server error response
                _logger.LogError(ex, "An error occurred during registration");
                return Problem(detail: ex.Message, title: "Registration Error", statusCode: 500);
            }
        }





        // Login user
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDTO userDTO)
        {
            _logger.LogInformation($"Login attempt for {userDTO.Email}");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return 400 if ModelState is invalid
            }

            try
            {
                if (!await _authManager.ValidateUser(userDTO))
                {
                    _logger.LogWarning($"Invalid login attempt for {userDTO.Email}");
                    return Unauthorized(new { message = "Invalid credentials" });
                }

                var token = await _authManager.CreateToken();
                return Ok(new { message = "Login successful", token });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login");
                return Problem(detail: ex.Message, title: "Login Error", statusCode: 500);
            }
        }
    }
}
