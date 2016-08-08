var returnUrl = "/"; //返回地址
function Login() {
    try {
        var username = document.getElementById("Username").value;
        var password = document.getElementById("Password").value;
       
        if (username.length == 0) {
            alert("账号不能为空");
            return;
        }
        if (password.length == 0) {
            alert("密码不能为空");
            return;
        }
        alert(1);
        var param = new Object();
        param["Username"] = username;
        param["Password"] = password;

        $.post("/account/login", param, Success);
    } catch (e) {
        alert(e);
    } 
   
}


function Success(parameters) {
    if (parameters == "OK") {
        window.location.href = returnUrl;
    }
}