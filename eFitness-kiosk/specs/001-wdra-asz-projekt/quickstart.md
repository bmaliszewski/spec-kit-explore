# Quickstart: Autonomiczne Kluby Fitness eFitness-Kiosk

Ten dokument opisuje, jak uruchomić i przetestować aplikację "eFitness-Kiosk".

## Uruchomienie Aplikacji

### Backend-for-Frontend (BFF)
1.  Sklonuj repozytorium.
2.  Przejdź do katalogu `backend/`.
3.  Zainstaluj zależności (`dotnet restore`).
4.  Uruchom aplikację (`dotnet run`).

### Frontend (SPA)
1.  Przejdź do katalogu `frontend/`.
2.  Zainstaluj zależności (`npm install`).
3.  Uruchom aplikację w trybie deweloperskim (`npm start`).
4.  Aplikacja będzie dostępna pod adresem `http://localhost:3000`.

## Scenariusze Testowe

### 1. Rejestracja nowego użytkownika
1.  Otwórz aplikację.
2.  Kliknij przycisk "Zarejestruj się".
3.  Wypełnij formularz danymi nowego użytkownika.
4.  Zaakceptuj zgody RODO.
5.  Kliknij "Zarejestruj".
6.  **Oczekiwany rezultat**: Użytkownik zostaje zalogowany i przekierowany do swojego profilu.

### 2. Zakup karnetu
1.  Zaloguj się do aplikacji.
2.  Przejdź do sekcji "Karnety".
3.  Wybierz jeden z dostępnych karnetów.
4.  Wybierz metodę płatności.
5.  Kliknij "Kup i zapłać".
6.  Zostaniesz przekierowany na stronę PayU w celu dokonania płatności.
7.  Po pomyślnej płatności, zostaniesz przekierowany z powrotem do aplikacji.
8.  **Oczekiwany rezultat**: W profilu użytkownika widoczny jest nowo zakupiony, aktywny karnet.

### 3. Sprawdzenie historii płatności
1.  Zaloguj się do aplikacji.
2.  Przejdź do sekcji "Płatności".
3.  **Oczekiwany rezultat**: Widoczna jest lista wszystkich dokonanych transakcji wraz z ich statusami i logami.
