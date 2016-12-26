var gulp = require("gulp");

gulp.task("vendor:js", function () {
    return gulp.src([
        "bower_components/angular/angular.js",
        "bower_components/bootstrap/dist/js/bootstrap.js",
        "bower_components/jquery/dist/jquery.js"
    ])
    .pipe(gulp.dest("wwwroot/js"));
})

gulp.task("vendor:css", function () {
    return gulp.src([
            "bower_components/bootstrap/dist/css/bootstrat.css",
            "bower_components/bootstrap/dist/css/bootstrap-theme.css",
    ])
    .pipe(gulp.dest("wwwroot/css"));
})

gulp.task("vendor:fonts", function () {
    return gulp.src([
            "bower_components/bootstrap/dist/fonts/*.*",
    ])
    .pipe(gulp.dest("wwwroot/fonts"));
})

gulp.task("html", function () {
    return gulp.src([
        "src/html/**/*.*",
    ])
    .pipe(gulp.dest("wwwroot"));
})

gulp.task("build", ["vendor:js", "vendor:css", "vendor:fonts", "html"], function () { });

gulp.task("default", ["build"], function () { });

gulp.task("watch", function () {
    gulp.watch("src/html/**/*.*", ["html"]);
});