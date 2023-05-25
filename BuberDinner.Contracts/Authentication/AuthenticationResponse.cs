namespace BuberDinner.Contracts.Authentication;

public record AuthenticationResponse(
    string FirstName,
    string LastName,
    string Email,
    string Token);