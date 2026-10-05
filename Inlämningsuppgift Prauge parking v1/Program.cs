const int AntalPlatser = 100;
const int MaxLängdRegistreringsnummer = 10;

const string Car = "CAR";
const string MC = "MC";

const char TypSkiljare = '#';
const char FordonSkiljare = '|';

// Data

string[] parkingGarage = new string[AntalPlatser];

// tom plats ska vara "" och inte null, annars blir det problem med att skriva ut parkeringen
for (int i = 0; i < parkingGarage.Length; i++)
{
    parkingGarage[i] = "";
}

// huvudloop

var fortsätt = true;
while (fortsätt)
{
    VisaMeny();
    var val = LäsText("Välj ett alternativ: ");
    Console.WriteLine();

    switch (val)
    {
        case "1":
            ParkeraFordon(parkingGarage);
            break;
        case "2":
            HämtaUtFordon(parkingGarage);
            break;
        case "3":
            FlyttaFordon(parkingGarage);
            break;
        case "4":
            SökFordon(parkingGarage);
            break;
        case "5":
            VisaHelaParkeringen(parkingGarage);
            break;
        case "0":
            fortsätt = false;
            break;
        default:
            Console.WriteLine("Ogiltigt val. Försök igen.");
            break;
    }
}

Console.WriteLine("Programmet avslutat. Hejdå!");
Console.WriteLine("\n\nTryck på valfri tangent för att stänga");
Console.ReadKey();


// menyval
static void VisaMeny()
{
    Console.WriteLine();
    Console.WriteLine("Välkommen till parkeringsgaraget!");
    Console.WriteLine("1. Parkera fordon");
    Console.WriteLine("2. Hämta ut fordon");
    Console.WriteLine("3. Flytta fordon");
    Console.WriteLine("4. Sök fordon");
    Console.WriteLine("5. Visa hela parkeringen");
    Console.WriteLine("0. Avsluta programmet");
}


static void ParkeraFordon(string[] parkingGarage)
{
    var fordonstyp = LäsText("Ange fordonstyp (CAR/MC): ").ToUpper();
    if (fordonstyp != Car && fordonstyp != MC)
    {
        Console.WriteLine("Ogiltig fordonstyp. Försök igen.");
        return;
    }

    var registreringsnummer = LäsText("Ange registreringsnummer: ").ToUpper();
    if (registreringsnummer.Length == 0 || registreringsnummer.Length > MaxLängdRegistreringsnummer)
    {
        Console.WriteLine($"Registreringsnumret ska vara 1-{MaxLängdRegistreringsnummer} tecken.");
        return;
    }

    // Skiljetecknen skulle förstöra lagringsformatet
    if (registreringsnummer.Contains(TypSkiljare) || registreringsnummer.Contains(FordonSkiljare))
    {
        Console.WriteLine($"Registreringsnumret får inte innehålla {TypSkiljare} eller {FordonSkiljare}.");
        return;
    }

    // Kontrollera om fordonet redan är parkerat
    if (HittaFordon(parkingGarage, registreringsnummer) != -1)
    {
        Console.WriteLine("Fordonet är redan parkerat.");
        return;
    }

    // Hitta ledig plats (MC letar först efter en ensam MC)
    int ledigPlatsIndex = HittaLedigPlats(parkingGarage, fordonstyp);
    if (ledigPlatsIndex == -1)
    {
        Console.WriteLine("Tyvärr, garaget är fullt.");
        return;
    }

    // Spara i formatet "CAR#ABC123"
    LäggTillPåPlats(parkingGarage, ledigPlatsIndex, SkapaFordon(fordonstyp, registreringsnummer));
    Console.WriteLine($"Fordonet med registreringsnummer {registreringsnummer} har parkerats på plats {ledigPlatsIndex + 1}.");
}


