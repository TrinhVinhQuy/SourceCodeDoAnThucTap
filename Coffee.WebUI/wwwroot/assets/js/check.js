// Check Email
document.getElementById("submitBtn").addEventListener("click", function () {
    if (!validateEmail()) {
        return false;
    }
    document.getElementById("contactForm").submit();
});
function validateEmail() {
    var email = document.getElementById("email");
    var filter = /^([a-zA-Z0-9_\.\-])+\@(([a - zA - Z0 - 9\-]) +\.)+([a-zA-Z0-9]{2,4})+$/;
    if (!filter.test(email.value)) {
        document.getElementById("checkEmailFalse").style.display = "block";
        email.focus;
        return false;
    }
    return true;
}