//#region Elements
var pass = document.getElementById("regPass");
var passConf = document.getElementById("regPassConf");
var email = document.getElementById("regEmail");
var check = true;
//#endregion
//#region functions
function showPassMatch() {
	document.getElementById("passMatch").style.display = "block";
}
function showPassEmpty() {
	document.getElementById("passEmpty").style.display = "block";
}
function hidePass() {
	document.getElementById("passMatch").style.display = "none";
	document.getElementById("passEmpty").style.display = "none";
}
function showEmail() {
	document.getElementById("emailCheck").style.display = "block";
}
function hideEmail() {
	document.getElementById("emailCheck").style.display = "none";
}
function checkPass() {
	if (pass.value === "") {
		if (passConf.value === "") {
			hidePass();
			return;
		}
		hidePass();
		showPassEmpty();
	}
	else {
		if (passConf.value === "") {
			showPassEmpty();
			return;
		}
		else if (pass.value === passConf.value) {
			hidePass();
			return;
		}
		hidePass();
		showPassMatch();
	}
}
//#endregion
//#region events
pass.onkeyup = function () { checkPass(); };
passConf.onkeyup = function () { checkPass(); };
email.onkeyup = function () { checkEmail() };
//#endregion