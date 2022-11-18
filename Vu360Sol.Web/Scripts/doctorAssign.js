


//show doctor assign popup
//var assign = function (Id) {
//    $("#assignModel").modal("show");
//    $('#drID').val(Id);
//}

//SalePerson Dropdown for doctor assign popup

$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "/SalePerson/SalePersonDropDown",
        dataType: "json",
        success: function (result) {
            $('#salePersonDD').html('');
            var rows1 = "<option value='' selected disabled > --select SalePerson--</option >";
            $('#salePersonDD').append(rows1);

            $.each(result, function (i, item) {
                var rows;
                rows = "<option value='" + item.Id + "'>" + item.User.FullName + "</option>";
                $('#salePersonDD').append(rows);
            });
        }
    });
});

//submit doctor assign popup

var saveNote = function () {
    debugger
    if (!RequiredField()) {
        $('#errorSummery').show();
        $('#errorSummery').text("Please fill Notes field").fadeOut(5000);
        return false;
    }
    else {

    //$("#assignModel").modal("hide");
    
    $.ajax({
        type: "GET",
        url: "/DoctorAssign/Create",
        data: $("#saveNote").serialize(),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $('#assign-note').val('');
            $('#salePersonDD').val('');
            var id = $('#drID').val();
            AssignNotes(id);
            //Swal.fire(
            //    'Success',
            //    'Assigned Sucessfully',
            //    'success'
            //)

        }
    });

    }
}





//display doctor assign list

var assign = function (Id) {
    $("#assignModel").modal("show");
    $('#drID').val(Id);
    AssignNotesDoctorAssignList(Id);
    
}

function AssignNotesDoctorAssignList(Id) {

    // Shahid dr assign js
    debugger
    $.ajax({
        type: "GET",
        url: "/DoctorAssign/GetByDoctorId/" + Id,
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (data) {
            debugger
            $('#AssignNotesTable').html('');
            $.each(data, function (i, item) {
                debugger
                var assignedName = '';
                if (item.SalePerson != null) {
                    
                    assignedName= item.SalePerson.User.FullName;
                }
                var Discription = '';
                if (item.Note != null && item.Note.Discription  != null) {

                    Discription = item.Note.Discription ;
                }

                rows = "<tr><td>" + Discription + "</td><td>" + item.Date + "</td><td>" + assignedName + "</td><td><a data-model-id='" + item.Id + "' onclick='DeleteAssignedDoctor(this)' style='cursor:pointer;color:red'><i class='icon icon-trash'></i></a></td></tr>";
                $('#AssignNotesTable').append(rows);
            });
        },
        error: function (result) {
            alert('Please Try Again...');
        }
    });
}

//Delete  doctor assign 


function DeleteAssignedDoctor(obj) {
    var ele = $(obj);
    var Id = ele.data("model-id");
    var url = "/DoctorAssign/Delete/" + Id;
    //var ans = confirm("Are you sure you want to delete this Note ?");
    swal({
        title: "Are you sure?",
        text: "Are you sure you want to delete this Note ?",
        icon: "warning",
        buttons: true,
        dangerMode: true,
    })
        .then((ans) => {
            if (ans) {
                $.ajax({
                    url: url,
                    type: "POST",
                    success: function () {
                        ele.closest("tr").remove();
                    },
                    error: function () {
                        
                        swal("Please Try Again...", {
                            icon: "error",
                        });
                    }
                });
            }
        });
    
}

// validation
function RequiredField() {
    debugger
    //var data = $("#salePersonDD").val();
    var data2 = $("#assign-note").val();
    //var x = document.getElementById("assign-note").required; 
    if (data2 != "") {
        return true;
    }
    else {
        return false;
    }
}