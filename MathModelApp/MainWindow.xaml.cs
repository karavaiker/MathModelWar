using System;
using System.Collections.Generic;
using System.Data;
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
using System.Xml;
using System.Xml.Linq;

namespace MathModelApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AlgoritmP AP = new AlgoritmP(); //Далее обращаемся к алгоритму П через AP;
        public MainWindow()
        {
            InitializeComponent();
            FormularP7inTextArea();
            ResetData();
            ShowGrid(WelcomeSlide);
            BindingDataWindow();

            
            
        }

        //Перерасчет
        void ResetData() 
        {
            startFindNullTextBox();
            SaveXMLFormular();
            //datasetAttack.WriteXml("xml/FormularP81.xml"); 
            //datasetDefender.WriteXml("xml/FormularP82.xml");              
            AP.Starter(); // Запуск алгоритма П;            
            CreateVolumeXMLColumnsAttack();
            CreateVolumeXMLColumnsDefender();
            WriteDataMainVolume();
            SetDataForChart();
            RecomendText.Text = AP.RecomendationsText();
            
        }

        private void FormularP7inTextArea()  //Вывод переменных из xml в поля для ввода
        {
            XElement root = XElement.Load("xml/FormularP7.xml");            
            IEnumerable<XElement> tests =
                from el in root.Elements("VariableInformation")
                //where (string)el.Element("j") == Convert.ToString(j)
                select el;
            foreach (XElement el in tests)
            {
                //Вывод переменных из xml в поля для ввода

                SizeAreaDefenseAlongFrontText.Text = (string)el.Element("SizeAreaDefenseAlongFront");
                SizeAreaDefenseAlongDepthText.Text = (string)el.Element("SizeAreaDefenseAlongDepth");
                AutopsiesDgreeObjectsAttackText.Text = (string)el.Element("AutopsiesDgreeObjectsAttack");
                DegreeOfDestructionByFireText.Text = (string)el.Element("DegreeOfDestructionByFire");
                DepthHerniationAttackText.Text = (string)el.Element("DepthHerniationAttack");
                ProportionOfFalseObjectsText.Text = (string)el.Element("ProportionOfFalseObjects");
                ProportionOfKilledText.Text = (string)el.Element("ProportionOfKilled");
                TimeToPrepareDefenceText.Text = (string)el.Element("TimeToPrepareDefence");

                FrontOffensiveText.Text = (string)el.Element("FrontOffensive");
                GivenDepthText.Text = (string)el.Element("GivenDepth");
                PeriodOfExecutionText.Text = (string)el.Element("PeriodOfExecution");
                SpeedOfMovementText.Text = (string)el.Element("SpeedOfMovement");
                CoeffSpeedRedutionWhenManeuveringText.Text = (string)el.Element("CoeffSpeedRedutionWhenManeuvering");
                AutopsiesDgreeObjectsDefenderText.Text = (string)el.Element("AutopsiesDgreeObjectsDefender");
                DegreeOfDestructionByFireDefenderText.Text = (string)el.Element("DegreeOfDestructionByFireDefender");
                AddLossesInMeleeText.Text = (string)el.Element("AddLossesInMelee");
                ShareDestroyMeansOfAirDefenderText.Text = (string)el.Element("ShareDestroyMeansOfAirDefender");
                DegreeOfExcellenceInManagementText.Text = (string)el.Element("DegreeOfExcellenceInManagement");
                DegreeOfExcellenceInLevelOfCombatCapabilityText.Text = (string)el.Element("DegreeOfExcellenceInLevelOfCombatCapability");
            }
        }

        //Проверка TextBox на 0 значение
        void FindNullInTextBox(TextBox TBox)
        {
            if (TBox.Text == "")
            {
                TBox.Text = "0";
            }
        }
        void startFindNullTextBox()
        {
            FindNullInTextBox(SizeAreaDefenseAlongFrontText);
            FindNullInTextBox(SizeAreaDefenseAlongDepthText);
            FindNullInTextBox(AutopsiesDgreeObjectsAttackText);
            FindNullInTextBox(DegreeOfDestructionByFireText);
            FindNullInTextBox(DepthHerniationAttackText);
            FindNullInTextBox(ProportionOfFalseObjectsText);
            FindNullInTextBox(ProportionOfKilledText);
            FindNullInTextBox(TimeToPrepareDefenceText);
            FindNullInTextBox(FrontOffensiveText);
            FindNullInTextBox(GivenDepthText);
            FindNullInTextBox(PeriodOfExecutionText);
            FindNullInTextBox(SpeedOfMovementText);
            FindNullInTextBox(CoeffSpeedRedutionWhenManeuveringText);
            FindNullInTextBox(AutopsiesDgreeObjectsDefenderText);
            FindNullInTextBox(DegreeOfDestructionByFireDefenderText);
            FindNullInTextBox(AddLossesInMeleeText);
            FindNullInTextBox(ShareDestroyMeansOfAirDefenderText);
            FindNullInTextBox(DegreeOfExcellenceInManagementText);
            FindNullInTextBox(DegreeOfExcellenceInLevelOfCombatCapabilityText);
        }
 
       
        
        private void SaveXMLFormular()
        {

            XDocument xDoc = XDocument.Load("xml/FormularP7.xml");
            IEnumerable<XElement> xElms;
            xElms = xDoc.Descendants("VariableInformation");
                
               
                
                //Вывод переменных из xml в поля для ввода
            foreach (XElement el in xElms)
	            {
                el.Element("SizeAreaDefenseAlongFront").Value = SizeAreaDefenseAlongFrontText.Text;
                el.Element("SizeAreaDefenseAlongDepth").Value = SizeAreaDefenseAlongDepthText.Text;
                el.Element("AutopsiesDgreeObjectsAttack").Value = AutopsiesDgreeObjectsAttackText.Text;
                el.Element("DegreeOfDestructionByFire").Value = DegreeOfDestructionByFireText.Text;         
                el.Element("DepthHerniationAttack").Value = DepthHerniationAttackText.Text ;            
                el.Element("ProportionOfFalseObjects").Value = ProportionOfFalseObjectsText.Text ;         
                el.Element("ProportionOfKilled").Value = ProportionOfKilledText.Text ;               
                el.Element("TimeToPrepareDefence").Value = TimeToPrepareDefenceText.Text ;             
                el.Element("FrontOffensive").Value = FrontOffensiveText.Text ;                   
                el.Element("GivenDepth").Value = GivenDepthText.Text;                        
                el.Element("PeriodOfExecution").Value = PeriodOfExecutionText.Text;                 
                el.Element("SpeedOfMovement").Value = SpeedOfMovementText.Text;                   
                el.Element("CoeffSpeedRedutionWhenManeuvering").Value = CoeffSpeedRedutionWhenManeuveringText.Text; 
                el.Element("AutopsiesDgreeObjectsDefender").Value = AutopsiesDgreeObjectsDefenderText.Text;     
                el.Element("DegreeOfDestructionByFireDefender").Value = DegreeOfDestructionByFireDefenderText.Text; 
                el.Element("AddLossesInMelee").Value = AddLossesInMeleeText.Text ;                 
                el.Element("ShareDestroyMeansOfAirDefender").Value = ShareDestroyMeansOfAirDefenderText.Text  ;
                el.Element("DegreeOfExcellenceInManagement").Value = DegreeOfExcellenceInManagementText.Text;
                el.Element("DegreeOfExcellenceInLevelOfCombatCapability").Value = DegreeOfExcellenceInLevelOfCombatCapabilityText.Text;
                }

            xDoc.Save("xml/FormularP7.xml");
        }

        private void BindingDataWindow()
        {
            XElement DataWarList = XElement.Load("xml/FormularP81.xml");
            EditAttackOTF.DataContext = DataWarList;
        }

        private void MainWindowApp_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.F11)
            {
                AP.DebugerText();
            }
            
        }

