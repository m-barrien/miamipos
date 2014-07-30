$(function () {
  // Sets up PDF Download Link
  var url = window.location.pathname;
  var filename = url.substring(url.lastIndexOf('/')+1);
  var filenamePDF = filename.replace(".htm", ".pdf");
  $("#download").attr("href", filenamePDF);

  // Sets up report document dialog box
	$("#dialog").dialog({
		modal: true,
		autoOpen: false,
		resizable: false,
		width: 270,
		buttons: {
			"Send": function () {
				
				var valid = false;
				var comments = $("#comments").val();
				var copyright = $("#copyright").is(':checked');
				var offensive = $("#offensive").is(':checked');
				var adult = $("#adult").is(':checked');
				var other = $("#other").is(':checked');

				if (offensive || adult || copyright || other || (comments != ""))
					valid = true;
				
				if (valid)
				{
					var data = "copyright=" + copyright + "&offensive=" + offensive + "&adult=" + adult + "&other=" + other + "&comments=" + comments + "&url=" + location.href;
					$.ajax({
						type: "GET",
						url: "../../../../includes/email.php",
						data: data
					});

					$(this).dialog("close");
					$("#confirm").dialog("open");
				}
				else
				{
					$("#dialog-message").text("Please enter reason for removal");
				}
			},
			
			Cancel: function () {
				$(this).dialog("close");
			}
		},
		close: function () {
			$('input:checkbox').removeAttr('checked');
			$("#dialog-message").text("Tell us why this document is inappropriate");
			$("#comments").val("");
		}
	});

	$("#flag").click(function () {
		$("#dialog").dialog("open");
	});

	$("#confirm").dialog({
		modal: true,
		autoOpen: false,
		resizable: false,
		buttons: {
			Close: function () {
					$(this).dialog("close");
				}
		}
	});

});
