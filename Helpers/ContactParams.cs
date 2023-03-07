namespace api.Helpers
{
    public class ContactParams
    {
        public int Page { get; set; } = 1;
        private int _maxItemsperPage = 50;
        private int itemsPerPage;
        public int ItemsPerPage
        {
            get => itemsPerPage;
            set => itemsPerPage = value > _maxItemsperPage ? _maxItemsperPage : value;
        }
        
    }
}