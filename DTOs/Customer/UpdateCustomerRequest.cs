using System.ComponentModel.DataAnnotations;

namespace ThrottlePro.Shared.DTOs.Customer;

/// <summary>
/// Request DTO for updating an existing Customer (PATCH support - all nullable)
/// </summary>
public class UpdateCustomerRequest
{
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    [EmailAddress]
    [MaxLength(200)]
    public string? Email { get; set; }

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
    public bool? EmailOptIn { get; set; }
    public bool? SmsOptIn { get; set; }
    public bool? DirectMailOptIn { get; set; }

    public bool? IsActive { get; set; }
}
