$(document).ready(function () {
    $("body").on("change", "#image1", function (e) {
        previewImage(this, 'previewImage1');
    });


});
$(document).ready(function () {
    $("body").on("change", "#image2", function (e) {
        previewImage(this, 'previewImage2');
    });


});
$(document).ready(function () {
    $("body").on("change", "#image3", function (e) {
        previewImage(this, 'previewImage3');
    });


});





function previewImage(input, targetDivToShow) {
    var files = input.files;
    var filename;
    var done = function (url) {
        filename = $('#' + targetDivToShow).attr('src', url);
        $('#' + targetDivToShow).attr('src', url);

    };
    if (input.files && input.files[0]) {
        file = input.files[0];
        if (URL) {

            done(URL.createObjectURL(file));
        }
        else if (FileReader) {
            var reader = new FileReader();
            reader.onload = function (e) {

                done(reader.result);

            }
            reader.readAsDataURL(file);


        }

    }


}
