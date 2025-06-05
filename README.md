# Invoice Manager - ABP.io Aplikace

## Popis domény

Aplikace eviduje **faktury** a jejich **položky** postavená na ABP Framework 9.1.1.

### Doménový model

**Invoice (Faktura):**
- InvoiceId, InvoiceNumber, IssueDate, TotalAmount
- State: Created → Approved → Paid
- Kolekce InvoiceLine

**InvoiceLine (Řádek faktury):**
- ProductName, Quantity, UnitPrice, TotalPrice
- TotalPrice = Quantity × UnitPrice

**Business pravidla:**
- TotalAmount faktury se automaticky přepočítává při změnách řádků
- Faktura musí mít řádky před schválením
- Pouze schválené faktury lze uhradit

## Architektura

Projekt byl vytvořen pomocí **ABP CLI**:
```bash
abp new InvoiceManager --ui no-ui -m none --theme basic -csf
```

Aplikace využívá **layered architecture** ABP.io s důrazem na efektivní použití ABP frameworku:

- **Domain** - entity Invoice, InvoiceLine s business logikou
- **Application** - InvoiceAppService pro CRUD operace a změny stavů
- **Infrastructure** - EF Core repository a DbContext
- **Web** - ASP.NET Core s REST API a Swagger UI

### Využití ABP.io architektury

- **Moduly pro autentizace** - ABP Identity modul pro registraci/přihlášení
- **Auditing** - `[Audited]` atribut na InvoiceAppService pro automatické logování
- **Base classes** - `FullAuditedAggregateRoot<Guid>` pro Invoice entity
- **DTO mapování** - AutoMapper integrace s `InvoiceManagerAppService` base třídou
- **Repository pattern** - `IRepository<T>` interface s custom `IInvoiceRepository`
- **Exception handling** - `BusinessException` pro business validace
- **Authorization** - `[Authorize]` a `[AllowAnonymous]` atributy

## REST API

### Veřejné
- `GET /api/app/invoice/{id}` - detail faktury
- `GET /api/app/invoice` - seznam faktur

### Autorizované  
- `POST /api/app/invoice` - vytvoření faktury
- `POST /api/app/invoice/{id}/lines` - přidání řádku
- `PUT /api/app/invoice/{invoiceId}/lines/{lineId}` - úprava řádku
- `DELETE /api/app/invoice/{invoiceId}/lines/{lineId}` - smazání řádku
- `POST /api/app/invoice/{id}/approve` - schválení
- `POST /api/app/invoice/{id}/pay` - uhrazení

### Autentizace pro autorizované endpointy
Pro přístup k autorizovaným endpointům je nutné:

1. **Přihlášení:** `POST /api/account/login` (username/password)
2. **Registrace:** `POST /api/account/register` (pro nové uživatele)

**Výchozí admin účet:** ABP automaticky vytváří během database seedu defaultního uživatele:
- Username: `admin`
- Password: `1q2w3E*`

ABP automaticky poskytuje tyto endpointy díky `AbpAccountWebOpenIddictModule`.

## Spuštění aplikace

### Předpoklady
- .NET 9.0 SDK
- SQL Server

### Kroky
1. **Instalace balíčků:** `abp install-libs`
2. **Migrace databáze:** `dotnet run --project src/InvoiceManager.DbMigrator`
3. **Spuštění:** `dotnet run --project src/InvoiceManager.Web`

**Poznámka:** Všechny příkazy volat z root adresáře projektu.

4. **Přístup:** 
   - **Aplikace:** `https://localhost:44393/swagger`
   - **Databáze:** Server: `(LocalDb)\MSSQLLocalDB`, Databáze: `InvoiceManager`

## Validace

### Formální validace (Data Annotations)
- Required atributy pro povinná pole
- StringLength validace pro číslo faktury a název produktu
- Range validace pro množství (min 1) a jednotkovou cenu (min 0.01)
- ABP automaticky aplikuje tyto validace před voláním aplikačních služeb

### Business validace
- **Invoice entity:** kontrola stavů při změnách, validace neprázdné faktury před schválením
- **InvoiceLine entity:** kontrola kladných hodnot pomocí ABP Check utility
- **Aplikační služby:** kontrola jedinečnosti čísla faktury, ověření existence entit
- Automatické přepočítávání celkových částek při změnách řádků
- Použití ABP BusinessException pro business chyby

## Testování

Aplikace obsahuje **unit testy** pro doménové objekty (`InvoiceTests`, `InvoiceLineTests`) ověřující správnost business logiky, validací a stavových změn entit.