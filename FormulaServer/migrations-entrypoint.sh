# from https://docs.docker.com/samples/aspnet-mssql-compose/

until dotnet ef database update; do
>&2 echo "SQL Server is starting up"
sleep 1
done

>&2 echo "SQL Server is up - executing command"