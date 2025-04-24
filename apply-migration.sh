#!/bin/bash

# Exit script on error
set -e

PROJECT_PATH="src/PersonDirectoryApi/PersonDirectoryApi.csproj"

echo "Applying EF Core migrations..."
dotnet ef database update --project "$PROJECT_PATH"

echo "Migrations applied successfully!"