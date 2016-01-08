var indexx, indexy, indexvalue;
$(document).ready(function () {

    $("#btncheckgame").click(function () {
        Refresh();
    });

    function Refresh() {
        location.reload();
    }

    $('input[type=text]').keyup(function (control) {
        var txtInput = $(this);
        //Validation first
        if (txtInput.val() == 0) {
            return;
        }
        //check if the inserted length is equal, if so reset and return
        if (txtInput.val().length == 0) {
            txtInput.val(0);
            return;
        }        
        if (isNaN(txtInput.val())) {
            txtInput.val(0);
            alert('Voer een getal in.');
            return;
        }
        if (txtInput.val() < 1 || txtInput.val() > 9) {
            txtInput.val(0);
            alert('Voer een getal in van 1 t/m 9.');
            return;
        }
        var txtInputName = txtInput.attr('name');
        var locationValues = txtInputName.split(",");
        //Ajax call to method, we don't want a postback to occur
        $.ajax({
            url: "/Home/SetValue/?posx=" + locationValues[0] + "&posy=" + locationValues[1] + "&value=" + txtInput.val(),
            type: "GET",
            error: function (response) {
                alert("Er ging iets mis bij het opslaan van de waarde");
            },
        });
    });
});