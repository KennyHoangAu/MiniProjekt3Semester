using System;

namespace Data
{
	public class SubjectData
	{
		// Properties
		public int Id { get; set;}
		public string Name { get; set;}


		// Konstruktør
		public SubjectData(string name)
		{
			this.Name = name;
		}
	}
}
