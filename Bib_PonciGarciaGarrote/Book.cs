using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bib_PonciGarciaGarrote
{
    internal interface ILendable
    {
        bool IsAvailable { get; set; }
        DateTime BorrowingDate { get; set; }
        int BorrowDays { get; set; }
        void Borrow();
        void Return();
    }

    internal class Book: ILendable
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Ongeldig invoer. Titel van het boek mag niet leeg zijn!");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ongeldig invoer. Titel van het boek mag niet leeg of een spatie zijn!");
                }
                else
                {
                    name = value;
                }
            }
        }
        private string author;
        public string Author
        {
            get { return author; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Ongeldig invoer. Autheur van het boek mag niet leeg zijn!");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ongeldig invoer. Autheur van het boek mag niet leeg of een spatie zijn!");
                }
                else
                {
                    author = value;
                }
            }
        }
        private string isbnNumber; //978-90-1234-567-8, 13 cijfers zonder "-"
        public string IsbnNumber
        {
            get { return isbnNumber; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Ongeldig invoer. ISBN-nummer van het boek mag niet leeg zijn!");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ongeldig invoer. ISBN-nummer van het boek mag niet leeg of een spatie zijn!");
                }
                else if (value.Length != 13 || !value.All(char.IsDigit))
                {
                    throw new InvalidIsbnException("ISBN-nummer moet uit cijfers bestaan EN EXACT 13 cijfers lang zijn.");
                }
                else
                {
                    isbnNumber = value;
                }
            }
        }

        private BookGenres genre;
        public BookGenres Genre
        {
            get { return genre; }
            set
            {
                if (!Enum.IsDefined(typeof(BookGenres), value))
                {
                    throw new InvalidEnumArgumentException();
                }
                else
                {
                    genre = value;
                }                
            }
        }
        private string publisher;
        public string Publisher
        {
            get { return publisher; }
            set
            {

                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Ongeldig invoer. Uitgeverij van het boek mag niet leeg zijn!");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ongeldig invoer. Uitgeverij van het boek mag niet leeg of een spatie zijn!");
                }
                else
                {
                    publisher = value;
                }                
            }
        }
        private int numberOfPages;
        public int NumberOfPages
        {
            get { return numberOfPages; }
            set
            {
                if (value < 1 || value > 8000) // Langste boek ooit geschreven is < 8000 blz
                {
                    throw new InvalidPageCountException("Het aantal bladzijden mag niet kleiner zijn dan 1 of groter zijn dan 8000!");
                }
                else
                {
                    numberOfPages = value;
                }
            }
        }
        private string language;
        public string Language
        {
            get { return language; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Ongeldig invoer. Taal van het boek mag niet leeg zijn!");
                }
                else if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Ongeldig invoer. Taal van het boek mag niet leeg of een spatie zijn!");
                }
                else
                {
                    language = value;
                }
            }
        }
        private DateTime publicationDate;
        public DateTime PublicationDate
        {
            get { return publicationDate; }
            set
            {
                if (value < new DateTime(1500, 1, 1)) // Example: Earliest valid date is 1500
                {
                    throw new InvalidPublicationDateException("De uitgiftedatum is te ver in het verleden!");
                }
                else
                {
                    publicationDate = value;
                }
                
            }
        }
        private bool isAvailable;
        public bool IsAvailable
        {
            get { return isAvailable; }
            set { isAvailable = value; } // Dit wordt niet door users rechtstreeks aangepast
        }
        private DateTime borrowingDate;
        public DateTime BorrowingDate
        {
            get { return borrowingDate; }
            set { borrowingDate = value; } // Dit wordt niet door users rechtstreeks aangepast
        }
        private int borrowDays;
        public int BorrowDays
        {
            get { return borrowDays; }
            set { borrowDays = value; } // Dit wordt niet door users rechtstreeks aangepast
        }



        /* CONSTRUCTORS */

        public Book(string bName, string bAuthor, Library lib)
        {
            try
            {
                this.Name = bName;
                this.Author = bAuthor;
                this.IsAvailable = true;
                lib.ListOfBooks.Add(this);
                Console.WriteLine("Boek succesvol toegevoegd.");
            }
            catch(ArgumentException aex)
            {
                Console.WriteLine(aex.Message);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }            
        }

        /* METHODS */
        public void DisplayBook()
        {
            Console.WriteLine(this.name.ToUpper());
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine($"* Auteur: {this.author}");
            Console.WriteLine($"* Genre: {this.genre}");
            Console.WriteLine($"* Aantal bladzijden: {this.numberOfPages}");
            Console.WriteLine($"* Uitgeverij: {this.publisher}");
            Console.WriteLine($"* Uitgebracht op: {this.publicationDate.Day}/{this.publicationDate.Month}/{this.publicationDate.Year}");
            Console.WriteLine($"* Taal: {this.language}");
            if (this.isbnNumber is null)
            {
                Console.WriteLine($"* ISBN: ");
            }
            else if (this.isbnNumber is not null)
            {
                Console.WriteLine($"* ISBN: {this.isbnNumber.Substring(0, 3)}-{this.isbnNumber.Substring(3, 2)}-{this.isbnNumber.Substring(5, 4)}-{this.isbnNumber.Substring(9, 3)}-{this.isbnNumber.Substring(12, 1)}");//978-90-1234-567-8
            }            
            Console.WriteLine("-------------------------------------------------\n");
        }

        public static void ReadBooksFromCsv(string filePath, Library lib)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                Dictionary<string, BookGenres> genreDictionary = new Dictionary<string, BookGenres>();
                genreDictionary.Add("Drama", BookGenres.Drama);
                genreDictionary.Add("Educatief", BookGenres.Educatief);
                genreDictionary.Add("Fantasie", BookGenres.Fantasie);
                genreDictionary.Add("Horror", BookGenres.Horror);
                genreDictionary.Add("Mysterie", BookGenres.Mysterie);
                genreDictionary.Add("Romantiek", BookGenres.Romantiek);
                genreDictionary.Add("Sciencefiction", BookGenres.Sciencefiction);
                genreDictionary.Add("Thriller", BookGenres.Thriller);

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] columns = lines[i].Split(",");
                    Book newBook = new Book(columns[0], columns[1], lib);
                    try
                    {
                        newBook.IsbnNumber = columns[2];
                    }
                    catch (ArgumentException aex)
                    {
                        Console.WriteLine(aex.Message);
                    }
                    catch (InvalidIsbnException isbnex)
                    {
                        Console.WriteLine(isbnex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }

                    newBook.Genre = BookGenres.Onbekend;
                    foreach (var item in genreDictionary)
                    {
                        if (item.Key == columns[3])
                        {
                            newBook.Genre = item.Value;
                        }
                    }
                    newBook.Publisher = columns[4];
                    newBook.NumberOfPages = Convert.ToInt32(columns[5]);
                    newBook.Language = columns[6];
                    int year = Convert.ToInt32(columns[7].Substring(0, 4));
                    int month = Convert.ToInt32(columns[7].Substring(5, 2));
                    int day = Convert.ToInt32(columns[7].Substring(8, 2));
                    DateTime date = new DateTime(year, month, day);
                    newBook.PublicationDate = date;
                }
                Console.WriteLine("Boeken uit CSV-bestand succesvol ingeschreven in het systeem.");
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"CSV-bestand niet gevonden in pad: \"{filePath}\"");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void Borrow()
        {
            if (IsAvailable == false)
            {
                Console.WriteLine("Boek is reeds uitgeleend.");
            }
            else
            {
                BorrowingDate = DateTime.Now;
                IsAvailable = false;
                if (Genre == BookGenres.Schoolboek)
                {
                    BorrowDays = 10;
                }
                else
                {
                    BorrowDays = 20;
                }
                Console.WriteLine($"Je hebt het boek {Name} voor {BorrowDays} dagen uitgeleend. Gelieve het boek ten laatste terug te brengen op {BorrowingDate.AddDays(BorrowDays).Day}/{BorrowingDate.AddDays(BorrowDays).Month}/{BorrowingDate.AddDays(BorrowDays).Year}");
            }
        }

        public void Return()
        {
            if (IsAvailable == true)
            {
                Console.WriteLine("Dit boek is niet uitgeleend.");
            }
            else
            {
                IsAvailable = true;

                DateTime returnDate = BorrowingDate.AddDays(BorrowDays);
                DateTime testDate = DateTime.Now;
                //DateTime testDate = new DateTime(2025, 12, 31);
                if (testDate > returnDate)
                {
                    Console.WriteLine($"Het boek is te laat ingeleverd! Het moest op {returnDate.ToShortDateString()} terug zijn en het is nu {testDate.ToShortDateString()}.");
                }
                else
                {
                    Console.WriteLine($"Het boek is op tijd ingeleverd! Het moest op {returnDate.ToShortDateString()} terug zijn.");
                }
            }            
        }
    }


}
