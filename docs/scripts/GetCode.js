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
            return new URLSearchParams(window.location.search).get("code");
        }
        else {
            return "";
        }
    };
    GetDestinyCode.prototype.HasCodeInQueryString = function () {
        var queryString = window.location.search;
        var search = new URLSearchParams(queryString);
        var data = search.get("code");
        if (data != null && data != "") {
            return true;
        }
        else {
            return false;
        }
    };
    return GetDestinyCode;
}());
var HelperFunctions = (function () {
    function HelperFunctions() {
    }
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
    HelperFunctions.BeginRedirect = function (waitTime) {
        var _this = this;
        if (waitTime === void 0) { waitTime = 10000; }
        setTimeout(function () {
            _this.DoRedirect();
        }, waitTime);
    };
    HelperFunctions.DoRedirect = function () {
        //Called when redirect fires
        window.location.replace(this.GetRootPageUrl() + "home.html");
    };
    HelperFunctions.GetRootPageUrl = function () {
        return "" + window.location.origin + window.location.pathname;
    };
    return HelperFunctions;
}());
var CodeTool = new GetDestinyCode();
//# sourceMappingURL=GetCode.js.map