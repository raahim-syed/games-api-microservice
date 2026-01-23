using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repositories;
using static Play.Catalog.Service.Dtos.Dtos;

namespace Play.Catalog.Service.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        // private static readonly List<ItemDto> items = new()
        // {
        //     new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
        //     new ItemDto(Guid.NewGuid(), "Antidote", "Cures poison", 7, DateTimeOffset.UtcNow),
        //     new ItemDto(Guid.NewGuid(), "Sword", "Deals a moderate amount of damage", 20, DateTimeOffset.UtcNow)
        // };

        private readonly IItemsRepository repository;

        public ItemsController(IItemsRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<ItemDto>> GetItems()
        {
            var items = (await repository.GetAllAsync())
                            .Select(item => item.AsDto()).ToList();

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetById (Guid id)
        {
            var item = await repository.GetItemByIdAsync(id);

            if(item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<ItemDto>> Post(CreateItemDto createItemDto)
        {
            var item = new Item{
                Id= Guid.NewGuid(),
                Name= createItemDto.Name, 
                Description= createItemDto.Description, 
                Price= createItemDto.Price, 
                CreatedDate= DateTimeOffset.UtcNow
            };
            
            await repository.CreateItemAsync(item);

            return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public  async Task<ActionResult> Put(Guid id, UpdateItemDto updateItemDto)
        {
            var item = (await repository.GetItemByIdAsync(id));
            if (item == null)
            {
                return NotFound();
            }

            var updatedItem = new Item
            {
                Id = item.Id,
                Name = updateItemDto.Name,
                Description = updateItemDto.Description,
                Price = updateItemDto.Price,
                CreatedDate = item.CreatedDate
            };

            await repository.UpdateItemAsync(updatedItem);

            return Ok("Item updated successfully with id: " + id);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var item = (await repository.GetItemByIdAsync(id));
            if (item == null)
            {
                return NotFound();
            }

            await repository.DeleteItemAsync(id);

            return Ok("Item deleted successfully with id: " + id);
        }

    }
}
