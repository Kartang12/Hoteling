namespace HotelingLibrary.Messages
{
    public class UserDataCahngedEvent : EventBase
    {
        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
        public int ReviewsAmount { get; set; }
    }
}
