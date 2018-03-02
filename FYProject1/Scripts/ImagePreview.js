function previewImages() {

    var $preview = $('#preview').empty();
    if (this.files) $.each(this.files, readAndPreview);

    function readAndPreview(i, file) {

        if (!/\.(jpe?g|png|gif)$/i.test(file.name)) {
            return alert(file.name + " is not an image");
        } // else...

        var reader = new FileReader();

        $(reader).on("load", function () {
            $preview.append($("<img/>", { src: this.result, height: 100 }));
        });

        reader.readAsDataURL(file);

    }

}