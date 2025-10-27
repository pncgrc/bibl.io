using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_PonciGarciaGarrote
{
    internal class InvalidIsbnException: ApplicationException
    {

        public InvalidIsbnException(): base("Ongeldig ISBN-nummer") { } //Standaard constructor aangemaakt just in case

        public InvalidIsbnException(string message): base(message) { }
    }
}
