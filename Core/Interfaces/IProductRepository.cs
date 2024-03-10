using Core.Entities;

namespace Core.Interfaces
{
    public interface IProductRepository
    {
        //return a task of type product
        Task<Product> GetProductByIdAsync(int id);

        //return a IReadOnlyList of type product
        Task<IReadOnlyList<Product>> GetProductsAsync();

        //return a IReadOnlyList of type product brand
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        //return a IReadOnlyList of type product type
        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();


    }
}