namespace BioInfo.Web.Analytics.Services.Interfaces


type IStorageServices =
    abstract member WriteToStorage: seq<'a> -> bool