using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Escrutinador.UnitTests.Extensions
{
    public class OtherEntityStub
    {
        [Required]
        public EntityStub Parent { get; set; }

        [MinLength(2)]
        public IList<string> Lines { get; set; }

        [Url]
        public string Url { get; set; }
    }
}
