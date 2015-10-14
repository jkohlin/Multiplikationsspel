// © Johan Kohlin
using System;
using System.Timers;


namespace Multiplikationsspel
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			bool playing = true;	
			while (playing){
				Console.WriteLine ("Välj en tabell melan 1-12 som du vill öva på. Ange 0 för att öva på alla.");
				new Game (int.Parse (Console.ReadLine()));
				Console.WriteLine ("Vill du köra igen? [Y] [N]");
				if (Console.ReadLine().ToLower() == "n"){
					playing = false;
				}
				Console.Clear ();
			}
			Environment.Exit (0);
		}
	}
	class Question
	{
		public int left;
		public int right;
		public int correctAnswer;
		public int yourAnswer;
		public bool success;
		private int newRandom(){
			Random rnd = new Random();
			return rnd.Next (1, 12);
		}
		public Question(int aTable){
			if (aTable == 0) {
				left = newRandom();
			} else if(1 <= aTable && aTable <= 12){
				left = aTable;
			} else{
				left = 133;
				Console.WriteLine ("Rackla inte ful din tjavo. Nu får du ett skitsvårt tal bara för det.");
			}
			right = newRandom ();
			correctAnswer = left * right;
			success = false;
		}	
	}

	class Game{
		static int score; // access via Game.score
		//bool playing = true;
		int index = 0;
		private Timer gameTime = new Timer(10000); // skapa en timer på 20 s.
		Question[] questions = new Question[1]; // new array to hold questions
		private void startGame(int aTable){    // game loop: adds new question to array 
			while (gameTime.Enabled) {
				Console.Clear();
				questions [index] = new Question (aTable);
				string questionString = "Vad är " + questions[index].left + " x " + questions[index].right + " ?";
				Console.WriteLine(questionString); // ask the question via Console
				questions [index].yourAnswer = int.Parse (Console.ReadLine ()); //Read answer
				if (questions [index].yourAnswer == questions [index].correctAnswer) { //compare answers
					Game.score++; // add 1 to score if correct
					questions [index].success = true; // makes it easier to sort out correct answers
				}
				if (!gameTime.Enabled) {
					summary ();
					//playing = false;
				}
				Array.Resize(ref questions, (++index + 1));

			}
		}
		private void summary(){
			string showErrors = "";
			int successCount = 0;
			int totalCount = 0;
			foreach (Question element in questions)
			{
				totalCount++;
				if(!element.success){
					showErrors += element.left + "x" + element.right + " är inte " + element.yourAnswer + " utan " + element.correctAnswer + "\n"; 
				} else{
					successCount++;
				}
			}
			Console.WriteLine ("Bra jobbat. Du har nu svarat rätt på " + successCount + " frågor av " + totalCount);
			Console.WriteLine(showErrors);

		}
		public Game(int aChoice){
			Game.score = 0;
			gameTime.AutoReset = false;
			gameTime.Start();
			startGame (aChoice);
		}
	}
}
