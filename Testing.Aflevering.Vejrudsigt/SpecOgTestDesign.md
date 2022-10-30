# Punkt 1, specifikation af program opførelse

Min tjeneste beskriver ikke ændringer i vejret, men derimod dagens vejr.

Selve vejr-typen, det der hedder conditions i koden, ændrer den returnede beskrivelse en-til-en. dvs. at den bliver direkte integreret i beskrivelse uden ændringer.

Det der udgør udfald for den returnerede tekst er temperaturen, da jeg personligt er dårlig til at håndtere varme og især alt for meget af den, ligemeget hvad vejret er.

# Testdesign

Jeg har testet mit program både ved manuelt at køre det og bede om en beskrivlse for at se om det virker, og derefter lavet unit tests.
Disse unit tests fokuserer mest på en Output based testing stil da jeg fokuserer på DescriptionGenerator klassen som er lavet efter funktionel programmering.

## Grænseværdianalyse

For at finde grænseværdier har jeg først lavet en ækvivalenspartitionering

### **Conditions**

| Invalid | Valid                               |
| ------- | ----------------------------------- |
| Hej     | Klart vejr, Regn, Sne, Skyet, Andet |

### **Temperature**

| Invalid | Valid     | Invalid |
| ------- | --------- | ------- |
| <= -90  | -89 -> 56 | > 56    |

**Valgte Grænseværdier**
**Conditions:**
Hej, Regn
**Temperature:** -90, -89, 56, 57

## Beslutningstabel

De værdier jeg har brugt til testing er de samme som jeg bruger til denne beslutningstabel. Alle undtagen den sidste er værdier fundet i Grænseværdi analysen, dog medtaget for at teste alle mulige udkom fra programmet.

| Conditions | Temperature | Result                                                       |
| ---------- | ----------- | ------------------------------------------------------------ |
| Regn       | -90         | InvalidDataException                                         |
| Hej        | -89         | InvalidDataException                                         |
| Regn       | 57          | InvalidDataException                                         |
| Regn       | -89         | Regn og temperatur hvor man faktisk kan overleve!!. PERFEKT! |
| Regn       | 56          | Regn og varme nok til at slå en elefant ihjel. FUCKING LORT! |
| Regn       | 23          | Regn og den der irriterende slags varme!. meh..              |
