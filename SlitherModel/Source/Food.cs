﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using SlitherModel;
//
//    var food = Food.FromJson(jsonString);

namespace SlitherModel.Source
{
    using Newtonsoft.Json;

    public partial class Food
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("xx")]
        public long Xx { get; set; }

        [JsonProperty("yy")]
        public long Yy { get; set; }

        [JsonProperty("rx")]
        public double Rx { get; set; }

        [JsonProperty("ry")]
        public double Ry { get; set; }

        [JsonProperty("rsp")]
        public long Rsp { get; set; }

        [JsonProperty("cv")]
        public long Cv { get; set; }

        [JsonProperty("rad")]
        public long Rad { get; set; }

        [JsonProperty("sz")]
        public double Sz { get; set; }

        [JsonProperty("lrrad")]
        public long Lrrad { get; set; }

        [JsonProperty("cv2")]
        public long Cv2 { get; set; }

        [JsonProperty("fi")]
        public Fi Fi { get; set; }

        [JsonProperty("fw")]
        public long Fw { get; set; }

        [JsonProperty("fh")]
        public long Fh { get; set; }

        [JsonProperty("fw2")]
        public long Fw2 { get; set; }

        [JsonProperty("fh2")]
        public long Fh2 { get; set; }

        [JsonProperty("ofi")]
        public Fi Ofi { get; set; }

        [JsonProperty("ofw")]
        public long Ofw { get; set; }

        [JsonProperty("ofh")]
        public long Ofh { get; set; }

        [JsonProperty("ofw2")]
        public double Ofw2 { get; set; }

        [JsonProperty("ofh2")]
        public double Ofh2 { get; set; }

        [JsonProperty("gcv")]
        public long Gcv { get; set; }

        [JsonProperty("gfi")]
        public Fi Gfi { get; set; }

        [JsonProperty("gfw")]
        public long Gfw { get; set; }

        [JsonProperty("gfh")]
        public long Gfh { get; set; }

        [JsonProperty("gfw2")]
        public double Gfw2 { get; set; }

        [JsonProperty("gfh2")]
        public double Gfh2 { get; set; }

        [JsonProperty("g2cv")]
        public long G2Cv { get; set; }

        [JsonProperty("g2fi")]
        public Fi G2Fi { get; set; }

        [JsonProperty("g2fw")]
        public long G2Fw { get; set; }

        [JsonProperty("g2fh")]
        public long G2Fh { get; set; }

        [JsonProperty("g2fw2")]
        public double G2Fw2 { get; set; }

        [JsonProperty("g2fh2")]
        public double G2Fh2 { get; set; }

        [JsonProperty("fr")]
        public long Fr { get; set; }

        [JsonProperty("gfr")]
        public double Gfr { get; set; }

        [JsonProperty("gr")]
        public double Gr { get; set; }

        [JsonProperty("wsp")]
        public double Wsp { get; set; }

        [JsonProperty("eaten_fr")]
        public long EatenFr { get; set; }

        [JsonProperty("sx")]
        public long Sx { get; set; }

        [JsonProperty("sy")]
        public long Sy { get; set; }

        [JsonProperty("distance")]
        public double Distance { get; set; }
    }

    public partial class Fi
    {
    }

    public partial class Food
    {
        public static Food FromJson(string json) => JsonConvert.DeserializeObject<Food>(json, SlitherModel.Source.Converter.Settings);
    }

    public static class SerializeFood
    {
        public static string ToJson(this Food self) => JsonConvert.SerializeObject(self, SlitherModel.Source.Converter.Settings);
    }

}
