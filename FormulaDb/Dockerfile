# based on: https://dotnetthoughts.net/initialize-mssql-in-docker-container/

FROM mcr.microsoft.com/mssql/server:2017-latest

USER root

COPY setup.sql setup.sql
COPY import-data.sh import-data.sh
COPY entry-point.sh entry-point.sh

RUN chmod +x entry-point.sh

CMD /bin/bash ./entry-point.sh