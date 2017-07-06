This repository contains 2 main .NET applications and their corresponding libraries. I have made them available onto Microsoft Azure http://topfivewebsites.azurewebsites.net/ as app service (web app) and web job (scheduled job). The database used is MS SQL Server and hosted in Azure SQL service.

Azure app serivce details:
+ App service name: TopFiveWebsites
+ Web app url: http://topfivewebsites.azurewebsites.net/
+ Test user credential: dickens.code@gmail.com / Abcd1234!
+ SQL server: topfivewebsitesdbserver.database.windows.net
+ SQL database: TopFiveWebsites

Azure web job details:
+ Web job name: TopFiveWebsites-DataImporter
+ Running schedule: every 30 seconds

`data.csv` file drop details:
+ Ftp site: ftp://waws-prod-hk1-023.ftp.azurewebsites.windows.net
+ File location: /data/csv/data.csv
+ Upload credential: dickens / Abcd1234!

Here is the high level architecture:
+ `Manulife.TopFiveWebsites.Web` - A ASP.net MVC5 application presumably hosted in MS IIS or Azure app service. It contains all the layout/web pages ready to be browsed and consumed by users. This is the main entry point for application users.
+ `Manulife.TopFiveWebsites.DataImporter` - A .NET console app possibly triggered by Windows Scheduler or hosted in Azure Web Job with CRON schedule definition. It scans the configured data folder for `data.csv` file every 30 seconds, parse it with CsvHelper library and persist them into data store
+ `Manulife.TopFiveWebsites.Service` - A business logic layer to consolidate input from users and manipulate data store entities in order to generate useful output. The `SearchService` aggregates website visit log results and handle data grid operations. The `VisitLogService` manipulates visit logs and exclusion entries. The `LoginService` handles user login and logout actions.
+ `Manulife.TopFiveWebsites.Repository` - A data access layer to communicate with data store (via ORM Entity Framework) or to retrieve Restful resource (ie. exclusion list)
+ `Manulife.TopFiveWebsites.Entity` - ORM layer to map db models to object models
+ `Manulife.TopFiveWebsites.Service.Test`, `Manulife.TopFiveWebsites.Repository.Test` - Unit test projects with MS unit test framework and Moq framework

Feature highlights:
+ Responsive grid technology `datatables.net` - feature-rich grid presentation and large community support
+ Cloud service (Azure) deployment - easily scalable and re-deployable
+ Authentication - Forms authentication is implemented
+ Dependency injection - Ninject is used as IoC container, facilitating unit testing where dependent objects can be mocked up to control area of code being analysed
+ Dependency inversion
+ ORM layer - Entity Framework is used

Deployment instructions
1. git clone the repository
1. open solution in VS 2015/2017 and compile
1. publish web app project `Manulife.TopFiveWebsites.Web` as a web deploy package
1. in Windows Server 2012 R2, set up an empty web application with `Anonymous Authentication=true`, `Forms Authentication=true`, `Windows Authentication=false`
1. xcopy web deploy package to web application folder
1. xcopy `Manulife.TopFiveWebsites.DataImporter` build output (\bin\Release) to system `Program Files` folder
1. in Windows Scheduler, set up a recurring task running at every 30 seconds, and execute `Manulife.TopFiveWebsites.DataImporter.exe` at `Manulife.TopFiveWebsites.DataImporter` folder
1. in (same) Windows Server 2012 R2, install MS SQL Server 2012 Standard SP3
1. Execute solution\Misc\TopFiveWebsites.20170707.sql to create database `TopFiveWebsites` with preset test user credentials

Assumptions:
+ Visit logs are culumative. Each `data.csv` file represents a set of valid visit log from external source and should be added to total visit counts. Therefore, even there are 2 entries in separate `data.csv` file with same website name and date, the engine will sum them up into total count.
+ After `data.csv` is consumed by `DataImporter`, the file is deleted.

Futher improvements:
+ To store and manipulate log records efficiently, it makes sense to store them into NoSQL data store eg. `Elasticsearch` which provides auto record versioning (in case records in `data.csv` may be treated as "versions") and improved search speed (even at large data volume)
+ To improve scalability and extensibility, several features can be implemented via mircoservice strategy/components. For example, scheduler can be implemented via Mesos to boost better CPU and memory management. Another area is that we can leverage NoSQL data store and build a small aggregation engine to search and consolidate visit logs, then the web app can be reduced to a thin presentation layer of the results. If, however, we go onto micro service direction, authentication can be fairly complex and we may have to seek a good way to do SSO.
+ To remove dependency on MS IIS+ASP.NET, forms authentication should be switched to Owin cookie authentication middleware. However, the current implementation of Owin depends on Entity Framework ORM modules which would take me some time to refactor with.
