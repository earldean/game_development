using System;

/// <summary>
/// Main class. 
/// Calculates distance and angle between two points
/// </summary>
class MainClass
{
	// point 1		
	private float x1;
	private float y1;

	// point 2
	private float x2;
	private float y2;

	/// <summary>
	/// Run this instance.
	/// </summary>
	public void Run ()
	{
		Console.WriteLine ("Welcome, this program accepts the coordinates of two points " +
			"and calculates the distance and angle between them" + "\n");

		// promt user for x and y values of both points
		Console.WriteLine ("Enter x value of first point" + "\n");
		this.x1 = float.Parse (Console.ReadLine ());

		Console.WriteLine ("Enter y value of first point" + "\n");
		this.y1 = float.Parse (Console.ReadLine ());

		Console.WriteLine ("Enter x value of second point" + "\n");
		this.x2 = float.Parse (Console.ReadLine ());

		Console.WriteLine ("Enter y value of second point" + "\n");
		this.y2 = float.Parse (Console.ReadLine ());

		double distance = this.Distance (this.x1, this.y1, this.x2, this.y2);

		double angle = Math.Atan2 (this.x2 - this.x1, this.y2 - this.y1) * (180 / Math.PI);

		Console.WriteLine ("The distance between the two points is: " + String.Format("{0:#, 0.000}", distance) 
			+ "\n" + " The angle between the points is: " + angle);
	}

	/// <summary>
	/// Calculates the distance between two points
	/// </summary>
	/// <param name="x1">The first x value.</param>
	/// <param name="y1">The first y value.</param>
	/// <param name="x2">The second x value.</param>
	/// <param name="y2">The second y value.</param>
	public double Distance (float x1, float y1, float x2, float y2)
	{
		return Math.Sqrt (Math.Pow((x2 - x1), 2) + Math.Pow((y2 - y1), 2));
	}

	/// <summary>
	/// The entry point of the program, where the program control starts and ends.
	/// </summary>
	/// <param name="args">The command-line arguments.</param>
	public static void Main (string[] args)
	{
		MainClass mainClass = new MainClass ();
		mainClass.Run ();
	}
}
