using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Bib_PonciGarciaGarrote
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library grimoireLibrary = new Library("Grimoire Library of the Last City");
            Console.WriteLine($"Welkom bij {grimoireLibrary.Name}'s beheersysteem!");

            while (true)
            {
                Console.WriteLine("\nWat wil je doen? (geef \"exit\" in om de app af te sluiten)");
                Console.WriteLine("1. Boek toevoegen op basis van titel & auteur");
                Console.WriteLine("2. Voeg informatie toe aan een boek");
                Console.WriteLine("3. Toon alle informatie over een boek op basis van titel & auteur");
                Console.WriteLine("4. Boek(en) opzoeken");
                Console.WriteLine("5. Boek verwijderen");
                Console.WriteLine("6. Toon alle boeken in dit bibliotheek");
                Console.WriteLine("7. Lees boeken in van CSV-bestand");
                Console.WriteLine("8. Een krant of maandblad toe te voegen");
                Console.WriteLine("9. Alle kranten tonen");
                Console.WriteLine("10. Alle maandbladen tonen");
                Console.WriteLine("11. Aanwinsten van de leeszaal opvragen");
                Console.WriteLine("12. Een boek uitlenen");
                Console.WriteLine("13. Een boek terugbrengen");

                string userMenuChoice = Console.ReadLine();
                if (userMenuChoice == "exit") { break; }

                // user input var's
                string userInputTitle;
                string userInputAuthor;
                string userInputIsbn;
                string userInputPublisher;
                string userInputNumberOfPages;
                BookGenres userInputGenre;

                Console.WriteLine();
                if (userMenuChoice == "1")
                {
                    Console.Write("Geef de titel in: ");
                    userInputTitle = Console.ReadLine();
                    if (userInputTitle == "exit") { break; }
                    Console.Write("Geef de autheur in: ");
                    userInputAuthor = Console.ReadLine();
                    if (userInputAuthor == "exit") { break; }

                    Book newBook = new Book(userInputTitle, userInputAuthor, grimoireLibrary);

                    Console.WriteLine();
                }
                else if (userMenuChoice == "2")
                {
                    try
                    {
                        Console.Write("Geef de titel in: ");
                        userInputTitle = Console.ReadLine();
                        if (userInputTitle == "exit") { break; }
                        Console.Write("Geef de autheur in: ");
                        userInputAuthor = Console.ReadLine();
                        if (userInputAuthor == "exit") { break; }

                        Book bookToEdit = grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor);
                        if (grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor) is not null)
                        {
                            Book b = grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor);
                            Console.WriteLine($"Gekozen boek om aan te passen: \"{b.Name}\" van {b.Author}");
                        }
                        Console.WriteLine("\nWat mag er aangepast worden?");
                        Console.WriteLine("1. ISBN-nummer");
                        Console.WriteLine("2. Genre");
                        Console.WriteLine("3. Uitgeverij");
                        Console.WriteLine("4. Aantal bladzijden");
                        Console.WriteLine("5. Uitgiftedatum");

                        userMenuChoice = Console.ReadLine();
                        if (userMenuChoice == "exit") { break; }

                        if (userMenuChoice == "1")
                        {
                            Console.Write("Geef het ISBN-nummer in (13 cijfers): ");
                            userInputIsbn = Console.ReadLine();
                            try
                            {
                                bookToEdit.IsbnNumber = userInputIsbn;
                                Console.WriteLine($"Nieuw ISBN-nummer: {userInputIsbn.Substring(0, 3)}-{userInputIsbn.Substring(3, 2)}-{userInputIsbn.Substring(5, 4)}-{userInputIsbn.Substring(9, 3)}-{userInputIsbn.Substring(12, 1)}");
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
                                Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
                            }
                        }
                        else if (userMenuChoice == "2")
                        {
                            Console.WriteLine("\nWelke genre mag het boek krijgen?");
                            Console.WriteLine($"{(int)BookGenres.Drama + 1}. {BookGenres.Drama}");
                            Console.WriteLine($"{(int)BookGenres.Educatief + 1}. {BookGenres.Educatief}");
                            Console.WriteLine($"{(int)BookGenres.Fantasie + 1}. {BookGenres.Fantasie}");
                            Console.WriteLine($"{(int)BookGenres.Horror + 1}. {BookGenres.Horror}");
                            Console.WriteLine($"{(int)BookGenres.Mysterie + 1}. {BookGenres.Mysterie}");
                            Console.WriteLine($"{(int)BookGenres.Romantiek + 1}. {BookGenres.Romantiek}");
                            Console.WriteLine($"{(int)BookGenres.Sciencefiction + 1}. {BookGenres.Sciencefiction}");
                            Console.WriteLine($"{(int)BookGenres.Thriller + 1}. {BookGenres.Thriller}");
                            Console.WriteLine($"{(int)BookGenres.Schoolboek + 1}. {BookGenres.Schoolboek}");
                            Console.WriteLine($"{(int)BookGenres.Onbekend + 1}. {BookGenres.Onbekend}");

                            try
                            {
                                userInputGenre = (BookGenres)Convert.ToInt32(Console.ReadLine()) - 1;

                                try
                                {
                                    bookToEdit.Genre = userInputGenre;
                                    Console.WriteLine($"Nieuw genre: {userInputGenre}");
                                }
                                catch (InvalidEnumArgumentException)
                                {
                                    Console.WriteLine("Genre is niet bekend. Gelieve een nummer in te geven uit de lijst!");
                                }
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Keuze mag niet leeg zijn én moet een nummer zijn.");
                            }
                            catch (ArgumentException aex)
                            {
                                Console.WriteLine(aex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
                            }
                        }
                        else if (userMenuChoice == "3")
                        {
                            Console.Write("Geef de uitgeverij in: ");
                            try
                            {
                                userInputPublisher = Console.ReadLine();
                                bookToEdit.Publisher = userInputPublisher;
                                Console.WriteLine($"Nieuwe uitgeverij: {userInputPublisher}");
                            }
                            catch (ArgumentException aex)
                            {
                                Console.WriteLine(aex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
                            }
                        }
                        else if (userMenuChoice == "4")
                        {
                            Console.Write("Geef het aantal bladzijden in: ");
                            try
                            {
                                userInputNumberOfPages = Console.ReadLine();
                                bookToEdit.NumberOfPages = Convert.ToInt32(userInputNumberOfPages);
                                Console.WriteLine($"Nieuw aantal bladzijden: {userInputNumberOfPages}");
                            }
                            catch (InvalidPageCountException ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            catch (FormatException)
                            {
                                Console.WriteLine("Ongeldige invoer. Voer een geldig getal in!");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
                            }


                        }
                        else if (userMenuChoice == "5")
                        {
                            Console.Write("Geef de uitgifte datum in (formaat JJJJ-MM-DD): ");
                            userMenuChoice = Console.ReadLine();

                            try
                            {
                                if (string.IsNullOrWhiteSpace(userMenuChoice))
                                {
                                    throw new InvalidPublicationDateException("Datum mag niet leeg zijn!");
                                }
                                else if (userMenuChoice.Length != 10)
                                {
                                    throw new InvalidPublicationDateException("Datum moet in het formaat JJJJ-MM-DD zijn.");
                                }
                                else if (userMenuChoice.Substring(4, 1) != "-" || userMenuChoice.Substring(7, 1) != "-")
                                {
                                    throw new FormatException("Datum is in een verkeerd formaat ingegeven (\"-\" niet gevonden op de verwachte plaats).");
                                }
                                else
                                {
                                    int year = Convert.ToInt32(userMenuChoice.Substring(0, 4));
                                    int month = Convert.ToInt32(userMenuChoice.Substring(5, 2));
                                    int day = Convert.ToInt32(userMenuChoice.Substring(8, 2));
                                    DateTime date = new DateTime(year, month, day);

                                    bookToEdit.PublicationDate = date;
                                    Console.WriteLine($"Nieuw uitgiftedatum: {day}/{month}/{year}");
                                }
                            }
                            catch (InvalidPublicationDateException ipdex)
                            {
                                Console.WriteLine(ipdex.Message);
                            }
                            catch (FormatException fex)
                            {
                                Console.WriteLine(fex.Message);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Er is een onverwachte fout opgetreden: {ex.Message}");
                            }
                        }
                    }
                    catch (ArgumentException aex)
                    {
                        Console.WriteLine(aex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    

                    Console.WriteLine();
                }
                else if (userMenuChoice == "3")
                {
                    try
                    {
                        Console.Write("Geef de titel in: ");
                        userInputTitle = Console.ReadLine();
                        if (userInputTitle == "exit") { break; }
                        Console.Write("Geef de autheur in: ");
                        userInputAuthor = Console.ReadLine();
                        if (userInputAuthor == "exit") { break; }

                        if (grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor) is null)
                        {
                            Console.WriteLine($"Geen boek gevonden met naam \"{userInputTitle}\" & autheur \"{userInputAuthor}\".");
                        }
                        else
                        {
                            grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor).DisplayBook();
                        }
                    }                    
                    catch (ArgumentException aex)
                    {
                        Console.WriteLine(aex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }

                    Console.WriteLine();
                }
                else if (userMenuChoice == "4")
                {
                    Console.WriteLine("Op welke manier wil je opzoeken?");
                    Console.WriteLine("1. Boek opzoeken op basis van titel & auteur");
                    Console.WriteLine("2. Boek opzoeken op basis van ISBN-nummer");
                    Console.WriteLine("3. Boeken opzoeken van een bepaald autheur");
                    Console.WriteLine("4. Boeken opzoeken van een bepaald genre");
                    userMenuChoice = Console.ReadLine();
                    if (userMenuChoice == "exit") { break; }
                    Console.WriteLine();

                    if (userMenuChoice == "1")
                    {
                        try
                        {
                            Console.Write("Geef de titel in: ");
                            userInputTitle = Console.ReadLine();
                            if (userInputTitle == "exit") { break; }
                            Console.Write("Geef de autheur in: ");
                            userInputAuthor = Console.ReadLine();
                            if (userInputAuthor == "exit") { break; }

                            if (grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor) is not null)
                            {
                                Book b = grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor);
                                Console.WriteLine($"\"{b.Name}\" van {b.Author}");
                            }
                            else if (grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor) is null)
                            {
                                Console.WriteLine($"Geen boek gevonden met naam \"{userInputTitle}\" & autheur \"{userInputAuthor}\".");
                            }
                        }
                        catch (ArgumentException aex)
                        {
                            Console.WriteLine(aex.Message);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        Console.WriteLine();
                    }
                    else if (userMenuChoice == "2")
                    {
                        try
                        {
                            Console.Write("Geef het ISBN-nummer in (enkel getallen): ");
                            userInputIsbn = Console.ReadLine();
                            if (userInputIsbn == "exit") { break; }

                            if (grimoireLibrary.LookUpBook_ISBN(userInputIsbn) is not null)
                            {
                                Book b = grimoireLibrary.LookUpBook_ISBN(userInputIsbn);
                                Console.WriteLine($"\"{b.Name}\" van {b.Author}");
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Fout opgetreden: {ex.Message}");
                        }
                        

                        Console.WriteLine();
                    }
                    else if (userMenuChoice == "3")
                    {
                        Console.Write("Geef de autheur in: ");
                        userInputAuthor = Console.ReadLine();
                        if (userInputAuthor == "exit") { break; }

                        if (grimoireLibrary.LookUpListOfBooks_Author(userInputAuthor) is null)
                        {
                            Console.WriteLine($"Geen boek(en) teruggevonden met autheur \"{userInputAuthor}\"");
                        }
                        else
                        {
                            Console.WriteLine($"\nBoek(en) teruggevonden met autheur \"{userInputAuthor}\":");
                            int counter = 1;
                            foreach (Book b in grimoireLibrary.LookUpListOfBooks_Author(userInputAuthor))
                            {
                                Console.WriteLine($"{counter}. \"{b.Name}\" van {b.Author}");
                                counter++;
                            }
                        }

                        Console.WriteLine();
                    }
                    else if (userMenuChoice == "4")
                    {
                        Console.WriteLine("Welke genre?");
                        Console.WriteLine($"{(int)BookGenres.Drama + 1}. {BookGenres.Drama}");
                        Console.WriteLine($"{(int)BookGenres.Educatief + 1}. {BookGenres.Educatief}");
                        Console.WriteLine($"{(int)BookGenres.Fantasie + 1}. {BookGenres.Fantasie}");
                        Console.WriteLine($"{(int)BookGenres.Horror + 1}. {BookGenres.Horror}");
                        Console.WriteLine($"{(int)BookGenres.Mysterie + 1}. {BookGenres.Mysterie}");
                        Console.WriteLine($"{(int)BookGenres.Romantiek + 1}. {BookGenres.Romantiek}");
                        Console.WriteLine($"{(int)BookGenres.Sciencefiction + 1}. {BookGenres.Sciencefiction}");
                        Console.WriteLine($"{(int)BookGenres.Thriller + 1}. {BookGenres.Thriller}");
                        Console.WriteLine($"{(int)BookGenres.Schoolboek + 1}. {BookGenres.Schoolboek}");
                        Console.WriteLine($"{(int)BookGenres.Onbekend + 1}. {BookGenres.Onbekend}");
                        userInputGenre = (BookGenres) Convert.ToInt32(Console.ReadLine()) - 1;

                        if (grimoireLibrary.LookUpListOfBooks_Genre(userInputGenre) is null)
                        {
                            //Afhandeling door Library.LookUpListOfBooks_Genre
                        }
                        else
                        {
                            Console.WriteLine($"\nBoek(en) teruggevonden met genre \"{userInputGenre}\":");
                            int counter = 1;
                            foreach (Book b in grimoireLibrary.LookUpListOfBooks_Genre(userInputGenre))
                            {
                                Console.WriteLine($"{counter}. \"{b.Name}\" van {b.Author}");
                                counter++;
                            }
                        }

                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige keuze!");
                    }

                    Console.WriteLine();
                }
                else if (userMenuChoice == "5")
                {
                    try
                    {
                        Console.Write("Geef de titel in: ");
                        userInputTitle = Console.ReadLine();
                        if (userInputTitle == "exit") { break; }
                        Console.Write("Geef de autheur in: ");
                        userInputAuthor = Console.ReadLine();
                        if (userInputAuthor == "exit") { break; }

                        if (grimoireLibrary.ListOfBooks.Contains(grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor)))
                        {
                            grimoireLibrary.DeleteBook(userInputTitle, userInputAuthor);
                            Console.WriteLine($"Boek \"{userInputTitle}\" is verwijderd.");
                        }
                        else
                        {
                            Console.WriteLine($"Boek \"{userInputTitle}\" bestaat niet in het systeem.");
                        }
                    }
                    catch (ArgumentException aex)
                    {
                        Console.WriteLine(aex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    

                    Console.WriteLine();
                }
                else if (userMenuChoice == "6")
                {
                    if (grimoireLibrary.ListOfBooks.Count == 0)
                    {
                        Console.WriteLine("Geen boeken in het systeem.");
                    }
                    else
                    {
                        foreach (Book b in grimoireLibrary.ListOfBooks)
                        {
                            b.DisplayBook();
                        }
                    }
                    
                    Console.WriteLine();
                }
                else if (userMenuChoice == "7")
                {
                    Book.ReadBooksFromCsv("C:\\books.csv", grimoireLibrary);
                    Console.WriteLine();
                }
                else if (userMenuChoice == "8")
                {
                    Console.Write("Wat wil je toevoegen: krant of maandblad? ");
                    userMenuChoice = Console.ReadLine();

                    if (userMenuChoice.ToLower() == "krant")
                    {
                        grimoireLibrary.AddNewspaper();
                    }
                    else if (userMenuChoice.ToLower() == "maandblad")
                    {
                        grimoireLibrary.AddMagazine();
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige keuze!");
                    }
                    Console.WriteLine();
                }
                else if (userMenuChoice == "9")
                {
                    grimoireLibrary.ShowAllNewspapers();
                    Console.WriteLine();
                }
                else if (userMenuChoice == "10")
                {
                    grimoireLibrary.ShowAllMagazines();
                    Console.WriteLine();
                }
                else if (userMenuChoice == "11")
                {
                    grimoireLibrary.AcquisitionsReadingRoomToday();
                    Console.WriteLine();
                }
                else if (userMenuChoice == "12")
                {
                    try
                    {
                        Console.Write("Geef de titel in: ");
                        userInputTitle = Console.ReadLine();
                        if (userInputTitle == "exit") { break; }
                        Console.Write("Geef de autheur in: ");
                        userInputAuthor = Console.ReadLine();
                        if (userInputAuthor == "exit") { break; }

                        if (grimoireLibrary.ListOfBooks.Contains(grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor)))
                        {
                            Book bookToBorrow = grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor);
                            bookToBorrow.Borrow();
                        }
                        else
                        {
                            Console.WriteLine($"Boek \"{userInputTitle}\" bestaat niet in het systeem.");
                        }
                    }
                    catch (ArgumentException aex)
                    {
                        Console.WriteLine(aex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                }
                else if (userMenuChoice == "13")
                {
                    try
                    {
                        Console.Write("Geef de titel in: ");
                        userInputTitle = Console.ReadLine();
                        if (userInputTitle == "exit") { break; }
                        Console.Write("Geef de autheur in: ");
                        userInputAuthor = Console.ReadLine();
                        if (userInputAuthor == "exit") { break; }

                        if (grimoireLibrary.ListOfBooks.Contains(grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor)))
                        {
                            Book bookToBorrow = grimoireLibrary.LookUpBook_NameAuthor(userInputTitle, userInputAuthor);
                            bookToBorrow.Return();
                        }
                        else
                        {
                            Console.WriteLine($"Boek \"{userInputTitle}\" bestaat niet in het systeem.");
                        }
                    }
                    catch (ArgumentException aex)
                    {
                        Console.WriteLine(aex.Message);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine();
                    
                }
                else
                {
                    Console.WriteLine("Ongeldige keuze!");
                }
            }
        }
    }
}