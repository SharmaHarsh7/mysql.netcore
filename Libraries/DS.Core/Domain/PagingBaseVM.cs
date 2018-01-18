namespace DS.Core.Domain
{
    public abstract class PagingBaseVM
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 25;
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public bool IsFirstQuery { get; set; } = true;
    }
}
