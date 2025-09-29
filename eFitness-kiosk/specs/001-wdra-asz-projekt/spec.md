# Specyfikacja Funkcjonalności: Autonomiczne Kluby Fitness eFitness-Kiosk

**Gałąź Funkcjonalności**: `001-wdra-asz-projekt`
**Utworzono**: 2025-09-26
**Status**: Wersja robocza
**Wejście**: Opis użytkownika: "Wdrażasz projekt autonomicznych klubów fitness, w których użytkownik będzie mógł załatwić wszystkie sprawy organizacyjne związane z wejściem do klubu pod nieobecność pracowników w aplikacji eFiness-Kiosk serwowanej przez sprzętowe terminale typu kiosk (ekran dotykowy). Twoje Kluby używają systemu eFitness do swojej operacyjnej działalności. Nowi użytkownicy poprzez aplikację na terminalu będą mogli w systemie eFitness utworzyć konto, zalogawać się, zaktualizować dane osobe w zalogowanym profilu, wybrać metodę płatności, wybrać i opłacić jednen z dostępnych karnetów, sprawdzić status opłat za swoje karnety, dokonac opłat za wybrane raty karnetu oraz udzielić wszystkich oczekiwanych przez system zgód RODO. W każdym klubie jest zainstalowany serwer ACS (Access Control Server) z systemem operacyjnym Windows który może służyć do serwowania aplikacji eFiness-Kiosk na terminalach. Zaprojektuj aplikację eFiness-Kiosk oraz całą architekturę rozwiązania. Wykorzystaj dostępne API systemu eFitness: https://apimember.efitness.com.pl/docs/index.html"

## Wyjaśnienia

### Sesja 2025-09-26
- P: W jaki sposób użytkownik ma dostęp do logu z komunikacją o płatnościach? → O: W aplikacji jest dostępna sekcja z historią transakcji i logami
- P: W jakiej technologii powinna być zaimplementowana aplikacja "eFitness-Kiosk" na terminalu? → O: SPA
- P: Jaki serwer webowy będzie używany do hostowania aplikacji SPA na serwerze ACS? → O: IIS (Internet Information Services)
- P: Jaki framework zostanie użyty do stworzenia aplikacji SPA? → O: React.js

### Sesja 2025-09-26 (2)
- P: Czy dobrze rozumiem, że aplikacja "eFitness-Kiosk" (poprzez BFF) powinna integrować się tylko z API eFitness do obsługi płatności, a eFitness zajmie się dalszą komunikacją z PayU lub Espago? → O: Tak
- P: Jak aplikacja powinna prezentować użytkownikowi wybór metody płatności? → O: Jedna lista rozwijana z opcjami "Karta płatnicza" i "Szybki przelew"

## Scenariusze Użytkownika i Testowanie *(obowiązkowe)*

### Główna Historia Użytkownika
Jako nowy użytkownik klubu fitness, chcę móc samodzielnie zarejestrować się, zarządzać moim kontem i karnetami oraz dokonywać płatności za pomocą aplikacji "eFitness-Kiosk" na terminalu dotykowym, bez potrzeby interakcji z personelem klubu.

### Scenariusze Akceptacyjne
1. **Mając** status nowego użytkownika, **Kiedy** podchodzę do terminala "eFitness-Kiosk", **Wtedy** mogę utworzyć nowe konto w systemie eFitness.
2. **Mając** utworzone konto, **Kiedy** loguję się do aplikacji, **Wtedy** mogę zaktualizować swoje dane osobowe.
3. **Mając** zalogowane konto, **Kiedy** wybieram opcję zakupu karnetu, **Wtedy** mogę wybrać jeden z dostępnych karnetów i metodę płatności.
4. **Mając** wybrany karnet i metodę płatności, **Kiedy** dokonuję płatności, **Wtedy** otrzymuję potwierdzenie zakupu, a mój karnet jest aktywny.
5. **Mając** aktywny karnet, **Kiedy** sprawdzam status moich płatności, **Wtedy** widzę historię opłat i nadchodzące raty.
6. **Mając** zaległą ratę, **Kiedy** wybieram opcję płatności, **Wtedy** mogę opłacić zaległość.
7. **Mając** nowe konto, **Kiedy** system prosi o zgody RODO, **Wtedy** mogę je zaakceptować.

