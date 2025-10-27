# 📚 bibl.io

C#-toepassing voor bibliotheekbeheer met boeken, kranten en maandbladen. Bevat uitleenfunctionaliteit, leeszaalbeheer, CSV-import, exception handling en toepassing van OOP-principes zoals inheritance en interfaces.

---

## 🧩 Functionaliteiten

- Controles op invoer (titel niet leeg, geldig ISBN, enz.)  
- Importeren van boeken uit een **CSV-bestand**  
- Interactief **menu** om boeken toe te voegen, te verwijderen, te zoeken en weer te geven  
- Mogelijkheid om:
  - Kranten en maandbladen toe te voegen  
  - Alle leeszaalitems te tonen  
  - Aanwinsten van de huidige dag op te vragen  
- Uitleenbeheer    
- **Exception handling** bij validatie en gebruikersinvoer  
- Twee **zelfgeschreven exceptions** voor foutdetectie

---

## 🖥️ Menu-opties
Bij het starten van de toepassing wordt een bibliotheek aangemaakt.  
Het menu biedt opties om:
1. Boeken toe te voegen of informatie aan te vullen  
2. Boeken te zoeken op titel, auteur of ISBN  
3. Leeszaalitems te beheren  
4. Boeken te ontlenen of terug te brengen  
5. Alle aanwezige items weer te geven  
6. Het programma te verlaten via `exit`

---

## 🧠 Toegepaste OOP-concepten
- **Encapsulation:** eigenschapsvalidatie via getters/setters  
- **Inheritance:** `ReadingRoomItem` → `NewsPaper` / `Magazine`  
- **Polymorphism:** abstracte en interface-implementaties  
- **Exception handling:** aangepaste fouten en veilige gebruikersinteractie  
- **Composition:** `Library` bevat collecties van `Book`- en `ReadingRoomItem`-objecten  

---

## ⚙️ Technologieën
- **C# (.NET Console Application)**  
- **CSV-bestanden** voor data-import  
- **Enumerations**, **interfaces**, **abstracte klassen**  
- **Exception handling**  
