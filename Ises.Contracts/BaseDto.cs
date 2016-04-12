namespace Ises.Contracts
{
    public class BaseDto
    {
        public BaseDto()
        {
            MappingScheme = "FullGraph";
        }
        public string MappingScheme { get; set; }
    }
}
