cd ../../

rm ./Migrations/*

dotnet clean
dotnet build
dotnet ef migrations add --context ForumDb "Init forum" --no-build
dotnet clean
dotnet build

dotnet ef database update --context ForumDb --no-build

exit
