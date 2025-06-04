cd ../

dotnet clean
dotnet build
dotnet ef migrations add --context MariaDbContext "Update tables" --no-build
dotnet clean
dotnet build

dotnet ef database update --context MariaDbContext --no-build

exit
