/// <binding ProjectOpened='run-all-tasks' />
module.exports = function (grunt) {
    grunt.initConfig({
        // less
        less: {
            development: {
                options: {
                    plugins: [new (require("less-plugin-autoprefix"))({ browsers: ["last 10 versions"] })],
                    paths: ["./wwwroot/css/stylesheets"],
                    yuicompress: true
                },
                files: {
                    "./wwwroot/css/stylesheets/site.css": "./wwwroot/css/stylesheets/site.less",
                    "./wwwroot/css/stylesheets/home/index.css": "./wwwroot/css/stylesheets/home/index.less"
                }
            }
        },
        // uglify
        uglify: {
            my_target: {
                files: [
                    // Home views
                    { "./wwwroot/js/home/index.min.js": "./wwwroot/js/home/index.js" },
                    // Layout views
                    { "./wwwroot/js/layout/layout.min.js": "./wwwroot/js/layout/layout.js" }
                ]
            }
        },
        // cssmin
        cssmin: {
            options: {
                level: {
                    1: { specialComments: 0 }
                }
            },
            target: {
                files: [
                    {
                        expand: true,
                        cwd: "./wwwroot/css/stylesheets",
                        src: ["site.css"],
                        dest: "./wwwroot/css/stylesheets",
                        ext: ".min.css"
                    },
                    {
                        expand: true,
                        cwd: "./wwwroot/css/stylesheets/home",
                        src: ["index.css"],
                        dest: "./wwwroot/css/stylesheets/home",
                        ext: ".min.css"
                    },
                    {
                        expand: true,
                        cwd: "./wwwroot/css/vendors",
                        src: ["bootstrap-icons.less"],
                        dest: "./wwwroot/css/vendors",
                        ext: ".min.css"
                    }
                ]
            }
        },
        // clean
        clean: [
            "./wwwroot/js/layout/*.min.js",
            "./wwwroot/js/orders/*.min.js",
            "./wwwroot/js/home/*.min.js",
            "./wwwroot/css/stylesheets/urls/*.css",
            "./wwwroot/css/stylesheets/**/*.css",
            "./wwwroot/css/stylesheets/*.css",
            "!./wwwroot/css/stylesheets/**/*.less",
            "./wwwroot/css/vendors/**/*.css",
            "!./wwwroot/css/vendors/**/*.less"
        ],
        // concat
        concat: {
            dist_css: {
                src: ["./wwwroot/css/urls/vendor-urls.css", "./wwwroot/css/stylesheets/site.min.css"],
                dest: "./wwwroot/css/stylesheets/site.min.concat.css"
            }
        },
        // watch
        watch: {
            files: [
                "./wwwroot/css/**/*.less",
                "./wwwroot/css/**/**/*.less",
                "./wwwroot/css/urls/*.css",
                "!./wwwroot/js/**/*.min.js",
                "./wwwroot/js/**/*.js"
            ],
            tasks: ["clean", "less", "uglify", "cssmin", "concat"]
        }
    });
    grunt.loadNpmTasks("grunt-contrib-less");
    grunt.loadNpmTasks("grunt-contrib-uglify");
    grunt.loadNpmTasks("grunt-contrib-cssmin");
    grunt.loadNpmTasks("grunt-contrib-clean");
    grunt.loadNpmTasks("grunt-contrib-concat");
    grunt.loadNpmTasks("grunt-contrib-watch");

    grunt.registerTask("run-all-tasks", ["watch"]);
};
