namespace Limupa.Comment.Api.Entities
{
    public class UserComment
    {
        public int UserCommentID { get; set; }
        public string UserCommentName { get; set; }
        public string UserCommentSurname { get; set; }
        public string? UserCommentImageUrl { get; set; }
        public string UserCommentEmail { get; set; }
        public string UserCommentDetail { get; set; }
        public int UserCommentRating { get; set; }
        public DateTime UserCommentCreatedDate { get; set; }
        public bool UserCommentStatus { get; set; }
        public string ProductID { get; set; }
    }
}
