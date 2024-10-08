
using Domain.UserAgg;
using Query.User._RequestBox.DTOs;
namespace Query.User._RequestBox
{
    public static class RequestBoxMapper 
    {
        public static RequestDto MapListRequestBox (this Request requestBox)
        {
            var model = new RequestDto()
            {
                SenderId = requestBox.SenderId,
                RecipientId = requestBox.RecipientId,
                CreationDate = requestBox.CreationDate,
                Description = requestBox.Description,
                Id = requestBox.Id,
                Title = requestBox.Title,
            };
            
            return model;
        }
        public static RequestBoxFilterData MapFilterRequestBox(this RequestBoxFilterData requestBox)
        {
        //    var model = new RequestDto()
        //    {
        //        SenderId = requestBox.SenderId,
        //        RecipientId = requestBox.RecipientId,
        //        CreationDate = requestBox.CreationDate,
        //        Description = requestBox.Description,
        //        Id = requestBox.Id,
        //        Title = requestBox.Title,
        //    };

            return requestBox;
        }
    }
}
