using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_PonciGarciaGarrote
{
    internal class NewsPaper: ReadingRoomItem
    {
		private DateTime date;
		public DateTime Date
		{
			get { return date; }
			set { date = value; }
		}
        public override string Identification
		{
			get
			{
                string id = Title.Substring(0, 1).ToUpper();
                if (Title.IndexOf(" ") < 0)
                {
                    id += Title.Substring(1, 1).ToUpper();
                }
                else
                {
                    for (int i = 1; i < Title.Length; i++)
                    {
                        if (Title[i].ToString() == " ")
                        {
                            if (id.Length < 3)
                            {
                                id += Title[i + 1].ToString().ToUpper();
                            }
                        }
                    }
                }
                id += date.ToString("ddMMyyyy");
				return id;
			}
		}

        public override string Category
		{
			get { return "Krant"; }
		}

		//CONSTRUCTOR

		public NewsPaper(string title, string publisher, DateTime date): base(title, publisher)
		{
			this.date = date;
		}
    }
}
