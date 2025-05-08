cd ../../

rm ./Migrations/*

dotnet clean
dotnet build
dotnet ef migrations add --context IdentityDb "Init Identity" --no-build
dotnet clean
dotnet build

dotnet ef database update --context IdentityDb --no-build

exit
