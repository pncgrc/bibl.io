using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Bib_PonciGarciaGarrote
{
    internal class Library
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
            }
        }
        public List<Book> ListOfBooks = new List<Book>();
        private Dictionary<DateTime, ReadingRoomItem> allReadingRoom = new Dictionary<DateTime, ReadingRoomItem>();

        public Dictionary<DateTime, ReadingRoomItem> AllReadingRoom
        {
            get { return allReadingRoom; }
        }


        // CONSTRUCTOR
        public Library(string libName)
        {
            this.name = libName;
        }

        //METHODS

        public void AcquisitionsReadingRoomToday()
        {
            DateTime today = DateTime.Now;
            Console.WriteLine($"Aanwinsten in de leeszaal van {today.ToString("d/MM/yyyy")}");
            foreach (var item in AllReadingRoom)
            {
                if (item.Key.Day == today.Day && item.Key.Month == today.Month && item.Key.Year == today.Year)
                {
                    Console.WriteLine($"{item.Value.Title} met id {item.Value.Identification}");
                }                
            }
        }

        public void ShowAllNewspapers()
        {
            Console.WriteLine("De kranten in de leeszaal:");
            foreach (var item in AllReadingRoom)
            {
                if (item.Value is NewsPaper np)
                {
                    Console.WriteLine($"- {np.Title} van {np.Date.ToString("dddd d MMMM yyyy")} van uitgeverij {np.Publisher}");
                }
            }
        }

        public void ShowAllMagazines()
        {
            Console.WriteLine("Alle maandbladen uit de leeszaal:");
            foreach (var item in AllReadingRoom)
            {
                if (item.Value is Magazine mag)
                {
                    Console.WriteLine($"- {mag.Title} van {mag.Month}/{mag.Year} van uitgeverij {mag.Publisher}");
                }
            }
        }

        public void AddMagazine()
        {
            Console.WriteLine("Wat is de naam van het maandblad?");
            string title = Console.ReadLine();
            Console.WriteLine("Wat is de maand van het maandblad?");
            string month = Console.ReadLine();
            Console.WriteLine("Wat is het jaar van het maandblad?");
            string year = Console.ReadLine();
            Console.WriteLine("Wat is de uitgeverij van het maandblad?");
            string publisher = Console.ReadLine();

            if (title != "" && month != "" && year != "" && publisher != "")
            {
                //MONTH
                byte monthConverted = Convert.ToByte(month);

                //YEAR
                uint yearConverted = Convert.ToUInt32(year);

                Magazine newMagazine = new Magazine(title, publisher, monthConverted, yearConverted);
                DateTime dateOfCreation = DateTime.Now;
                AllReadingRoom.Add(dateOfCreation, newMagazine);
            }
            else
            {
                Console.WriteLine("Alle vragen moeten een geldig antwoord hebben om een krant toe te voegen!");
            }
        }

        public void AddNewspaper()
        {
            Console.WriteLine("Wat is de naam van de krant?");
            string title = Console.ReadLine();
            Console.WriteLine("Wat is de datum van de krant?");
            string date = Console.ReadLine();
            Console.WriteLine("Wat is de uitgeverij van de krant?");
            string publisher = Console.ReadLine();

            if (title != "" && date != "" && publisher != "")
            {
                int index = date.IndexOf("/");
                int day = 1;
                int month = 1;
                int year = 1;

                //DAY
                if (index == 1)
                {
                    day = Convert.ToInt32(date.Substring(0, 1));
                }
                else if (index == 2)
                {
                    day = Convert.ToInt32(date.Substring(0, 2));
                }
                else
                {
                    Console.WriteLine("Verkeerde datumformaat. Moet \"dag/maand/jaar\" zijn.");
                }
                string date2 = date.Substring(index + 1);
                index = date2.IndexOf("/");
                //MONTH
                if (index == 1)
                {
                    month = Convert.ToInt32(date2.Substring(0, 1));
                }
                else if (index == 2)
                {
                    month = Convert.ToInt32(date2.Substring(0, 2));
                }
                else
                {
                    Console.WriteLine("Verkeerde datumformaat. Moet \"dag/maand/jaar\" zijn.");
                }
                //YEAR
                if (date2.Substring(index + 1).Length == 4)
                {
                    year = Convert.ToInt32(date2.Substring(index + 1));
                }
                else
                {
                    Console.WriteLine("Verkeerde datumformaat. Moet \"dag/maand/jaar\" zijn.");
                }

                DateTime npDate = new DateTime(year, month, day);
                NewsPaper newNewspaper = new NewsPaper(title, publisher, npDate);
                DateTime dateOfCreation = DateTime.Now;
                allReadingRoom.Add(dateOfCreation, newNewspaper);
            }
            else
            {
                Console.WriteLine("Alle velden moeten ingevuld zijn om een krant toe te voegen!");
            }
        }

        public void DeleteBook(string bName, string bAuthor)
        {
            for (int i = 0; i < ListOfBooks.Count; i++)
            {
                if (ListOfBooks[i].Name.ToLower() == bName.ToLower() && ListOfBooks[i].Author.ToLower() == bAuthor.ToLower())
                {
                    ListOfBooks.RemoveAt(i);
                }
            }
        }

        public Book LookUpBook_NameAuthor(string bName, string bAuthor)
        {
            if (string.IsNullOrEmpty(bName) || string.IsNullOrWhiteSpace(bName) || string.IsNullOrEmpty(bAuthor) || string.IsNullOrWhiteSpace(bAuthor))
            {
                throw new ArgumentException("Naam & autheur zijn verplicht in te geven");
            }
            else
            {
                foreach (Book b in ListOfBooks)
                {
                    if (b.Name.ToLower() == bName.ToLower() && b.Author.ToLower() == bAuthor.ToLower())
                    {
                        return b;
                    }
                }
            }
            return null;
        }

        public Book LookUpBook_ISBN(string isbn)
        {
            foreach (Book b in ListOfBooks)
            {
                if (b.IsbnNumber == isbn)
                {
                    return b;
                }
            }
            Console.WriteLine($"Geen boek gevonden met ISBN nummer \"{isbn.Substring(0, 3)}-{isbn.Substring(3, 2)}-{isbn.Substring(5, 4)}-{isbn.Substring(9, 3)}-{isbn.Substring(12, 1)}\".");
            return null;
        }

        public List<Book> LookUpListOfBooks_Author(string bAuthor)
        {
            List<Book> bookList = new List<Book>();
            foreach (Book b in ListOfBooks)
            {
                if (b.Author.ToLower() == bAuthor.ToLower())
                {
                    bookList.Add(b);
                }
            }

            if (bookList.Count == 0)
            {
                Console.WriteLine($"Geen boeken gevonden met autheur \"{bAuthor}\".");
            }            
            return bookList;
        }
        public List<Book> LookUpListOfBooks_Genre(BookGenres bGenre)
        {
            List<Book> bookList = new List<Book>();
            foreach (Book b in ListOfBooks)
            {
                if (b.Genre == bGenre)
                {
                    bookList.Add(b);
                }
            }

            if (bookList.Count == 0)
            {
                Console.WriteLine($"Geen boeken gevonden met genre \"{bGenre}\".");
                return null;
            }
            else
            {
               return bookList;
            }           
        }
    }
}
