FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as builder
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . .
RUN dotnet build -c Release --no-restore
RUN dotnet publish --no-build -c Release -o /out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf
WORKDIR /app
COPY --from= builder /out .
ENV ASPNETCORE_URLS=https://0.0.0.0:5001
EXPOSE 5001
CMD["dotnet", "./Auth.dll"]