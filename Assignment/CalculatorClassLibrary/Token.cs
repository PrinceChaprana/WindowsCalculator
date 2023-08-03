namespace CalculatorClassLibrary
{
    public class Token
    {
        public string Value { get; set; }
        public TokenTypeEnum TokenType { get; set; }

        public Token(string value ,TokenTypeEnum tokenType) {
            this.Value = value;
            this.TokenType = tokenType;
        }
    }
}