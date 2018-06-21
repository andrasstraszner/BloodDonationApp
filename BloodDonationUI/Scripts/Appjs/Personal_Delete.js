$("#deletePersonal").on("click", function (e) {
	$("#deletePersonalModal").modal({
		keyboard: true
	}, 'show');
});

$("#deletePersonalConfirm").on("click", function (e) {
	$("#deletePersonalModal").modal('hide');
});
