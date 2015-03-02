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
		// src: https://stackoverflow.com/questions/974598/find-all-controls-in-wpf-window-by-type
		public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
		{
			if (depObj != null)
			{
				for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
				{
					DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
					if (child != null && child is T)
					{
						yield return (T)child;
					}

					foreach (T childOfChild in FindVisualChildren<T>(child))
					{
						yield return childOfChild;
					}
				}
			}
		}

		public MainWindow()
		{
			InitializeComponent();
		}

		private void Window_Loaded(object sender, RoutedEventArgs e)
		{
			// make all labels focus their target on click
			foreach (Label label in FindVisualChildren<Label>(this))
			{
				label.MouseLeftButtonDown += Label_MouseLeftButtonDown;
			}
		}

		private void Label_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			// src: https://stackoverflow.com/questions/5825651/clicking-on-a-label-to-focus-another-control-in-wpf
			// not sure what this if is for
			if (e.ClickCount == 1)
			{
				Label label = (Label)sender;
				Keyboard.Focus(label.Target);
			}
		}

		private void runButton_Click(object sender, RoutedEventArgs e)
		{
			string input = this.inputBox.Text;

			Polynomial polynomial = new Polynomial(input);
			polynomial.Normalize();

			this.normalizedBox.Text = polynomial.ToString();
			this.derivativeBox.Text = polynomial.Differentiate();
			this.integralBox.Text = polynomial.Integrate();
		}
	}
}
