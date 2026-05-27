using System.ComponentModel.DataAnnotations;

namespace HomeGrown.Core.Application.DTOs.Farms;

public record FarmPracticeDto(Guid Id, string Name);

public record AddPracticeRequest([Required][MaxLength(100)] string Name);
