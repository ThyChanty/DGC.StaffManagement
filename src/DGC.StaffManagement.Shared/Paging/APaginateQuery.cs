namespace DGC.StaffManagement.Shared.Paging;

public abstract class APaginateQuery
{
    public string? Term { get; set; }
    public int Index { get; set; }

    public int Size { get; set; }

    public string? OrderBy { get; set; }
    public string? OrderDir { get; set; }
}