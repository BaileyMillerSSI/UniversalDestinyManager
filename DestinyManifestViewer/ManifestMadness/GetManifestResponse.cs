using System;
using System.Collections.Generic;
using System.Text;

namespace ManifestMadness
{
    public class GetManifestResponse
    {
        public Response Response { get; set; }
        public int ErrorCode { get; set; }
        public int ThrottleSeconds { get; set; }
        public string ErrorStatus { get; set; }
        public string Message { get; set; }
        public Messagedata MessageData { get; set; }
    }

    public class Response
    {
        public string version { get; set; }
        public string mobileAssetContentPath { get; set; }
        public Mobilegearassetdatabas[] mobileGearAssetDataBases { get; set; }
        public Mobileworldcontentpaths mobileWorldContentPaths { get; set; }
        public string mobileClanBannerDatabasePath { get; set; }
        public Mobilegearcdn mobileGearCDN { get; set; }
    }

    public class Mobileworldcontentpaths
    {
        public string en { get; set; }
        public string fr { get; set; }
        public string es { get; set; }
        public string de { get; set; }
        public string it { get; set; }
        public string ja { get; set; }
        public string ptbr { get; set; }
        public string esmx { get; set; }
        public string ru { get; set; }
        public string pl { get; set; }
        public string zhcht { get; set; }
    }

    public class Mobilegearcdn
    {
        public string Geometry { get; set; }
        public string Texture { get; set; }
        public string PlateRegion { get; set; }
        public string Gear { get; set; }
        public string Shader { get; set; }
    }

    public class Mobilegearassetdatabas
    {
        public int version { get; set; }
        public string path { get; set; }
    }

    public class Messagedata
    {
    }

}
