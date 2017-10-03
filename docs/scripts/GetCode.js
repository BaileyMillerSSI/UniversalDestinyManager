var GetDestinyCode = (function () {
    function GetDestinyCode() {
    }
    GetDestinyCode.prototype.LaunchUwpApp = function (code, override) {
        if (override === void 0) { override = false; }
        if (this.HasCodeInQueryString() || override) {
            var launcher = document.createElement("a");
            launcher.id = "UwpLauncher";
            launcher.href = "udm://" + this.GetCodeFromQueryString();
            document.body.appendChild(launcher);
            launcher.click();
            setTimeout(function () {
                document.body.removeChild(launcher);
            }, 5000);
        }
    };
    GetDestinyCode.prototype.GetCodeFromQueryString = function () {
        if (this.HasCodeInQueryString()) {
            var results = new RegExp('[\?&]' + "code" + '=([^&#]*)').exec(window.location.href);
            return decodeURI(results[1]);
        }
        else {
            return null;
        }
    };
    GetDestinyCode.prototype.HasCodeInQueryString = function () {
        var results = new RegExp('[\?&]' + "code" + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return false;
        }
        else {
            return true;
        }
    };
    return GetDestinyCode;
}());
var HelperFunctions = (function () {
    function HelperFunctions() {
    }
    HelperFunctions.TryCloseWindow = function () {
        try {
            window.close();
        }
        catch (e) {
        }
    };
    HelperFunctions.IsVisible = function (id) {
        var element = document.getElementById(id);
        if (element != null) {
            return element.style.display === "";
        }
        else {
            return false;
        }
    };
    HelperFunctions.ToggleVisibleState = function (id) {
        var element = document.getElementById(id);
        if (element != null) {
            if (this.IsVisible(id)) {
                element.style.display = "none";
            }
            else {
                element.style.display = "";
            }
        }
    };
    HelperFunctions.BeginRedirect = function (path, waitTime) {
        var _this = this;
        if (path === void 0) { path = ""; }
        if (waitTime === void 0) { waitTime = 10000; }
        setTimeout(function () {
            _this.DoRedirect(path);
        }, waitTime);
    };
    HelperFunctions.DoRedirect = function (path) {
        if (path === void 0) { path = ""; }
        //Called when redirect fires
        if (path === "") {
            window.location.replace(this.GetRootPageUrl() + "home.html");
        }
        else {
            window.location.replace(this.GetRootPageUrl() + path);
        }
    };
    HelperFunctions.GetRootPageUrl = function () {
        return "" + window.location.origin + window.location.pathname;
    };
    return HelperFunctions;
}());
var CodeTool = new GetDestinyCode();
//# sourceMappingURL=GetCode.js.map