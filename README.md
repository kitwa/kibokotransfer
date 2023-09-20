# Create a new app
 1. Remove migrations folder in Data folder
 2. Move to API folder and Run 'dotnet ef migrations add PostgresInitial -o Data/Migrations' 
 3. Run 'dotnet ef database update'

 
### CREATE MIGRATIONS
dotnet ef migrations add InitialCreate -o Data/Migrations
dotnet ef migrations add AddMorefieldsUsers
dotnet ef migrations remove  ##remove previous migration

#### To delete db and recreate with all migrations
dotnet ef database drop
dotnet ef database update

### UPDATE DATABASE
dotnet ef database update
