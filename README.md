# Web API Details

* Web API has been developed in Dot net 6.
* Test projects are created in XUnit framework using Moq.
* Swagger API service has been added to view and test all the endpoints exposed by webapi.
* Implemented centralized logging using Serilog and error handling.

# Setup Web API Application

* Run the attached db script DataBaseScript.sql in your available MS SQL server. It contains scripts to create the required tables and load test data.
* Change the connection string of webapi in appsettings.json file.
* Build the application and run.