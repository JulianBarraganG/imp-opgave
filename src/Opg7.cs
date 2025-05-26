using ScottPlot;
using System.Diagnostics;
public class Opg7
{
	public static void Run()
	{
		Int32 n = (1 << 16) + 1;
		Int32 l = 16;
		UInt16[] t_values = { 6, 12, 24 }; 
		Stopwatch sw = new Stopwatch();
		Tuple<ulong, int>[] stream = Stream.CreateStream(n, l).ToArray();


		//We run MulShiftHas to get the true sum in the fastest way
		sw.Start();
		MulShiftHash mulShiftHash = new MulShiftHash((UInt16)l);
		HashTable shifthashTable = Opg3.populateHashTable(mulShiftHash.Hash, stream, l);
		Int64 trueSum = 0;
		Int32[] X_estimates = new Int32[100];

		foreach (var list in shifthashTable.table)
		{
			if (list != null)
			{
				foreach (var item in list)
				{
					trueSum += item.Item2 * item.Item2;
				}
			}
		}

		sw.Stop(); 
		Console.WriteLine($"MulShiftHas spent {sw.ElapsedMilliseconds} ms");

		foreach (UInt16 t in t_values)
		{

			for (int i = 0; i < 100; i++)
			{
				if (i == 0) { sw.Restart(); }
				FourUniHash hash = new FourUniHash();
				Int32[] C = hash.GetSketch(stream, t);
				Int32 estimate = hash.CountSketch(C);
				if (i == 0) { sw.Stop(); }
				X_estimates[i] = estimate;
			}


			Int32[] sorted_estimates = new Int32[100];
			X_estimates.CopyTo(sorted_estimates, 0);
			Array.Sort(sorted_estimates);


			Int64 MSE = 0;

			for (int i = 0; i < 100; i++)
			{

				MSE += (X_estimates[i] - trueSum) * (X_estimates[i] - trueSum);
				//Console.WriteLine($"X {i} : {sorted_estimates[i]}");
			}
			MSE = MSE / 100;

			//Console.WriteLine($"True value: {trueSum}");
			Console.WriteLine($"For m = {1 << t} it took {sw.ElapsedMilliseconds}ms to get an estimate");
			Console.WriteLine($"Over 100 estimates, the Mean Squared Error was: {MSE}");
			Console.WriteLine($"The expected Mean Squared Error was: {(2 * trueSum * trueSum) / (1 << t)}");

			Int32[] medianList = new Int32[9];
			Int32[] temp_list = new Int32[11];
			

			for (int i = 0; i < 100; i++)
			{
				if (i % 11 == 0 && i != 0)
				{
					Array.Sort(temp_list);
					//Console.WriteLine("[{0}]", string.Join(", ", temp_list));
					Int32 median = temp_list[5];
					int index = (int)i / 11 - 1;
					medianList[index] = median;

					temp_list = new Int32[11];


				}
				temp_list[i % 11] = X_estimates[i];
			}



			//Console.WriteLine("[{0}]", string.Join(", ", medianList));


			//Plotting and saving the plots

			Int32[] xs_est = Enumerable.Range(0, 100).ToArray();
			Int32[] xs_medians = Enumerable.Range(0, 9).ToArray();
			Array.Sort(medianList);

			ScottPlot.Plot sortedPlot = new();
			sortedPlot.Add.Scatter(xs_est, sorted_estimates);
			sortedPlot.Add.HorizontalLine(trueSum);
			sortedPlot.Axes.AutoScale();
			var sorted_limits = sortedPlot.Axes.GetLimits();

			sortedPlot.SavePng($"sorted_estimates_when_t_{t}.png", 800, 600);


			ScottPlot.Plot medianPlot = new();
			medianPlot.Add.Scatter(xs_est, medianList);
			medianPlot.Add.HorizontalLine(trueSum);
			medianPlot.Axes.AutoScale();
			var median_limits = medianPlot.Axes.GetLimits();


			medianPlot.Axes.SetLimits(median_limits.Left, median_limits.Right, sorted_limits.Bottom, sorted_limits.Top);
			medianPlot.SavePng($"medians_sorted_when_t_{t}.png", 800, 600);
		

		}

	}
}
