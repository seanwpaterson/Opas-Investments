using System.ComponentModel.DataAnnotations;

namespace Opas.Core.DataService.Models;

public class UploadedFile
{
    [Key]
    public int Id { get; set; }

    public string? FileName { get; set; }

    public string? FileNameWithoutExtension => FileName == null ? null : Path.GetFileNameWithoutExtension(FileName);

    public string? Extension => FileName == null ? null : Path.GetExtension(FileName)?.TrimStart('.');

    public string? FileId { get; set; }

    public string? Category { get; set; }

    public string? SubCategory { get; set; }

    public long SizeInBytes { get; set; }
}
