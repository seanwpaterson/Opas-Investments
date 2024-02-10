namespace Opas.Core.Data.Models;

public class UploadedFile
{
    public string? FileName { get; set; }

    public string? FileNameWithoutExtension => FileName == null ? null : Path.GetFileNameWithoutExtension(FileName);

    public string? Extension => FileName == null ? null : Path.GetExtension(FileName)?.TrimStart('.');

    public string? FileId { get; set; }

    public string? Category { get; set; }

    public string? SubCategory { get; set; }

    public long SizeInBytes { get; set; }
}
