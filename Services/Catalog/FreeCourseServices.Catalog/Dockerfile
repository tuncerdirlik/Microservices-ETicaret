FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/FreeCourse.Shared/*.csproj Shared/FreeCourse.Shared/
COPY Services/Catalog/FreeCourseServices.Catalog/*.csproj Services/Catalog/FreeCourseServices.Catalog/
RUN dotnet restore Services/Catalog/FreeCourseServices.Catalog/*.csproj
COPY . .
RUN dotnet publish Services/Catalog/FreeCourseServices.Catalog/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "FreeCourseServices.Catalog.dll"]


