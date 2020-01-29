# FlightFinder

It is a .NET Core Web Application, based on SkyScanner API. 
As a user you can get valid information about flights by the added criteria.
(The Login and Register buttons works only with your own database, but you can find the sql queries down below -> ".sql" files. Just add them in your database to the Stored Procedures folder)

# Built with
* .NET Core Web Api
* Restsharp for API requests
* Bootstrap
* JavaScript
* MS SQL

# How to use?
On the main page after you entering your departure and arrival countries, you should choose an airport from the dopdown list.
The next step is to select the inbound and outbound date of your journey, then select which cabin class you would like to travel to and how many people you will travel with. After you enter the parameters, you will get up to date information of the flights:
*agency
*date and time of the flight
*name of the departure and arrival airport
*price

Each flight has a purchause button which renders the user to the real booking page.
