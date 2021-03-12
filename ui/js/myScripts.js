function showModal(){
    $("#exampleModal").modal()
}

$(document).ready(function(){
    $("#place").cick (function(){
        $(".hidden").css({"display":"Block","visibility":"visible"});
    });

});
