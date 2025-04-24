#!/bin/bash

# Exit script on error
set -e

PROJECT_PATH="src/PersonDirectoryApi/PersonDirectoryApi.csproj"

echo "removing EF Core migrations..."
dotnet ef migrations remove --project "$PROJECT_PATH"

echo "Migrations removed successfully!"