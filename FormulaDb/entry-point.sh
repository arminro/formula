#start SQL Server, start the script to create the DB and import the data, start the app
# the original MS docs has this in the reverse order as noted by https://dotnetthoughts.net/initialize-mssql-in-docker-container/
/import-data.sh & /opt/mssql/bin/sqlservr