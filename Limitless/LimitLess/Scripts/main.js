$(document).on("checkbox", "input:checkbox", function () {

});
function loadpage(container, url) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        global: true,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (data, textStatus, jqXHR) {
            $(container).html(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("body").tLoader("hide");
        }
    });
    //$(container).load(url, function () {
    //});
}
function loadpageAsync(container, url) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        global: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (data, textStatus, jqXHR) {
            $(container).html(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("body").tLoader("hide");
        }
    });
    //$(container).load(url, function () {
    //});
}
function openTab(container, url) {
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        global: false,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (data, textStatus, jqXHR) {
            $(container).html(data);
            reSizeTabs();
        },
        error: function (jqXHR, textStatus, errorThrown) {
            $("body").tLoader("hide");
        }
    });
    //$(container).load(url, function () {
    //});
}
function openPopup(url) {
    $(".modal").show();
    $.ajax({
        url: url,
        type: "GET",
        dataType: "html",
        global: true,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        success: function (data, textStatus, jqXHR) {
            $("#popupContainer").show("clip", 1000);
            $("#popupContainer").html(data);
        },
        error: function (jqXHR, textStatus, errorThrown) {
        }
    });
}
$(document).off().on("click", ".closePopup", function () {
    $(".modal").hide();
});
$(document).bind("ajaxStart", function () {
    //$("body").tLoader();
    $("#loading").show();
}).bind("ajaxStop", function () {
    setTimeout(function () {
        //$("body").tLoader("hide");
        $("#loading").hide();
    }, 1000);
});
toastr.options = {
    "closeButton": false,
    "debug": false,
    "newestOnTop": true,
    "progressBar": true,
    "positionClass": "toast-bottom-right",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}
