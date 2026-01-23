using System;
using Play.Catalog.Service.Entities;
using static Play.Catalog.Service.Dtos.Dtos;

namespace Play.Catalog.Service.Dtos
{
    public static class Extensions
    {
        // Turn Item Entity into ItemDto
        public static ItemDto AsDto(this Item item)
        {
            return new ItemDto(item.Id, item.Name, item.Description, item.Price, item.CreatedDate);
        }
    }
}
