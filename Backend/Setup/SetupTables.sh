cd ../

rm ./Migrations/*

dotnet clean
dotnet build
dotnet ef migrations add --context MariaDbContext "Init tables" --no-build
dotnet clean
dotnet build

dotnet ef database update --context MariaDbContext --no-build

exit
