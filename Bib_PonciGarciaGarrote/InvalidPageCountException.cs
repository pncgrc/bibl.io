using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_PonciGarciaGarrote
{
    internal class InvalidPageCountException: ApplicationException
    {
        public InvalidPageCountException(): base("Ongeldig aantal bladzijden") { } //Standaard constructor aangemaakt just in case

        public InvalidPageCountException(string message): base(message) { }
    }
}
