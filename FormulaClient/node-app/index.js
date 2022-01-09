var express = require('express');
var server = express();
var options = {
  index: false
};
const PORT = process.env.PORT || 80;

server.get(`/assets/config/config.${process.env}.json`, (req, res) => {
  const data = {
    apiBaseUrl: process.env.apiServer.baseUrl
  };

  res.json(data);
});

server.use('/', express.static('./dist/Portfolio', options));

server.get('*', (req, res) => {
  res.sendFile('dist/Portfolio/index.html', { root: __dirname });
});

server.listen(PORT, () => {
  console.log(`Server is listening on port ${PORT}`);
});
