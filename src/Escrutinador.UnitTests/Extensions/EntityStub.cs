using System;
using System.ComponentModel.DataAnnotations;

namespace Escrutinador.UnitTests.Extensions
{
    public class EntityStub
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        [Display(Order = 1)]
        public string UserName { get; set; }

        [Display(Order = 0)]
        [StringLength(50, MinimumLength = 6)]
        public string Password { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }

        [Required]
        public long OtherId { get; set; }

        public DateTimeKind DateKind { get; set; }

        public int ThatOrderId { get; set; }
    }
}
