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
        if (this.HasCodeInQueryString())
        {
            var results = new RegExp('[\?&]' + "code" + '=([^&#]*)').exec(window.location.href);
            return decodeURI(results[1]);
        } else
        {
            return null;
        }
    }

    public HasCodeInQueryString(): boolean
    {
        var results = new RegExp('[\?&]' + "code" + '=([^&#]*)').exec(window.location.href);
        if (results == null) {
            return false;
        }
        else {
            return true;
        }

    }
}
class HelperFunctions
{
    public static TryCloseWindow(): void
    {
        try {
            window.close();
        } catch (e) {

        }
    }

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

    public static BeginRedirect(path: string = "", waitTime: number = 10000) {
        setTimeout(() => {
            this.DoRedirect(path);
        }, waitTime);
    }

    public static DoRedirect(path: string = "") {
        //Called when redirect fires
        if (path === undefined || path === "") {
            window.location.replace(this.GetRootPageUrl() + "home.html");
        } else {
            window.location.replace(this.GetRootPageUrl() + path);
        }
        
    }

    private static GetRootPageUrl(): string {
        var data = `${window.location.origin}${this.CleanPathName()}/`
        return data;
    }

    private static CleanPathName(): string {
        var containsFilePath = window.location.pathname.indexOf('html') != -1
        if (!containsFilePath) {
            return window.location.pathname;
        } else {
            return "";
        }
    }
}
var CodeTool = new GetDestinyCode();