# Loggbok – Prague Parking 1.0

## 2026-09-28

**Gjorde:**
Läste uppgiften, skapade projektet och satte upp ett publikt
GitHub-repo. Skrev konstanter, arrayen med 100 platser och
huvudloopen med menyval i en switch.

**Problem:**
Menyval 5 gjorde ingenting. `break` stod före metodanropet,
så koden efter kördes aldrig.

**Lösning:**
Bytte plats på raderna. Först anropet, sedan `break`.

**Nästa steg:**
Skriva metoderna VisaMeny och LäsText så att programmet går att köra.

## 2026-10-05

**Gjorde:**
Skrev menyn (VisaMeny) och inläsningsmetoden LäsText. Skrev metoderna
för alla menyval: parkera, hämta ut, flytta, söka och visa hela
parkeringen. Alla metoder tar emot parkingGarage som parameter, som i
Claes demo "14 Rullande logg" från vektorlektionen.

**Problem 1:**
Copilot föreslog kod för ParkeraFordon med Array.Exists och lambdor
(plats => ...). Det har vi inte gått igenom, och koden sparade bara
registreringsnumret, inte fordonstypen. Då gick det inte att veta om en
plats hade en bil eller en MC, och två MC kunde aldrig dela plats.

**Lösning:**
Bytte ut lambdorna mot egna hjälpmetoder med vanliga for-loopar.
Fordonet sparas nu i formatet "CAR#ABC123".

**Problem 2 (inte löst än):**
Programmet ger 15 fel. Metoderna anropar hjälpmetoder som HittaFordon
och LäggTillPåPlats, men de finns inte i filen än.

**Lärde mig:**
- När en array skickas till en metod jobbar metoden på samma array,
  inte en kopia.
- Index börjar på 0, så plats 1 är index 0. Därför +1 vid utskrift
  och -1 vid inmatning.

**Nästa steg:**
Lägga till hjälpmetoderna så att felen försvinner. Sedan testa alla
funktioner.

## 2026-10-07

**Gjorde:**
Lade till hjälpmetoderna som saknades: HittaFordon, HittaLedigPlats,
FinnsPlatsFör, ÄrEnsamMc, LäggTillPåPlats, TaBortFrånPlats,
SkapaFordon och DelaUppFordon.

**Lösning på Problem 2 (från 2026-10-05):**
De 15 felen försvann när hjälpmetoderna lades till. Felen berodde på
att menymetoderna anropade metoder som inte fanns än.

**Lärde mig:**
- En metod kan bara returnera ett värde. Med out-parametrar kan den
  lämna tillbaka två (DelaUppFordon ger både typ och regnr).
- -1 används som "hittades inte", eftersom inget index kan vara negativt.

**Nästa steg:**
Testa att flytta och hämta ut MC från en plats med två MC.


## 2026-10-08

**Problem:**
Hur flyttar man en MC som står på en plats med två MC, utan att den
andra försvinner?

**Lösning:**
TaBortFrånPlats delar upp platsen med Split('|'), loopar igenom
fordonen med foreach och delar varje fordon med DelaUppFordon.
Det fordon som har rätt regnr tas bort, och det andra sparas tillbaka
på platsen. Jämförde med en klasskamrats lösning i Discord, som
byggde på samma idé men skrev Split('#') direkt i loopen.

**Lärde mig:**
Det är bättre att samla återkommande kod i en egen metod
(DelaUppFordon) än att skriva Split('#') på flera ställen.


## 2026-10-10

**Testade:**
- Parkera bil och två MC. MC:na hamnade på samma plats.
- Söka med små bokstäver. Fordonet hittades ändå.
- Flytta en bil till en plats med en MC. Det nekades och bilen stod kvar.
- Hämta ut en av två MC. Den andra stod kvar.
[Skriv om något test inte fungerade och hur du löste det.]

**Status:**
Prague Parking 1.0 är klar.