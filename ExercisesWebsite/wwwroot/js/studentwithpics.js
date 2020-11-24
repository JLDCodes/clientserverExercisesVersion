$(function () { // updatewithpics

    $("#getbutton").click(async (e) => {  // get button click event handler
        try {
            let lastname = $("#TextBoxFindLastname").val();
            $("#status").text("please wait...");
            $("#theModal").modal("toggle");  // pop the modal
            let response = await fetch(`api/student/${lastname}`);
            if (response.ok) {
                let data = await response.json();  // this returns a promise, so we await it.
                if (data.lastname !== "not found") {
                    $("#TextBoxEmail").val(data.email);
                    $("#TextBoxTitle").val(data.title);
                    $("#TextBoxFirstname").val(data.firstName);
                    $("#TextBoxLastname").val(data.lastName);
                    $("#TextBoxPhoneno").val(data.phoneNo);
                    $("#status").text("student found");
                    $("#ImageHolder").html(`<img height ="120" width="110" src="data:img/png;base64,${data.picture64}" />`);
                    $("#modalstatus").text("student found");
                    // return these non-mutated values later
                    sessionStorage.setItem("id", data.id);
                    sessionStorage.setItem("divisionId", data.divisionId);
                    sessionStorage.setItem("timer", data.timer);
                    sessionStorage("picture", data.picture64);
                } else {
                    $("#TextBoxFirstname").val("not found");
                    $("#TextBoxLastname").val("");
                    $("#TextBoxEmail").val("");
                    $("#TextBoxTitle").val("");
                    $("#TextBoxPhoneno").val("");
                    $("status").text("no such student");
                }
            } else if (response.status !== 404) {
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            } else {
                $("#status").text("no such path on server");
            }
        } catch (error) {
            $("#status").text(error.message);
        }
    });


    $("#updatebutton").click(async (e) => {
        try {
            stu = new Object();
            // populate the properties
            stu.title = $("#TextBoxTitle").val();
            stu.firstName = $("#TextBoxFirstname").val();
            stu.lastName = $("#TextBoxLastname").val();
            stu.phoneNo = $("#TextBoxPhoneno").val();
            stu.email = $("#TextBoxEmail").val();
            stu.divisionName = "";
            stu.picture64 = "";

            stu.id = parseInt(sessionStorage.getItem("id"));
            stu.divisionId = parseInt(sessionStorage.getItem("divisionId"));
            stu.timer = sessionStorage.getItem("timer");
            sessionStorage.getItem("picture")
                ? stu.picture64 = sessionStorage.getItem("picture")
                : stu.picture64 = null;

            let response = await fetch("api/student", {
                method: "PUT",
                headers: { "Content-Type": "application/json; charset=utf-8" },
                body: JSON.stringify(stu)
            });

            if (response.ok) {
                let payload = await response.json();
                $("#status").text(payload.msg);
            } else if (response.status !== 404) {
                let problemJson = await response.json();
                errorRtn(problemJson, response.status);
            } else {
                $("#status").text("no such path on server");
            }
        } catch (error) {
            $("#status").text(error.message);
            console.table(error);
        }
        $("#theModal").modal("toggle");
    }); // update button click




    //do we have a picture?
    $("input:file").change(() => {
        const reader = new FileReader();
        const file = $("#uploader")[0].files[0];

        file ? reader.readAsBinaryString(file) : null;

        reader.onload = (readerEvt) => {
            // get binary data then convert to encoded string
            const binaryString = reader.result;
            const encodedString = btoa(binaryString);
            sessionStorage.setItem('picture', encodedString);
        };
    });
});


const errorRtn = (problemJson, status) => {
    if (status > 499) {
        $("#status").text("Problem server side, see debug console");
    } else {
        let keys = Object.keys(problemJson.errors)
        problem = {
            status: status,
            statusText: problemJson.errors[keys[0]][0],
        };
        $("#status").text("Problem client side, see browser console");
        console.log(problem);
    }
}