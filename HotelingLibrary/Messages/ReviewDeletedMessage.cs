namespace HotelingLibrary.Messages
{
    public class ReviewDeletedMessage : MessageBase
    {
        //Represents users whose reviews were deleted
        public List<Guid> UsersDeletedReviews { get; set; }
    }
}
