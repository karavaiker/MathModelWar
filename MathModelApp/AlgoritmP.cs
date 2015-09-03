using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MathModelApp
{
    class AlgoritmP
    {
               
        public void Starter() 
        {
            GetFormularP1();
            GetFormularP7();
            ProbabilisticPerformanceDuelFightOneUBE();
            InitialSuperiorityInAttacking();

            // П17 Проверка Степень огневого поражения обороняющихся от РВ и А авиации и ВТО наступающих задана?
            if (DegreeOfDestructionByFireDefender > 0)
            {
                ODegreeOfFireDamageDefender = DegreeOfDestructionByFireDefender;
            }
            else
            {
                SummSVNforIAttack();
                SummTheFireImpactAttack();
                TotalQuantityOfAmmunitionToSuppress();
                DegreeOfFireDamageDefender();                
            }

            // П20 Задана доля ложных объектов у обороняющихся?
            if (ProportionOfFalseObjects == 0)
            {
                double PosobilityDefender = 0.07 ; // возможности обороняющихся по сооружению ложных объектов за 1 час (bo   0,07).
                ProportionOfFalseObjects = TimeToPrepareDefence * PosobilityDefender;
            }

            // П30 Анализ: задана степень огневого поражения наступающих от РВ и А, ВТО и авиации обороняющихся?
            if (DegreeOfDestructionByFire > 0)
            {
                ODegreeOfFireDamageAttacking = DegreeOfDestructionByFire;
            }
            else
            {
                SummTheFireImpactDefender();
                TotalQuantityOfAmmunitionToSuppressfordefender();
                DegreeOfFireDamageAttacking();
            }

            СalculationOfFireSuperiorityUpcoming();
            ExpectedTempoAttack();
            StartP42toP45();
            RequiredSettings();
            LossCalculationSides();
            ProbabilityCharacteristicsOfTheCombatMission();           
            
        }



        //AlgoritmP2 MemVal = new AlgoritmP2(); // выделяю память для хранения конечных значений функции;
        TextRezult OutResult = new TextRezult();
        
        

        //Это промежуточный вывод в отладочное окно
        public void DebugerText()
        {
            ForTestAlgoritms TestWindow = new ForTestAlgoritms(); ///выделяю память для окна отладочной информации
            OutResult.Starter();
            TestWindow.TextAlgoritmP.Text = OutResult.FinalText;
            TestWindow.Show();
                  
            OutResult.ExitP14 += "Определение фактической скорострельности одной УБЕ  обороняющейся стороны: " + OActualRateOneUBE + "\n Pасчет вероятности уничтожения танка контратакующих одним выстрелом танка отражающих контратаку: " + OProbabilityOneShot1 + "\nРасчет возможной глубины продвижения наступающих: " + OPossibleDepthPromotion;
        }




        /////////////////////////++++++++++++++++++++++++++++++++++++++++++///////////////////////////////////////

        #region //обявляю переменные


        //П1

        double MeanProbabilityWithOneShotDefender1; // вероятность поражения УБЕ (среднего танка) обороняющихся
        double MeanProbabilityWithOneShotAttack1; // вероятность поражения УБЕ (среднего танка) наступающих

        double MeanRateAttacking;
        double MeanEffectingFiringRangeAttacking;

        double MeanRateDefender;
        double MeanEffectingFiringRangeDefender;

        public double TimeSpentOnDefender;
        public double DegreeOfProtection;        
        public double VelocityAttacking;

        public double ProbabilityOfHittingDefender1ForMediumTank;
        public double ProbabilityOfHittingAttacking1ForMediumTank;

        double StafingUnitAttack;
        double StafingUnitDefender;

        //П2
        public string NameJ;
        public double ProbabilityHittingOneShotAttack1;
        public double ProbabilityHittingOneShot1Defender1;
        public double RateAtack;
        public double RateDefender;
        public double EffectiveFiringRangeAtack;
        public double EffectiveFiringRangeDefender;


        #region //Формуляр 7

        // Данные о обороняющихся
        public double SizeAreaDefenseAlongFront; // Размеры р-на обо-роны по ФРОНТУ
        public double SizeAreaDefenseAlongDepth;  // Размеры р-на обо-роны по глубине
        public double AutopsiesDgreeObjectsAttack; //Степень вскры-тия гр-ки пр-ка Sн
        public double DegreeOfDestructionByFire; // Степень огнев. пораж. пр-ка Zн
        public double DepthHerniationAttack; // Пред.-доп.  глубина вклинения наступ.  Гд
        public double ProportionOfFalseObjects; //Доля ложных объектов Lо
        public double ProportionOfKilled; //Доля  уни-чтож. СВН наступаю-щих WH
        public double TimeToPrepareDefence; //Время под-готовки обороны tо  (час)


        // данные о наступающих
        public double FrontOffensive; //Фронт наступления ФН
        public double GivenDepth; //глубина  бл. зад. ГЗ  ,км
        public double PeriodOfExecution; //срок выпол-нения ТЗН (час)
        public double SpeedOfMovement; //Доп. скорость движ. по местно-сти VM (км/час)
        public double CoeffSpeedRedutionWhenManeuvering; //Коэф. сниж. скор. при ма-невр.  КМ
        public double AutopsiesDgreeObjectsDefender; //Сте-пень вскры-тия гр-ки пр-ка   SO
        public double DegreeOfDestructionByFireDefender; //Сте-пень огнев. пораж пр-ка ZO
        public double AddLossesInMelee; //Доп. по-тери в бл. бою   Qнд
        public double ShareDestroyMeansOfAirDefender; //Доля уни-чтож СВН обо-рон. WО 
        public double DegreeOfExcellenceInManagement; //Степень превосход-ства в общем уровне управл Пну
        public double DegreeOfExcellenceInLevelOfCombatCapability; //Степень превосход-ства в уровне боеспос. л/сост. Пнл



        #endregion

        #endregion


        /////////////////////////+++++++++++++++++++++++++++++++++++++++++/////////////////////////////

        #region //Подключаю Xml C формулярами
        void GetFormularP1()
        {
            XElement root = XElement.Load("xml/FormularP1.xml");
            IEnumerable<XElement> tests =
                from el in root.Elements("AverageParameters")                
                select el;
            foreach (XElement el in tests)
            {
                MeanProbabilityWithOneShotDefender1 = (double)el.Element("ProbabilityWithOneShotDefender1ForMediumTank");
                MeanProbabilityWithOneShotAttack1 = (double)el.Element("ProbabilityWithOneShotAttacking1ForMediumTank");
                MeanRateAttacking = (double)el.Element("MeanRateAttacking");
                MeanEffectingFiringRangeAttacking = (double)el.Element("MeanEffectingFiringRangeAttacking");
                MeanRateDefender = (double)el.Element("MeanRateDefender");
                MeanEffectingFiringRangeDefender = (double)el.Element("MeanEffectingFiringRangeDefender");
                
                TimeSpentOnDefender = (double)el.Element("TimeSpentOnDefender");
                VelocityAttacking = (double)el.Element("VelocityAttacking");
                
                StafingUnitAttack = (double)el.Element("StafingUnitAttack");
                StafingUnitDefender = (double)el.Element("StafingUnitDefender");

                //OutResult.TextAlgoritmP.Text = ProbabilityWithOneShotDefender1ForMediumTank + " " + ProbabilityWithOneShotAttacking1ForMediumTank + " " + MeanRateAttacking + " " + MeanEffectingFiringRangeAttacking + " " + MeanRateDefender + " " + MeanEffectingFiringRangeDefender + " " + TimeSpentOnDefender + " " + VelocityAttacking;
            }
        }
        void GetFormularP2(int j)
        {
            XElement root = XElement.Load("xml/FormularP2.xml");
            IEnumerable<XElement> tests =
                from el in root.Elements("DataAboutClosecombat")
                where (string)el.Element("j") == Convert.ToString(j)
                select el;
            foreach (XElement el in tests)
            {
                //Вывод в переменные
                j = (int)el.Element("j");
                NameJ = (string)el.Element("NameJ");
                ProbabilityHittingOneShotAttack1 = (double)el.Element("ProbabilityHittingOneShotAttack1");
                ProbabilityHittingOneShot1Defender1 = (double)el.Element("ProbabilityHittingOneShot1Defender1");
                RateAtack = (double)el.Element("RateAtack");
                RateDefender = (double)el.Element("RateDefender");
                EffectiveFiringRangeAtack = (double)el.Element("EffectiveFiringRangeAtack");
                EffectiveFiringRangeDefender = (double)el.Element("EffectiveFiringRangeDefender");
            }
        }
        void GetFormularP3(int i, out string NameOfTypeOfLesion, out double CoefficentCommensurateERB, out double AmountOFAmmunation)
        {

            NameOfTypeOfLesion = null;
            CoefficentCommensurateERB = 0;
            AmountOFAmmunation = 0;

            XElement root = XElement.Load("xml/FormularP3.xml");
            IEnumerable<XElement> tests =
                from el in root.Elements("TZCVRFandAnyArmy")
                where (string)el.Element("i") == Convert.ToString(i)
                select el;
            foreach (XElement el in tests)
            {
                //Вывод в переменные
                i = (int)el.Element("i");
                NameOfTypeOfLesion = (string)el.Element("NameOfTypeOfLesion");
                CoefficentCommensurateERB = (double)el.Element("CoefficentCommensurateERB");
                AmountOFAmmunation = (double)el.Element("AmountOFAmmunation");

                //textbox1.Text += i +"       "+ NameOfTypeOfLesion+"     " + CoefficentCommensurateERB+"      " + AmountOFAmmunation + "\n";
            }
        }
        void GetFormularP4(int i, out string NameObjects, out double RequiredAmmountERBDefender, out double RequiredAmmountERBAttack)
        {

            NameObjects = null;
            RequiredAmmountERBDefender = 0;
            RequiredAmmountERBAttack = 0;

            XElement root = XElement.Load("xml/FormularP4.xml");
            IEnumerable<XElement> tests =
                from el in root.Elements("TZCVRFandAnyArmy")
                where (string)el.Element("i") == Convert.ToString(i)
                select el;
            foreach (XElement el in tests)
            {
                //Вывод в переменные
                i = (int)el.Element("i");
                NameObjects = (string)el.Element("NameOfTypeOfLesion");
                RequiredAmmountERBDefender = (double)el.Element("NumberOfERBForTheSuppressionOfObjinDefender");
                RequiredAmmountERBAttack = (double)el.Element("NumberOfERBForTheSuppressionOfObjinAttack");

            }
        }
        void GetFormularP5(int i, int j, out string NameOTF, out double NumberOfWeapons)
        {
            NameOTF = null;
            NumberOfWeapons = 0;
            XElement root = XElement.Load("xml/FormularP5.xml");
            IEnumerable<XElement> tests =
                from el in root.Elements("StafingNumberOfWeaponsForRus")
                where (string)el.Element("j") == Convert.ToString(j) & (string)el.Element("i") == Convert.ToString(i)
                select el;
            foreach (XElement el in tests)
            {
                //Вывод в переменные
                j = (int)el.Element("j");
                i = (int)el.Element("i");
                NameOTF = (string)el.Element("NameOTF");
                NumberOfWeapons = (double)el.Element("NumberOfWeapons");

            }
        }
        void GetFormularP6(int i, int j, out string NameOTF, out double NumberOfWeapons)
        {
            NameOTF = null;
            NumberOfWeapons = 0;
            XElement root = XElement.Load("xml/FormularP6.xml");
            IEnumerable<XElement> tests =
                from el in root.Elements("StafingNumberOfWeaponsForForeignArmy")
                where (string)el.Element("j") == Convert.ToString(j) & (string)el.Element("i") == Convert.ToString(i)
                select el;
            foreach (XElement el in tests)
            {
                //Вывод в переменные
                j = (int)el.Element("j");
                i = (int)el.Element("i");
                NameOTF = (string)el.Element("NameOTF");
                NumberOfWeapons = (double)el.Element("NumberOfWeapons");

            }
        }



        void GetFormularP7()
        {
            XElement root = XElement.Load("xml/FormularP7.xml");
            IEnumerable<XElement> tests =
                from el in root.Elements("VariableInformation")
                //where (string)el.Element("j") == Convert.ToString(j)
                select el;
            foreach (XElement el in tests)
            {
                //Вывод в переменные
                SizeAreaDefenseAlongFront = (double)el.Element("SizeAreaDefenseAlongFront");
                SizeAreaDefenseAlongDepth = (double)el.Element("SizeAreaDefenseAlongDepth");
                AutopsiesDgreeObjectsAttack = (double)el.Element("AutopsiesDgreeObjectsAttack");
                DegreeOfDestructionByFire = (double)el.Element("DegreeOfDestructionByFire");
                DepthHerniationAttack = (double)el.Element("DepthHerniationAttack");
                ProportionOfFalseObjects = (double)el.Element("ProportionOfFalseObjects");
                ProportionOfKilled = (double)el.Element("ProportionOfKilled");
                TimeToPrepareDefence = (double)el.Element("TimeToPrepareDefence");
                FrontOffensive = (double)el.Element("FrontOffensive");
                GivenDepth = (double)el.Element("GivenDepth");
                PeriodOfExecution = (double)el.Element("PeriodOfExecution");
                SpeedOfMovement = (double)el.Element("SpeedOfMovement");
                CoeffSpeedRedutionWhenManeuvering = (double)el.Element("CoeffSpeedRedutionWhenManeuvering");
                AutopsiesDgreeObjectsDefender = (double)el.Element("AutopsiesDgreeObjectsDefender");
                DegreeOfDestructionByFireDefender = (double)el.Element("DegreeOfDestructionByFireDefender");
                AddLossesInMelee = (double)el.Element("AddLossesInMelee");
                ShareDestroyMeansOfAirDefender = (double)el.Element("ShareDestroyMeansOfAirDefender");
                DegreeOfExcellenceInManagement = (double)el.Element("DegreeOfExcellenceInManagement");
                DegreeOfExcellenceInLevelOfCombatCapability = (double)el.Element("DegreeOfExcellenceInLevelOfCombatCapability");
            }
        }
        IEnumerable<XElement> ElementsRFArmy = XElement.Load("xml/FormularP81.xml").Elements("CompositionOfGroupsOTF");
        IEnumerable<XElement> ElementsForignArmy = XElement.Load("xml/FormularP82.xml").Elements("CompositionOfGroupsOTF");
        IEnumerable<XElement> ElementsRVAforRFArmy = XElement.Load("xml/FormularP91.xml").Elements("ParametrFireImpact");
        IEnumerable<XElement> ElementsRVAforForignArmy = XElement.Load("xml/FormularP92.xml").Elements("ParametrFireImpact");

        #endregion

        /////////////////////////++++++++++++++++B E G I N ++++++++++++++++//////////////////////////////////

        void ProbabilisticPerformanceDuelFightOneUBE()
        {
            DegreeOfProtection = 1 - Math.Exp(-0.07 * TimeSpentOnDefender);            
            ProbabilityOfHittingDefender1ForMediumTank = 1 - Math.Pow((1 - (1 - DegreeOfProtection) * MeanProbabilityWithOneShotAttack1), MeanRateAttacking * MeanEffectingFiringRangeAttacking / VelocityAttacking);
            ProbabilityOfHittingAttacking1ForMediumTank = 1 - Math.Pow(1 - (1 - MeanProbabilityWithOneShotDefender1), MeanRateDefender * MeanEffectingFiringRangeDefender / VelocityAttacking);
            OutResult.ExitP1 += "Вероятность попадания одним выстрелом:\n-для атакующ.  " + ProbabilityOfHittingAttacking1ForMediumTank + "\n-для обороняющ " + ProbabilityOfHittingDefender1ForMediumTank;
           
        }
        double CoefficientOFCommensurabilityMeansMeleeAttack(int i, int j, double StafingUnitAttack)
        {
            double numberOFWeaponsInI;
            string outnameP5;            
            DegreeOfProtection = 1 - Math.Exp(-0.07 * TimeSpentOnDefender);
            GetFormularP2(j);
            GetFormularP5(i, j, out outnameP5, out numberOFWeaponsInI);
            
            double ProbabilityOfHittingTankAttack = 0;
            double CoefficientOfCommensurability = 0;
            double NumberOfWeaponsInSubdivisionsUBE = 0;

            if (outnameP5 != null)
            {
                ProbabilityOfHittingTankAttack = 1 - Math.Pow((1 - (1 - DegreeOfProtection) * ProbabilityHittingOneShotAttack1), RateAtack * EffectiveFiringRangeAtack / VelocityAttacking);
                CoefficientOfCommensurability = ProbabilityOfHittingTankAttack / ProbabilityOfHittingDefender1ForMediumTank;
                NumberOfWeaponsInSubdivisionsUBE = CoefficientOfCommensurability * numberOFWeaponsInI * StafingUnitAttack;


                OutResult.ExitP2 += "Подразделение " + Convert.ToString(i) + "\t" + Convert.ToString(outnameP5) + " \t Средство:" + Convert.ToString(j) +"\t"+NameJ + " \t \t \tВероятность поражения танка: \t" + Convert.ToString(ProbabilityOfHittingTankAttack) + " \t \tКоэффицент соизмеримости: " + Convert.ToString(CoefficientOfCommensurability) + " \t \t Количество средств в подразделении: " + Convert.ToString(NumberOfWeaponsInSubdivisionsUBE + "\n");

                return NumberOfWeaponsInSubdivisionsUBE;
            }
            return 0;
        }
        double CoefficientOFCommensurabilityMeansMeleeDefender(int i, int j, double StafingUnitDefender)
        {
            double numberOFWeaponsInI;          
            
            string outnameP6;
            double ProbabilityOfHittingTankDefender = 0;
            double CoefficientOfCommensurability = 0;
            double NumberOfWeaponsInSubdivisionsUBE = 0;
            GetFormularP2(j);
            GetFormularP6(i, j, out outnameP6, out numberOFWeaponsInI);


            if (outnameP6 != null)
            {
                ProbabilityOfHittingTankDefender = 1 - Math.Pow(1 - ProbabilityHittingOneShot1Defender1, MeanRateDefender * EffectiveFiringRangeDefender / VelocityAttacking);
                CoefficientOfCommensurability = ProbabilityOfHittingTankDefender / ProbabilityOfHittingAttacking1ForMediumTank;
                NumberOfWeaponsInSubdivisionsUBE = CoefficientOfCommensurability * numberOFWeaponsInI * StafingUnitDefender;


                OutResult.ExitP3 += "Подразделение " + Convert.ToString(i) + "\t" + Convert.ToString(outnameP6) + " \tСредство типа:" + Convert.ToString(j)+"\t"+ NameJ + " \tВероятность поражения танка:\t" + Convert.ToString(ProbabilityOfHittingTankDefender) + " \tКоэффицент соизмеримости: \t " + Convert.ToString(CoefficientOfCommensurability) + " \tКоличество средств в подразделении: " + Convert.ToString(NumberOfWeaponsInSubdivisionsUBE + "\n");
                return NumberOfWeaponsInSubdivisionsUBE;
            }

            return 0;
        }

        void InitialSuperiorityInAttacking()
        {
                double InitialSuperiority;
                double SummWeaponesAttack = 0;          
                double SummWeaponeDefender = 0;
                double CoeffValue = 0;
                double CoeffValue1 = 0;

                int iAttack;
                int iDefender;

                string NameOTF;
                double Staffing;                

            #region //для нападающих
            foreach (XElement el in ElementsRFArmy)
	        {
                iAttack = (int)el.Element("i");
                NameOTF = (string)el.Element("NameOTF");
                Staffing = (double)el.Element("Staffing");
	
                for (int j = 1; j <= 99; j++)
                {
                    CoeffValue = CoefficientOFCommensurabilityMeansMeleeAttack(iAttack, j, Staffing);
                    if (CoeffValue != 0)
                    {
                        SummWeaponesAttack += CoeffValue;
                        OutResult.ExitP4 += "Количество орудий (j = " + j + " )\t в " + NameOTF + " подразделении атакующих равно\t" + Math.Round(CoeffValue, 2) + "\n";
                    }
                }                
            }

            OutResult.ExitP4 += "Сумма для подразделений атакующего " + SummWeaponesAttack + "\n\n";
            #endregion

            #region // для обороняющихся
            foreach (XElement el in ElementsForignArmy)
            {
                iDefender = (int)el.Element("i");
                NameOTF = (string)el.Element("NameOTF");
                Staffing = (double)el.Element("Staffing");
                for (int j = 100; j <= 199; j++)
                {
                    CoeffValue1 = CoefficientOFCommensurabilityMeansMeleeDefender(iDefender, j, Staffing);
                    if (CoeffValue1 != 0)
                    {
                        SummWeaponeDefender += CoeffValue1;
                        OutResult.ExitP4 += "Количество орудий (j = " + j + " )\t в " + NameOTF + " подразделении обороняющихся равно \t" + Math.Round(CoeffValue1, 2) + "\n";
                    }
                }                 
            }
            OutResult.ExitP4 += "Сумма для подразделений обороняющ. " + SummWeaponeDefender + "\n";
            #endregion

            OWeaponesAttack = SummWeaponesAttack;
            OWeaponesDefender = SummWeaponeDefender;

            InitialSuperiority = SummWeaponesAttack / SummWeaponeDefender;
            OutResult.ExitP4 += "Начальное превосходство в силах и средствах " + InitialSuperiority + "\n";
            
            OInitialSuperiorityInAttacking = InitialSuperiority;
            
        }

        ///(указать переменные с П17 до П23) Проверка условно - Переменной информации в этих блоках



        #region//////////////////////////////////П24 - П29///////////////////////////////////////////////////////////


        //Растчет огневого воздействия (п24)
        double CalculationOfTheFireImpactForIAttack()
        {

            string NameOfTypeOfLesion;
            double CoefficentCommensurateERB;
            double AmountOFAmmunation;

            int i;
            double NumberOfFire;
            double ConsumptionInAmmunition;

            double TheFireImpactForI;
            double TheFireImpact = 0;

            foreach (XElement el in ElementsRVAforRFArmy)
            {
                i = (int)el.Element("i");
                NumberOfFire = (double)el.Element("NumberOfFire");
                ConsumptionInAmmunition = (double)el.Element("ConsumptionInAmmunition");
                
                GetFormularP3(i, out NameOfTypeOfLesion, out CoefficentCommensurateERB, out AmountOFAmmunation);
                TheFireImpactForI = NumberOfFire * AutopsiesDgreeObjectsAttack * ConsumptionInAmmunition * AmountOFAmmunation * CoefficentCommensurateERB;
                TheFireImpact += TheFireImpactForI;
                OutResult.ExitP5 += "величина огневого воздествия для " + i + "-го средства\t" + NumberOfFire + " * " + AutopsiesDgreeObjectsAttack + " * " + ConsumptionInAmmunition + " * " + AmountOFAmmunation + " * " + CoefficentCommensurateERB + "\t = " + TheFireImpactForI + "\n";

            }         
           //Расчет величины огневого воздествия

            OutResult.ExitP5 += "\n\n Общее значение величины ";
            return TheFireImpact;
        }


        // Расчет суммы количества СВН
        void SummSVNforIAttack()
        {
            double NumberOfFire;
            double ConsumptionInAmmunition;
            double Summ = 0;
            int i;
            string NameOfTypeOfLesion;
            double CoefficentCommensurateERB;
            double AmountOFAmmunation;

            foreach (XElement el in ElementsRVAforRFArmy)
            {
                i = (int)el.Element("i");
                NumberOfFire = (double)el.Element("NumberOfFire");
                ConsumptionInAmmunition = (double)el.Element("ConsumptionInAmmunition");

                GetFormularP3(i, out NameOfTypeOfLesion, out CoefficentCommensurateERB, out AmountOFAmmunation);
                
                Summ += NumberOfFire * CoefficentCommensurateERB;

                //TestWindow.DebugText += "Значения: Сумма += NumberOfFire * CoefficentCommensurateERB" + Summ + "\n"
                //                        + "NameOfTypeOfLesion\t" + NameOfTypeOfLesion + "\n"
                //                        + "CoefficentCommensurateERB\t" + CoefficentCommensurateERB + "\n"
                //                        + "AmountOFAmmunation\t" + AmountOFAmmunation + "\n"
                //                        + "NumberOfFire\t" + NumberOfFire + "\n"
                //                        + "ConsumptionInAmmunition\t" + ConsumptionInAmmunition + "\n";
            }
            OSummSVNforIAttack = Summ;
        }

        //П26
        double TheFireImpactForMeansOfAirAttack()
        {
            double TheFireImpactForMeansOfAirAttack1;           
            TheFireImpactForMeansOfAirAttack1 = AutopsiesDgreeObjectsDefender * (1 - ProportionOfKilled) * OSummSVNforIAttack;
            OutResult.ExitP6 += "Для  СВН \t" + AutopsiesDgreeObjectsDefender + " * (1 - " + ProportionOfKilled + " )* " + OSummSVNforIAttack + "\t = " + TheFireImpactForMeansOfAirAttack1 + "\n";
            return TheFireImpactForMeansOfAirAttack1;
        }

        void SummTheFireImpactAttack()
        {
            double SummTheFireImpact1;
            SummTheFireImpact1 = CalculationOfTheFireImpactForIAttack() + TheFireImpactForMeansOfAirAttack();
            OSummTheFireImpactAttack = SummTheFireImpact1;
            
        }

        //П28
        void TotalQuantityOfAmmunitionToSuppress()
        {

            double TotalQuantity = 0;
            int i;
            string NameObjects;
            double RequiredAmmountERBDefender;
            double RequiredAmmountERBAttack;
            double Quantity; // количество I-х , боеприпасов
            string Name = null;
            double Staff = 0;

            foreach (XElement el in ElementsForignArmy)
            {
                i = (int)el.Element("i");
                Name = (string)el.Element("NameNameOTF");
                Staff = (double)el.Element("Staffing");
                GetFormularP4(i, out NameObjects, out RequiredAmmountERBDefender, out RequiredAmmountERBAttack);
                
                Quantity = Staff * RequiredAmmountERBDefender;
                TotalQuantity += Quantity;

                if (Staff != 0 || RequiredAmmountERBDefender !=0) 
                {
                    OutResult.ExitP7 += "Количество боеприпасов " + i + "-го типа \t" + Staff + " * " + RequiredAmmountERBDefender + " = " + Quantity + "\n";
                }
                
            }
            OutResult.ExitP7 += "Общее кол-во = " + TotalQuantity;
            OTotalQuantityOfAmmunitionToSuppress = TotalQuantity;
        }

        //П29
        void DegreeOfFireDamageDefender()
        {
            double DegreeOfFireDamage = 1 - Math.Exp(-0.31 * OSummTheFireImpactAttack / (OTotalQuantityOfAmmunitionToSuppress * (1 + ProportionOfFalseObjects)));
            OutResult.ExitP8 += "Степень огневого поражения равна " + DegreeOfFireDamage + "\n";
            ODegreeOfFireDamageDefender = DegreeOfFireDamage;  // Расчитаная степень огневого поражения
        }
        #endregion

        #region //////////////////////////////////П34 - П40///////////////////////////////////////////////////////////

        // P34 Расчет величины огневого воздействия i - го средства обороняющихся по объектам наступающих:
        double FireExposureForITheDefenderOnObjects()
        {
            string NameOfTypeOfLesion;
            double CoefficentCommensurateERB;
            double AmountOFAmmunation;

            int i;
            double NumberOfFire;
            double ConsumptionInAmmunition;

            double TheFireImpactForI;
            double TheFireImpact = 0;

            foreach (XElement el in ElementsRVAforForignArmy)
            {
                i = (int)el.Element("i");
                NumberOfFire = (double)el.Element("NumberOfFire");
                ConsumptionInAmmunition = (double)el.Element("ConsumptionInAmmunition");
                
                GetFormularP3(i, out NameOfTypeOfLesion, out CoefficentCommensurateERB, out AmountOFAmmunation);
                
                TheFireImpactForI = NumberOfFire * AutopsiesDgreeObjectsAttack * ConsumptionInAmmunition * AmountOFAmmunation * CoefficentCommensurateERB;
                
                TheFireImpact += TheFireImpactForI;
                OutResult.ExitP9 += "величина огневого воздествия для " + i + "-го средства\t" + NumberOfFire + " * " + AutopsiesDgreeObjectsAttack + " * " + ConsumptionInAmmunition + " * " + AmountOFAmmunation + " * " + CoefficentCommensurateERB + "\t = " + TheFireImpactForI + "\n";

            }         
            

            //Расчет величины огневого воздествия

            
            OutResult.ExitP9 += "\n\n Общее значение величины "+ TheFireImpact +"\n";
            return TheFireImpact;
        }
        //П36
        void SummSVNforIDefender()
        {
            double NumberOfFire;
            double ConsumptionInAmmunition;
            double Summ = 0;
            int i;
            string NameOfTypeOfLesion;
            double CoefficentCommensurateERB;
            double AmountOFAmmunation;

            foreach (XElement el in ElementsRVAforForignArmy)
            {
                i = (int)el.Element("i");
                NumberOfFire = (double)el.Element("NumberOfFire");
                ConsumptionInAmmunition = (double)el.Element("ConsumptionInAmmunition");

                GetFormularP3(i, out NameOfTypeOfLesion, out CoefficentCommensurateERB, out AmountOFAmmunation);

                Summ += NumberOfFire * CoefficentCommensurateERB;
            }
            OSummSVNforIDefender = Summ;
        }
        
        double TheFireImpactForMeansOfAirDefender()
        {
            SummSVNforIDefender();
            double TheFireImpactForMeansOfAirDefender1 = AutopsiesDgreeObjectsAttack * (1 - ShareDestroyMeansOfAirDefender) * OSummSVNforIDefender;
            return TheFireImpactForMeansOfAirDefender1;
        }



        // Общее суммирование
        void SummTheFireImpactDefender()
        {
            double SummTheFireImpact1 = 0;
            
                    SummTheFireImpact1 += FireExposureForITheDefenderOnObjects() + TheFireImpactForMeansOfAirDefender();

            //textbox1.Text += "SummFireimpact = " + SummTheFireImpact1;
            OSummTheFireImpactDefender = SummTheFireImpact1;
        }
        //П38
        void TotalQuantityOfAmmunitionToSuppressfordefender()
        {

            double TotalQuantity = 0;


            string NameObjects;
            double RequiredAmmountERBDefender;
            double RequiredAmmountERBAttack;
            double Quantity; // для промедуточных расчетов и вывода

            string Name = null;
            double Staff = 0;
            int i;


            foreach (XElement el in ElementsRFArmy)
            {

                i = (int)el.Element("i");
                Name = (string)el.Element("NameNameOTF");
                Staff = (double)el.Element("Staffing");

                
                GetFormularP4(i, out NameObjects, out RequiredAmmountERBDefender, out RequiredAmmountERBAttack);
                Quantity = Staff * RequiredAmmountERBAttack;
                TotalQuantity += Quantity;

                //вывод 

                    OutResult.ExitP10 += "Количество боеприпасов " + i + "-го типа \t" + Staff + " * " + RequiredAmmountERBDefender + " = " + Quantity + "\n";

            }

            OutResult.ExitP10 += "Общее кол-во = " + TotalQuantity;
            OTotalQuantityOfAmmunitionToSuppressfordefender = TotalQuantity;
        }

        //П39
        void DegreeOfFireDamageAttacking()
        {
            double DegreeOfFireDamage = 1 - Math.Exp(-0.31 * OSummTheFireImpactDefender / OTotalQuantityOfAmmunitionToSuppressfordefender);

            OutResult.ExitP11 += "Степень огневого поражения равна " + DegreeOfFireDamage + "\n";
            ODegreeOfFireDamageAttacking = DegreeOfFireDamage;
        }

        //П40
        void СalculationOfFireSuperiorityUpcoming()
        {
            double FireSuperiorityUpcoming = (1 - ODegreeOfFireDamageAttacking) / (1 - ODegreeOfFireDamageDefender);
            OutResult.ExitP12 += "Превосходство: " + FireSuperiorityUpcoming;
            OCalculationOfFireSuperiorityUpcoming = FireSuperiorityUpcoming;
        }
        #endregion

        #region        //////////////////////////////////П41 - П46///////////////////////////////////////////////////////////

        // П41
        void ExpectedTempoAttack()
        {
            double TimeTargetDetection = 1 / MeanRateAttacking;
            double RequiredProbability = 0.9;
            
            double ExpectedTempo = 1 / (1 / (CoeffSpeedRedutionWhenManeuvering * SpeedOfMovement) + ((TimeTargetDetection * FrontOffensive * Math.Log(1 - RequiredProbability)) / (SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront * AddLossesInMelee * OInitialSuperiorityInAttacking * OCalculationOfFireSuperiorityUpcoming * DegreeOfExcellenceInLevelOfCombatCapability * DegreeOfExcellenceInManagement * Math.Log(1 - MeanProbabilityWithOneShotAttack1 * (1 - MeanProbabilityWithOneShotDefender1)))));
            OutResult.ExitP13 += "Ожидаемый темп настуления, при требуемой вероятнсти поражения одной УБЕ=0,9 равен: " + ExpectedTempo;
            OExpectedTempoAttack = ExpectedTempo;
        }

        // П42 -П45
        void StartP42toP45()
        {
            // П42
            OActualRateOneUBE = MeanRateDefender * (1 - Math.Exp(-2.3 * OInitialSuperiorityInAttacking * OCalculationOfFireSuperiorityUpcoming * SizeAreaDefenseAlongFront / FrontOffensive));
        

        // П44

            OProbabilityOneShot1 = MeanProbabilityWithOneShotDefender1 * (1 - MeanProbabilityWithOneShotAttack1);
       

        // П45

            OPossibleDepthPromotion = Math.Sqrt((AddLossesInMelee * OInitialSuperiorityInAttacking * OCalculationOfFireSuperiorityUpcoming * DegreeOfExcellenceInLevelOfCombatCapability * DegreeOfExcellenceInManagement * SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront * OExpectedTempoAttack) / (OActualRateOneUBE * FrontOffensive * OProbabilityOneShot1));
        }   
         
        #endregion
        
        #region        //////////////////////////////////П47 - П55///////////////////////////////////////////////////////////

        //П46

        //П47
        void RequiredSettings()
        {
            //для наступающих
            //1 Требуемое значение начального превосходства в силах и средствах
            double InitialSuperiorityDesired = Math.Pow(GivenDepth, 2) * OActualRateOneUBE * FrontOffensive * OProbabilityOneShot1 / (AddLossesInMelee * DegreeOfExcellenceInManagement * DegreeOfExcellenceInLevelOfCombatCapability * OCalculationOfFireSuperiorityUpcoming * SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront * OExpectedTempoAttack);

            //2 требуемое значение огневого превосходства
            double FireSuperiorityDesired = Math.Pow(GivenDepth, 2) * OActualRateOneUBE * FrontOffensive * OProbabilityOneShot1 / (AddLossesInMelee * OInitialSuperiorityInAttacking * DegreeOfExcellenceInLevelOfCombatCapability * DegreeOfExcellenceInManagement * SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront * OExpectedTempoAttack);

            //3 требуемое значение превосходства в общем уровне управления
            double DegreeOfExcellenceInManagementDesired = Math.Pow(GivenDepth, 2) * OActualRateOneUBE * FrontOffensive * OProbabilityOneShot1 / (AddLossesInMelee * OInitialSuperiorityInAttacking * DegreeOfExcellenceInLevelOfCombatCapability * OCalculationOfFireSuperiorityUpcoming * SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront * OExpectedTempoAttack);

            //4 требуемое значение предельно-допустимых по-терь в ближнем бою

            double AddLossesInMeleeDesired = Math.Pow(GivenDepth, 2) * OActualRateOneUBE * FrontOffensive * OProbabilityOneShot1 / (OInitialSuperiorityInAttacking * DegreeOfExcellenceInLevelOfCombatCapability * OCalculationOfFireSuperiorityUpcoming * DegreeOfExcellenceInManagement * SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront * OExpectedTempoAttack);

            OutResult.ExitP15 = null;
            OutResult.ExitP15 += "Требуемые значения параметров решения для выполнения условия \nдля наступающих - Гн=Гз:\n";

            OutResult.ExitP15 += "1) требуемое значение начального превосходства в силах и средствах: \t" +  Math.Round(InitialSuperiorityDesired, 2) + "\n" +
                                 "2) или требуемое значение огневого превосходства: \t\t\t" + Math.Round(FireSuperiorityDesired, 2) + "\n" +
                                 "3) или требуемое значение превосходства в общем уровне управления:\t" + Math.Round(DegreeOfExcellenceInManagementDesired, 2) + "\n" +
                                 "4) или требуемое значение предельно-допустимых потерь в ближнем бою:\t" + Math.Round(AddLossesInMeleeDesired, 2) + "\n";
            
            //5 требуемое значение темпа наступления
            double TempoOffensiveDesired = Math.Pow(GivenDepth, 2) * OActualRateOneUBE * FrontOffensive * OProbabilityOneShot1 / (AddLossesInMelee * OInitialSuperiorityInAttacking * DegreeOfExcellenceInLevelOfCombatCapability * OCalculationOfFireSuperiorityUpcoming * DegreeOfExcellenceInManagement * SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront);

            //6 требуемое значение фонта наступления
            double FrontOffensiveDesired = AddLossesInMelee * OInitialSuperiorityInAttacking * DegreeOfExcellenceInLevelOfCombatCapability * OCalculationOfFireSuperiorityUpcoming * DegreeOfExcellenceInManagement * SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront * OExpectedTempoAttack / (Math.Pow(GivenDepth, 2) * OActualRateOneUBE * OProbabilityOneShot1);

            //7 Рекомендуемое время выполненя задачи
            double DepthForTaskTime;

            if (OPossibleDepthPromotion > GivenDepth)
	            {
		            DepthForTaskTime = GivenDepth;
	            } else {
                    DepthForTaskTime = OPossibleDepthPromotion;
                }

            double TaskTimeDesired = 1.21 * Math.Pow(GivenDepth, 2) / (DepthForTaskTime * OExpectedTempoAttack);

            //8 Рекомендуемая глубина задачи

            double DepthOfProblemDesired;

            if (GivenDepth <= OPossibleDepthPromotion)
            {
                DepthOfProblemDesired = Math.Sqrt(0.826 * GivenDepth * OExpectedTempoAttack * PeriodOfExecution );
            }
            else
            {
                DepthOfProblemDesired = Math.Sqrt(0.826 * OPossibleDepthPromotion * OExpectedTempoAttack * PeriodOfExecution);
            }

            OutResult.ExitP15 +=

                "5) или требуемое значение темпа наступления: \t\t\t\t" + Math.Round(TempoOffensiveDesired, 2) + "\n" +
                "6) или требуемое значение фронта наступления: \t\t\t\t" + Math.Round(FrontOffensiveDesired, 2) + "\n" +
                "7) или рекомендуемое время выполнения с\n    вероятностью Рзн =0,9 задачи на глубину Гз:\t\t\t\t" + Math.Round(TaskTimeDesired, 2) + "\n" +
                "8) или рекомендуемая глубина задачи, которую можно \nс    вероятностью Рзн =0,9 выполнить за время Тзн: \t\t\t" + Math.Round(DepthOfProblemDesired, 2) + "\n" +

                "\n";
            if (AddLossesInMeleeDesired >= 1)
            {
                OutResult.ExitP15 += "Задача НЕ ВЫПОЛНИМА!!!: Доп потери в ближнем бою больше 1\n";
            }
            else
            {
                OutResult.ExitP15 += "Задача ВЫПОЛНИМА\n";
            }


            OutResult.ExitP15 += "\nТребуемые значения параметров решения для выполнения условия \n для обороняющихся - Гн=Гд:\n";

            //1) требуемое значение начального превосходства в силах и средствах
            double InitialSuperiorityDesiredDefender = AddLossesInMelee * DegreeOfExcellenceInManagement * DegreeOfExcellenceInLevelOfCombatCapability * OCalculationOfFireSuperiorityUpcoming * SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront * OExpectedTempoAttack / (Math.Pow(DepthHerniationAttack, 2) * OActualRateOneUBE * FrontOffensive * OProbabilityOneShot1);

            //2) Требуемое значение огнвого првосходства
            double SuperiorityDesiredDefender = AddLossesInMelee * OInitialSuperiorityInAttacking * DegreeOfExcellenceInLevelOfCombatCapability * DegreeOfExcellenceInManagement * SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront * OExpectedTempoAttack / (Math.Pow(DepthHerniationAttack, 2) * OActualRateOneUBE * FrontOffensive * OProbabilityOneShot1);

            //3) Требуемое значение превосходства в общем уровне управления
            double ManagementlevelDesiredDefender = AddLossesInMelee * OInitialSuperiorityInAttacking * DegreeOfExcellenceInLevelOfCombatCapability * OCalculationOfFireSuperiorityUpcoming * SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront * OExpectedTempoAttack / (Math.Pow(DepthHerniationAttack, 2) * OActualRateOneUBE * FrontOffensive * OProbabilityOneShot1);
        
            //4) Требуемое значение фронта обороны
            double DefensiveFrontDesire = Math.Pow(DepthHerniationAttack, 2) * OActualRateOneUBE * FrontOffensive * OProbabilityOneShot1 / (AddLossesInMelee * OInitialSuperiorityInAttacking * DegreeOfExcellenceInLevelOfCombatCapability * DegreeOfExcellenceInManagement * OCalculationOfFireSuperiorityUpcoming * SizeAreaDefenseAlongDepth * OExpectedTempoAttack);

            OutResult.ExitP15 +=

                "1) требуемое значение начального превосходства \n   в силах и средствах (по боевым потенциалам):\t\t" + Math.Round(InitialSuperiorityDesiredDefender, 2) + "\n" +
                "2) или требуемое значение огневого превосходства:\t " + Math.Round(SuperiorityDesiredDefender, 2) + "\n" +
                "3) или требуемое значение \n   превосходства в общем уровне управления:\t\t" + Math.Round(ManagementlevelDesiredDefender, 2) + "\n" +
                "4) или требуемое значение фронта обороны:\t\t" + Math.Round(DefensiveFrontDesire, 2) + "\n";               
        
        }

        ///Запись в окно результатов
        public string RecomendationsText()
        {
            return OutResult.ExitP15;
        }

        //П48 - П50
        void LossCalculationSides()
        {
            if (OPossibleDepthPromotion > SizeAreaDefenseAlongDepth)
	        {
		        OPossibleDepthPromotion = SizeAreaDefenseAlongDepth;
	        }
            //Расчет потерь сторон в ближнем бою
            OLossSidesMeleeAttack = AddLossesInMelee;
            OLossSidesMeleeDefender = OPossibleDepthPromotion * FrontOffensive / (SizeAreaDefenseAlongDepth * SizeAreaDefenseAlongFront);

            //Расчет общих потерь с учетом ближнего боя ( с учетом ближнего боя, огня РВ и А, Авиации)
            OTotalLossAttack = ODegreeOfFireDamageAttacking + (1 - ODegreeOfFireDamageAttacking) * OLossSidesMeleeAttack;
            OTotalLossDefender = ODegreeOfFireDamageDefender + (1 - ODegreeOfFireDamageDefender) * OLossSidesMeleeDefender;

            //Расчет условных коэффициентов боевой эффективности одной УБЕ
            OCombatEffectivenessDefender = AddLossesInMelee * DegreeOfExcellenceInLevelOfCombatCapability * OCalculationOfFireSuperiorityUpcoming * DegreeOfExcellenceInManagement;
            OCombatEffectivenessAttack = OLossSidesMeleeDefender / (DegreeOfExcellenceInLevelOfCombatCapability * OCalculationOfFireSuperiorityUpcoming * DegreeOfExcellenceInManagement);

            //Уточнить новые значения укомплектованности каждого i – го подразделения наступающих и обороняющихся с учетом потерь
            //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!

            //П50 
            // расчет времени продвижения на глубину
            OTimingAdvanceToTheDepth = OPossibleDepthPromotion / OExpectedTempoAttack;
        }

        //П51
        void ProbabilityCharacteristicsOfTheCombatMission()
        {
            //расчет вероятностных характеристик выполнения боевой задачи
            
            // 1) наступающими:
            // Вероятность выполнения задачи на глубину Гз
            OProbabilityOfaTaskToDepth = 1 - Math.Exp(-1.9 * OPossibleDepthPromotion / GivenDepth);
            // Вероятность выполнения задачи на глубину Гз за время Tзн
            OProbabilityOfaTaskToDepthTime = 1 - Math.Exp(-1.9 * OPossibleDepthPromotion * OExpectedTempoAttack * OTimingAdvanceToTheDepth / Math.Pow(GivenDepth, 2));
            // Степень риска для наступающих
            ODegreeOfRiskAttack1 = 1 - OProbabilityOfaTaskToDepth;
            ODegreeOfRiskAttack2 = 1 - OProbabilityOfaTaskToDepthTime;

            //2) обороняющимися
            // устойчивость обороны
            OStabilityOfDefense = 1 - OProbabilityOfaTaskToDepth;
            // вероятность недопущения вклинения наступающих в оборону на глубину более Гд
            if (DepthHerniationAttack == 0)
            {
                DepthHerniationAttack = 0.5 * SizeAreaDefenseAlongDepth; 
            }

            if (DepthHerniationAttack > OPossibleDepthPromotion)
            {
                OProbabilityOfAvoidingHerniation = (DepthHerniationAttack - OPossibleDepthPromotion) / DepthHerniationAttack;
            }
            else
            {
                OProbabilityOfAvoidingHerniation = 0;
            }
        
        }


        ///////////////////////////////////////Финальные значения///////////////////////////////////////////////////////////////////////
        public double OInitialSuperiorityInAttacking;  //начальное превосходстово
        public double OSummSVNforIAttack; // Расчет суммы количества СВН
        public double OSummTheFireImpactAttack; //Величина огневого воздействия
        public double OTotalQuantityOfAmmunitionToSuppress; //
        public double ODegreeOfFireDamageDefender; // Степень огневого поражения
        public double OSummSVNforIDefender;
        public double OSummTheFireImpactDefender;

        public double OWeaponesAttack;
        public double OWeaponesDefender;

        public double OTotalQuantityOfAmmunitionToSuppressfordefender;
        public double ODegreeOfFireDamageAttacking;
        public double OCalculationOfFireSuperiorityUpcoming;
        public double OExpectedTempoAttack;
        public double OActualRateOneUBE;
        public double OProbabilityOneShot1;
        public double OPossibleDepthPromotion;



        //// расчет потерь сторон
        public double OLossSidesMeleeAttack;
        public double OLossSidesMeleeDefender;
        public double OTotalLossAttack;
        public double OTotalLossDefender;
        public double OCombatEffectivenessAttack;
        public double OCombatEffectivenessDefender;

        // расчет времени продвижения наступающих
        public double OTimingAdvanceToTheDepth;

        //Расчет вероятностных характеристик выполнения боевой задачи 
        //наступающими
        // Вероятность выполнения задачи на глубину Гз
        public double OProbabilityOfaTaskToDepth;
        public double OProbabilityOfaTaskToDepthTime;
        //Степень риска для наступающих
        public double ODegreeOfRiskAttack1;
        public double ODegreeOfRiskAttack2;

        //обороняющимися
        // устойчивость обороны
        public double OStabilityOfDefense;
        // вероятность недопущения вклинения наступаю-щих в оборону на глубину более Гд 
        public double OProbabilityOfAvoidingHerniation;
        
        #endregion
    }
}
