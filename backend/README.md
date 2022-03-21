1. Make a copy of appsettings.json.backup without the ".backup"
2. Enter the relevant values into that file

#Seeding the database:
**With make installed:** run `make seed` in the terminal

**Without make installed:** 
1. run `dotnet ef database drop`
2. run `dotnet ef database update`
3. run `dotnet run seed`
