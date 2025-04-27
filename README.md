## Person Directory system created as TBC Bank technical task for interview process

### Endpoints Definition
* CityController - returns cities to be chosen for person creation process. data is seeded using ef core data seeding, 
but if this project should be separate, cities should be synced from another system.
* MultimediaController - in some PersonController endpoints ImageUrl parameter should be passed, 
PersonController mustn't take IFormFile directly, it must be uploaded separately and url should be passed.

### Running
project can be run using ``` dotnet run``` locally, in this way if you want to change db connection string using ```appsettings.Development.json``` file.

or you can run as docker container. docker compose command below will start both api and postgres database
```bash
docker compose up --build -d 
```

### Database Schema
Project uses ef core to communicate to the database, so database changes are controlled using ef core migrations.
for simplicity of this project, database creation and applying migrations happens while application starts.