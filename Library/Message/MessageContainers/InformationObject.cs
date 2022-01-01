namespace ROELibrary
{
    class InformationObject
    {
        public EInformationSymbols setting { get; set; }
        public EMessageSymbols? settingStatus { get; set; } //information if setting is to set on robot or get from robot(set or get)
        public string value { get; set; }
    }
}
