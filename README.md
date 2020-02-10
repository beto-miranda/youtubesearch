# youtubesearch

dotnet ef migrations add -c YoutubeSearch.Web.Data.IdentityDbContext -o Data/Migrations/IdentityDatabase Initial

dotnet ef migrations add -c YoutubeSearch.Persistence.ApplicationDbContext -o Data/Migrations/ApplicationDatabase Initial


dotnet ef database update -c YoutubeSearch.Web.Data.IdentityDbContext 

dotnet ef database update -c YoutubeSearch.Persistence.ApplicationDbContext

