function showModal(){
    $("#exampleModal").modal()
}

$(document).ready(function(){
    $(".add-place").click (function(){
        $("#place").toggleClass('hidden');
    });
    $("#add-company").click (function(){

        $("#company").toggleClass('hidden');
    });

    var names  = ["ahmed", "wael", "yasser"];
    

    $("#chosen").autocomplete({
        source: names
       
    });
});
