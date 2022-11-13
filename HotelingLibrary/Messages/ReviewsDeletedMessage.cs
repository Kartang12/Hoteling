namespace HotelingLibrary.Messages
{
    public class ReviewsDeletedMessage : MessageBase
    {
        //Represents users whose reviews were deleted
        public List<Guid> UsersDeletedReviews { get; set; }
    }
}
