namespace HotelingLibrary.Messages
{
    public class UserDataChangedMessage : MessageBase
    {
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
