$(document).ready(function () {

    $("#submit").click(function () {
        var name = $("#txtValue1").val();
        var amount = $("#txtValue2").val();
        var areValuesValid = true;
        if (name.length == 0) {
            areValuesValid = false;
            $("#name").css('visibility', 'visible');
        }
        if (amount.length == 0) {
            areValuesValid = false;
            $("#amount").css('visibility', 'visible');
        }
        if (amount <= 0) {
            areValuesValid = false;
            $("#amount").css('visibility', 'visible');
            $('#amount').text('Please enter a positive number greater than 0!!');
        }
        var input = {
            Name: name,
            Amount: amount
        };
        var urlString = "/api/Conversion/ConvertAmountToAplha";

        if (areValuesValid) {
            $.ajax({
                url: urlString,
                type: "POST",
                dataType: 'json',
                data: input,
                success: function (result) {
                    $("#name").css('visibility', 'hidden');
                    $("#amount").css('visibility', 'hidden');
                    $("#resultName").text(result.Name);
                    $("#resultAmount").text(result.Amount);

                },
                error: function (xhr, status, error) {
                    var err = eval("(" + xhr.responseText + ")");
                    $("#resultName").text(err);
                    $("#resultAmount").text(err);
                }
            });
        }

    });
});