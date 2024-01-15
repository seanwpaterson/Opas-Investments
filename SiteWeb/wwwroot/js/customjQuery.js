$(document).ready(function () {
    $('.cstm-date-picker').pickadate({
        format: 'dd/mm/yyyy',
        // selectMonths: true,
        // selectYears: true,
        // min: new Date(2023, 4, 15), // Minimum selectable date is today
        // max: new Date(2023, 11, 31), // Maximum selectable date is the end of the year
        today: true, // Show the current date when the date picker is opened
        clear: true, // Add a "Clear" button to the date picker
        close: true, // Add a "Close" button to the date picker
        closeOnSelect: true // Automatically close the date picker after a date is selected
    });



});

jQuery(document).ready(function (e) {
    function t(t) {
        e(t).bind("click", function (t) {
            t.preventDefault();
            e(this).parent().fadeOut()
        })
    }
    e(".team-drop").click(function () {
        var t = e(this).parents(".search-filter-all").children(".dropdown-content").is(":hidden");
        e(".search-filter-all .dropdown-content").hide();
        e(".search-filter-all .team-drop").removeClass("active");
        if (t) {
            e(this).parents(".search-filter-all").children(".dropdown-content").toggle().parents(".search-filter-all").children(".team-drop").addClass("active")
        }
    });
    e(document).bind("click", function (t) {
        var n = e(t.target);
        if (!n.parents().hasClass("search-filter-all")) e(".search-filter-all .dropdown-content").hide();
    });
    e(document).bind("click", function (t) {
        var n = e(t.target);
        if (!n.parents().hasClass("search-filter-all")) e(".search-filter-all .team-drop").removeClass("active");
    })
});