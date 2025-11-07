using System.ComponentModel.DataAnnotations;

namespace ThrottlePro.Shared.DTOs.Customer;

/// <summary>
/// Request DTO for creating a new Customer
/// </summary>
public class CreateCustomerRequest
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Phone]
    [MaxLength(50)]
    public string? Phone { get; set; }

    [MaxLength(200)]
    public string? Address { get; set; }

    [MaxLength(100)]
    public string? City { get; set; }

    [MaxLength(50)]
    public string? State { get; set; }

    [MaxLength(20)]
    public string? ZipCode { get; set; }

    public DateTime? DateOfBirth { get; set; }

    // Marketing preferences
    public bool EmailOptIn { get; set; } = true;
    public bool SmsOptIn { get; set; } = false;
    public bool DirectMailOptIn { get; set; } = true;
}
