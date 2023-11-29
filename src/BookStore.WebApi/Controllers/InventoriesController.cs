using AutoMapper;
using BookStore.Domain.Interfaces;
using BookStore.Domain.Models;
using BookStore.WebApi.Dtos.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class InventoriesController(IMapper mapper,
            IInventoryService inventoryService)
        : ControllerBase
    {
        [HttpGet("{bookId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int bookId)
        {
            var inventory = await inventoryService.GetById(bookId);

            if (inventory == null) return NotFound();

            return Ok(mapper.Map<InventoryResultDto>(inventory));
        }


        [HttpGet]
        [Route("get-inventory-by-book-name/{bookName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<Inventory>>> SearchInventoryForBook(string bookName)
        {
            var inventory = mapper.Map<List<Inventory>>(await inventoryService.SearchInventoryForBook(bookName));

            if (inventory.Count == 0) return NotFound("None inventory was founded");

            return Ok(mapper.Map<IEnumerable<InventoryResultDto>>(inventory));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Add([FromBody]InventoryAddDto inventoryDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var inventory = mapper.Map<Inventory>(inventoryDto);
            var inventoryResult = await inventoryService.Add(inventory);

            if (inventoryResult == null) return BadRequest();

            return Ok(mapper.Map<InventoryResultDto>(inventoryResult));
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody]InventoryEditDto inventoryDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            await inventoryService.Update(mapper.Map<Inventory>(inventoryDto));

            return Ok(inventoryDto);
        }

        [HttpDelete("{bookId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Remove(int bookId)
        {
            var inventory = await inventoryService.GetById(bookId);
            if (inventory == null) return NotFound();

            var result = await inventoryService.Remove(inventory);
            if (!result) return BadRequest();

            return Ok();
        }
    }
}