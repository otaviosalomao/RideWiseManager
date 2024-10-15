# Use a imagem oficial do .NET SDK 8.0
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Defina o diretório de trabalho no contêiner
WORKDIR /app

EXPOSE 5000
EXPOSE 5001

# Copie os arquivos de projeto e restaure as dependências
COPY *.sln ./
COPY RideWise.Api/*.csproj RideWise.Api/
COPY RideWise.Common/*.csproj RideWise.Common/
COPY RideWise.UnitTest/*.csproj RideWise.UnitTest/
COPY RideWise.IntegrationTest/*.csproj RideWise.IntegrationTest/
COPY RideWise.Notification/*.csproj RideWise.Notification/
RUN dotnet restore

# Copie o restante dos arquivos do projeto
COPY . ./

# Compile o projeto
RUN dotnet publish -c Release -o out

# Copie o restante dos arquivos do projeto (se necessário)
COPY . ./

# Use a imagem oficial do .NET Runtime 8.0 para a fase de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime

# Defina o diretório de trabalho no contêiner
WORKDIR /app

# Copie os arquivos compilados da fase de build
COPY --from=build /app/out .

# Defina o comando de entrada para o contêiner
#CMD ["dotnet", "RideWise.Api.dll"]
