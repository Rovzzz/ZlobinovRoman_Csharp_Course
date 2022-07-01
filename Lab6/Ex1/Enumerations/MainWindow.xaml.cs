using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using StressTest;

namespace Enumerations
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Material[] materialsList = (Material[])System.Enum.GetValues(typeof(Material));
            for (int i = 0; i < materialsList.Length; i++)
            {
                materials.Items.Add(materialsList[i]);
            }

            CrossSection[] crossSectionList = (CrossSection[])System.Enum.GetValues(typeof(CrossSection));
            for (int i = 0; i < crossSectionList.Length; i++)
            {
                crossSections.Items.Add(crossSectionList[i]);
            }

            TestResult[] testResultsList = (TestResult[])System.Enum.GetValues(typeof(TestResult));
            for (int i = 0; i < testResultsList.Length; i++)
            {
                testResults.Items.Add(testResultsList[i]);
            }

            materials.SelectedIndex = 0;
            crossSections.SelectedIndex = 0;
            testResults.SelectedIndex = 0;
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (materials.SelectedIndex == -1 || crossSections.SelectedIndex == -1 || testResults.SelectedIndex == -1) return;
            Material selectedMaterial = (Material)materials.SelectedItem;
            CrossSection selectedCrossSection = (CrossSection)crossSections.SelectedItem;
            TestResult selectedTestResult = (TestResult)testResults.SelectedItem;

            StringBuilder selectionStringBuilder = new StringBuilder();

            switch (selectedMaterial)
            {
                case Material.StainlessSteel:
                    selectionStringBuilder.Append("Материал: Нержавеющая сталь, ");
                    break;
                case Material.Aluminium:
                    selectionStringBuilder.Append("Материал: Алюминий, ");
                    break;
                case Material.ReinforcedConcrete:
                    selectionStringBuilder.Append("Материал: Железобетон, ");
                    break;
                case Material.Composite:
                    selectionStringBuilder.Append("Материал: Композитный, ");
                    break;
                case Material.Titanium:
                    selectionStringBuilder.Append("Материал: Титан, ");
                    break;
            }

            switch (selectedCrossSection)
            {
                case CrossSection.IBeam:
                    selectionStringBuilder.Append("Поперечное сечение: Двутавровая балка, ");
                    break;
                case CrossSection.Box:
                    selectionStringBuilder.Append("Поперечное сечение: Коробка, ");
                    break;
                case CrossSection.ZShaped:
                    selectionStringBuilder.Append("Поперечное сечение: Z-образное, ");
                    break;
                case CrossSection.CShaped:
                    selectionStringBuilder.Append("Поперечное сечение: С-образное, ");
                    break;
            }

            switch (selectedTestResult)
            {
                case TestResult.Pass:
                    selectionStringBuilder.Append("Результат: Проходит.");
                    break;
                case TestResult.Fail:
                    selectionStringBuilder.Append("Результат: Сбой.");
                    break;
            }
            testDetails.Content = selectionStringBuilder.ToString();
        }
    }
}
