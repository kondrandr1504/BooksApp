import React from 'react'
const proxy = require("http-proxy-middleware");

module.exports = function (app:any) {
  app.use(
    proxy("/books", {
      target: "https://localhost:5001",
      secure: false,
      changeOrigin: true,
    })
  );
};
