using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_PonciGarciaGarrote
{
    internal abstract class ReadingRoomItem
    {
		private string title;
		public string Title
		{
			get { return title; }
		}
		private string publisher;
		public string Publisher
        {
			get { return publisher; }
			set { publisher = value; }
		}
		private string identification;
        abstract public string Identification { get; }
        private string category;
        abstract public string Category { get; }

		// CONSTRUCTOR
		public ReadingRoomItem(string title, string publisher)
		{
			this.title = title;
			this.publisher = publisher;
		}
    }
}