///////////// Навигация


        private void ResetBackgroundButtons(object sender)
        {
            EditForcesButton.Background = null;
            EditValueButton.Background = null;
            ExitValueButton.Background = null;
            SchemaButton.Background = null;
            EditFormulars.Background = null;
            RecomendationsButton.Background = null;
            //
            ((Button)sender).Background = Brushes.LightSeaGreen;
        }

        private void ShowGrid(Grid VisibleElement)
        {
            WelcomeSlide.Visibility = Visibility.Hidden;
            
            
            EditForcesSlide.Visibility = Visibility.Hidden;
            EditValueSlide.Visibility = Visibility.Hidden;
            ExitValueSlide.Visibility = Visibility.Hidden;
            Schema1.Visibility = Visibility.Hidden;
            RecomendationsSlide.Visibility = Visibility.Hidden;
			WelcomeSlide.Visibility = Visibility.Hidden;
            EditorOTF.Visibility = Visibility.Hidden;
            //
            VisibleElement.Visibility = Visibility.Visible;
        }

        private void EditForcesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowGrid(EditForcesSlide);
            ResetBackgroundButtons(sender);
        }

        private void EditValueButton_Click(object sender, RoutedEventArgs e)
        {
            ShowGrid(EditValueSlide);
            ResetBackgroundButtons(sender);
        }

        private void ExitValueButton_Click(object sender, RoutedEventArgs e)
        {
            ShowGrid(ExitValueSlide);
            ResetBackgroundButtons(sender);
        }

        private void SchemaButton_Click(object sender, RoutedEventArgs e)
        {
            ShowGrid(Schema1);
            ResetBackgroundButtons(sender);
        }

        private void RecomendationsButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ShowGrid(RecomendationsSlide);
            ResetBackgroundButtons(sender);
        }

        private void EditFormulars_Click(object sender, RoutedEventArgs e)
        {
            EditFormulars editformulars = new EditFormulars();
            editformulars.Show();
            editformulars.Activate();
        }

