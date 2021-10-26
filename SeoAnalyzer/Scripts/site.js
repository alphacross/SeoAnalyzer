$(document).ready(function () {
	$(document).ajaxStart(function () {
		StartLoading();
	});

	$(document).ajaxStop(function () {
		EndLoading();
	});
})

function StartLoading() {
	if ($('.loadingModal').length == 0) {
		var mod = $('<div class="loadingModal"><div class="spinner-border" role="status"><span class="sr-only"></span></div></div>');
		$('body').append(mod);
	}
	$('.loadingModal').toggleClass('toggled');
}

function EndLoading() {
	$('.loadingModal').toggleClass('toggled');
}