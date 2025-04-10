using Query.Comment.DTOs;

namespace Query.Comment
{
    public static class CommentMapper
    {
        public static CommentDto? Map(this Domain.CommentAgg.Comment? comment)
        {
            if (comment == null)
                return null;
            return new CommentDto()
            {
                Id = comment.Id,
                CreationDate = comment.CreationDate,
                Status = comment.Status,
                UserId = comment.UserId,
                PostId = comment.PostId,
                Text = comment.Text,

            };
        }
        public static CommentFilterData MapFilterComment(this Domain.CommentAgg.Comment comment)
        {
            if (comment == null)
                return null;
            return new CommentFilterData()
            {
                Id = comment.Id,
                CreationDate = comment.CreationDate,
                Status = comment.Status,
                UserId = comment.UserId,
                PostId = comment.PostId,
                Text = comment.Text,

            };
        }
        //public static CommentFilterDataProduct MapFilterCommentProduct(this Domain.CommentAgg.Comment comment, BlogContext context)
        //{
        //    var avatarName = context.Users.Where(i => i.Id == comment.UserId).Select(i => i.AvatarName).FirstOrDefault();
        //    var userName = context.Users.Where(i => i.Id == comment.UserId).Select(i => i.Name + " " + i.Family).FirstOrDefault();
        //    if (comment == null)
        //        return null;
        //    return new CommentFilterDataProduct()
        //    {
        //        Id = comment.Id,
        //        CreationDate = comment.CreationDate,
        //        Status = comment.Status,
        //        UserId = comment.UserId,
        //        PostId = comment.PostId,
        //        Text = comment.Text,
        //        userName = userName,
        //        userAvatar = avatarName,
        //        UpdateDate = comment.UpdateDate
        //    };
        //}
    }
}