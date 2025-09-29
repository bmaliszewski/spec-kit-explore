# Tasks: Autonomiczne Kluby Fitness eFitness-Kiosk

**Input**: Design documents from `/specs/001-wdra-asz-projekt/`
**Prerequisites**: plan.md, research.md, data-model.md, contracts/

## Phase 3.1: Setup

### Backend (BFF)
- [x] T001 [P] Utwórz nowy projekt .NET Core Web API w katalogu `backend/`.
- [x] T002 [P] Dodaj niezbędne pakiety NuGet (np. Swashbuckle.AspNetCore, Microsoft.AspNet.WebApi.Client).
- [x] T003 [P] Skonfiguruj projekt do generowania dokumentacji Swagger/OpenAPI na podstawie kodu.

### Frontend
- [x] T004 [P] Utwórz nową aplikację React.js w katalogu `frontend/` za pomocą `create-react-app`.
- [x] T005 [P] Zainstaluj zależności: `axios` do zapytań HTTP, `react-router-dom` do routingu.
- [x] T006 [P] Skonfiguruj podstawową strukturę katalogów (`components`, `pages`, `services`).

## Phase 3.2: Backend - Modele i Serwisy
- [x] T007 [P] Zaimplementuj modele C# (`User.cs`, `Pass.cs`, `Payment.cs`) w `backend/src/models/` na podstawie `data-model.md`.
- [x] T008 [P] Utwórz serwis `EFitnessAuthService` do obsługi logowania i autoryzacji z API eFitness.
- [x] T009 [P] Utwórz serwis `EFitnessUserService` do zarządzania danymi użytkowników.
- [x] T010 [P] Utwórz serwis `EFitnessPassService` do pobierania informacji o karnetach.
- [x] T011 [P] Utwórz serwis `EFitnessPaymentService` do obsługi płatności przez API eFitness.

## Phase 3.3: Backend - Kontrolery API
- [x] T012 Zaimplementuj `AuthController` (`/auth/login`).
- [x] T013 Zaimplementuj `UsersController` (`/users`, `/users/me`).
- [x] T014 Zaimplementuj `PassesController` (`/passes`).
- [x] T015 Zaimplementuj `PaymentsController` (`/payments/passes/{passId}`, `/payments/me`).

## Phase 3.4: Backend - Testy
- [x] T016 [P] Napisz testy jednostkowe dla serwisów (mockując API eFitness).
- [x] T017 [P] Napisz testy integracyjne dla kontrolerów API.

## Phase 3.5: Frontend - Serwisy i Routing
- [x] T018 [P] Utwórz `api.js` lub podobny moduł do obsługi zapytań do BFF za pomocą `axios`.
- [x] T019 [P] Zaimplementuj serwisy (`authService.js`, `userService.js`, etc.) do komunikacji z poszczególnymi endpointami BFF.
- [x] T020 Skonfiguruj routing aplikacji (`react-router-dom`) w `App.js` dla wszystkich stron.

## Phase 3.6: Frontend - Komponenty i Strony
- [x] T021 [P] Utwórz komponenty UI (np. `Button`, `Input`, `Dropdown`, `Spinner`).
- [x] T022 [P] Zaimplementuj stronę logowania (`LoginPage.js`).
- [x] T023 [P] Zaimplementuj stronę rejestracji (`RegisterPage.js`).
- [x] T024 [P] Zaimplementuj stronę profilu użytkownika (`ProfilePage.js`).
- [x] T025 [P] Zaimplementuj stronę z listą karnetów (`PassesPage.js`).
- [x] T026 [P] Zaimplementuj stronę z historią płatności (`PaymentsPage.js`).
- [x] T027 [P] Zaimplementuj mechanizm wylogowania po 1 minucie bezczynności.

## Phase 3.7: Frontend - Testy
- [x] T028 [P] Napisz testy jednostkowe dla komponentów UI.
- [x] T029 [P] Napisz testy integracyjne dla poszczególnych stron (np. proces logowania).

## Phase 3.8: Integracja i Wdrożenie
- [x] T030 Skonfiguruj serwer IIS na Windows do serwowania aplikacji React.js oraz BFF .NET Core.
- [x] T031 Przeprowadź testy end-to-end na terminalu kiosk.

## Dependencies
- Backend (T007-T015) musi być gotowy przed rozpoczęciem prac nad frontendem (T018-T026).
- Testy (T016, T017, T028, T029) powinny być pisane równolegle z implementacją kodu, który testują (TDD).
