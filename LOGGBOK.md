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

