using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
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

namespace UOP.Agent
{
    /// <summary>
    /// Interaction logic for TestAnalysisControl.xaml
    /// </summary>
    public partial class TestAnalysisControl : UserControl
    {
        public TestAnalysisControl(RevitApiAnalysis content)
        {
            InitializeComponent();

			   Conclusion.Text = content.test_group_final_suggestion_or_conclusion;
         ApprovedTests.Text = content.successful_tests_number;
         FailedTests.Text = content.failed_tests_number;

			AppendItemList(Description, content.step_by_step_description_of_the_test_workflow);
			AppendItemList(Suggestions, content.suggestions);

			Insights.Text = content.clever_insights;
		}

		public void AppendItemList(StackPanel control, List<string> list)
		{
			foreach (var item in list)
			{
				var a = new TextBlock();
				a.Text = $"- {item}";
				a.Margin = new Thickness(0, 10, 0, 0);
				control.Children.Add(a);
			}
		}
	}
}
