using System.ComponentModel.DataAnnotations;

namespace RecordManagement.Api.DTOs;

public class CreateRecordRequest
{
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public decimal Alpha { get; set; }
    
    [Required]
    public decimal Beta { get; set; }
    
    [Required]
    public decimal Gamma { get; set; }
    
    [Required]
    public decimal Delta { get; set; }
    
    public DateTime? Milestone1StartDate { get; set; }
    
    public DateTime? Milestone1CompletionDate { get; set; }
    
    public DateTime? Milestone2StartDate { get; set; }
    
    public DateTime? Milestone2CompletionDate { get; set; }
    
    [Required]
    [StringLength(200)]
    public string ClientName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200)]
    public string AgentName { get; set; } = string.Empty;
}

public class UpdateRecordRequest
{
    [Required]
    public int Id { get; set; }
    
    [Required]
    public DateTime Date { get; set; }
    
    [Required]
    [StringLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public decimal Alpha { get; set; }
    
    [Required]
    public decimal Beta { get; set; }
    
    [Required]
    public decimal Gamma { get; set; }
    
    [Required]
    public decimal Delta { get; set; }
    
    public DateTime? Milestone1StartDate { get; set; }
    
    public DateTime? Milestone1CompletionDate { get; set; }
    
    public DateTime? Milestone2StartDate { get; set; }
    
    public DateTime? Milestone2CompletionDate { get; set; }
    
    [Required]
    [StringLength(200)]
    public string ClientName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(200)]
    public string AgentName { get; set; } = string.Empty;
}
