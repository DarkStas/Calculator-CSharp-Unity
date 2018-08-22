using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using System.Linq;

namespace CalcLibrary
{
	public class Calc : MonoBehaviour
	{

		public Text CalcPanelNow;
		public Text CalcPanelWas;
		public double ShowNumber1 = 0;
		public double ShowNumber2 = 0;
		public double ShowResult = 0;

		public int OperatorType = (int)MathOperations.NoOperator;
		public string WasForCalc;
		bool Clean = false;
		bool LockedByMaxNum;
		int MaxNum = 15;

		public enum MathOperations
		{
			NoOperator = 0,
			Add,
			Minus,
			Multiply,
			Divide
		}


		// Use this for initialization
		void Start()
		{
			CalcPanelWas.text = "0";
			OperatorType = (int)MathOperations.NoOperator;
		}

		// Update is called once per frame
		void Update()
		{
			#region Keyboard
			if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
				ButtonZero();
			else if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
				ButtonOne();
			else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
				ButtonTwo();
			else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
				ButtonThree();
			else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
				ButtonFour();
			else if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
				ButtonFive();
			else if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
				ButtonSix();
			else if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
				ButtonSeven();
			else if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
				ButtonEight();
			else if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
				ButtonNine();
			else if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
				ButtonPlus();
			else if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
				ButtonMinus();
			else if (Input.GetKeyDown(KeyCode.KeypadMultiply) || Input.GetKeyDown(KeyCode.KeypadMultiply))
				ButtonMultiply();
			else if (Input.GetKeyDown(KeyCode.KeypadDivide) || Input.GetKeyDown(KeyCode.KeypadDivide))
				ButtonDivide();
			else if (Input.GetKeyDown(KeyCode.Backspace))
				ButtonDel();
			else if (Input.GetKeyDown(KeyCode.Delete))
				ButtonCE();
			else if (Input.GetKeyDown(KeyCode.Equals) || Input.GetKeyDown(KeyCode.KeypadEquals) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
				ButtonEqually();
			else if (Input.GetKeyDown(KeyCode.Period) || Input.GetKeyDown(KeyCode.KeypadPeriod))
				ButtonDot();
			else if (Input.GetKeyDown(KeyCode.Escape))
				ExitPressed();
			#endregion
		}
		#region CalcHelpCode
		public void AddNumber(string number)
		{
			if (Clean == true)
			{
				CalcPanelNow.text = "0";
				Clean = false;
			}
			if (CalcPanelNow.text == "0")
			{
				CalcPanelNow.text = "";
			}
			if (CalcPanelNow.text.Length <= MaxNum)
				LockedByMaxNum = false;
			if(LockedByMaxNum == false)
				CalcPanelNow.text = CalcPanelNow.text + number;
			if (CalcPanelNow.text.Length > MaxNum)
				LockedByMaxNum = true;
		}
		public void CalcSymbols(int WhatButtonSymbol)
		{
			if (CalcPanelNow.text != "0" && CalcPanelWas.text == "0")
			{
				OperatorType = WhatButtonSymbol;
				if (CalcPanelWas.gameObject.activeSelf == false)
				{
					CalcPanelWas.gameObject.SetActive(true);
				}
				CalcPanelWas.text = CalcPanelNow.text;
				WasForCalc = CalcPanelWas.text;
				ShowOperator();
				CalcPanelNow.text = "0";
			}
			if (CalcPanelNow.text != "0" && CalcPanelWas.text != "0" && OperatorType != (int)MathOperations.NoOperator)
			{
				HelpResult();
				CalcPanelWas.text = Convert.ToString(ShowResult);
				WasForCalc = CalcPanelWas.text;
				ShowResult = 0;

			}
			if (OperatorType != (int)MathOperations.NoOperator)
			{
				OperatorType = WhatButtonSymbol;
				ShowOperator();
				CalcPanelNow.text = "0";
			}
		}
		public void HelpResult()
		{
			ShowNumber1 = Convert.ToDouble(CalcPanelNow.text);
			ShowNumber2 = Convert.ToDouble(WasForCalc);
			WhenNeedCalc();
			ShowNumber1 = 0;
			ShowNumber2 = 0;
		}
		public void WhenNeedCalc()
		{
			switch (OperatorType)
			{
				case (int)MathOperations.Add:
					ShowResult = ShowNumber2 + ShowNumber1;
					break;
				case (int)MathOperations.Minus:
					ShowResult = ShowNumber2 - ShowNumber1;
					break;
				case (int)MathOperations.Multiply:
					ShowResult = ShowNumber2 * ShowNumber1;
					break;
				case (int)MathOperations.Divide:
					ShowResult = ShowNumber2 / ShowNumber1;
					break;
			}
		}
		public void ShowOperator()
		{
			if (OperatorType != 0)
			{

				switch (OperatorType)
				{
					case (int)MathOperations.Add:
						CalcPanelWas.text = WasForCalc + " " + "+";
						break;
					case (int)MathOperations.Minus:
						CalcPanelWas.text = WasForCalc + " " + "-";
						break;
					case (int)MathOperations.Multiply:
						CalcPanelWas.text = WasForCalc + " " + "*";
						break;
					case (int)MathOperations.Divide:
						CalcPanelWas.text = WasForCalc + " " + "/";
						break;
				}
			}
		}
		public void HelpCode1()
		{
			WasForCalc = Convert.ToString(ShowResult);
			if (CalcPanelWas.gameObject.activeSelf == false)
			{
				CalcPanelWas.gameObject.SetActive(true);
			}
			CalcPanelWas.text = WasForCalc;
			ShowResult = 0;
			OperatorType = 1;
			ShowOperator();
			CalcPanelNow.text = "0";
			ShowNumber1 = 0;
		}
		public void HelpCode2()
		{
			CalcPanelNow.text = Convert.ToString(ShowResult);
			HelpResult();
			ShowResult = 0;
			ShowNumber1 = 0;
		}
		#endregion
		#region NumericButtons
		public void ButtonOne()
		{
			AddNumber("1");
		}

		public void ButtonTwo()
		{
			AddNumber("2");
		}

		public void ButtonThree()
		{
			AddNumber("3");
		}

		public void ButtonFour()
		{
			AddNumber("4");
		}

		public void ButtonFive()
		{
			AddNumber("5");
		}

		public void ButtonSix()
		{
			AddNumber("6");
		}

		public void ButtonSeven()
		{
			AddNumber("7");
		}

		public void ButtonEight()
		{
			AddNumber("8");
		}

		public void ButtonNine()
		{
			AddNumber("9");
		}

		public void ButtonZero()
		{
			AddNumber("0");
		}
		#endregion
		#region DelButtons
		public void ButtonC()
		{

			CalcPanelNow.text = "0";
			CalcPanelWas.text = "0";
			WasForCalc = "0";
			OperatorType = 0;
			if (CalcPanelWas.gameObject.activeSelf == true)
			{
				CalcPanelWas.gameObject.SetActive(false);
			}
		}

		public void ButtonCE()
		{
			CalcPanelNow.text = "0";
		}

		public void ButtonDel()
		{
			int b = CalcPanelNow.text.Length;
			if (CalcPanelNow.text != "0" && b != 1)
			{
				CalcPanelNow.text = CalcPanelNow.text.Substring(0, b - 1);
			}
			if (b == 1)
			{
				CalcPanelNow.text = "0";
			}
		}
		#endregion
		#region CalcButtons
		public void ButtonPlus()
		{
			CalcSymbols(1);
		}

		public void ButtonMinus()
		{
			CalcSymbols(2);
		}

		public void ButtonMultiply()
		{
			CalcSymbols(3);
		}

		public void ButtonDivide()
		{
			CalcSymbols(4);
		}

		public void ButtonEqually()
		{
			if (CalcPanelNow.text != "0" && CalcPanelWas.text != "0" && OperatorType != (int)MathOperations.NoOperator)
			{
				HelpResult();
				CalcPanelNow.text = Convert.ToString(ShowResult);
				CalcPanelWas.text = "0";
				if (CalcPanelWas.gameObject.activeSelf == true)
				{
					CalcPanelWas.gameObject.SetActive(false);
				}
				ShowResult = 0;
				OperatorType = 0;
				WasForCalc = "0";
				Clean = true;
			}
		}
		#endregion
		#region ElseButtons
		public void ButtonPercent()
		{
			if (CalcPanelNow.text != "0" && CalcPanelWas.text != "0")
			{
				ShowNumber1 = Convert.ToDouble(CalcPanelNow.text);
				ShowNumber2 = Convert.ToDouble(WasForCalc);
				double a = ShowNumber1 / 100;
				double b = a * ShowNumber2;
				ShowResult = b + ShowNumber2;
				WasForCalc = Convert.ToString(ShowResult);
				CalcPanelWas.text = WasForCalc;
				ShowNumber1 = 0;
				ShowNumber2 = 0;
				ShowResult = 0;
				ShowOperator();
				CalcPanelNow.text = "0";
			}

		}



		public void ButtonPlusMinus()
		{
			if (!CalcPanelNow.text.Contains("-"))
			{
				CalcPanelNow.text = "-" + CalcPanelNow.text;
			}
			else
			{
				CalcPanelNow.text = CalcPanelNow.text.Trim('-');
			}
		}

		public void ButtonDot()
		{
			if (!CalcPanelNow.text.Contains("."))
			{
				CalcPanelNow.text += ".";
			}
		}

		public void ButtonSqrt()
		{
			if (CalcPanelNow.text != "0" && CalcPanelWas.text == "0" && OperatorType == (int)MathOperations.NoOperator && !CalcPanelNow.text.Contains("-"))
			{
				ShowNumber1 = Convert.ToDouble(CalcPanelNow.text);
				ShowResult = Math.Sqrt(ShowNumber1);
				HelpCode1();
			}
			if (CalcPanelNow.text != "0" && CalcPanelWas.text != "0" && OperatorType != (int)MathOperations.NoOperator)
			{
				ShowNumber1 = Convert.ToDouble(CalcPanelNow.text);
				ShowResult = Math.Sqrt(ShowNumber1);
				HelpCode2();
			}
		}

		public void ButtonSquare()
		{
			if (CalcPanelNow.text != "0" && CalcPanelWas.text == "0" && OperatorType == (int)MathOperations.NoOperator)
			{
				ShowNumber1 = Convert.ToDouble(CalcPanelNow.text);
				ShowResult = ShowNumber1 * ShowNumber1;
				HelpCode1();
			}
			if (CalcPanelNow.text != "0" && CalcPanelWas.text != "0" && OperatorType != (int)MathOperations.NoOperator)
			{
				ShowNumber1 = Convert.ToDouble(CalcPanelNow.text);
				ShowResult = ShowNumber1 * ShowNumber1;
				HelpCode2();
			}
		}

		public void ButtonOneDivideX()
		{
			if (CalcPanelNow.text != "0" && CalcPanelWas.text == "0" && OperatorType == (int)MathOperations.NoOperator)
			{
				ShowNumber1 = Convert.ToDouble(CalcPanelNow.text);
				ShowResult = 1 / ShowNumber1;
				HelpCode1();
			}
			if (CalcPanelNow.text != "0" && CalcPanelWas.text != "0" && OperatorType != (int)MathOperations.NoOperator)
			{
				ShowNumber1 = Convert.ToDouble(CalcPanelNow.text);
				ShowResult = 1 / ShowNumber1;
				HelpCode2();
			}
		}
		#endregion
		public void ExitPressed()
		{
			Application.Quit();//Exit game
		}
	}
}
