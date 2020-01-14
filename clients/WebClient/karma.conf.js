// Karma configuration file, see link for more information
// https://karma-runner.github.io/1.0/config/configuration-file.html

module.exports = function(config) {
     config.set({
          basePath: "",
          frameworks: ["jasmine", "@angular-devkit/build-angular"],
          //frameworks: ['jasmine', '@angular/cli'],
          plugins: [
               require("karma-jasmine"),
               require("karma-chrome-launcher"),
               require("karma-jasmine-html-reporter"),
               require("karma-coverage-istanbul-reporter"),
               require("@angular-devkit/build-angular/plugins/karma")
               //require('@angular/cli/plugins/karma')
          ],
          client: {
               clearContext: true // leave Jasmine Spec Runner output visible in browser
               ,captureConsole: true
          },
          coverageIstanbulReporter: {
               //dir: require('path').join(__dirname, './coverage/WMMPC'),
               reports: ["html", "lcovonly", "text-summary"],
               fixWebpackSourcePaths: true
          },
          angularCli: {
               environment: "dev"
          },
          reporters: ["progress", "kjhtml"],
          port: 9999,
          colors: true,
          logLevel: config.LOG_INFO,
          autoWatch: true,
          //browsers: ['ChromeHeadless'],
          browsers: ["Chrome"],
          singleRun: false,
          restartOnFileChange: true,
     });
};
