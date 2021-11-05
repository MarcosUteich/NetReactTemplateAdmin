namespace NetReactTemplateAdmin.Responses
{
    public class Series
    {
        public string Name { get; set; }
        public int[] Data { get; set; }
    }
    public class ChartDataResponse
    {
        public Series Series { get; set; }
    }
}
