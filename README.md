# spec-kit explore

## case #1
### eFiness-Kiosk

#### constitution

/constitution Create principles focused on code quality, testing standards, user experience consistency, performance requirements, and hardware reusage and simple straight forward solution. Use Polish langage for all specifications.

#### specify

/specify Wdrażasz projekt autonomicznych klubów fitness, w których użytkownik będzie mógł załatwić wszystkie sprawy organizacyjne związane z wejściem do klubu pod nieobecność pracowników w aplikacji "eFiness-Kiosk" serwowanej przez sprzętowe terminale typu kiosk (ekran dotykowy). Twoje Kluby używają systemu eFitness do swojej operacyjnej działalności. Nowi użytkownicy poprzez aplikację na terminalu będą mogli w systemie eFitness utworzyć konto, zalogawać się, zaktualizować dane osobe w zalogowanym profilu, wybrać metodę płatności, wybrać i opłacić jednen z dostępnych karnetów, sprawdzić status opłat za swoje karnety, dokonac opłat za wybrane raty karnetu oraz udzielić wszystkich oczekiwanych przez system zgód RODO. W każdym klubie jest zainstalowany serwer ACS (Access Control Server) z systemem operacyjnym Windows który może służyć do serwowania aplikacji "eFiness-Kiosk" na terminalach. Zaprojektuj aplikację "eFiness-Kiosk" oraz całą architekturę rozwiązania. 
Wykorzystaj dostępne API systemu eFitness: https://apimember.efitness.com.pl/docs/index.html

#### clarify

/clarify W przypadku utraty połączenia z internetem użytkownik musi zostać wylogowany. Cała komunikacja związana z płatnościami jest zapisywana do logu. Użytkownik ma dostęp do logu. W przypadku problemów z realizacją danej transakcji, użytkownik może zawsze przerwać jej realizację i wykonać ją ponownie. Nie można zapłacić 2 razy tej samej raty. W przypadku bezczynności przez dłużej niż 1min. nastąpuje wykologowanie użytkownika. Jeśli użytkownik przerwie proces rejestracji i nie zatwierdzi formularza rejestracji, to aplikacji czyści dane w formularzu i powraca do okna umożliwiającego zalogowanie się lub utworzenie nowego konta.


#### Review & Acceptance Checklist

 Read the review and acceptance checklist, and check off each item in the checklist if the feature spec meets the criteria. Leave it empty if it does not.

#### plan

/plan

#### clarify 2

/clarify Obsługa płatności jest realizowana poprzez backend efitness. Można w nim wybrać pomiędzy dostawcami usług płatności: Espago oraz PayU. Espago obsługuje karty płatnicze, a szybkie przelewy są obsługiwane przez PayU.

#### tasks 

/tasks


#### implement

/implement


