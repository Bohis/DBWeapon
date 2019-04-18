using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using WeAndArDbase.Model;
using System;

namespace WeAndArDbase.View {
	/// <summary>
	/// Логика взаимодействия для FormRequest.xaml
	/// </summary>
	public partial class FormRequest : Window {

		public FormRequest() {
			InitializeComponent();

			/// Задание источников для List - ов
			ComboBoxCartridgeSize.ItemsSource = Model.WorkWithDB.ThisObject.NameCartridge;
			ComboBoxCategoryWeapon.ItemsSource = Model.WorkWithDB.ThisObject.NameCategoryWeapon;
			ComboBoxCountryOfDevelopment.ItemsSource = Model.WorkWithDB.ThisObject.NameCountry;
			ComboBoxClassArmorDestroy.ItemsSource = Model.WorkWithDB.ThisObject.ClassArmor;

			TextBoxRateOfFire.TextChanged += TextBox_TextChangedDigit;
			TextBoxStartingSpeed.TextChanged += TextBox_TextChangedDigit;
			TextBoxSightingRange.TextChanged += TextBox_TextChangedDigit;
			TextBoxWeightW.TextChanged += TextBox_TextChangedDigit;

			DataGridMainField.ItemsSource = Model.WorkWithDB.ThisObject.WSearchColl;

			ButtonUpdate.Click += UpdateEvent;
			KeyDown += MainWindow_KeyDown;

			UpdateEvent( null, null );

			ButtonClear.Click += ButtonClear_Click;
			ButtonSearch.Click += ButtonSearch_Click;

			ButtonSearch_Click( null, null );
		}

		private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
			if (e.Key == System.Windows.Input.Key.F5)
				UpdateEvent( null, null );
		}

		private void UpdateEvent(object sender, RoutedEventArgs e) {

			if (!WorkWithDB.ThisObject.RequestAllRecord()) {
				MessageBox.Show( "При запросе к БД произошла ошибка", "Ошибка" );
				TextBlocStatusUpdate.Text = "обновление завершено с ошибкой " + DateTime.Now.ToString( "hh:mm:ss" );

				ButtonSearch_Click( null, null );
			}
			else
				TextBlocStatusUpdate.Text = "обновление прошло успешно " + DateTime.Now.ToString( "hh:mm:ss" );
		}

		private void ButtonSearch_Click(object sender, RoutedEventArgs e) {
			try {
				if (!WorkWithDB.ThisObject.RequestSearch( ( ComboBoxCategoryWeapon.SelectedItem as string ), TextBoxNameWeapon.Text, TextBoxYearOfDeveloper.Text, TextBoxYearOfAdopter.Text,
					( ComboBoxCountryOfDevelopment.SelectedItem as string ), ( ComboBoxCartridgeSize.SelectedItem as string ), ( ComboBoxClassArmorDestroy.SelectedItem as string ),
					TextBoxRateOfFire.Text, TextBoxStartingSpeed.Text, TextBoxSightingRange.Text, TextBoxWeightW.Text ))
					throw new Exception( "Ошибка поиска" );
			}
			catch (Exception error) {
				MessageBox.Show( error.Message );
			}
		}

		private void TextBox_TextChangedDigit(object sender, TextChangedEventArgs e) {
			char? forFloat(string str) { foreach (char c in str) { if (!( char.IsDigit( c ) || c == '.' || c == ',' || c == '-' )) return c; } return null; }

			( (TextBox)sender ).Text = forFloat( ( (TextBox)sender ).Text ) == null ? ( (TextBox)sender ).Text : String.Join( "", ( (TextBox)sender ).Text.Split( (char)forFloat( ( (TextBox)sender ).Text ) ) );
		}

		private void ButtonClear_Click(object sender, RoutedEventArgs e) {
			ComboBoxCategoryWeapon.SelectedIndex = -1;

			TextBoxNameWeapon.Text = "";
			TextBoxYearOfDeveloper.Text = "";
			TextBoxYearOfAdopter.Text = "";

			ComboBoxCountryOfDevelopment.SelectedIndex = -1;
			ComboBoxCartridgeSize.SelectedIndex = -1;
			ComboBoxClassArmorDestroy.SelectedIndex = -1;

			TextBoxRateOfFire.Text = "";
			TextBoxStartingSpeed.Text = "";
			TextBoxSightingRange.Text = "";
			TextBoxWeightW.Text = "";
		}
	}
}