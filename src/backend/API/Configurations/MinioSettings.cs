using System.ComponentModel.DataAnnotations;

namespace API.Configurations;

public class MinioSettings
{
    [Required]
    public string Endpoint { get; set; } = default!;
    [Required]
    public string PublicEndpoint { get; set; } = default!;
    [Required]
    public string AccessKey { get; set; } = default!;
    [Required]
    public string SecretKey { get; set; } = default!;
    [Required]
    public string Bucket { get; set; } = default!;
    public bool UseSSL { get; set; }
}
