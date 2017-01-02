namespace WindowsFormsApplication1
{
    public class UserMaster
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int UserNameLength
        {
            get
            {
                return UserName.Length;
            }
        }
        public int PasswordLength
        {
            get
            {
                return Password.Length;
            }
        }
        public string Password { get; set; }
        public bool Active { get; set; }
        public bool Administrator { get; set; }
        public int UserRoleId { get; set; }
        public string userEmailId { get; set; }
    }
}
