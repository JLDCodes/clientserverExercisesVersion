$(function () {
	const getAll = async (msg) => {
		try {
			$("#studentList").text("Finding Student Information...");
			let response = await fetch(`api/student`);
			if (response.ok) {
				let payload = await response.json();
				buildStudentList(payload);
				msg === "" ?
					$("#status").text("Students Loaded") : $("#status").text(`${msg} - Students Loaded`);
			} else if (response.status !== 404) {
				let problemJson = await response.json();
				errorRtn(problemJson, response.status);
			} else {
				$("#status").text("no such path on server");
			}
		} catch (error) {
			$("#status").text(error.message);
		}

	};



	$("#ddlCourses").change(function () {

		//var selectedCourse = $("#ddlCourses").children("option").val()
		var studentPicked = sessionStorage.getItem('studentId');
		data = JSON.parse(sessionStorage.getItem("allstudents"));
		$("#TextBoxMark").show();
		$("#CommentsRow").show();
		testy(studentPicked, data);
	});

	const testy = async (id, data) => {
	
		let response = await fetch(`api/grade/` + id);
		if (response.ok) {
			let grades = await response.json();
			var selectedCourse = $("#ddlCourses").prop('selectedIndex');
			var studentPicked = sessionStorage.getItem('studentId');
			let count = 0;
			var mark;
			var selectOption = document.getElementById("ddlCourses");
			var e = selectOption.value;
			var comment;
			grades.map(grade => grade.studentId === parseInt(studentPicked) && grade.courseId === parseInt(e) ? mark = grade.mark : count++);
			grades.map(grade => grade.studentId === parseInt(studentPicked) && grade.courseId === parseInt(e) ? comment = grade.comments : count++);
			//mark = 420;
			//comment = "You did it!";
			document.getElementById("TextBoxMark").value = mark;
			document.getElementById("TextBoxComments").value = comment;
			comment;
			count++;
		} else if (response.status !== 404) {
			let problemJson = await response.json();
			errorRtn(problemJson, response.status);
		} else {
			$("#status").text("no such path on server");
		}

	}; 
	const setupForUpdate = async (id, data) => {
		$("#actionbutton").val("update grade");
		$("#modaltitle").html("<h4>Grade Entry/Update</h4>");
		//$("#deletebutton").show();
		// get division data
		let response = await fetch(`api/course/` + id);
		if (response.ok) {
			let course = await response.json();
			var idCount = sessionStorage.getItem('divisionId');
			sessionStorage.setItem('studentId', id);
			
			let html = '';
			$('#ddlCourses').empty();
			//course.map(div => html += `<option value ="${div.id}">${div.name}</option>`);
			course.map(course => course.divisionId === parseInt(idCount) ? html += `<option value ="${course.courseId}">${course.name}</option>` : html);
			$('#ddlCourses').append(html);
			$('#ddlCourses').val(-1);


		} else if (response.status !== 404) {
			let problemJson = await response.json();
			errorRtn(problemJson, response.status);
		} else {
			$("#status").text("no such path on server");
		}
		clearModalFields();

		data.map(student => {
			if (student.id === parseInt(id)) {

				$("#TextBoxFirstname").val(student.firstName);
				$("#TextBoxLastname").val(student.lastName);
				sessionStorage.setItem("divisionId", student.divisionId);
				sessionStorage.setItem("timer", student.timer);
				$("#modalstatus").text("update data");
				//loadDivisionDDL(student.id.toString())
				$("#theModal").modal("toggle");
			}
		});
	}; 



	const clearModalFields = () => {
		$("#TextBoxFirstname").val("");
		$("#TextBoxLastname").val("");
		$("#TextBoxComments").val("");
		$("#TextBoxMark").hide();
		$("#MarkRow").hide();
		$("#CommentsRow").hide();
		sessionStorage.removeItem("id");
		sessionStorage.removeItem("divisionId");
		sessionStorage.removeItem("timer");
	};

	const update = async () => {
		try {
			grade = new Object();

			grade.mark = $("#TextBoxMark").val();
			grade.comments = $("#TextBoxComments").text();
			
			let response = await fetch("api/grade", {
				method: "PUT",
				headers: { "Content-Type": "application/json; charset=utf-8" },
				body: JSON.stringify(grade)
			});
			if (response.ok) {
				let data = await response.json();
				getAll(data.msg);
			} else if (response.status !== 404) {
				let problemJson = await response.json();
				errorRtn(problemJson, response.status);
			} else {
				$("#status").text("no such path on server");
			}
		} catch (error) {
			$("#status").text(error.message);
		}
		$("#theModal").modal("toggle");
	}
	$("#actionbutton").click(() => {
		$("#actionbutton").val() === "update" ? update() : add();
	});

	$('[data-toggle=confirmation]').confirmation({
		rootSelector: '[data-toggle=confirmation]'
	}); // confirmation

	$("#srch").keyup(() => {
		let alldata = JSON.parse(sessionStorage.getItem("allstudents"));
		let filtereddata = alldata.filter((stu) => stu.lastName.match(new RegExp($("#srch").val(), 'i')));
		buildStudentList(filtereddata, false);
	}); // search keyup 


	$("#studentList").click((e) => {
		if (!e) e = window.event;
		let id = e.target.parentNode.id;
		if (id === "studentList" || id === "") {
			id = e.target.id;
		}
		if (id !== "status" && id !== "heading") {
			let data = JSON.parse(sessionStorage.getItem("allstudents"));
			id === "0" ? setupForAdd() : setupForUpdate(id, data);
		} else {
			return false;
		}
	});


	const buildStudentList = (data, usealldata = true) => {
		$("#studentList").empty();
		div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Student Info</div>
        <div class= "list-group-item row d-flex text-center" id="heading">
        <div class= "col-4 h4">Title</div>
        <div class= "col-4 h4">First</div>
        <div class= "col-4 h4">Last</div>
        </div>`);
		div.appendTo($("#studentList"));
		usealldata ? sessionStorage.setItem("allstudents", JSON.stringify(data)) : null;
		data.map(stu => {
			btn = $(`<button class="list-group-item row d-flex" id="${stu.id}">`);
			btn.html(`<div class="col-4" id="studenttitle${stu.id}">${stu.title}</div>
                      <div class="col-4" id="studentfname${stu.id}">${stu.firstName}</div>
                      <div class="col-4" id="studentlastnam${stu.id}">${stu.lastName}</div>`
			);
			btn.appendTo($("#studentList"));
		});
	};
	getAll("");
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