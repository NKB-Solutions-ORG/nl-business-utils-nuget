# NLBusinessUtils

Validatie- en formatteringshulpmiddelen voor Nederlandse zakelijke gegevens: BSN,
RSIN, KVK-nummer, vestigingsnummer, btw-nummer, loonheffingennummer, IBAN
(inclusief bank-lookup), postcode, telefoonnummer en euro-bedragen. Geen
dependencies, target `netstandard2.0` (werkt op .NET Framework 4.6.1+, .NET
Core 2.0+ en alle moderne .NET-versies).

Ook beschikbaar voor Node.js: [nl-business-utils op npm](https://www.npmjs.com/package/nl-business-utils).

## Installatie

```bash
dotnet add package NLBusinessUtils
```

## Gebruik

```csharp
using NLBusinessUtils;

Bsn.IsValid("123456782"); // true
KvkNumber.IsValid("12345678"); // true

Iban.IsValidDutch("NL91ABNA0417164300"); // true
Iban.Format("nl91abna0417164300"); // "NL91 ABNA 0417 1643 00"
DutchBank.GetName("NL91ABNA0417164300"); // "ABN AMRO Bank N.V."

Postcode.IsValidDutch("1234ab"); // true
Postcode.FormatDutch("1234ab"); // "1234 AB"

PhoneNumber.IsValidDutch("06-12345678"); // true
PhoneNumber.FormatDutch("06-12345678"); // "+31612345678"

LoonheffingenNummer.IsValid("123456782L01"); // true
Vestigingsnummer.IsValid("123456789012"); // true

VatCalculator.AddVat(100m, VatRates.Standard); // 121
VatCalculator.RemoveVat(121m, VatRates.Standard); // 100

Currency.FormatEuro(1234.5m); // "€ 1.234,50"
Currency.ParseEuroAmount("€ 1.234,50"); // 1234.5m
```

## API

### Bsn / Rsin

- `Bsn.IsValid(string value) : bool` — valideert een burgerservicenummer met de elfproef. Accepteert 8 of 9 cijfers.
- `Bsn.Normalize(string value) : string` — geeft de canonieke 9-cijferige vorm terug, of gooit een `ArgumentException`.
- `Rsin.IsValid(string value) : bool` / `Rsin.Normalize(string value) : string` — zelfde elfproef, voor rechtspersonen.

### KvkNumber

- `KvkNumber.IsValid(string value) : bool` — controleert of de invoer uit precies 8 cijfers bestaat.
  > De KVK publiceert geen checksum-algoritme voor haar nummers. Dit is dus een
  > formaatcontrole, geen bewijs dat het nummer daadwerkelijk geregistreerd is —
  > gebruik hiervoor de KVK API.

### VatNumber (btw-nummer)

- `VatNumber.IsValidFormat(string value) : bool` — controleert de structuur `NL` + 9 cijfers + `B` + 2 cijfers (01-99).
- `VatNumber.HasValidLegacyChecksum(string value) : bool` — controleert de elfproef op het 9-cijferige deel.
  > **Let op:** dit werkt alleen voor btw-nummers van vóór 2020, die zijn afgeleid
  > van het BSN/RSIN. Sinds 1 januari 2020 krijgen natuurlijke personen
  > (eenmanszaken/zzp'ers) een nieuw, privacy-vriendelijk nummer dat *per ontwerp*
  > niet aan de elfproef voldoet — de Belastingdienst heeft het algoritme
  > daarachter niet gepubliceerd. Een `false` resultaat betekent dus **niet**
  > automatisch dat het nummer ongeldig is. Gebruik voor zekerheid de
  > [VIES-dienst](https://ec.europa.eu/taxation_customs/vies/) van de EU.
- `VatNumber.Format(string value) : string` — canonieke `NLxxxxxxxxxBxx`-vorm, of gooit een `ArgumentException`.

### Iban / DutchBank

- `Iban.IsValidDutch(string value) : bool` — structuur + MOD-97 checksum (ISO 13616).
- `Iban.Format(string value) : string` — groepeert in blokken van 4, bv. `NL91 ABNA 0417 1643 00`.
- `DutchBank.GetName(string value) : string?` — banknaam op basis van de 4-letterige bankcode in een geldige IBAN. Dekt een handmatig samengestelde en gecontroleerde lijst met grote Nederlandse banken (ABN AMRO, ING, Rabobank, SNS, ASN, Triodos, Knab, bunq, RegioBank, Achmea Bank); geeft `null` terug voor een ongeldige IBAN of een bank die niet in de lijst staat, in plaats van te gokken.

### Vestigingsnummer

- `Vestigingsnummer.IsValid(string value) : bool` — controleert of de invoer uit precies 12 cijfers bestaat.
  > Ook hiervoor publiceert de KVK geen checksum-algoritme — formaatcontrole only.

### LoonheffingenNummer

- `LoonheffingenNummer.IsValid(string value) : bool` — controleert de structuur BSN/RSIN (9 cijfers, elfproef) + `L` + 2-cijferig volgnummer (01-99), bv. `123456782L01`.
- `LoonheffingenNummer.Format(string value) : string` — canonieke vorm, of gooit een `ArgumentException`.

### Postcode

- `Postcode.IsValidDutch(string value) : bool` — 4 cijfers (1000-9999) + 2 letters, met of zonder spatie. Sluit de door PostNL nooit uitgegeven combinaties `SS`, `SA`, `SD` uit.
- `Postcode.FormatDutch(string value) : string` — normaliseert naar `1234 AB`.

### PhoneNumber

- `PhoneNumber.IsValidDutch(string value) : bool` — nationaal (`0...`) of met `+31`/`0031` prefix, 10 cijfers. Formaatcontrole, geen controle tegen een netnummer-database.
- `PhoneNumber.IsDutchMobile(string value) : bool` — `true` voor mobiele nummers (`06...`).
- `PhoneNumber.FormatDutch(string value) : string` — E.164-formaat, bv. `+31612345678`.

### VatRates / VatCalculator

- `VatRates.Standard` / `VatRates.Reduced` / `VatRates.Zero` — `21m`, `9m`, `0m`.
- `VatCalculator.CalculateVatAmount(decimal bedragExclBtw, decimal tarief) : decimal`
- `VatCalculator.AddVat(decimal bedragExclBtw, decimal tarief) : decimal`
- `VatCalculator.RemoveVat(decimal bedragInclBtw, decimal tarief) : decimal`

Alle bedragen worden op hele centen afgerond (`MidpointRounding.AwayFromZero`).

### Currency

- `Currency.FormatEuro(decimal bedrag) : string` — formatteert als Nederlandse euro-string, bv. `1234.5m` → `"€ 1.234,50"`, `-19.99m` → `"-€ 19,99"`. Bewust handmatig geïmplementeerd (niet via cultuur-afhankelijke formattering) zodat de opmaak stabiel is, onafhankelijk van de globalization-configuratie van de host.
- `Currency.ParseEuroAmount(string waarde) : decimal` — parseert een Nederlandse euro-string (met of zonder `€`-teken) terug naar een decimal. Gooit een `ArgumentException` bij onherkenbare invoer.

## Licentie

MIT — gratis te gebruiken, ook commercieel.
