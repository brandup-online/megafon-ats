namespace MegafonATS.Models.Attributes
{
    public class MapNameAttribute : Attribute
    {
        public MapNameAttribute(string Name)
        {
            this.Name = Name;
        }
        public string Name { get; set; }
    }
}
