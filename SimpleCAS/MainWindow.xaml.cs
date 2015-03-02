using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SimpleCAS
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void runButton_Click(object sender, RoutedEventArgs e)
		{
			Console.Out.WriteLine("run!");
			string input = this.inputBox.Text;

			Polynomial polynomial = new Polynomial(input);
			polynomial.Normalize();

			this.normalizedBox.Text = polynomial.ToString();
			this.derivativeBox.Text = polynomial.Differentiate();
			this.integralBox.Text = polynomial.Integrate();
		}
	}
}
