using System.ComponentModel.DataAnnotations;

namespace WarehouseService.Domain.Entities.Base
{
    public class EntityBase
    {
        [MaxLength(4)]
        public int Id { get; set; }
    }
}