//////////// Чтение XML ОТФ СТОРОН
        DataSet datasetAttack = new DataSet();
        DataSet datasetDefender = new DataSet();
        private void CreateVolumeXMLColumnsAttack()
        {
            datasetAttack.Reset();
            datasetAttack.ReadXml("xml/FormularP81.xml");
            DataView dataView = new DataView(datasetAttack.Tables[0]);
            AboutOTFAttack.ItemsSource = dataView;                   
        }

        private void CreateVolumeXMLColumnsDefender()
        {
            datasetDefender.Reset();
            datasetDefender.ReadXml("xml/FormularP82.xml");
            DataView dataView = new DataView(datasetDefender.Tables[0]);
            AboutOTFDeffender.ItemsSource = dataView;                 
        }
////Запись переменных в таблицу основных показателей
        private void WriteDataMainVolume()
        {
            WeaponesAttack.Content = Math.Round(AP.OWeaponesAttack, 2);
            WeaponesDefender.Content = Math.Round(AP.OWeaponesDefender, 2);

            AverageDensityAttack.Content = Math.Round(AP.OWeaponesAttack / AP.FrontOffensive, 2);
            AverageDensityDefender.Content = Math.Round(AP.OWeaponesDefender / AP.SizeAreaDefenseAlongFront, 2);
            
            InitialSuperiorityAttack.Content = Math.Round(AP.OInitialSuperiorityInAttacking, 2);
            InitialSuperiorityDefender.Content = Math.Round((1 / AP.OInitialSuperiorityInAttacking), 2);

            CalculationOfFireAttack.Content = Math.Round(AP.OCalculationOfFireSuperiorityUpcoming, 2);
            CalculationOfFireDefender.Content = Math.Round(1 / AP.OCalculationOfFireSuperiorityUpcoming, 2);

            DegreeOfDamadeAttak.Content = Math.Round(AP.ODegreeOfFireDamageAttacking * 100, 2) + "%";
            DegreeOfDamageDefender.Content = Math.Round(AP.ODegreeOfFireDamageDefender * 100, 2) + "%";
            
            PosibleDepthPromotion.Content = Math.Round(AP.OPossibleDepthPromotion, 2);
            Givendepth.Content = Math.Round(AP.GivenDepth, 2);

            StabilityOfDefense.Content = Math.Round(AP.OStabilityOfDefense, 2);

            LossSidesMeleeAttack.Content = Math.Round(AP.OLossSidesMeleeAttack * 100, 2) + "%";
            LossSidesMeleeDefender.Content = Math.Round(AP.OLossSidesMeleeDefender * 100, 2) + "%";

            TotalLossAttack.Content = Math.Round(AP.OTotalLossAttack * 100, 2) + "%";
            TotalLossDefender.Content = Math.Round(AP.OTotalLossDefender * 100, 2) + "%";

            ExpectedTempo.Content = Math.Round(AP.OExpectedTempoAttack, 2);

            TimingAdvance.Content = Math.Round(AP.OTimingAdvanceToTheDepth, 2);

            ProbabilityAttack.Content = Math.Round(AP.OProbabilityOneShot1, 2);
            ProbabilityDefender.Content = Math.Round(1 - AP.OProbabilityOneShot1, 2);
        }
