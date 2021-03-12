function showModal(){
    $("#exampleModal").modal()
}

$(document).ready(function(){
    $(".add-place").click (function(){
        $(".place").css({"display":"Block","visibility":"visible"});
    });
    $("#add-company").click (function(){
        $("#company").css({"display":"Block","visibility":"visible"});
    });
});
