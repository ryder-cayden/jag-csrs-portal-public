// Karma configuration file, see link for more information // https://karma-runner.github.io/1.0/config/configuration-file.html
module.exports = function (config) {
  config.set({
    basePath: "",
    frameworks: ["jasmine", "@angular-devkit/build-angular"],
    plugins: [
      require("karma-jasmine"),
      require("karma-chrome-launcher"),
      require("karma-jasmine-html-reporter"),
      require("karma-coverage"),
      require("@angular-devkit/build-angular/plugins/karma"),
    ],
    client: {
      captureConsole: false,
      clearContext: false, // leave Jasmine Spec Runner output visible in browser
    },
    browserConsoleLogOptions: {
      level: "disable",
      terminal: false,
    },
    reporters: ["progress", "coverage", "kjhtml"],
    preprocessors: {
      'src/**/*.ts': ['coverage']
    },
    coverageReporter: {
      dir: require("path").join(__dirname, "./coverage/csrs-portal"),
      reporters: [{ type: 'lcov', subdir: 'report-lcov' }]
    },
    port: 9876,
    colors: true,
    logLevel: config.LOG_DISABLE,
    autoWatch: true,
    browsers: ["Chrome"],
    singleRun: false,
    restartOnFileChange: true,
  });
};
