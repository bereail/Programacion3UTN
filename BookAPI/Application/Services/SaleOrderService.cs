using Application.Dtos.SaleOrderDTOs;
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
        public SaleOrderDTO? GetSaleOrder(int SaleOrderId)
        {
            var saleOrder = _saleOrderRepository.GetSaleOrder(SaleOrderId);
            return _mapper.Map<SaleOrderDTO>(saleOrder);
        }
        public ICollection<SaleOrderDTO> GetAllSaleOrders()
        {
            var saleOrders = _saleOrderRepository.GetAllSaleOrders();
            return _mapper.Map<ICollection<SaleOrderDTO>>(saleOrders);
        }

        public SaleOrderDTO? AddSaleOrder(SaleOrderToCreateDTO saleOrderToCreateDTO, int clientId)
        {
           
            Console.WriteLine("Creating SaleOrder...");
            Console.WriteLine($"BookId: {saleOrderToCreateDTO.BookId}, BookQuantity: {saleOrderToCreateDTO.BookQuantity}");

            var newSaleOrder = _mapper.Map<SaleOrder>(saleOrderToCreateDTO);
            newSaleOrder.ClientId = clientId;
            newSaleOrder.Status = SaleOrderStatus.Finalizado;

           
            Console.WriteLine($"Checking stock for BookId: {newSaleOrder.BookId}, Quantity: {newSaleOrder.BookQuantity}");

            var availableStock = _bookService.GetBookStock(newSaleOrder.BookId); 
            if (availableStock >= newSaleOrder.BookQuantity)
            {
              
                var book = _bookService.GetBookById(newSaleOrder.BookId);
                if (book != null)
                {
                    _bookService.UpdateBookStock(newSaleOrder.BookId, book.Stock - newSaleOrder.BookQuantity);
                }

                
                _saleOrderRepository.AddSaleOrder(newSaleOrder);
                _saleOrderRepository.SaveChanges();
                Console.WriteLine("SaleOrder created successfully!");
                return _mapper.Map<SaleOrderDTO>(newSaleOrder);
            }

            Console.WriteLine("Error: Book stock verification failed.");
            return null;
        }




        public SaleOrderDTO? DeleteSaleOrder(int saleOrderId)
        {
            var saleOrder = _saleOrderRepository.GetSaleOrder(saleOrderId);

            if (saleOrder != null)
            {
                _saleOrderRepository.DeleteSaleOrder(saleOrderId);
                _saleOrderRepository.SaveChanges();
            }

            return _mapper.Map<SaleOrderDTO>(saleOrder);
        }

        public SaleOrderStatusDTO? UpdateSaleOrderStatus(int saleOrderId)
        {
            var saleOrderToUpdate = _saleOrderRepository.GetSaleOrder(saleOrderId);
            if (saleOrderToUpdate != null)
            {
                if (saleOrderToUpdate.Status == SaleOrderStatus.Cancelado)
                    saleOrderToUpdate.Status = SaleOrderStatus.Finalizado;
                else
                    saleOrderToUpdate.Status = SaleOrderStatus.Cancelado;

                _saleOrderRepository.SaveChanges();

            }

            return _mapper.Map<SaleOrderStatusDTO>(saleOrderToUpdate);
        }


    }
}