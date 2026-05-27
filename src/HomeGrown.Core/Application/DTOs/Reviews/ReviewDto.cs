using System.ComponentModel.DataAnnotations;

namespace HomeGrown.Core.Application.DTOs.Reviews;

public record ReviewDto(
    Guid Id,
    Guid FarmId,
    string ReviewerName,
    string? ReviewerImageUrl,
    int Rating,
    string Text,
    DateTime CreatedAt
);

public record CreateReviewRequest(
    [Range(1, 5)] int Rating,
    [Required, MaxLength(2000)] string Text
);
