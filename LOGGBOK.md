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