### Przypadki Brzegowe
- W przypadku utraty połączenia z internetem, użytkownik jest automatycznie wylogowywany.
- W przypadku problemów z płatnością, użytkownik może przerwać transakcję i spróbować ponownie.
- W przypadku przerwania rejestracji, formularz jest czyszczony, a aplikacja wraca do ekranu logowania/rejestracji.

## Wymagania *(obowiązkowe)*

### Wymagania Funkcjonalne
- **WF-001**: System MUSI pozwalać na tworzenie konta użytkownika w systemie eFitness.
- **WF-002**: System MUSI pozwalać na logowanie do istniejącego konta.
- **WF-003**: Użytkownicy MUSZĄ mieć możliwość aktualizacji swoich danych osobowych.
- **WF-004**: System MUSI prezentować listę dostępnych karnetów.
- **WF-005**: System MUSI pozwalać na wybór i zakup karnetu.
- **WF-006**: System MUSI integrować się z API eFitness w celu obsługi transakcji płatniczych.
- **WF-007**: System MUSI wyświetlać status płatności za karnety.
- **WF-008**: System MUSI pozwalać na opłacanie rat za karnet.
- **WF-009**: System MUSI obsługiwać zgody RODO.
- **WF-010**: Aplikacja "eFitness-Kiosk" MUSI być serwowana przez serwer IIS (Internet Information Services) na serwerze ACS z systemem Windows.
- **WF-011**: Aplikacja MUSI integrować się z API eFitness: `https://apimember.efitness.com.pl/docs/index.html`.
- **WF-012**: System MUSI logować całą komunikację związaną z płatnościami.
- **WF-013**: W aplikacji MUSI być dostępna sekcja z historią transakcji i logami komunikacji o płatnościach.
- **WF-014**: System MUSI uniemożliwić podwójną opłatę tej samej raty.
- **WF-015**: System MUSI automatycznie wylogować użytkownika po 1 minucie bezczynności.
- **WF-016**: Aplikacja "eFitness-Kiosk" MUSI być zaimplementowana jako aplikacja internetowa typu Single Page Application (SPA) z wykorzystaniem frameworka React.js.
- **WF-017**: Aplikacja MUSI prezentować wybór metody płatności jako listę rozwijaną z opcjami "Karta płatnicza" i "Szybki przelew".

### Kluczowe Encje *(dołącz, jeśli funkcjonalność dotyczy danych)*
- **Użytkownik**: Reprezentuje klienta klubu fitness. Atrybuty: imię, nazwisko, e-mail, hasło, dane osobowe, zgody RODO.
- **Karnet**: Reprezentuje subskrypcję na usługi klubu. Atrybuty: nazwa, cena, okres ważności, status.
- **Płatność**: Reprezentuje transakcję finansową. Atrybuty: kwota, data, status, metoda płatności.

---

## Lista Kontrolna Przeglądu i Akceptacji
*BRAMKA: Zautomatyzowane kontrole uruchamiane podczas wykonania main()*

### Jakość Treści
- [ ] Brak szczegółów implementacyjnych (języki, frameworki, API)
- [x] Skupienie na wartości dla użytkownika i potrzebach biznesowych
- [x] Pisane dla nietechnicznych interesariuszy
- [x] Wszystkie obowiązkowe sekcje uzupełnione

### Kompletność Wymagań
- [x] Nie pozostały żadne znaczniki [WYMAGA WYJAŚNIENIA]
- [x] Wymagania są testowalne i jednoznaczne
- [x] Kryteria sukcesu są mierzalne
- [x] Zakres jest jasno określony
- [x] Zidentyfikowano zależności i założenia
