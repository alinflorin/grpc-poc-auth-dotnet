﻿FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder
WORKDIR /app
COPY ./Auth/Auth.csproj ./Auth/Auth.csproj
COPY ./Auth.sln ./Auth.sln
RUN dotnet restore
COPY . .
RUN dotnet build -c Release --no-restore ./Auth/Auth.csproj
RUN dotnet publish --no-build -c Release -o /out ./Auth/Auth.csproj

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf
WORKDIR /app
COPY --from= builder /out .
ENV ASPNETCORE_URLS=https://0.0.0.0:5001
ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 5001
CMD ["dotnet", "./Auth.dll"]