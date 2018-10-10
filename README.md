
# Przykłady ze szkolenia EF Core 2.1

## Migracje

* wygenerowane skryptu kompletnej bazy danych
~~~ powershell
Script-Migration 
~~~


* wygenerowanie skryptu od podanej migracji
~~~ powershell
Script-Migration AddProductBarcode
~~~

* wygenerowanie skryptu pomiędzy migracjami
~~~ powershell
Script-Migration -from AddProductBarcode -to InitialCreate
~~~

## Zarządzanie stanem obiektu

Pytanie: _Mamy na przykład sesję w banku. W jaki sposób obsługiwać to w REST API?_

Sesję powinniśmy potraktować jako zasób.

* bieżąca sesja
~~~
GET /api/session/current
~~~

* historyczne sesje
~~~
GET /api/session/20181010
GET /api/session/20181009
~~~

* zamknięcie sesji
~~~
DELETE /api/session/current - zamyka sesję
~~~

* przywrócenie sesji
~~~
PUT /api/session/current - przywraca sesję
~~~

* utworzenie nowej sesji
~~~
POST /api/session/current - tworzy nową sesję
~~~


