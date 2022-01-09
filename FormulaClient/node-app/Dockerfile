FROM node:14.18.0-stretch as base
EXPOSE 80
CMD ["node", "index.js"]
WORKDIR /app
COPY ["node-app/","."]
RUN npm ci

FROM node:14.18.0-stretch as build
WORKDIR /src
COPY ["./package.json","./package-lock.json","./"]
RUN npm ci
COPY . .
RUN npm run build -- --c production

FROM base as final
COPY --from=build ["/src/dist","/app/dist"]