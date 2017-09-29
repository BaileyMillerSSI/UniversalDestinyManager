var GetDestinyCode = (function () {
    function GetDestinyCode() {
    }
    GetDestinyCode.prototype.LaunchUwpApp = function (code) {
        if (this.HasCodeInQueryString()) {
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
        return (data != "" || data != null) ? true : false;
    };
    return GetDestinyCode;
}());
var CodeTool = new GetDestinyCode();
//# sourceMappingURL=GetCode.js.map