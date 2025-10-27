using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bib_PonciGarciaGarrote
{
    internal class Magazine: ReadingRoomItem
    {
		private byte month;
		public byte Month
		{
			get { return month; }
			set 
			{
				if (value > 12)
				{
                    Console.WriteLine("De maand is maximaal 12");
				}
				else
				{
					month = value;
				}
			}
		}
		private uint year;
		public uint Year
		{
			get { return year; }
			set
            {
                if (value > 2500)
                {
                    Console.WriteLine("Het jaartal is maximaal 2500");
                }
                else
                {
                    year = value;
                }
            }
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
                id += month.ToString() + year.ToString();
                return id;
            }
        }
        public override string Category
        {
            get { return "Maandblad"; }
        }

        //CONSTRUCTOR

        public Magazine(string title, string publisher, byte month, uint year): base(title, publisher)
        {
            this.month = month;
            this.year = year;
        }

    }
}
