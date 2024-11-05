namespace JeopardyAPI.Models
{
    public class Question
    {
        public string QuestionText { get; set; }
        public string Answer { get; set; }
        public string Clue { get; set; }
        public int Points { get; set; }
    }
}