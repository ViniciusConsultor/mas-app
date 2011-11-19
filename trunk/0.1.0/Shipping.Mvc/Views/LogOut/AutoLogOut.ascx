<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Shipping.Mvc.Models.LogOut.AutoLogOutModel>" %>

<div style="display:none">
	<div id="logout-message">
		<fieldset id="logout">
			<h2 class="clock">Inactivity Warning!</h2>
			<div>
				<div>
					You will be logged out of VJP in <strong><span id="logout-time">120</span> seconds</strong>, due to inactivity.
				</div>
				<div class="buttons">
					<button type="submit" class="primary check" id="stop-logout">I'm Still Here!</button>
				</div>
			</div>
		</fieldset>
	</div>
</div>
<a id="logout-link" href="#logout-message" style="display:none"></a>

<script type="text/javascript">
	var autoLogoutTimer;
	var autoLogoutInterval;
	var logoutWait = 120;
	var logoutCountdown = logoutWait;
	var logoutDelay = <%: (Model.Timeout * 60000) %> - (logoutWait * 1000);

	$(document).ready(function () {

		setupAutoLogout();

		$('#logout-link').fancybox({
			'transitionIn': 'fade',
			'transitionOut': 'fade',
			'speedIn': 500,
			'speedOut': 500,
			'centerOnScroll': true,
			'showCloseButton': false,
			'hideOnOverlayClick': false,
			'padding': 0
		});

		$('#stop-logout').click(function () {
			$.ajax({
				type: 'POST',
				url: '<%: Url.Action("Reset", "Logout") %>/',
				success: function () {
					clearInterval(autoLogoutInterval);
					clearTimeout(autoLogoutTimer);
					$.fancybox.close();
					setupAutoLogout();
					logoutCountdown = logoutWait;
				}
			});
		});
	});

	function setupAutoLogout() {
		autoLogoutTimer = setTimeout(function () {
			$('#logout-time').text(logoutCountdown);
			$('#logout-link').trigger('click');
			autoLogoutInterval = setInterval(function () {
				logoutCountdown--;
				$('#logout-time').text(logoutCountdown);

				if (logoutCountdown <= 0) {
					window.location.href = '<%: Url.Action("RedirectToLogIn", "Logout") %>/?autoLogOut=true&returnUrl=' + escape(window.location.href);
				}
			}, 1000);
		}, logoutDelay);
	}
</script>