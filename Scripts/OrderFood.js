var totalPrice = new Number(0).toFixed(2);
function toggleFields(rowNum)
{
    if ($("#cb_" + rowNum).is(":checked"))
    {
        //checkbox checked operations
        $("#fd_qty_" + rowNum).attr('disabled', false);
        $("#fd_total_" + rowNum).text(new Number(0).toFixed(2));
    }
    else
    {
        //checkbox unchecked operations
        $("#fd_qty_" + rowNum).attr('disabled', true);
        $("#fd_total_" + rowNum).text("");
    }
}

function calculateTotal(rowNum)
{
    var oldamount = new Number($("#fd_total_"+rowNum).text());
    var amt = new Number($("#fd_cost_" + rowNum).text() * $("#fd_qty_" + rowNum).val()).toFixed(2);
    $("#fd_total_" + rowNum).text(amt);
    updatePrice(oldamount, amt, true);
}

function validate(event) {
    var key = window.event ? event.keyCode : event.which;
    if (event.keyCode == 8 || event.keyCode == 4 || event.keyCode == 37 || event.keyCode == 39
        || event.keyCode == 36 || event.keyCode == 35)
    {
        return true;
    }
    else if (key < 48 || key > 57)
    {
        return false;
    }
    else
    {
        return true;
    }
}

function updatePrice(oldamount, newAmt)
{
    totalPrice = parseFloat(totalPrice) - parseFloat(oldamount)
               + parseFloat(newAmt);
    $('#totalPrice').empty();
    $('#totalPrice').text(totalPrice.toFixed(2));
}

function gatherData()
{
    var data = "";
    var checked = false;
    console.log("Inside gatherData");
    $("input[type=checkbox]:checked").each(function () {
        checked = true;
        var rowNum = this.id.split("_")[1];
        console.log("RowNum : " + rowNum);
        if ($("#fd_total_" + rowNum).text() == "0.00")
        {
            checked = false;
            return false;
        }
        data += $("#fd_name_" + rowNum).text() + "|" + $("#fd_total_" + rowNum).text()
                + "~";
        console.log($("#fd_name_" + rowNum).text() + "--" + $("#fd_total_" + rowNum).text());
    });
    if (!checked)
        data = "no data";
    $("#MainContent_OrderData").val(data);
    $("#MainContent_TotalPrice").val($("#totalPrice").text());
}

function test()
{
    alert('hello');
}