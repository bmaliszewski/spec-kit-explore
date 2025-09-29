# Konfiguracja IIS dla aplikacji eFitness-Kiosk

Ten dokument zawiera szczegółowe instrukcje dotyczące ręcznej konfiguracji serwera IIS (Internet Information Services) na systemie Windows w celu hostowania aplikacji React.js (frontend) oraz BFF .NET Core (backend).

## Wymagania wstępne

1.  Zainstalowany serwer IIS na systemie Windows Server.
2.  Zainstalowany .NET Hosting Bundle dla wersji .NET używanej przez BFF (np. .NET 8.0 Hosting Bundle).
3.  Zainstalowany moduł URL Rewrite dla IIS.
4.  Dostęp do plików aplikacji frontend (zbudowana aplikacja React.js) i backend (opublikowana aplikacja .NET Core).

## 1. Konfiguracja Backendu (BFF .NET Core)

### 1.1. Publikacja aplikacji .NET Core

Opublikuj aplikację .NET Core BFF do wybranego katalogu na serwerze IIS (np. `C:\inetpub\wwwroot\eFitnessKioskApi`).

```bash
dotnet publish -c Release -o C:\inetpub\wwwroot\eFitnessKioskApi
```

### 1.2. Utworzenie puli aplikacji (Application Pool)

1.  Otwórz Menedżera Internet Information Services (IIS).
2.  W panelu "Połączenia" rozwiń węzeł serwera i kliknij "Pule aplikacji".
3.  W panelu "Akcje" kliknij "Dodaj pulę aplikacji...".
4.  Podaj nazwę puli aplikacji (np. `eFitnessKioskApiPool`).
5.  Wybierz wersję .NET CLR: "Brak kodu zarządzanego" (No Managed Code), ponieważ .NET Core działa jako samodzielny proces.
6.  Tryb potoku zarządzanego: "Zintegrowany".
7.  Zaznacz "Uruchom pulę aplikacji natychmiast".
8.  Kliknij "OK".
9.  Wybierz nowo utworzoną pulę aplikacji, a następnie w panelu "Akcje" kliknij "Ustawienia zaawansowane...".
10. Upewnij się, że "Model procesu -> Tożsamość" jest ustawiona na konto z odpowiednimi uprawnieniami (np. `ApplicationPoolIdentity` lub dedykowane konto użytkownika).

### 1.3. Utworzenie witryny/aplikacji w IIS

1.  W Menedżerze IIS, w panelu "Połączenia" rozwiń węzeł serwera i kliknij "Witryny".
2.  W panelu "Akcje" kliknij "Dodaj witrynę..." (lub "Dodaj aplikację..." jeśli dodajesz do istniejącej witryny).
3.  **Nazwa witryny**: `eFitnessKioskApi`.
4.  **Pula aplikacji**: Wybierz `eFitnessKioskApiPool` (utworzoną w poprzednim kroku).
5.  **Ścieżka fizyczna**: Wskaż katalog, do którego opublikowano aplikację .NET Core (np. `C:\inetpub\wwwroot\eFitnessKioskApi`).
6.  **Typ**: `http` (lub `https` jeśli masz certyfikat).
7.  **Adres IP**: `Wszystkie nieprzypisane` lub konkretny adres IP serwera.
8.  **Port**: `80` (dla http) lub `443` (dla https). Upewnij się, że port nie jest używany przez inną witrynę.
9.  **Nazwa hosta**: Opcjonalnie, jeśli aplikacja ma być dostępna pod konkretną nazwą domenową (np. `api.efitness-kiosk.local`).
10. Kliknij "OK".

## 2. Konfiguracja Frontendu (React.js SPA)

### 2.1. Publikacja aplikacji React.js

Zbuduj aplikację React.js do katalogu `build` (domyślnie) i skopiuj jego zawartość do wybranego katalogu na serwerze IIS (np. `C:\inetpub\wwwroot\eFitnessKioskFrontend`).

```bash
npm run build
# Następnie skopiuj zawartość katalogu build do C:\inetpub\wwwroot\eFitnessKioskFrontend
```

