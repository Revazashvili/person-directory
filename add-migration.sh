#!/bin/bash

# Exit script on error
set -e

PROJECT_PATH="src/PersonDirectoryApi/PersonDirectoryApi.csproj"

echo "adding EF Core migrations..."
dotnet ef migrations add "$1" --project "$PROJECT_PATH" -o "Persistence/Migrations"

echo "Migrations added successfully!"