using System;

namespace Data
{
    public class QuestionData
    {
        // Propterties
        public int Id { get; set; }
        public SubjectData Subject { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Username { get; set; }
        public DateTime Date { get; set; }
        public int Upvote { get; set; }
        public int Downvote { get; set; }

        public List<AnswerData> Answers { get; set; }

        // Konstrukt�rer
        public QuestionData() { }

        public QuestionData(SubjectData subject, string title, string text, string username, DateTime date)
        {
            this.Subject = subject;
            this.Title = title;
            this.Text = text;
            this.Username = username;
            this.Date = date;
        }
    }
}