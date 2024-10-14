using System.ComponentModel.DataAnnotations;

public class UpdateTodoRequest
{
    public Guid? Id { get; set; }
    [StringLength(100)]
    public string? Title { get; set; }
    [StringLength(500)]
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    [Range(1, 5)]
    public int? Priority { get; set; }
    public bool? IsComplete { get; set; }

    public UpdateTodoRequest()
    {
        IsComplete = false;
    }
}