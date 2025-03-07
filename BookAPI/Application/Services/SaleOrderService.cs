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
        public SaleOrderDTO? GetSaleOrder(int saleOrderId)
        {
            try
            {
                if (saleOrderId <= 0)
                {
                    Console.WriteLine($"Error: Invalid saleOrderId {saleOrderId}");
                    throw new ArgumentException("Invalid sale order ID.");
                }

                var saleOrder = _saleOrderRepository.GetSaleOrder(saleOrderId);

                if (saleOrder == null)
                {
                    Console.WriteLine($"Error: Sale order with ID {saleOrderId} not found.");
                    return null;
                }

                return _mapper.Map<SaleOrderDTO>(saleOrder);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return null;
            }
        }


        public ICollection<SaleOrderDTO> GetAllSaleOrders()
        {
            var saleOrders = _saleOrderRepository.GetAllSaleOrders();
            return _mapper.Map<ICollection<SaleOrderDTO>>(saleOrders);
        }

        public SaleOrderDTO? AddSaleOrder(SaleOrderToCreateDTO saleOrderToCreateDTO, int clientId)
        {
            try
            {
                Console.WriteLine("Creating SaleOrder...");
                Console.WriteLine($"BookId: {saleOrderToCreateDTO.BookId}, BookQuantity: {saleOrderToCreateDTO.BookQuantity}");

                var newSaleOrder = _mapper.Map<SaleOrder>(saleOrderToCreateDTO);
                newSaleOrder.ClientId = clientId;
                newSaleOrder.Status = SaleOrderStatus.Finalizado;

                Console.WriteLine($"Checking stock for BookId: {newSaleOrder.BookId}, Quantity: {newSaleOrder.BookQuantity}");

                var availableStock = _bookService.GetBookStock(newSaleOrder.BookId);

                if (availableStock < newSaleOrder.BookQuantity)
                {
                    Console.WriteLine($"Error: Not enough stock for BookId {newSaleOrder.BookId}. Available: {availableStock}, Requested: {newSaleOrder.BookQuantity}");
                    throw new InvalidOperationException("Insufficient stock for the requested book.");
                }

                var book = _bookService.GetBookById(newSaleOrder.BookId);
                if (book == null)
                {
                    Console.WriteLine($"Error: Book with ID {newSaleOrder.BookId} not found.");
                    throw new ArgumentException("Invalid book ID.");
                }

                _bookService.UpdateBookStock(newSaleOrder.BookId, book.Stock - newSaleOrder.BookQuantity);

                _saleOrderRepository.AddSaleOrder(newSaleOrder);
                _saleOrderRepository.SaveChanges();
                Console.WriteLine("SaleOrder created successfully!");

                return _mapper.Map<SaleOrderDTO>(newSaleOrder);
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine($"Stock Error: {ex.Message}");
                return null;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Invalid Data Error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error: {ex.Message}");
                return null;
            }
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


        public IEnumerable<SaleOrderDTO> GetOrdersByUserId(int userId)
        {
            var saleOrders = _saleOrderRepository.GetOrdersByUserId(userId);
            return _mapper.Map<IEnumerable<SaleOrderDTO>>(saleOrders);
        }


    }
}