### 2.2. Utworzenie witryny/aplikacji w IIS

1.  W Menedżerze IIS, w panelu "Połączenia" rozwiń węzeł serwera i kliknij "Witryny".
2.  W panelu "Akcje" kliknij "Dodaj witrynę...".
3.  **Nazwa witryny**: `eFitnessKioskFrontend`.
4.  **Pula aplikacji**: Możesz użyć domyślnej puli aplikacji (`DefaultAppPool`) lub utworzyć nową.
5.  **Ścieżka fizyczna**: Wskaż katalog, do którego skopiowano zbudowaną aplikację React.js (np. `C:\inetpub\wwwroot\eFitnessKioskFrontend`).
6.  **Typ**: `http` (lub `https`).
7.  **Adres IP**: `Wszystkie nieprzypisane` lub konkretny adres IP serwera.
8.  **Port**: `81` (dla http) lub inny wolny port. Jeśli używasz tej samej nazwy hosta co backend, będziesz potrzebować innego portu lub innej nazwy hosta.
9.  **Nazwa hosta**: Opcjonalnie, jeśli aplikacja ma być dostępna pod konkretną nazwą domenową (np. `app.efitness-kiosk.local`).
10. Kliknij "OK".

### 2.3. Konfiguracja URL Rewrite dla SPA

Aplikacje SPA wymagają przekierowania wszystkich żądań do pliku `index.html`, aby routing po stronie klienta działał poprawnie. Utwórz plik `web.config` w katalogu głównym aplikacji frontend (np. `C:\inetpub\wwwroot\eFitnessKioskFrontend`) z następującą zawartością:

```xml
<?xml version="1.0" encoding="UTF-8"?>
<configuration>
  <system.webServer>
    <rewrite>
      <rules>
        <rule name="React Routes" stopProcessing="true">
          <match url=".*" />
          <conditions logicalGrouping="MatchAll">
            <add input="{REQUEST_FILENAME}" matchType="IsFile" negate="true" />
            <add input="{REQUEST_FILENAME}" matchType="IsDirectory" negate="true" />
            <add input="{REQUEST_URI}" pattern="^/(api)" negate="true" />
          </conditions>
          <action type="Rewrite" url="/index.html" />
        </rule>
      </rules>
    </rewrite>
  </system.webServer>
</configuration>
```

**Wyjaśnienie reguły:**
-   `match url=".*"`: Dopasowuje wszystkie żądania.
-   `conditions`: Sprawdza, czy żądanie nie jest plikiem, nie jest katalogiem i nie zaczyna się od `/api` (aby nie kolidować z BFF).
-   `action type="Rewrite" url="/index.html"`: Przekierowuje wszystkie pasujące żądania do `index.html`.

## 3. Konfiguracja komunikacji między Frontendem a Backendem

### 3.1. Ustawienie zmiennej środowiskowej w React.js

W pliku `.env` w katalogu `frontend/` ustaw adres URL do BFF:

```
REACT_APP_API_URL=http://api.efitness-kiosk.local/api
# lub REACT_APP_API_URL=http://localhost:80/api jeśli używasz portu 80 i nie masz nazwy hosta
```

Następnie zbuduj aplikację React.js ponownie (`npm run build`).

### 3.2. Konfiguracja CORS w BFF

W aplikacji .NET Core BFF, w pliku `Program.cs`, dodaj konfigurację CORS, aby frontend mógł komunikować się z backendem. Dodaj następujący kod przed `app.UseAuthorization();`:

```csharp
app.UseCors(builder => builder
    .WithOrigins("http://app.efitness-kiosk.local", "http://localhost:81") // Adres URL frontendu
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
```

Upewnij się, że adresy URL w `WithOrigins` odpowiadają adresowi, pod którym działa frontend.

## 4. Testowanie

Po wykonaniu wszystkich kroków, spróbuj uzyskać dostęp do aplikacji frontend poprzez przeglądarkę. Powinieneś zobaczyć stronę logowania. Sprawdź, czy komunikacja z backendem działa poprawnie (rejestracja, logowanie, pobieranie danych).
