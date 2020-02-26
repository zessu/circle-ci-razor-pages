FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /app
COPY ./*.csproj .
RUN dotnet restore

COPY . .
RUN mkdir out
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS runtime
LABEL author="zessu"
WORKDIR /app
COPY --from=build /app/out .
EXPOSE 80
ENTRYPOINT ["dotnet", "circle-ci-asp-net-razor-pages.dll"]
