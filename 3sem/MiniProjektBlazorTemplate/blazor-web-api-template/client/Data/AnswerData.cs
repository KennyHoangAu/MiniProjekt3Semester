using System;

namespace Data
{
	public class AnswerData
	{
		// Properties
		public int Id { get; set; }
		public QuestionData Question { get; set; }
		public string Text { get; set; }
		public string Username { get; set; }
		public DateTime Date { get; set; }
		public int Upvote { get; set; }
		public int Downvote { get; set; }


		// Konstruktører
		public AnswerData() { }

		public AnswerData(QuestionData question, string text, string username, DateTime date) 
		{ 
			this.Question = question;
			this.Text = text;
			this.Username = username;
			this.Date = date;
		}
	}
}