# Implementation Plan: Autonomiczne Kluby Fitness eFitness-Kiosk

**Branch**: `001-wdra-asz-projekt` | **Date**: 2025-09-26 | **Spec**: [./spec.md](./spec.md)
**Input**: Feature specification from `/specs/001-wdra-asz-projekt/spec.md`

## Summary
Celem projektu jest stworzenie aplikacji "eFitness-Kiosk" dla autonomicznych klubów fitness. Aplikacja, działająca na terminalach dotykowych, umożliwi nowym użytkownikom rejestrację, logowanie, zarządzanie kontem i karnetami oraz dokonywanie płatności w systemie eFitness bez udziału personelu. Architektura oprze się o aplikację SPA (React.js) hostowaną na serwerze IIS w systemie Windows, która będzie komunikować się z API eFitness poprzez dedykowany backend-for-frontend (BFF).

## Technical Context
**Language/Version**: JavaScript (ES2022+), React.js 18+
**Primary Dependencies**: React.js, React Router, Axios, eFitness API
**Storage**: N/A (dane przechowywane w systemie eFitness)
**Testing**: Jest, React Testing Library
**Target Platform**: Terminale kiosk z systemem Windows i przeglądarką internetową w trybie pełnoekranowym, aplikacja serwowana przez IIS.
**Project Type**: Aplikacja internetowa (SPA) z backend-for-frontend (BFF)
**Performance Goals**: Szybki czas reakcji interfejsu użytkownika, zoptymalizowany pod kątem terminali dotykowych.
**Constraints**: Aplikacja musi działać na serwerze ACS z systemem Windows i być serwowana przez IIS. Konieczność integracji z zewnętrznym systemem API eFitness.
**Scale/Scope**: Aplikacja będzie wdrażana w wielu klubach fitness.

## Constitution Check
*GATE: Must pass before Phase 0 research. Re-check after Phase 1 design.*

- **Code Quality**: Is the proposed solution clean, readable, and following best practices? - PASS (to be verified during implementation)
- **Testing Standards**: Does the plan include comprehensive testing (TDD encouraged)? - PASS (Jest and React Testing Library are included)
- **User Experience Consistency**: Does the design align with the established design system? - PASS (to be verified during implementation)
- **Performance Requirements**: Are performance implications considered and tested? - PASS (performance goals are defined)
- **Hardware Reusage**: Does the solution prioritize reusing existing hardware? - PASS (uses existing ACS servers)
- **Simplicity**: Is the solution simple and avoids over-engineering (YAGNI)? - PASS (BFF is a necessary complexity to handle external APIs securely)
- **Polish Language for Specifications**: Is the specification written in Polish? - PASS

## Project Structure

### Documentation (this feature)
```
specs/001-wdra-asz-projekt/
├── plan.md              # This file (/plan command output)
├── research.md          # Phase 0 output (/plan command)
├── data-model.md        # Phase 1 output (/plan command)
├── quickstart.md        # Phase 1 output (/plan command)
├── contracts/           # Phase 1 output (/plan command)
│   └── openapi.yaml
└── tasks.md             # Phase 2 output (/tasks command - NOT created by /plan)
```

### Source Code (repository root)
```
# Option 2: Web application (when "frontend" + "backend" detected)
backend/ # Backend-for-frontend (BFF)
├── src/
│   ├── controllers/     # Kontrolery API do komunikacji z frontendem
│   ├── services/        # Serwisy do obsługi logiki biznesowej i komunikacji z eFitness
│   └── utils/           # Funkcje pomocnicze
└── tests/

frontend/ # Aplikacja SPA (React.js)
├── src/
│   ├── components/      # Komponenty UI
│   ├── pages/           # Główne widoki aplikacji
│   ├── services/        # Serwisy do komunikacji z BFF
│   ├── hooks/           # Customowe hooki Reacta
│   └── App.js
└── tests/
```

**Structure Decision**: Wybrano strukturę z oddzielnym frontendem (aplikacja SPA) i backendem (BFF), aby odizolować logikę integracji z zewnętrznymi API i zwiększyć bezpieczeństwo (np. przez ukrycie kluczy API).

## Phase 0: Outline & Research
1. **Analiza API eFitness**: Dokładne zbadanie dokumentacji API (`https://apimember.efitness.com.pl/docs/index.html`) w celu zidentyfikowania wszystkich potrzebnych endpointów do realizacji wymagań funkcjonalnych, w tym obsługi płatności.
2. **Wybór technologii dla BFF**: Zdecydowanie o technologii dla serwera BFF (np. Node.js z Express.js, .NET Core Web API). Ze względu na hosting na Windows z IIS, .NET Core Web API będzie preferowanym wyborem.

**Output**: `research.md` z wynikami analizy i decyzjami technologicznymi.

## Phase 1: Design & Contracts
*Prerequisites: research.md complete*

1. **Model danych**: Stworzenie pliku `data-model.md` na podstawie kluczowych encji zdefiniowanych w specyfikacji.
2. **Kontrakty API (BFF)**: Zdefiniowanie kontraktu OpenAPI (`openapi.yaml`) dla wewnętrznego API dostarczanego przez BFF. API to będzie pośredniczyć w komunikacji między frontendem a zewnętrznymi systemami.
3. **Scenariusze testowe**: Zdefiniowanie scenariuszy testowych dla `quickstart.md`, które pokryją główne historie użytkownika.

**Output**: `data-model.md`, `contracts/openapi.yaml`, `quickstart.md`.

## Phase 2: Task Planning Approach
*This section describes what the /tasks command will do - DO NOT execute during /plan*

**Task Generation Strategy**:
- Na podstawie `contracts/openapi.yaml` zostaną wygenerowane zadania na implementację endpointów BFF oraz testów kontraktowych.
- Na podstawie `data-model.md` zostaną wygenerowane zadania na stworzenie modeli danych w BFF.
- Na podstawie scenariuszy z `quickstart.md` zostaną wygenerowane zadania na testy integracyjne.
- Dla frontendu zostaną wygenerowane zadania na stworzenie poszczególnych widoków (rejestracja, logowanie, profil, etc.) oraz komponentów UI.

**Ordering Strategy**:
1. Implementacja modeli danych w BFF.
2. Implementacja serwisów w BFF do komunikacji z eFitness.
3. Implementacja endpointów API w BFF.
4. Implementacja testów kontraktowych i integracyjnych dla BFF.
5. Implementacja serwisów we frontendzie do komunikacji z BFF.
6. Implementacja widoków i komponentów we frontendzie.

## Progress Tracking
*This checklist is updated during execution flow*

**Phase Status**:
- [ ] Phase 0: Research complete (/plan command)
- [ ] Phase 1: Design complete (/plan command)
- [ ] Phase 2: Task planning complete (/plan command - describe approach only)
- [ ] Phase 3: Tasks generated (/tasks command)
- [ ] Phase 4: Implementation complete
- [ ] Phase 5: Validation passed

**Gate Status**:
- [x] Initial Constitution Check: PASS
- [ ] Post-Design Constitution Check: PASS
- [ ] All NEEDS CLARIFICATION resolved
- [ ] Complexity deviations documented

---
*Based on Constitution v1.1.0 - See `/memory/constitution.md`*