$("body").delegate("#homeBtn", "click", function () {
    $(location).attr("href", "/");
});
var PostApi = function (url, data, cb) {
    var serviceuri = "http://limitlesstest.azurewebsites.net";
    $.ajax({
        url: serviceuri + url,
        global: true,
        async: true,
        type: "POST",
        dataType: "json",
        data: data,
        headers: {
            'Content-Type': "application/x-www-form-urlencoded; charset=UTF-8"
        },
        contentType: "application/json; charset=utf-8",
        success: cb,
        error: function (error) {
            toastr["error"]("Oops, something wasn't right,Please contact your system administrator.");
        }
    });
}
var Api_Get = function (url, cb, async) {
    if (typeof async === "undefined" || async === null) {
        async = true;
    }
    var serviceuri = "http://limitlesstest.azurewebsites.net";
    $.ajax({
        url: serviceuri + url,
        async: async,
        global: true,
        type: "GET",
        dataType: "json",
        headers: {
            'Content-Type': "application/x-www-form-urlencoded; charset=UTF-8"
        },
        contentType: "application/json; charset=utf-8",
        success: cb,
        error: function (error) {
            toastr["error"]("Oops, something wasn't right,Please contact your system administrator.");
        }
    });
}
var ApiGet = function (url, async) {
    if (typeof async === "undefined" || async === null) {
        async = true;
    }
    var serviceuri = "http://limitlesstest.azurewebsites.net";
    var response = $.ajax({
        url: serviceuri + url,
        async: async,
        global: true,
        type: "GET",
        dataType: "json",
        headers: {
            'Content-Type': "application/x-www-form-urlencoded; charset=UTF-8"
        },
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            return data;
        },
        error: function (error) {
            toastr["error"]("Oops, something wasn't right,Please contact your system administrator.");
        }
    }).responseText;
    return $.parseJSON(response);
}
$(document).on("blur keyup", ".required,.v-email,.reqField_login", function () {
    validateInput(this);
});
$(document).on("blur keyup", ".fieldSuccess", function () {
    validateInput(this);
});
function validateInput(input) {
    var count = 0;
    var errMsg = "";
    if ($(input).val() === "") {
        $(input).removeClass("fieldSuccess");
        $(input).addClass("mandatory");
    }
    if ($(input).hasClass("mandatory")) {
        if ($(input).val() === "") {
            errMsg = "Please enter " + $(input).attr("placeholder");
            count++;
        }
    }
    if ($(input).hasClass("v-email") && $(input).val() !== "") {
        if (!isValidEmailAddress($(input).val())) {
            errMsg = "Please enter valid email";
            count++;
        }
    }
    if (count > 0) {
        $(input).parent().append("<div class=\"ui corner label\"> <i class=\"erricon fa icon-info3\"  data-error=\"" + errMsg + "\"></i> </div>");
        $(input).next(".error-notification-icon").show();
        $(input).removeClass("fieldSuccess");
    } else {
        $(input).parent().find(".corner").remove();
        $(input).next(".error-notification-icon").hide();
        $(input).removeClass("mandatory");
        $(input).addClass("fieldSuccess");
    }
}
function isValidEmailAddress(emailAddress) {
    var pattern = new RegExp(/^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/);
    return pattern.test(emailAddress);
};
var validateForm = function () {
    validateSuccessField();
    //if ($(this).closest(".formFieldsbg").find(".mandatory"))
    if ($(".btnValidate").closest(".ContentContainer").find(".mandatory").length > 0) {
        toastr["error"]("Please correct the errors before submitting.");
        $(".required").blur();
        return false;
    } else {
        return true;
    }
}
var validateEditForm = function () {
    var Controls = $(".ContentContainer").find(".required");
    for (var i = 0; i < Controls.length; i++) {
        $(Controls[i]).removeClass("mandatory");
        $(Controls[i]).addClass("fieldSuccess");
    }
}
var validateSuccessField = function () {
    var Controls = $(".ContentContainer").find(".fieldSuccess :enabled");
    for (var i = 0; i < Controls.length; i++) {
        if ($(Controls[i]).val() == '') {
            alert($(Controls[i]).attr('placeholder'));
            $(Controls[i]).removeClass("fieldSuccess");
            $(Controls[i]).addClass("mandatory");
        }
    }
}
$(document).off("click", ".acc_title_bar").on("click", ".acc_title_bar", function () {
    $(this).toggleClass("default-title-bar-active");
});
$(document).on("mouseover", "[title]", function (e) {
    //$("[title]").hover(function (e) { // Hover event
    var titleText = $(this).attr("title");
    if (titleText !== "") {
        $(this)
          .data("tiptext", titleText)
          .addClass("tp")
          .removeAttr("title");

        var $this = $(this);
        var offset = $this.offset();
        var width = $this.width();
        var height = $this.height();

        var centerX = offset.left + width / 2;
        var centerY = offset.top + height / 2;
        $("<p class=\"tooltip\"></p>")
          .text(titleText)
          .appendTo("body")
          .css("top", (centerY - 45) + "px")
          .css("left", (centerX - 80) + "px")
          .fadeIn(100);
    }
});
$(document).on("mouseover", ".erricon", function (e) {
    //$("[title]").hover(function (e) { // Hover event
    var titleText = $(this).attr("data-error");
    $(this)
        .data("tiptext", titleText)
        .addClass("tperror")
        .removeAttr("data-error");

    var $this = $(this);
    var offset = $this.offset();
    var width = $this.width();
    var height = $this.height();
    console.log($this.offset());
    console.log(offset.left);
    console.log($(".errtip").width());
    var centerX = offset.left - $(".errtip").width();//+ width / 2;
    var centerY = offset.top + height / 2;
    $("<p class=\"errtip\"></p>")
        .text(titleText)
        .appendTo("body")
        .css("top", (centerY - 55) + "px")
        .css("left", (centerX) + "px");
    //.fadeIn(100);
    $(".errtip").css("left", offset.left - $(".errtip").width() + "px");
    $(".errtip").slideDown("100");
});
$(document).on("mouseleave", ".tp", function (e) {
    $(this).attr("title", $(this).data("tiptext"));
    $(".tooltip").remove();
});
$(document).on("mouseleave", ".erricon", function (e) {
    $(this).attr("data-error", $(this).data("tiptext"));
    $(".errtip").remove();
});
$(document).on("click", ".reset", function (e) {
    $(this).closest(".formFieldsbg").find("input[type=text]").val("");
    $(this).closest(".formFieldsbg").find("input[type=text].fieldSuccess").switchClass("fieldSuccess", "mandatory");
});
$(document).off("click", ".acc_title_bar").on("click", ".acc_title_bar", function (e) {
    $(this).next().slideToggle("fast");
    $(".acc_container").not($(this).next()).slideUp("fast");
});