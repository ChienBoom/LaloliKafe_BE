namespace LoveKafe_BE.Models
{
    public class ResultData<T>
    {
        public int TotalCount { get; set; }
        public List<T> Data { get; set; }
    }
}
