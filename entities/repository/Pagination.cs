namespace entities.repository
{
    public class Pagination
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public string Order { get; set; }

        public bool Desc { get; set; }
    }
}