static void HämtaUtFordon(string[] parkingGarage)
{
    var regnr = LäsRegnr();
    var platsIndex = HittaFordon(parkingGarage, regnr);

    if (platsIndex == -1)
    {
        Console.WriteLine($"Hittade inget fordon med registreringsnummer {regnr}.");
        return;
    }

    TaBortFrånPlats(parkingGarage, platsIndex, regnr);
    Console.WriteLine($"{regnr} är uthämtad från plats {platsIndex + 1}.");
}


static void FlyttaFordon(string[] parkingGarage)
{
    var regnr = LäsRegnr();
    var frånIndex = HittaFordon(parkingGarage, regnr);

    if (frånIndex == -1)
    {
        Console.WriteLine($"Hittade inget fordon med registreringsnummer {regnr}.");
        return;
    }

    Console.WriteLine($"{regnr} står på plats {frånIndex + 1}.");
    var indata = LäsText($"Flytta till plats (1-{AntalPlatser}): ");

    if (!int.TryParse(indata, out int platsnummer))
    {
        Console.WriteLine("Det där var inget tal.");
        return;
    }

    if (platsnummer < 1 || platsnummer > AntalPlatser)
    {
        Console.WriteLine($"Platsnummer måste vara mellan 1 och {AntalPlatser}.");
        return;
    }

    var tillIndex = platsnummer - 1;

    if (tillIndex == frånIndex)
    {
        Console.WriteLine("Fordonet står redan på den platsen.");
        return;
    }

    // Lyft bort fordonet först så vi vet vilken typ det är
    var fordon = TaBortFrånPlats(parkingGarage, frånIndex, regnr);
    DelaUppFordon(fordon, out string fordonstyp, out string flyttatRegnr);

    if (!FinnsPlatsFör(parkingGarage, tillIndex, fordonstyp))
    {
        // Ställ tillbaka fordonet så det inte försvinner
        LäggTillPåPlats(parkingGarage, frånIndex, fordon);
        Console.WriteLine($"Plats {platsnummer} har inte plats för fordonet.");
        return;
    }

    LäggTillPåPlats(parkingGarage, tillIndex, fordon);
    Console.WriteLine($"Flytta {flyttatRegnr} från plats {frånIndex + 1} till plats {platsnummer}.");
}


static void SökFordon(string[] parkingGarage)
{
    var regnr = LäsRegnr();
    var platsIndex = HittaFordon(parkingGarage, regnr);

    if (platsIndex == -1)
    {
        Console.WriteLine($"{regnr} finns inte på parkeringen.");
        return;
    }

    Console.WriteLine($"{regnr} står på plats {platsIndex + 1}.");
    SkrivUtPlats(parkingGarage, platsIndex);
}


static void VisaHelaParkeringen(string[] parkingGarage)
{
    var antalTomma = 0;

    for (int i = 0; i < parkingGarage.Length; i++)
    {
        if (parkingGarage[i] == "")
        {
            antalTomma++;
            continue;
        }

        SkrivUtPlats(parkingGarage, i);
    }

    Console.WriteLine();
    Console.WriteLine($"Tomma platser: {antalTomma} av {AntalPlatser}");
}


static void SkrivUtPlats(string[] parkingGarage, int platsIndex)
{
    Console.Write($"Plats {platsIndex + 1}: ");

    if (parkingGarage[platsIndex] == "")
    {
        Console.WriteLine("(tom)");
        return;
    }

    string[] fordonPåPlatsen = parkingGarage[platsIndex].Split(FordonSkiljare);

    foreach (string fordon in fordonPåPlatsen)
    {
        DelaUppFordon(fordon, out string typ, out string regnr);
        Console.Write($"{typ} {regnr}   ");
    }

    Console.WriteLine();
}

//  Inläsning

static string LäsText(string ledtext)
{
    Console.Write(ledtext);
    string indata = Console.ReadLine() ?? "";
    return indata.Trim();
}

// Versaler så att abc123 och ABC123 räknas som samma fordon
static string LäsRegnr()
{
    return LäsText("Registreringsnummer: ").ToUpper();
}


