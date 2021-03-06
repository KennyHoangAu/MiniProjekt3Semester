using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using System;

using Data;
using Model;

namespace Service;

public class DataService
{
	private QuestionContext db { get; }

	public DataService(QuestionContext db) {
		this.db = db;
    }

    /// <summary>
    /// Seeder noget nyt data i databasen hvis det er nødvendigt.
    /// </summary>
	public void SeedData()
	{
        Subject subject = db.Subjects.FirstOrDefault()!;
        if (subject == null) {
            subject = new Subject("Teknisk");
            db.Subjects.Add(subject);
            db.Subjects.Add(new Subject("Hjælp"));
            db.Subjects.Add(new Subject("Mad"));
            db.Subjects.Add(new Subject("Andet"));
        }

        Question question = db.Questions.FirstOrDefault()!;
        if (question == null)
        {
            question = new Question(subject, "Teknisk spørgsmål", "Virker mit spørgsmål?", "Mikkel", DateTime.Now);
            db.Questions.Add(question);
        }

        Answer answer = db.Answers.FirstOrDefault()!;
        if (answer == null)
        {
            answer = new Answer(question, "Ja, dit spørgsmål virker", "Simon", DateTime.Now);
            db.Answers.Add(answer);
        }

        db.SaveChanges();
    }

    // ---------------------------------------------------------------
    // -- Questions --

    public async Task<List<Question>> ListQuestions() {
		return await db.Questions
            .Include(q => q.Subject)
		    .ToListAsync();
	}

    public Question GetQuestionById(int questionId) {
        return db.Questions
            .Where(q => q.Id == questionId)
            .Include(q => q.Subject)
            .First();
    }

    public List<Question> ListQuestionsBySubjectId(int subjectId) {
        Subject subject = db.Subjects
            .Where(s => s.Id == subjectId)
            .Last();

        return db.Questions
            .Where(q => q.Subject == subject)
            .ToList();
    }

    public string createQuestion(int subjectId, string title, string text, string username)
    {
        Subject subject = db.Subjects
            .Where(s => s.Id == subjectId)
            .First();

        Question question = new Question(subject, title, text, username, DateTime.Now);

        db.Questions.Add(question);
        db.SaveChanges();

        return JsonSerializer.Serialize(
            new { msg = "New question created", newQuestion = question.Title }
        );
    }

    public string updateQuestionByIdUpvote(int questionId)
    {
        Question question = db.Questions
            .Where(q => q.Id == questionId)
            .First();

        question.Upvote++;

        db.Questions.Update(question);
        db.SaveChanges();

        return JsonSerializer.Serialize(
            new { msg = "Question upvoted", updatedQuestion = question.Title }
        );
    }

    public string updateQuestionByIdDownvote(int questionId)
    {
        Question question = db.Questions
            .Where(q => q.Id == questionId)
            .First();

        question.Downvote++;

        db.Questions.Update(question);
        db.SaveChanges();

        return JsonSerializer.Serialize(
            new { msg = "Question downvoted", updatedQuestion = question.Title }
        );
    }

    // ---------------------------------------------------------------
    // -- Subjects --

    public List<Subject> ListSubjects() {
        return db.Subjects
            .ToList();
    }

    public Subject GetSubjectById(int subjectId) {
        return db.Subjects
            .Where(s => s.Id == subjectId)
            .First();
    }

    // ---------------------------------------------------------------
    // -- Answers --

    public List<Answer> ListAnswersByQuestionId(int questionId) {
        return db.Questions
            .Where(q => q.Id == questionId)
            .SelectMany(a => a.Answers)
            .ToList();
    }

    public string CreateAnswer(int questionId, string text, string username) {
        Question question = db.Questions
            .Where(q => q.Id == questionId)
            .First();

        Answer answer = new Answer(question, text, username, DateTime.Now);

        db.Answers.Add(answer);
        db.SaveChanges();

        return JsonSerializer.Serialize(
            new { msg = "New answer created", newAnswer = answer.Text }
        );
    }

    public string updateAnswerByIdUpvote(int answerId)
    {
        Answer answer = db.Answers
            .Where(a => a.Id == answerId)
            .First();

        answer.Upvote++;

        db.Answers.Update(answer);
        db.SaveChanges();

        return JsonSerializer.Serialize(
            new { msg = "Answer upvoted", updatedAnswer = answer.Text }
        );
    }

    public string updateAnswerByIdDownvote(int answerId)
    {
        Answer answer = db.Answers
            .Where(a => a.Id == answerId)
            .First();

        answer.Downvote++;

        db.Answers.Update(answer);
        db.SaveChanges();

        return JsonSerializer.Serialize(
            new { msg = "Answer downvoted", updatedAnswer = answer.Text }
        );
    }


    // ---------------------------------------------------------------

}
