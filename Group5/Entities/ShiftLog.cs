namespace Group5.Entities
{
    public class ShiftLog
    {
        public int Shift_ID { get; set; }
        public int Employee_ID { get; set; }
        public string Clock_In { get; set; }
        public string Clock_Out { get; set; }
        public string Break_Start { get; set; }
        public string Break_End { get; set; }
    }
}