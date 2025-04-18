﻿using Application.Dtos.SaleOrderDTOs;
using Application.Interfaces.Repository;
using Application.Interfaces.Services;
using AutoMapper;
using Domain.Entities.Entities;
using Domain.Enums;


namespace Shop.API.Services.Implementations
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly ISaleOrderRepository _saleOrderRepository;
        private readonly IMapper _mapper;
        private readonly IBookService _bookService;

        public SaleOrderService(ISaleOrderRepository saleOrderRepository, IMapper mapper, IBookService bookService)
        {
            _saleOrderRepository = saleOrderRepository;
            _mapper = mapper;
            _bookService = bookService;
        }

        public IEnumerable<SaleOrderDTO> GetOrdersByUserId(int userId)
        {
            var saleOrders = _saleOrderRepository.GetOrdersByUserId(userId);
            return _mapper.Map<IEnumerable<SaleOrderDTO>>(saleOrders);
        }

        public SaleOrderDTO? GetSaleOrder(int saleOrderId)
        {
            try
            {
                if (saleOrderId <= 0)
                {                   
                    throw new ArgumentException("Invalid sale order ID.");
                }

                var saleOrder = _saleOrderRepository.GetSaleOrder(saleOrderId);

                if (saleOrder == null)
                {                   
                    return null;
                }

                return _mapper.Map<SaleOrderDTO>(saleOrder);
            }
            catch (ArgumentException ex)
            {                
                return null;
            }
            catch (Exception ex)
            {              
                return null;
            }
        }


        public ICollection<SaleOrderDTO> GetAllSaleOrders()
        {
            var saleOrders = _saleOrderRepository.GetAllSaleOrders();
            return _mapper.Map<ICollection<SaleOrderDTO>>(saleOrders);
        }

        public SaleOrderDTO CreateSaleOrder(SaleOrderToCreateDTO saleOrderToCreateDTO, int clientId)
        {
            var newSaleOrder = _mapper.Map<SaleOrder>(saleOrderToCreateDTO);
            newSaleOrder.ClientId = clientId;
            newSaleOrder.Status = SaleOrderStatus.Finalizado;

            if (newSaleOrder.BookQuantity <= 0)
            {
                throw new ArgumentException("La cantidad de libros a comprar debe ser mayor a 0.");
            }

            var availableStock = _bookService.GetBookStock(newSaleOrder.BookId);

         
            if (availableStock < newSaleOrder.BookQuantity)
            {
                return null;
            }

            var book = _bookService.GetBookById(newSaleOrder.BookId);
            if (book == null)
            {
                return null;
            }

            int newStock = book.Stock - newSaleOrder.BookQuantity;
            if (newStock < 0)
            {
                throw new InvalidOperationException("No se puede reducir el stock a un valor negativo.");
            }

        
            _bookService.UpdateBookStock(newSaleOrder.BookId, newStock);
            _saleOrderRepository.AddSaleOrder(newSaleOrder);

            return _mapper.Map<SaleOrderDTO>(newSaleOrder);
        }



        public SaleOrderDTO? CancelSaleOrder(int saleOrderId)
        {
            var saleOrder = _saleOrderRepository.GetSaleOrder(saleOrderId);

            if (saleOrder != null)
            {
                _saleOrderRepository.CancelSaleOrder(saleOrderId);
            }

            return _mapper.Map<SaleOrderDTO>(saleOrder);
        }


    }
}