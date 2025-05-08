cd ../../

rm ./Migrations/*

dotnet clean
dotnet build
dotnet ef migrations add --context ArticlesDb "Init articles" --no-build
dotnet clean
dotnet build

dotnet ef database update --context ArticlesDb --no-build

exit
