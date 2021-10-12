// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var currentDate = new Date();
var monthsMap = ["يناير", "فبراير", "مارس", "إبريل", "مايو", "يونيو", "يوليو", "أغسطس", "سبتمبر", "أكتوبر", "نوفمبر", "ديسمبر"];
var startYear = 2020;

$("#government").change(function () {
    getZoneByGovernmentId($(this).val());
})



function getZoneByGovernmentId(governmentId) {
    $.get("/Zones/getZone/" + governmentId).done(function (response) {
        $("#zone").html("");
        $("#zone").append(new Option("أختر المنطقة", ""))

        response.forEach(item => {
            $("#zone").append(new Option(item.name, item.id))
        });
    })
}

function getSitesByZoneId(zoneId) {
    $.get("/Sites/getSitesByZone/" + zoneId).done(function (response) {
        $("#Sites").html("");
        $("#Sites").append(new Option("أختر الموقع", ""))

        response.forEach(item => {
            $("#Sites").append(new Option(item.name, item.id))
        });
    })
}

function reportPrint(elId) {
    $(".noprint").addClass("d-none");
    printJS({
        printable: elId, type: 'html', targetStyles: ['*']
    })
    $(".noprint").removeClass("d-none");
}