﻿using API;
using API.Controllers;
using AutoMapper;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace Skinet.Controllers;


public class ProductsController : BaseApiController
{
    private readonly IGenericRepo<Product> products;
    private readonly IGenericRepo<ProductBrand> brand;
    private readonly IGenericRepo<ProductType> type;
    private readonly IMapper mapper;

    public ProductsController(
        IGenericRepo<Product> products,
        IGenericRepo<ProductBrand> brand,
        IGenericRepo<ProductType> type,
        IMapper mapper
        )
    {
        this.products = products;
        this.brand = brand;
        this.type = type;
        this.mapper = mapper;
    }
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProduct()
    {
        var spec = new ProductsWithType_BrandSpecification();
        var prod = await products.ListAsync(spec);
        return  Ok(mapper.Map<IReadOnlyList<Product>, IReadOnlyList< ProductToReturnDto>>(prod));
    }
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
    {
        var spec = new ProductsWithType_BrandSpecification(id);
        var oneProduct = await products.GetEntityWithSpecification(spec);
        if (oneProduct == null)
            return NotFound(new ApiResponse(404));

        return mapper.Map<Product ,ProductToReturnDto>(oneProduct);

    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrand()
    {
        return Ok(await brand.GetAsync());
    }
    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductType()
    {
        return Ok(await type.GetAsync());
    }
}
