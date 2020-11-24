﻿$(function () {
    let data;
    $("#loadbutton").click(async e => {
        if (sessionStorage.getItem("studentData") === null) {//if not loaded get data from github
            const url = "https://raw.githubusercontent.com/elauersen/info3070/master/jqueryex5.json";
            $('#results').text('Locating student data on GitHub, please wait..');

            try {
                let response = await fetch(url);
                if (!response.ok)//check responce
                    throw new Error(`Status - ${response.status}, Text - ${response.statusText}`);//fires catch
                data = await response.json(); //returns a promise, so we await it
                sessionStorage.setItem("studentData", JSON.stringify(data));
                $('#results').text('Student data on GitHub loaded!');
            } catch (error) {
                $("#results").text(error.message);
            }
        } else {
            data = JSON.parse(sessionStorage.getItem("studentData"));
        }
        let html = "";
        data.map(student => {
            html += `<div id= "${student.id}"
                class="list-group-item">${student.firstname},${student.lastname}
            </div>`;
        });

        //dump the dymanically generated html into an element with an ai attirubte of studentlist
        $("#studentList").html(html);
        $("#loadbutton").hide();
        $("#inputstuff").show();
        //$("#addbutton").show();
        //$("#removebutton").show();
    });
    $("#studentList").click(e => {
        const student = data.find(s => s.id === parseInt(e.target.id));
        $("#results").text(`you selected ${student.firstname},${student.lastname}`);
    });
    $("#addbutton").click(e => {
        const first = $("#txtfirstname").val();
        const last = $("#txtlastname").val();
        if (first.length > 0 && last.length > 0) {

            if (data.length > 0) {
                const student = data[data.length - 1];
                data.push({ "id": student.id + 101, "firstname": first, "lastname": last });
                $("#results").text(`added student ${student.id + 101}`);
            } else {
                data.push({ "id": 101, "firstname": "new", "lastname": "student" });
            }

            $("#txtlastname").val("");
            $("#txtfirstname").val("");
            sessionStorage.setItem("studentData", JSON.stringify(data));
            let html = "";
            data.map(student => {
                html += `<div id= ${student.id}" class="list-group-item">${student.firstname},${student.lastname} </div>`;
            });
            $("#studentList").html(html);
        }
        });
    $("#removebutton").click(e => {
        if (data.length > 0) {//if there is data then remove the last entry
            const student = data[data.length - 1];
            data.splice(-1, 1); //remove last entry in array
            $("results").text(`removed student ${student.id}`);
            sessionStorage.setItem("studentData", JSON.stringify(data));
            let html = "";
            data.map(student => {
                html += `<div id = "${student.id}" class="list-group-item">${student.firstname},${student.lastname} </div>`;
            });
            $("#studentList").html(html);
        } else {
            $("#results").text(`no students to remove`);
        }
    });

});

