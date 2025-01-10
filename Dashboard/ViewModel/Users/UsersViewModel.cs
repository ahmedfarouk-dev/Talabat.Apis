namespace Dashboard.ViewModel.Users
{
    public class UsersViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}
