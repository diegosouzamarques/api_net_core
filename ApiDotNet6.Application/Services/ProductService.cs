using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.DTOs.Validations;
using ApiDotNet6.Application.Services.Interface;
using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Repositories;
using AutoMapper;

namespace ApiDotNet6.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO)
        {
            if (productDTO == null)
                return ResultService.Fail<ProductDTO>("Objeto deve ser informado");

            var result = new ProductDTOValidator().Validate(productDTO);
            if (!result.IsValid)
                return ResultService.RequestError<ProductDTO>("Problemas na validação", result);

            var product = _mapper.Map<Product>(productDTO);
            var data = await _productRepository.CreateAsync(product);

            return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(data));
        }

        public async Task<ResultService<ICollection<ProductDTO>>> GetAsync()
        {
            var products = await _productRepository.GetProductsAsync();
            return ResultService.Ok<ICollection<ProductDTO>>(_mapper.Map<ICollection<ProductDTO>>(products));
        }

        public async Task<ResultService<ProductDTO>> GetByIdAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                return ResultService.Fail<ProductDTO>("Produto não encontrada");
            return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(product));
        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var person = await _productRepository.GetByIdAsync(id);
            if (person == null)
                return ResultService.Fail("Produto não encontrada");

            await _productRepository.DeleteAsync(person);

            return ResultService.Ok("Produto removido");

        }

        public async Task<ResultService<ProductDTO>> UpdateAsync(ProductDTO productDTO)
        {
            if (productDTO == null)
                return ResultService.Fail<ProductDTO>("Objeto deve ser informado");

            var validation = new ProductDTOValidator().Validate(productDTO);

            if (!validation.IsValid)
                return ResultService.RequestError<ProductDTO>("Problema com a validadção dos campos", validation);


            var producto = await _productRepository.GetByIdAsync(productDTO.Id);

            if (producto == null)
                return ResultService.Fail<ProductDTO>("Produto não encontrada");

            producto = _mapper.Map<ProductDTO, Product>(productDTO, producto);

            await _productRepository.EditAsync(producto);

            return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(producto));



        }
    }
}
