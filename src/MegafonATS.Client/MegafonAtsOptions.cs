namespace MefafonATS.Model
{
    public class MegafonAtsOptions
    {
        public string AtsName { get; set; }
        public string Token { get; set; }

        //public void Validation()
        //{
        //    if (ApiUrl == null)
        //        throw new ArgumentNullException(nameof(ApiUrl));
        //    if (!ApiUrl.IsAbsoluteUri)
        //        throw new InvalidOperationException($"Propery value is {nameof(ApiUrl)} require absolute url.");
        //}
    }
}