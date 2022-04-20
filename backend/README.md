1. Make a copy of appsettings.json.backup without the ".backup"
2. Enter the relevant values into that file

#Seeding the database:
**With make installed:** run `make seed` in the terminal

**Without make installed:** 
1. run `dotnet ef database drop`
2. run `dotnet ef database update`
3. run `dotnet run seed`

**Visual studio assuming no extra tools are installed**
1. Start without a .db file. If you have one just delete it
2. search for "package manager console" and open it
3. run `Update-Database` in the pacman console
4. once that is done open Tools -> Command Line -> Developer Console
5. run `dotnet run seed` and wait for it to finish.
6. Close console and run the backend the way you would normally