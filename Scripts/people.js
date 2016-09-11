function confirmDelete() {

    if(confirm('Are you sure you wish to remove this address? Click OK to continue'))
    {
        return true;
    }

    return false;
}
function insertNewAddress() {
    var newRow = '<tr>';
    newRow +='<td><label class="control-label col-md-2">Address</label></td>';
    newRow +='<td><input id="txtAddress" type="text" class="form-control" /></td>';

    newRow +='<td><label class="control-label col-md-2">City</label></td>';
    newRow +='<td><input id="txtCity" type="text" class="form-control" /></td>';

    newRow +='<td><label class="control-label col-md-2">State</label></td>';
    newRow +='<td><input id="txtState" type="text" class="form-control" /></td>';

    newRow +='<td><label class="control-label col-md-2">Zip</label></td>';
    newRow +='<td><input id="txtZip" type="text" class="form-control" /></td>';

    newRow += '<td><button name="btnSave" type="button" class="btn btn-default" value="Create" onclick="saveAddress();">Save</button></td>'
    newRow += '<td><button name="btnCancel" type="button" class="btn btn-default" value="Cancel" onclick="removeRow(this);">Cancel</button></td>'


    $('#addressTable').append(newRow);
}
function saveAddress(obj)
{
    var addr = $('#txtAddress').val();
    var city = $('#txtCity').val();
    var state = $('#txtState').val();
    var zip = $('#txtZip').val();

    if (addr && city && state && zip && !isNaN(zip))
    {
        var pID = $('#addressTable').attr('data-modelID');
        var url = '/People/AddAddress/';
        var addrargs = JSON.stringify({
            'PersonID': pID,
            'Address': addr,
            'City': city,
            'State': state,
            'Zip': zip
        });
        $.ajax({
            url: url,
            data: addrargs,
            type: 'POST',
            dataType: 'text',
            data: 'args=' + addrargs,
            success: function (result) {
                removeRow(obj);
                window.location.href = pID;
            },
            error: function () {
                alert('An error occurred saving address.');
            }
        });
    }
    else
    {
        alert('Invalid address.  Please try again');
        return false;
    }
}
function updateAddress(id, obj) {
    var row = $(obj).closest("tr");
    var addr = $(row).find('#item_Address').val();
    var city = $(row).find('#item_City').val();
    var state = $(row).find('#item_State').val();
    var zip = $(row).find('#item_Zip').val();

    if (addr && city && state && zip) {
        var pID = $('#addressTable').attr('data-modelID');
        var url = '/People/UpdateAddress/';
        var addrargs = JSON.stringify({
            'ID': id,
            'PersonID': pID,
            'Address': addr,
            'City': city,
            'State': state,
            'Zip': zip
        });
        $.ajax({
            url: url,
            data: addrargs,
            type: 'POST',
            dataType: 'text',
            data: 'args=' + addrargs,
            success: function (result) {
                removeRow(obj);
                window.location.href = pID;
            },
            error: function () {
                alert('An error occurred saving address.');
            }
        });
    }
    else {
        alert('Invalid address.  Please try again');
        return false;
    }
}
function removeRow(obj) {
    var row = $(obj).closest("tr");
    $(row).remove();

}