//////////Фукнкция расчета коэффицентов для граф отображения переменных
        Random rand = new Random();
        void FuncGrafValue(double Attack, double Defender,  ProgressBar Gred , ProgressBar Gblue)
        {
            double widthGraf;            
            widthGraf = rand.Next(6, 10) * 10;

            double ratio;

            if (Attack > Defender)
            {
                ratio = Defender / Attack;
                Gred.Value = widthGraf;
                Gblue.Value = ratio * widthGraf;
            }
            else
            {
                ratio = Attack / Defender;
                Gred.Value = ratio * widthGraf;
                Gblue.Value = widthGraf;
            }
        }

        void FuncGrafValueforPercent(double Attack, double Defender,  ProgressBar Gred , ProgressBar Gblue)
        {
            Gred.Value = Attack * 100;
            Gblue.Value = Defender * 100;
        }
///Установим значения графиков
        void SetDataForChart()
        {
            FuncGrafValue(AP.OWeaponesAttack, AP.OWeaponesDefender, WeaponesAttackProgr, WeaponesDefenderProgr);
            FuncGrafValue(AP.OWeaponesAttack / AP.FrontOffensive, AP.OWeaponesDefender / AP.SizeAreaDefenseAlongFront, AverageDensityAttProg, AverageDensityDefProg);
            FuncGrafValue(AP.OInitialSuperiorityInAttacking, 1 / AP.OInitialSuperiorityInAttacking, InitialSuperiorityChatrAttack, InitialSuperiorityChatrDefender);
            FuncGrafValue(AP.OCalculationOfFireSuperiorityUpcoming, 1 / AP.OCalculationOfFireSuperiorityUpcoming, CalculationOffireAttackChart, CalculationOffireDefenderChart);
            FuncGrafValueforPercent(AP.ODegreeOfFireDamageAttacking, AP.ODegreeOfFireDamageDefender, DegreeofAttackProg, DegreeofDefenderProg);
            FuncGrafValue(AP.OPossibleDepthPromotion, AP.GivenDepth, PosibleDepthProg, GivenDepthProg);
            FuncGrafValueforPercent(AP.OLossSidesMeleeAttack, AP.OLossSidesMeleeDefender, LossSidesMeleeAttackChart, LossSidesMeleeDefenderChart);
            FuncGrafValueforPercent(AP.OTotalLossAttack, AP.OTotalLossDefender, TotalLossAttackChart, TotalLossDefenderChart);
            FuncGrafValue(AP.OProbabilityOneShot1, 1 - AP.OProbabilityOneShot1, ProbabilityAttackChart, ProbabilitydefenderChart);
        }
//Элементы управления
        private void ellipse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ResetData();
        }

        private void PlusOTFButton_Click(object sender, RoutedEventArgs e)
        {
            EditorOTF.Visibility = Visibility.Visible;            
        }

        private void CloseWindowEdit_Click(object sender, RoutedEventArgs e)
        {
            EditorOTF.Visibility = Visibility.Hidden;
        }


          






    }
}
