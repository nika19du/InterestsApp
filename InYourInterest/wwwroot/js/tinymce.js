<script src="/lib/tinymce/tinymce.min.js" type="text/javascript"></script>
function tinymceStyle() {
    tinymce.init({
        selector: "textarea",
        plugins: [
            "image paste table link code media"
        ]
    });
}