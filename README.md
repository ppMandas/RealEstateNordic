# RealEstateNordic
This is the solution of the coding task provided by Danske Bank

## Design decisions
The application has been created as an API, due to easy build and run of the application, without
the need of external services either installed or dockerized.

An In-Memory database is being utilized, as to eliminate the necessity of setting up and connecting
to a real DB instance. A csv import example file has been provided, this file can also be used to 
initially fill the In-Memory database with entries for testing.

The application has been structured with 3 layers: Controller/Web, Service and Repository, as to
have a clear separation of functionality between the different layers.

EntityFramework has been used to simplify database interaction, Automapper has been used to simplify
transformation of object types between layers and CsvHelper has been used provide the functionality
to parse CSV files for import of DB records.

## Application functionality
The application has been created using Visual Studio 2022 and .NET SDK 8, it is therefore recommended
to open the solution file and run the application from Visual Studio.

The application has a profile "http" in the "launchSettings.json" file, running the application using
this profile will start the application using url: http://localhost:5178

The application provides 3 endpoints with following routes and query parameters:
- Functionality: Get tax by municipality and date
	- Method: GET
	- Route: api/municipalities/tax, 
	- QueryParameters: municipality (String), date (DateOnly)
	- Example: http://localhost:5178/api/municipalities/tax?municipality=copenhagen&date=2016.01.01
- Functionality: Insert new tax record for municipality
	- Method: POST
	- Route: api/municipalities/tax
	- QueryParameters: municipality (String), date (DateOnly), taxType (Enum: daily, weekly, monthly, yearly), tax (double)
	- Example: http://localhost:5178/api/municipalities/tax?municipality=copenhagen&date=2016.01.01&taxType=yearly&tax=0.1
- Functionality: Import CSV file (Postman recommended)
	- Method: POST
	- Route: api/municipalities/tax/csv
	- Headers:
		- Content-Type: multipart/form-data
	- Body: form-data, Key: any key, Value: *.csv file