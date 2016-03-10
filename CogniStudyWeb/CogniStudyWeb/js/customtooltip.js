$(document).on('mouseenter', ".row_dd", function (e) {
    var self = $(this).find(".dataDiv");
    var name = self.attr("data-name");
    var city = self.attr("data-city");
    var country = self.attr("data-country");
    var email = self.attr("data-email");
    var gender = self.attr("data-gender");

    $("#dd_name").html(name)
    $("#dd_country").html(country);
    $("#dd_city").html(city);
    $("#dd_gender").html(gender);
    $("#dd_email").html(email);

    $("#detailedData").position({
        my: "left top",
        of: event,
        collision: "flip"
    });

    $("#detailedData").show().delay(800).fadeIn(400);
});

$(document).on('mouseleave', ".row_dd", function (e) {
    $("#detailedData").hide();
});

$(document).on('mousemove', ".row_dd", function (e) {
    $("#detailedData").position({
        my: "left top",
        of: event,
        collision: "flip"
    });
});