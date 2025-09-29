# Specyfikacja Funkcjonalności: [NAZWA FUNKCJI]

**Gałąź Funkcjonalności**: `[###-nazwa-funkcji]`
**Utworzono**: [DATA]
**Status**: Wersja robocza
**Wejście**: Opis użytkownika: "$ARGUMENTS"

## Proces Wykonania (główny)
```
1. Przetwórz opis użytkownika z Wejścia
   → Jeśli pusty: BŁĄD "Nie podano opisu funkcjonalności"
2. Wyodrębnij kluczowe pojęcia z opisu
   → Zidentyfikuj: aktorów, akcje, dane, ograniczenia
3. Dla każdego niejasnego aspektu:
   → Oznacz jako [WYMAGA WYJAŚNIENIA: konkretne pytanie]
4. Wypełnij sekcję Scenariusze Użytkownika i Testowanie
   → Jeśli brak jasnego przepływu użytkownika: BŁĄD "Nie można określić scenariuszy użytkownika"
5. Wygeneruj Wymagania Funkcjonalne
   → Każde wymaganie musi być testowalne
   → Oznacz niejednoznaczne wymagania
6. Zidentyfikuj Kluczowe Encje (jeśli dotyczą danych)
7. Uruchom Listę Kontrolną Przeglądu
   → Jeśli istnieją [WYMAGA WYJAŚNIENIA]: OSTRZEŻENIE "Specyfikacja zawiera niejasności"
   → Jeśli znaleziono szczegóły implementacyjne: BŁĄD "Usuń szczegóły techniczne"
8. Zwróć: SUKCES (specyfikacja gotowa do planowania)
```

---

## ⚡ Krótkie Wytyczne
- ✅ Skup się na TYM, czego potrzebują użytkownicy i DLACZEGO
- ❌ Unikaj JAK implementować (bez stosu technologicznego, API, struktury kodu)
- 👥 Pisane dla interesariuszy biznesowych, a nie deweloperów

### Wymagania Dotyczące Sekcji
- **Sekcje obowiązkowe**: Muszą być uzupełnione dla każdej funkcjonalności
- **Sekcje opcjonalne**: Dołączaj tylko wtedy, gdy są istotne dla danej funkcjonalności
- Gdy sekcja nie ma zastosowania, usuń ją w całości (nie zostawiaj "N/A")

### Dla Generowania przez AI
Podczas tworzenia tej specyfikacji na podstawie podpowiedzi użytkownika:
1. **Oznacz wszystkie niejednoznaczności**: Użyj [WYMAGA WYJAŚNIENIA: konkretne pytanie] dla każdego założenia, które musiałbyś przyjąć
2. **Nie zgaduj**: Jeśli podpowiedź czegoś nie precyzuje (np. "system logowania" bez metody uwierzytelniania), oznacz to
3. **Myśl jak tester**: Każde niejasne wymaganie powinno oblać punkt kontrolny "testowalne i jednoznaczne"
4. **Częste obszary niedookreślone**:
   - Typy użytkowników i uprawnienia
   - Polityki przechowywania/usuwania danych
   - Cele wydajnościowe i skala
   - Zachowania w przypadku błędów
   - Wymagania integracyjne
   - Potrzeby związane z bezpieczeństwem/zgodnością

---

## Scenariusze Użytkownika i Testowanie *(obowiązkowe)*

### Główna Historia Użytkownika
[Opisz główną podróż użytkownika prostym językiem]

### Scenariusze Akceptacyjne
1. **Mając** [stan początkowy], **Kiedy** [akcja], **Wtedy** [oczekiwany wynik]
2. **Mając** [stan początkowy], **Kiedy** [akcja], **Wtedy** [oczekiwany wynik]

### Przypadki Brzegowe
- Co się stanie, gdy [warunek brzegowy]?
- Jak system obsługuje [scenariusz błędu]?

## Wymagania *(obowiązkowe)*

### Wymagania Funkcjonalne
- **WF-001**: System MUSI [określona zdolność, np. "pozwalać użytkownikom na tworzenie kont"]
- **WF-002**: System MUSI [określona zdolność, np. "walidować adresy e-mail"]
- **WF-003**: Użytkownicy MUSZĄ mieć możliwość [kluczowa interakcja, np. "resetowania swojego hasła"]
- **WF-004**: System MUSI [wymaganie dotyczące danych, np. "przechowywać preferencje użytkownika"]
- **WF-005**: System MUSI [zachowanie, np. "logować wszystkie zdarzenia związane z bezpieczeństwem"]

*Przykład oznaczania niejasnych wymagań:*
- **WF-006**: System MUSI uwierzytelniać użytkowników za pomocą [WYMAGA WYJAŚNIENIA: nie określono metody uwierzytelniania - e-mail/hasło, SSO, OAuth?]
- **WF-007**: System MUSI przechowywać dane użytkownika przez [WYMAGA WYJAŚNIENIA: nie określono okresu przechowywania]

### Kluczowe Encje *(dołącz, jeśli funkcjonalność dotyczy danych)*
- **[Encja 1]**: [Co reprezentuje, kluczowe atrybuty bez implementacji]
- **[Encja 2]**: [Co reprezentuje, relacje z innymi encjami]

---

## Lista Kontrolna Przeglądu i Akceptacji
*BRAMKA: Zautomatyzowane kontrole uruchamiane podczas wykonania main()*

### Jakość Treści
- [ ] Brak szczegółów implementacyjnych (języki, frameworki, API)
- [ ] Skupienie na wartości dla użytkownika i potrzebach biznesowych
- [ ] Pisane dla nietechnicznych interesariuszy
- [ ] Wszystkie obowiązkowe sekcje uzupełnione

### Kompletność Wymagań
- [ ] Nie pozostały żadne znaczniki [WYMAGA WYJAŚNIENIA]
- [ ] Wymagania są testowalne i jednoznaczne
- [ ] Kryteria sukcesu są mierzalne
- [ ] Zakres jest jasno określony
- [ ] Zidentyfikowano zależności i założenia

---

## Status Wykonania
*Aktualizowane przez main() podczas przetwarzania*

- [ ] Opis użytkownika przetworzony
- [ ] Wyodrębniono kluczowe pojęcia
- [ ] Oznaczono niejednoznaczności
- [ ] Zdefiniowano scenariusze użytkownika
- [ ] Wygenerowano wymagania
- [ ] Zidentyfikowano encje
- [ ] Lista kontrolna przeglądu zaliczona

---