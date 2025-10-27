using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_PonciGarciaGarrote
{
    internal class InvalidPublicationDateException: ApplicationException
    {
        public InvalidPublicationDateException(): base("Ongeldige datum") { } //Standaard constructor aangemaakt just in case
        public InvalidPublicationDateException(string message): base(message) { }
    }
}
