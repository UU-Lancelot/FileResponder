# Responder

Vycházím ze znalostí ETRM. Předpokládám, že u FMS to bude podobné.

ETRM tedy funguje tak, že v jedné složce má veškerou komunikaci k jedné 3. straně (např. OTE denní trh). Do této složky mohou přijít různé zprávy (vytvoření, úprava, zrušení, žádost o opis atd.). Na každou z těchto zpráv existují různé odpovědi (úspěch, chyba, upravené hodnoty atd.). Takto to odráží konfigurace. 

Při příjmu zprávy proběhne toto flow:
1. Vytvoření nového scope a MessageContextu - FolderWatcher
1. Výběr instance podle složky - InstanceSelector
1. Výběr formátu podle přípony souboru - FormatSelector
1. Výběr typu příchozí zprávy podle query - RequestSelector
1. Validace schema příchozí zprávy (pokud je nakonfigurováno) - InputValidator
1. Výběr odpovědi (pokud nebyla chyba ve validaci) - ResponseSelector 
1. Vytvoření odchozí zprávy (zpráv) ze šablony - MessageCreator
    1. Určení míst, které jsou potřeba v šabloně doplnit
    1. Doplnění míst v šabloně

Každý krok bude obstarávat jedna třída. Pro některé kroky bude existovat několik možností. To obstarají pluginy.
* 3+6a - FormatIO
    * Načítá informace z příchozí zprávy
    * Určuje, která místa se budou v šabloně nahrazovat
    * Př.: Xml, Json, Csv
* 4 - InputValidator
    * Zvaliduje input, v případě neúspěchu určí odpověď
    * Př.: Xsd, Json-schema
* 5 - ResponseSelector
    * Určuje podle nějakého pravidla, jaká odpověď se odešle
    * Př.: Iterate, Random, First
* 6b - Replacer
    * Nahrazuje místa v šabloně
    * Umí nahrazovat hodnoty nebo celé bloky
    * Př.: Input, Config, For, Random

O načítání a výběr správného pluginu se bude starat PluginLoader. 

Flow startu programu:
* Načtení pluginů - PluginLoader
* Načtení a validace konfigurace - ConfigurationLoader
* Spuštění sledování složek - FolderWatcher

Příklad konfigurace:
```json
{
  "instances": [
    {
      "name": "OTE daily market", // pro uživatele, do logů
      "inputFolder": "/DM/OTE/Output", // vstupní složka pro responder, výstupní pro Lancelot
      "outputFolder": "/DM/OTE/Input", // výstupní složka pro responder, vstupní pro Lancelot
      "requests": [ // jaké requesty mohou do složky přijít
        {
          "name": "811", // pro uživatele, pro RequestSelector
          "query": { // hodnoty, podle kterých RequestSelector rozhoduje; nepovinné, pokud je pouze jeden;
            "*[message-code]": "811"
          },
          "validation": { // validace vstupního souboru; nepovinné
            "validator": "XsdValidator", // název validátoru pro PluginLoader
            "filePath": "/DM/XSD/ISOTEDATA.XSD", // cesta k souboru se schematem
            "failedRespondName": "Invalid schema", // jaká odpověď se má vybrat, pokud je vstupní soubor nevalidní
          },
          "responseSelector": "Iterate", // jakým způsobem se vybere odpověď; nepovinné, default je Iterate
          "responds": [ // jaké odpovědi může request mít
            {
              "name": "Success 812", // název pro uživatele, pro ResponseSelector
              "multipleResponds": [ // na tuto zprávu se odpovídá několika po sobě jdoucími zprávami
                {
                  "templatePath": "/DM/Templates/812_success.xml", // cesta k souboru s šablonou
                  "values": { // hodnoty, které může použít Replacer Config nebo responseSelector; nepovinné
                    "code": "0200"
                  }
                },
                {
                  "templatePath": "/DM/Templates/813_success.xml"
                }
              ]
            },
            {
              "name": "Invalid schema",
              "templatePath": "/DM/Templates/812_invalidSchema.xml"
            }
          ]
        }
      ]
    }
  ]
}
```

Příklad šablony:
```xml
<?xml version="1.0" encoding="utf-8"?>
<RESPONSE
  date-time="{{ Datetime.Now }}"
  dtd-release="1"
  dtd-version="1"
  id="{{ Random.IntRange(10000000000000, 99999999999999) }}"
  message-code="812"
  xmlns="http://www.ote-cr.cz/schema/response">
    <SenderIdentification
      coding-scheme="{{ Input.*.ReceiverIdentification.coding-scheme }}"
      id="{{ Input.*.ReceiverIdentification.id }}" />
    <ReceiverIdentification
      coding-scheme="{{ Input.*.SenderIdentification.coding-scheme }}"
      id="{{ Input.*.SenderIdentification.id }}" />
    <Reference id="{{ Input.*[id] }}" />
    <Reason code="{{ Config.code }}" type="A01">Zpráva úspěšně přijata</Reason>
</RESPONSE>
```

Příklad šablony s nahrazením bloku:
```xml
<?xml version="1.0" encoding="UTF-8"?>
<ISOTEDATA
  xmlns:xsd="http://www.w3.org/2001/XMLSchema"
  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"
  answer-required="false"
  dtd-version="1" 
  dtd-release="1"
  message-code="813"
  id="{{ Random.IntRange(10000000000000, 99999999999999) }}"
  date-time="{{ Datetime.Now }}"
  xmlns="http://www.ote-cr.cz/schema/market/data">
    <SenderIdentification
      coding-scheme="{{ Input.*.ReceiverIdentification.coding-scheme }}"
      id="{{ Input.*.ReceiverIdentification.id }}" />
    <ReceiverIdentification
      coding-scheme="{{ Input.*.SenderIdentification.coding-scheme }}"
      id="{{ Input.*.SenderIdentification.id }}" />
    <Reference id="{{ Input.*[id] }}" />
    <Trade source-sys="OTE" sett-curr="CZK" util-flag="1" trade-market-flag="SPT" version="0" trade-state="N" trade-type="N" id="716347" trade-day="2019-11-02" trade-flag="N" trade-stage="P" acceptance="N" error-code="0" replacement="N">
      <TimeData datetime="2019-11-01T10:37:41" datetime-type="DTC" />
      <ProfileData profile-role="BP01">
        {{ For.item.In(5) }} <!-- 5x opakuj, index ulož do item -->
          <Data
            value="100.00"
            period="{{ item + 3 }}"
            splitting="A"
            unit="EUR" />
        {{ End }}
      </ProfileData>
      <ProfileData profile-role="BC01">
        {{ For.item.In(5) }}
          <Data 
            value="{{ Random.DecimalRange(0, 100) }}"
            period="{{ item + 3 }}"
            splitting="A"
            unit="EUR" />
        {{ End }}
      </ProfileData>
      <Comment />
      <Party id="8591824289006" role="TO" />
    </Trade>
</ISOTEDATA>
```