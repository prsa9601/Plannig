namespace Domain.CategoryAgg.Service
{
    public interface ICategoryService
    {
        bool IsTitleExist(string title);
        bool IsSlugExist(string slug);
    }
}
