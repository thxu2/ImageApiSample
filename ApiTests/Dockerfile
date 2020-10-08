FROM mcr.microsoft.com/dotnet/core/sdk:3.1-alpine AS build
WORKDIR /app
COPY . ./
WORKDIR /app/ApiTests
RUN dotnet test

