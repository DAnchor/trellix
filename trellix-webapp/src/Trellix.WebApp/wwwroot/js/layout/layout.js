$(window).on("DOMContentLoaded", function () {
    $(document).on("hidden.bs.modal", function () {
        if (document.activeElement) {
            document.activeElement.blur();
        }
    });
});