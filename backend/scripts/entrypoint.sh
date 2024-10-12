#!/bin/bash

check_db_connection() {
    echo "Verificando a conexão com o banco de dados..."

    until dotnet CSharpBackend.dll; do
        echo "Banco de dados ainda não está disponível. Tentando novamente em 5 segundos..."
        sleep 5
    done

    echo "Conexão com o banco de dados estabelecida!"
}

check_db_connection

exec dotnet CSharpBackend.dll
