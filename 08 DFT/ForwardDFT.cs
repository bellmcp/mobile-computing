using System;
using System.IO;

public class MyProg {

	public static void Main(string[] Args) {
	
		if (Args.Length != 3) {
			
			Console.WriteLine("ForwardDFT.exe [input_file] [sampling_rate] [output_file]");
			return;
		}
		
		string input_file = Args[0];
		double sampling_rate = Convert.ToDouble(Args[1]);
		string output_file = Args[2];
				
		int N = 0;
		
		StreamReader SR = new StreamReader(input_file);
		while (SR.ReadLine() != null) N++;
		SR.Close();
		
		if (N % 2 != 0) {
			
			Console.WriteLine("N must be an even number.");
			return;
		}
		
		double[] x = new double[N];
		
		SR = new StreamReader(input_file);
		for (int i = 0; i < N; i++) {

			x[i] = Convert.ToDouble(SR.ReadLine());
		}
		SR.Close();
		
		// page 158: The Scientist and Engineer's Guide to Digital Signal Processing
		
		double[] ReX = new double[N/2+1];
		double[] ImX = new double[N/2+1];
		
		for (int k = 0; k < N/2+1; k++) {
			ReX[k] = ImX[k] = 0;
			for (int i = 0; i < N; i++) {
				ReX[k] += x[i] * Math.Cos((double)2 * Math.PI * (double)k * (double)i / (double)N);
				ImX[k] += -x[i] * Math.Sin((double)2 * Math.PI * (double)k * (double)i / (double)N);
			}
		}
		
		// page 153: The Scientist and Engineer's Guide to Digital Signal Processing
		
		double[] ReX_ = new double[N/2+1];
		double[] ImX_ = new double[N/2+1];
		
		for (int k = 0; k < N/2+1; k++) {
			
			ReX_[k] = ReX[k] / (double)(N/2);
			ImX_[k] = -ImX[k] / (double)(N/2);
			
			// exception cases
			if (k == 0) ReX_[0] = ReX[0] / (double)N;
			if (k == N/2) ReX_[N/2] = ReX[N/2] / (double)N;
		}		
				
		StreamWriter SW = new StreamWriter(output_file);
		
		SW.WriteLine("k\tReX_[k]\tImX_[k]\tfrequency");
		for (int k = 0; k < N/2+1; k++) {
			
			double fraction = (double)k / (double)N;
			SW.WriteLine(k + "\t" + ReX_[k] + "\t" + ImX_[k] + "\t" + String.Format("{0:0.000000}", fraction * sampling_rate));
		}
		
		SW.Close();		
		
		return;
	}
}