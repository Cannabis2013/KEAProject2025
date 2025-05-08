cd ../../

rm ./Migrations/*

dotnet clean
dotnet build
dotnet ef migrations add --context MembersDb "Init members" --no-build
dotnet clean
dotnet build

dotnet ef database update --context MembersDb --no-build

exit
