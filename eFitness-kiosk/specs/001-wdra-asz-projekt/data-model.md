# Model Danych: Autonomiczne Kluby Fitness eFitness-Kiosk

Na podstawie sekcji "Kluczowe Encje" w specyfikacji funkcjonalności, definiujemy następujący model danych. Należy pamiętać, że głównym źródłem danych jest system eFitness, a ten model reprezentuje obiekty, z którymi aplikacja "eFitness-Kiosk" będzie pracować.

## Encje

### Użytkownik
- **Opis**: Reprezentuje klienta klubu fitness.
- **Atrybuty**:
    - `id`: unikalny identyfikator z systemu eFitness
    - `imie`: Imię użytkownika
    - `nazwisko`: Nazwisko użytkownika
    - `email`: Adres e-mail (służy jako login)
    - `haslo`: (tylko do zapisu, nigdy do odczytu)
    - `dane_osobowe`: Obiekt z dodatkowymi danymi (adres, telefon, etc.)
    - `zgody_rodo`: Lista udzielonych zgód

### Karnet
- **Opis**: Reprezentuje subskrypcję na usługi klubu.
- **Atrybuty**:
    - `id`: unikalny identyfikator z systemu eFitness
    - `nazwa`: Nazwa karnetu
    - `cena`: Cena karnetu
    - `okres_waznosci`: Okres ważności (np. 30 dni, 3 miesiące)
    - `status`: Status karnetu (aktywny, nieaktywny, zawieszony)

### Płatność
- **Opis**: Reprezentuje transakcję finansową.
- **Atrybuty**:
    - `id`: unikalny identyfikator z systemu eFitness lub PayU
    - `kwota`: Kwota płatności
    - `data`: Data transakcji
    - `status`: Status płatności (nowa, w trakcie, zakończona sukcesem, zakończona błędem)
    - `metoda_platnosci`: Wybrana metoda płatności (np. karta, przelew)
    - `id_karnetu`: ID karnetu, którego dotyczy płatność
