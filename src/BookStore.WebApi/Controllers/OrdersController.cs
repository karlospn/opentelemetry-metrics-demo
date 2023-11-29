using AutoMapper;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.WebApi.Dtos.Order;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController(IMapper mapper,
        IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var orders = await orderService.GetAll();

            return Ok(mapper.Map<IEnumerable<OrderResultDto>>(orders));
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await orderService.GetById(id);

            if (order is null) 
                return NotFound();

            return Ok(mapper.Map<OrderResultDto>(order));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody]OrderAddDto orderDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var order = mapper.Map<Order>(orderDto);
            var result = await orderService.Add(order);

            if (result == null) return BadRequest();

            return Ok(mapper.Map<OrderResultDto>(result));
        }


        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int id)
        {
            var order = await orderService.GetById(id);
            if (order == null) return NotFound();

            var result = await orderService.Remove(order);
            if (result == null)  return BadRequest();

            return Ok(mapper.Map<OrderResultDto>(result));
        }
    }
}