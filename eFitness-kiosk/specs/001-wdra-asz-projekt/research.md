# Research: Autonomiczne Kluby Fitness eFitness-Kiosk

## 1. Analiza API eFitness

**Dokumentacja**: `https://apimember.efitness.com.pl/docs/index.html`

**Kluczowe Endpointsy do zbadania**:
- **Uwierzytelnianie**: Proces logowania i autoryzacji, odświeżanie tokenów.
- **Konta użytkowników**: Tworzenie, odczyt, aktualizacja danych profilowych.
- **Karnety**: Pobieranie listy dostępnych karnetów, pobieranie szczegółów karnetu, zakup karnetu.
- **Płatności**: Inicjowanie płatności, pobieranie statusu płatności, historii opłat, obsługa rat. Należy zbadać, w jaki sposób API eFitness obsługuje różnych dostawców płatności (Espago, PayU) i jak aplikacja kliencka powinna prezentować te opcje użytkownikowi.
- **Zgody**: Zarządzanie zgodami RODO.

**Decyzja**: API eFitness jest centralnym punktem integracji. Należy dokładnie zmapować wszystkie wymagane operacje na dostępne endpointy API. Kluczowe jest zrozumienie, jak eFitness zarządza sesją płatności i przekierowaniami do zewnętrznych bramek płatniczych.

## 2. Wybór technologii dla BFF

**Opcje**:
1.  **.NET Core Web API**: Dobrze integruje się z IIS na Windows, wysoka wydajność, silne typowanie (C#).
2.  **Node.js z Express.js**: Szybki rozwój, duża społeczność, ale może wymagać dodatkowej konfiguracji (np. iisnode) do działania na IIS.

**Decyzja**: Wybieramy **.NET Core Web API**. Jest to naturalny wybór dla środowiska Windows/IIS, co ułatwi wdrożenie i utrzymanie. Zapewnia również solidne podstawy pod kątem bezpieczeństwa i wydajności.