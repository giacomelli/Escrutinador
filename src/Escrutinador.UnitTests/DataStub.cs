using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Escrutinador.UnitTests
{
    public class DataStub
    {
        [StringLength(50, MinimumLength = 10)]
        [Required]
        [Display(Order = 2)]
        public string Name { get; set; }

        [Display(Order = 1)]
        public string Description { get; set; }

        [StringLength(2, MinimumLength = 1)]
        public string Text { get; set; }


        public int OtherId { get; set; }

        [Url]
        public string Url { get; set; }

        public DateTimeKind DateKind { get; set; }

        [MinLength(2)]
        public IList<string> Lines { get; set; }
    }
}
