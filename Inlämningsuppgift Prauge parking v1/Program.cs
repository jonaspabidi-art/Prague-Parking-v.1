const int AntalPlatser = 100;
const int MaxLängdRegistreringsnummer = 10;

const string Car = "CAR";
const string MC = "MC";

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


