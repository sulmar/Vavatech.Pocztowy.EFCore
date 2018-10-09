
# Przyk�ady ze szkolenia EF Core 2.1

## Migracje

* wygenerowane skryptu kompletnej bazy danych
~~~ powershell
Script-Migration 
~~~


* wygenerowanie skryptu od podanej migracji
~~~ powershell
Script-Migration AddProductBarcode
~~~

* wygenerowanie skryptu pomi�dzy migracjami
~~~ powershell
Script-Migration -from AddProductBarcode -to InitialCreate
~~~

