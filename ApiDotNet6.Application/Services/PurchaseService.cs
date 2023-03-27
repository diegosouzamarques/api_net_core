using ApiDotNet6.Application.DTOs;
using ApiDotNet6.Application.DTOs.Validations;
using ApiDotNet6.Application.Services.Interface;
using ApiDotNet6.Domain.Entities;
using ApiDotNet6.Domain.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiDotNet6.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        private readonly IProductRepository _productRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PurchaseService(IPurchaseRepository purchaseRepository, IProductRepository productRepository, IPersonRepository personRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _purchaseRepository = purchaseRepository;
            _productRepository = productRepository;
            _personRepository = personRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Objeto dever ser informado.");

            var validate = new PurchaseDTOValidator().Validate(purchaseDTO);

            if (!validate.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas de validação", validate);

            try {
                await _unitOfWork.BeginTransaction();

                var productId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
                if (productId == 0)
                {
                    var product = new Product(purchaseDTO.ProductName, purchaseDTO.CodErp, purchaseDTO.Price ?? 0);
                    await _productRepository.CreateAsync(product);
                    productId = product.Id;
                }
                var personId = await _personRepository.GetByDocumentAsync(purchaseDTO.Document);
                var purchase = new Purchase(productId, personId);

                var data = await _purchaseRepository.CreateAsync(purchase);
                purchaseDTO.Id = data.Id;
                await _unitOfWork.Commit();
                return ResultService.Ok<PurchaseDTO>(purchaseDTO);

            } catch(Exception ex) {
                await _unitOfWork.Rollback();
                return ResultService.Fail<PurchaseDTO>(ex.Message);
            }

        }

        public async Task<ResultService> DeleteAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
                return ResultService.Fail("Compra não encontrada");

            await _purchaseRepository.DeleteAsync(purchase);

            return ResultService.Ok("Compra removida");
        }

        public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAsync()
        {
            var purchases = await _purchaseRepository.GetPurchasesAsync();
            return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
        }

        public async Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id)
        {
            var purchase = await _purchaseRepository.GetByIdAsync(id);
            if (purchase == null)
                return ResultService.Fail<PurchaseDetailDTO>("Compra não encontrada!");

            return ResultService.Ok(_mapper.Map<PurchaseDetailDTO>(purchase));
        }

        public async Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO)
        {
            if (purchaseDTO == null)
                return ResultService.Fail<PurchaseDTO>("Objeto dever ser informado");

            var result = new PurchaseDTOValidator().Validate(purchaseDTO);
            if (!result.IsValid)
                return ResultService.RequestError<PurchaseDTO>("Problemas de validação", result);

            var purchase = await _purchaseRepository.GetByIdAsync(purchaseDTO.Id);
            if (purchase == null)
                return ResultService.Fail<PurchaseDTO>("Compra não encontrada");

            var produtId = await _productRepository.GetIdByCodErpAsync(purchaseDTO.CodErp);
            var personId = await _personRepository.GetByDocumentAsync(purchaseDTO.Document);

            purchase.Edit(purchase.Id, produtId, personId);
            await _purchaseRepository.EditAsync(purchase);

            return ResultService.Ok(purchaseDTO);


        }
    }
}
