$(function () {
	$("a[data-modal]").on("click", function (e) {
		$("#DonationLogModalContent").load(this.href, function () {
			$("#DonationLogModal").modal({
				keyboard: true
			}, "show");
			bindForm(this);
		});
		return false;
	});
});

function bindForm(dialog) {
	$("form", dialog).submit(function () {
		$.ajax({
			url: this.action,
			type: this.method,
			data: $(this).serialize(),
			success: function (result) {
				if (result.success) {
					$("#DonationLogModal").modal("hide");
					location.reload();
				} else {
					$("#DonationLogModalContent").html(result);
					bindForm();
				}
			}
		});
		return false;
	});
}
