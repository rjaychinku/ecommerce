#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base

# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_12.x | bash - && \
    apt-get install -y build-essential nodejs
# End setup

WORKDIR /app
EXPOSE 80
EXPOSE 443


FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build

# BEGIN MODIFICATION - Node is needed for development (but not production)
RUN curl -sL https://deb.nodesource.com/setup_12.x | bash -
RUN apt-get install --assume-yes nodejs
RUN npm install @angular/cli -g
# END MODIFICATION
WORKDIR /src
COPY ["BuyABit/BuyABit.csproj", "BuyABit/"]
RUN dotnet restore "BuyABit/BuyABit.csproj"
COPY . .
WORKDIR "/src/BuyABit"
RUN dotnet build "BuyABit.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BuyABit.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BuyABit.dll"]
