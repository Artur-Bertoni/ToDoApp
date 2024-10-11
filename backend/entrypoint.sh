#!/bin/bash

if [ $(dotnet ef migrations list | wc -l) -eq 0 ]; then
  dotnet ef migrations add InitialCreate
fi

dotnet ef database update

dotnet CSharpBackend.dll
