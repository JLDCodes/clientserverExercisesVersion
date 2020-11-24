$(function () { // lab9a
    $("#getbutton").click(async (e) => { //click event handler makes asynchronous fetch to server
        try {
            $("#status").text("please wait...");
            let lastName = $("#TextBoxFindLastName").val();

            $("#theModal").modal("toggle"); // pop the modal

            let response = await fetch(`api/student/${lastName}`);

            if (!response.ok) // or check for response.status
                throw new Error(`Status - ${response.status}, Problem server side, see server console`);

            let data = await response.json(); // this returns a promise, so we await it
            sessionStorage.setItem('student', JSON.stringify(data));

            if (data.lastName !== "not found") {
                $('#TextBoxEmail').val(data.email);
                $('#TextBoxTitle').val(data.title);
                $('#TextBoxFirstName').val(data.firstName);
                $('#TextBoxLastName').val(data.lastName);
                $('#TextBoxPhone').val(data.phoneNo);


                sessionStorage.setItem('Id', data.id);
                sessionStorage.setItem('DivisionId', data.divisionId);
                sessionStorage.setItem('Timer', data.timer);

                $('#status').text('student found');
            } else {
                $('#TextBoxEmail').val('');
                $('#TextBoxTitle').val('');
                $('#TextBoxFirstName').val('');
                $('#TextBoxLastName').val('');
                $('#TextBoxPhone').val('');

                sessionStorage.setItem('Id', '');
                sessionStorage.setItem('DivisionId', '');
                sessionStorage.setItem('Timer', '');

                $('#status').text('No such student...');
            }//else
        } catch (error) {
            $("#status").text(error.message);
        }// try/catch     

    });// get button click 

    $("#updatebutton").mouseup(async (e) => { //click event handler makes asynchronous fetch to server
        try {
            //studentObject = new Object();
            studentObject = JSON.parse(sessionStorage.getItem('student'));

            studentObject.title = $('#TextBoxTitle').val();
            studentObject.phoneNo = $('#TextBoxPhone').val();
            studentObject.firstName = $('#TextBoxFirstName').val();
            studentObject.lastName = $('#TextBoxLastName').val();
            studentObject.email = $('#TextBoxEmail').val();


            //stored earlier, numbers needed for Ids or http 401
            studentObject.id = parseInt(sessionStorage.getItem('Id'));
            studentObject.divisionId = parseInt(sessionStorage.getItem('DivisionId'));
            studentObject.timer = sessionStorage.getItem('Timer');

            let response = await fetch('api/student', {
                method: 'PUT',
                headers: { 'Content-Type': 'application/json; charset=utf-8' },
                body: JSON.stringify(studentObject)
            });

            if (response.ok) { // or check for response.status
                let data = await response.json();
                $('#status').text(data.msg);
            }
            else {
                $('#status').text(`Status - ${response.status}`);
            }
        } catch (error) {
            $('#status').text(error.message);
        }// try/catch     

    });//update button click  
}); //jQuery ready method 

////server was rearched but server had a problem with the call 
//const errorRtn = (problemJson, status) => {
//    if (status > 499) {
//        $('#status').text("Problem server side, see debug console");
//    } else {
//        let keys = Object.keys(problemJson.errors)
//        problem = {
//            status: status,
//            statusText: problemJson.errors[keys[0]][0], //first error
//        };
//        $('#status').text("Problem cleint side, see brower console");
//        console.log(problem);
//    }//else
//}