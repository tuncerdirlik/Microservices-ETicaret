FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/FreeCourse.Shared/*.csproj Shared/FreeCourse.Shared/
COPY Services/Order/FreeCourseServices.Order.Application/*.csproj Services/Order/FreeCourseServices.Order.Application/
COPY Services/Order/FreeCourseServices.Order.Core/*.csproj Services/Order/FreeCourseServices.Order.Core/
COPY Services/Order/FreeCourseServices.Order.Domain/*.csproj Services/Order/FreeCourseServices.Order.Domain/
COPY Services/Order/FreeCourseServices.Order.Infrastructure/*.csproj Services/Order/FreeCourseServices.Order.Infrastructure/
COPY Services/Order/FreeCourseServices.Order.Api/*.csproj Services/Order/FreeCourseServices.Order.Api/
RUN dotnet restore Services/Order/FreeCourseServices.Order.Api/*.csproj
COPY . .
RUN dotnet publish Services/Order/FreeCourseServices.Order.Api/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "FreeCourseServices.Order.Api.dll"]


