module.exports = function(server) {
  server.route({
    method: "GET",
    path: "/WMMP/Works",
    handler: function(request, h) {
      var fs = require("fs");
      var data = fs.readFileSync("works.json", "utf8");

      return h.response(data)
      .type('text/plain')
      .header('X-Custom', 'some-value')
      .header('Content-Type', 'application/json')
      .header('Access-Control-Allow-Origin', '*')
      .header('Access-Control-Allow-Methods', 'GET, POST, OPTIONS, PUT, PATCH, DELETE')
      .header('Access-Control-Allow-Headers', 'X-Requested-With,content-type')
      .header('Access-Control-Allow-Credentials', true);
  
     /* try {
        var fs = require("fs");
        var data = fs.readFileSync("works.json", "utf8");
        return data;
      } catch (error) { 
        return error.message;
      }*/
    }
  });

  //About route
  server.route({
    method: "GET",
    path: "/calculator/about",
    handler: function(request, h) {
      var data = {
        message: "Welcome to our Calculator Service"
      };

      return data;
    }
  });

  //Add route
  server.route({
    method: "GET",
    path: "/calculator/add/{num1}/{num2}",
    handler: function(request, h) {
      const num1 = parseInt(request.params.num1);
      const num2 = parseInt(request.params.num2);

      var data = {
        answer: num1 + num2
      };

      return data;
    }
  });

  //Subtract route
  server.route({
    method: "GET",
    path: "/calculator/sub/{num1}/{num2}",
    handler: function(request, h) {
      const num1 = parseInt(request.params.num1);
      const num2 = parseInt(request.params.num2);

      var data = {
        answer: num1 - num2
      };

      return data;
    }
  });

  //Multiply route
  server.route({
    method: "GET",
    path: "/calculator/multi/{num1}/{num2}",
    handler: function(request, h) {
      const num1 = parseInt(request.params.num1);
      const num2 = parseInt(request.params.num2);

      var data = {
        answer: num1 * num2
      };

      return data;
    }
  });

  //Divid route
  server.route({
    method: "GET",
    path: "/calculator/div/{num1}/{num2}",
    handler: function(request, h) {
      const num1 = parseInt(request.params.num1);
      const num2 = parseInt(request.params.num2);

      var data = {
        answer: num1 / num2
      };

      return data;
    }
  });

};
