class GetDestinyCode
{
    public LaunchUwpApp(code: string, override: boolean = false)
    {
        if (this.HasCodeInQueryString() || override)
        {
            var launcher = document.createElement("a");
            launcher.id = "UwpLauncher";
            launcher.href = "udm://" + this.GetCodeFromQueryString();

            document.body.appendChild(launcher);
            launcher.click();

            setTimeout(() => {
                document.body.removeChild(launcher);
            }, 5000);
        }
    }
    
    public GetCodeFromQueryString(): string
    {
        if (this.HasCodeInQueryString()) {
            return new URLSearchParams(window.location.search).get("code");
        } else
        {
            return "";
        }
    }

    private HasCodeInQueryString(): boolean
    {
        var queryString = window.location.search;  
        var search = new URLSearchParams(queryString);
        var data = search.get("code");

        if (data != null && data != "") {
            return true;
        } else {
            return false;
        }
    }
}
class HelperFunctions
{
    public static IsVisible(id: string): boolean
    {
        var element = document.getElementById(id);

        if (element != null)
        {
            return element.style.display === "";
        }
        else {
            return false;
        }
    }

    public static ToggleVisibleState(id: string)
    {
        var element = document.getElementById(id);

        if (element != null)
        {
            if (this.IsVisible(id)) {
                element.style.display = "none";
            } else {
                element.style.display = "";
            }
        }
    }

    public static BeginRedirect(waitTime: number = 10000) {
        setTimeout(() => {
            this.DoRedirect();
        }, waitTime);
    }

    public static DoRedirect() {
        //Called when redirect fires
        window.location.replace(this.GetRootPageUrl() + "home.html");
    }

    private static GetRootPageUrl(): string {
        return `${window.location.origin}${window.location.pathname}`;
    }
}
var CodeTool = new GetDestinyCode();