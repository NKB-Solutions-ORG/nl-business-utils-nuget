# Changelog

Alle noemenswaardige wijzigingen in dit project worden hier bijgehouden.
Dit project volgt [Semantic Versioning](https://semver.org/lang/nl/).

## [1.0.0]

### Toegevoegd

- `DutchBank.GetName` — banknaam-lookup op basis van de bankcode in een geldige Nederlandse IBAN.
- `LoonheffingenNummer.IsValid` / `.Format` — validatie van het loonheffingennummer.
- `Vestigingsnummer.IsValid` — formaatvalidatie van het KVK-vestigingsnummer (12 cijfers).
- `Currency.FormatEuro` / `.ParseEuroAmount` — formatteren en parsen van Nederlandse euro-bedragen.

### Gewijzigd

- Publiceren naar NuGet.org gebeurt nu via OIDC trusted publishing in plaats van een opgeslagen API key.

## [0.1.0]

### Toegevoegd

- `Bsn.IsValid` / `.Normalize` — BSN-validatie met de elfproef.
- `Rsin.IsValid` / `.Normalize` — RSIN-validatie met de elfproef.
- `KvkNumber.IsValid` — formaatvalidatie van het KVK-nummer.
- `VatNumber.IsValidFormat` / `.HasValidLegacyChecksum` / `.Format` — btw-nummer validatie.
- `Iban.IsValidDutch` / `.Format` — IBAN-validatie (MOD-97) en -formattering.
- `Postcode.IsValidDutch` / `.FormatDutch` — postcode-validatie en -formattering.
- `PhoneNumber.IsValidDutch` / `.IsDutchMobile` / `.FormatDutch` — telefoonnummer-validatie en -formattering.
- `VatRates` / `VatCalculator.CalculateVatAmount` / `.AddVat` / `.RemoveVat` — btw-rekenmodule.
