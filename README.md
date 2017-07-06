This repository contains 2 main .NET applications and their corresponding libraries. I have made them available onto Microsoft Azure http://topfivewebsites.azurewebsites.net/ as app service (web app) and web job (scheduled job). The database used is MS SQL Server and hosted in Azure SQL service.

Here are the high level system architecture:
+ **Manulife.TopFiveWebsites.Web** - A ASP.net MVC5 application presumably hosted in MS IIS or Azure app service. It contains all the layout/web pages ready to be browsed and consumed by users. This is the main entry point for application users.
+ **Manulife.TopFiveWebsites.DataImporter** - A .NET console app possibly triggered by Windows Scheduler or hosted in Azure Web Job with CRON schedule definition. It scan the configured data folder for data.csv file, parse it with CsvHelper library and persist them into data store
+ **Manulife.TopFiveWebsites.Service** - A business logic layer to consolidate input from users and manipulate data store entities in order to generate useful output. The `SearchService` aggregates website visit log results and handle data grid operations. The `VisitLogService` manipulates visit logs and exclusion entries. The `LoginService` handles user login and logout actions.

Feature highlights:
+ Responsive grid technology `datatables.net`
+ Dependency injection
+ Dependency inversion
+ Cloud service (Azure) deployment
+ ORM layer Entity Framework

Deployment instructions
1. git clone the repository
1. open solution in VS 2015/2017 and compile
1. 

Assumptions:
+ Visit logs are culumative. Each `data.csv` file represents a set of valid visit log from external source and should be added to total visit counts. Therefore, even there are 2 entries in separate `data.csv` file with same website name and date, the engine will sum them up into total count.

Futher improvements:
+ To store and manipulate log records efficiently, it makes sense to store them into NoSql data store eg. `Elasticsearch` which provides auto record versioning (in case records in `data.csv` may be treated as "versions") and improved search speed (even at large data volume)
