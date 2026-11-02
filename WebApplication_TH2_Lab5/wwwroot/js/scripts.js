window.addEventListener('DOMContentLoaded', function () {
    // Bật/tắt sidebar
    var sidebarToggle = document.body.querySelector('#sidebarToggle');
    if (sidebarToggle) {
        sidebarToggle.addEventListener('click', function (event) {
            event.preventDefault();
            document.body.classList.toggle('sb-sidenav-toggled');
        });
    }
});
