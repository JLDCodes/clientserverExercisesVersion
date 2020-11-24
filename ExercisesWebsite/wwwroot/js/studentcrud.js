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

		// get division data
		response = await fetch(`api/division`);
		if (response.ok) {
			let divs = await response.json();
			sessionStorage.setItem("alldivisions", JSON.stringify(divs));
		} else if (response.status !== 404) {
			let problemJson = await response.json();
			errorRtn(problemJson, response.status);
		} else {
			$("#status").text("no such path on server");
		}
	};

	const setupForUpdate = (id, data) => {
		$("#actionbutton").val("update");
		$("#modaltitle").html("<h4>update student</h4>");
		$("#deletebutton").show();

		clearModalFields();
		data.map(student => {
			if (student.id === parseInt(id)) {
				$("#TextBoxTitle").val(student.title);
				$("#TextBoxFirstname").val(student.firstName);
				$("#TextBoxLastname").val(student.lastName);
				$("#TextBoxPhone").val(student.phoneNo);
				$("#TextBoxEmail").val(student.email);
				sessionStorage.setItem("id", student.id);
				sessionStorage.setItem("divisionId", student.divisionId);
				sessionStorage.setItem("timer", student.timer);
				$("#modalstatus").text("update data");
				loadDivisionDDL(student.divisionId.toString())
				$("#theModal").modal("toggle");
			}
		});
	};

	const setupForAdd = () => {
		$("#actionbutton").val("add");
		$("#modaltitle").html("<h4>add student</h4>");
		$("#theModal").modal("toggle");
		$("#modalstatus").text("add new student");
		$("#deletebutton").hide();
		clearModalFields();
	};

	const clearModalFields = () => {
		loadDivisionDDL(-1);
		$("#TextBoxTitle").val("");
		$("#TextBoxFirstname").val("");
		$("#TextBoxLastname").val("");
		$("#TextBoxPhone").val("");
		$("#TextBoxEmail").val("");
		sessionStorage.removeItem("id");
		sessionStorage.removeItem("divisionId");
		sessionStorage.removeItem("timer");
	};

	const loadDivisionDDL = (studiv) => {
		html = '';
		$('#ddlDivisions').empty();
		let alldivisions = JSON.parse(sessionStorage.getItem('alldivisions'));
		alldivisions.map(div => html += `<option value ="${div.id}">${div.name}</option>`);
		$('#ddlDivisions').append(html);
		$('#ddlDivisions').val(studiv);
	}; // loadDivisionDDL

	const add = async () => {
		try {
			stu = new Object();
			stu.title = $("#TextBoxTitle").val();
			stu.firstname = $("#TextBoxFirstName").val();
			stu.lastname = $("#TextBoxLastName").val();
			stu.phoneno = $("#TextBoxPhone").val();
			stu.email = $("#TextBoxEmail").val();
			stu.divisionId = parseInt($("#ddlDivisions").val());
			stu.id = -1;
			stu.timer = null;
			stu.picture64 = null;

			let response = await fetch("api/student", {
				method: "POST",
				headers: {
					"Content-Type": "application/json; charset=utf-8"
				},
				body: JSON.stringify(stu)
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
	};

	const update = async () => {
		try {
			stu = new Object();

			stu.title = $("#TextBoxTitle").val();
			stu.firstName = $("#TextBoxFirstname").val();
			stu.lastName = $("#TextBoxLastname").val();
			stu.phoneNo = $("#TextBoxPhone").val();
			stu.email = $("#TextBoxEmail").val();
			stu.divisionId = parseInt($("ddlDivisions").val());
			stu.id = parseInt(sessionStorage.getItem("id"));
			stu.divisionId = parseInt(sessionStorage.getItem("divisionId"));
			stu.timer = sessionStorage.getItem("timer");
			stu.picture64 = null;
			let response = await fetch("api/student", {
				method: "PUT",
				headers: { "Content-Type": "application/json; charset=utf-8" },
				body: JSON.stringify(stu)
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

	$('#deletebutton').click(() => {
		_delete();// if yes was chosen
	}); // delete button click

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


	const _delete = async () => {
		try {
			let response = await fetch(`api/student/${sessionStorage.getItem('id')}`, {
				method: 'DELETE',
				headers: { 'Content-Type': 'application/json; charset=utf-8' }
			});

			if (response.ok) { // or check for response.status
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
	};



	const buildStudentList = (data) => {
		$("#studentList").empty();
		div = $(`<div class="list-group-item text-white bg-secondary row d-flex" id="status">Student Info</div>
        <div class= "list-group-item row d-flex text-center" id="heading">
        <div class= "col-4 h4">Title</div>
        <div class= "col-4 h4">First</div>
        <div class= "col-4 h4">Last</div>
        </div>`);
		div.appendTo($("#studentList"));
		sessionStorage.setItem("allstudents", JSON.stringify(data));
		btn = $(`<button class="list-group-item row d-flex" id="0"><div class="col-12 text-left">...click to add student</div></button>`);
		btn.appendTo($("#studentList"));
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