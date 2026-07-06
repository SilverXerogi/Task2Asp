using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Services.Products.Repositories;
using Goods.Tools.Types.Results;
using Students.Domain.Enums;

namespace Goods.Services.Products;

public class ProductsService(IProductsRepository productsRepository) : IProductsService
{
    public async Task<Result> SaveProduct(StudentBlank productBlank)
    {
        DataResult<Students> validationResult = await ValidateProductBlank(productBlank);
        if (validationResult.IsFail(out Students product)) return validationResult.ToResult();

        await productsRepository.SaveProduct(product);

        return Result.Success();
    }

    #region Validation

    private async Task<DataResult<Students>> ValidateProductBlank(StudentBlank productBlank)
    {
        DataResult<Guid?> existProductValidationResult = await ValidateExistProduct(productBlank);
        if (existProductValidationResult.IsFail(out Guid? id)) return DataResult<Students>.Fail(existProductValidationResult);

        DataResult<ProductCategory> categoryValidationResult = ValidateProductCategory(productBlank);
        if (categoryValidationResult.IsFail(out ProductCategory category)) return DataResult<Students>.Fail(categoryValidationResult);

        DataResult<String> nameValidationResult = await ValidateProductName(productBlank);
        if (nameValidationResult.IsFail(out String name)) return DataResult<Students>.Fail(nameValidationResult);

        DataResult<Decimal> priceValidationResult = ValidateProductPrice(productBlank);
        if (priceValidationResult.IsFail(out Decimal price)) return DataResult<Students>.Fail(priceValidationResult);

        Students product = new(
            id ?? Guid.NewGuid(),
            category,
            name,
            productBlank.Description,
            price,
            isRemoved: false
        );

        return DataResult<Students>.Success(product);
    }

    private async Task<DataResult<Guid?>> ValidateExistProduct(StudentBlank productBlank)
    {
        if (productBlank.Id is not { } id)
            return DataResult<Guid?>.Success(null);

        Students existProduct = await GetProduct(id);
        if (existProduct.IsRemoved) return DataResult<Guid?>.Fail("Продукт удален");

        return DataResult<Guid?>.Success(id);
    }

    private DataResult<ProductCategory> ValidateProductCategory(StudentBlank productBlank)
    {
        if (productBlank.Category is not { } category)
            return DataResult<ProductCategory>.Fail("Выберите категорию продукта");

        if (!Enum.IsDefined(category))
            throw new Exception($"Категория {category} не существует");

        return DataResult<ProductCategory>.Success(category);
    }

    private async Task<DataResult<String>> ValidateProductName(StudentBlank productBlank)
    {
        if (String.IsNullOrWhiteSpace(productBlank.Name))
            return DataResult<String>.Fail("Не указано название продукта");

        const Int32 maxProductNameLength = 255;
        if (productBlank.Name.Length >= maxProductNameLength)
            return DataResult<String>.Fail($"Название продукта слишком длинное. Максимально допустимо {maxProductNameLength} символов");

        Students? productWithSameName = await GetProduct(productBlank.Name);
        if (productWithSameName is not null && productWithSameName.Id != productBlank.Id)
            return DataResult<String>.Fail("Продукт с таким названием уже существует");

        return DataResult<String>.Success(productBlank.Name);
    }

    private DataResult<Decimal> ValidateProductPrice(StudentBlank productBlank)
    {
        if (productBlank.Price is not { } price)
            return DataResult<Decimal>.Fail("Не указана стоимость продукта");

        if (price < 0)
            return DataResult<Decimal>.Fail("Указана некорректная стоимость продукта");

        return DataResult<Decimal>.Success(price);
    }
    
    #endregion Validation

    public async Task<Students> GetProduct(Guid productId)
    {
        Students? product = await productsRepository.GetProduct(productId);
        if (product is null) throw new Exception($"Продукт {productId} не найден");

        return product;
    }

    private Task<Students?> GetProduct(String name)
    {
        return productsRepository.GetProduct(name);
    }

    public Task<Page<Students>> GetProducts(Int32 page, Int32 countInPage)
    {
        return productsRepository.GetProducts(page, countInPage);
    }

    public async Task<Result> RemoveProduct(Guid id)
    {
        Students product = await GetProduct(id);
        if (product.IsRemoved) return Result.Fail("Продукт уже удален");

        await productsRepository.RemoveProduct(id);

        return Result.Success();
    }
}
