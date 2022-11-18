


//show doctor assign popup
//var assign = function (Id) {
//    $("#assignModel").modal("show");
//    $('#drID').val(Id);
//}



//display doctor assign list

//var assign = function (Id) {
//    //dr contectedJS
//    $("#assignModel").modal("show");
//    $('#drID').val(Id);
//    AssignNotesContectedDoctors(Id);
//}

//function AssignNotesContectedDoctors(Id) {
//    //dr contectedJ
//    debugger
//    $.ajax({
//        type: "GET",
//        url: "/DoctorAssign/GetByDoctorId/" + Id,
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        success: function (data) {
//            debugger
//            $('#AssignNotesTable').html('');
//            $.each(data, function (i, item) {
//                debugger
//                var assignedName = '';
//                if (item.SalePerson != null) {
                    
//                    assignedName= item.SalePerson.User.FullName;
//                }
//                var Discription = '';
//                if (item.Note != null && item.Note.Discription  != null) {

//                    Discription = item.Note.Discription ;
//                }
//                $("#ContactDR").html(item.Doctor.User.FullName);
//                rows = "<tr><td>" + Discription + "</td><td>" + item.Date + "</td><td>" + item.Time + "</td></tr>";
//                $('#AssignNotesTable').append(rows);
//            });
//        },
//        error: function (result) {
//            alert('Please Try Again...');
//        }
//    });
//}
