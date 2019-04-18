using System;
using System.Collections.ObjectModel;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using WeAndArDbase.Model;

namespace WeAndArDbase.View {
	/// <summary>
	/// Логика взаимодействия для FormRequest.xaml
	/// </summary>
	public partial class FormEdit : Window {
		private Int16 _selectedIndex;
		delegate void _baseEvent();
		event _baseEvent ChangeTabItem;

		public FormEdit() {
			InitializeComponent();

			ButtonUpdate.Click += UpdateEvent;
			KeyDown += MainWindow_KeyDown;

			ButtonClear.Click += ButtonClear_Click;
			ButtonNewRecord.Click += ButtonNewRecord_Click;
			ButtonSave.Click += ButtonSave_Click;
			ButtonSaveDB.Click += ButtonSaveDB_Click;

			TabControlEdit.SelectionChanged += TabControlEdit_SelectionChanged;
			ChangeTabItem += FormEdit_ChangeTabItem;

			TextBoxCartridgeDiameter.TextChanged += TextBox_TextChangedDigit;
			TextBoxCartridgeLenght.TextChanged += TextBox_TextChangedDigit;
			TextBoxCartridgeWeight.TextChanged += TextBox_TextChangedDigit;

			TextBoxRateOfFire.TextChanged += TextBox_TextChangedDigit;
			TextBoxStartingSpeed.TextChanged += TextBox_TextChangedDigit;
			TextBoxSightingRange.TextChanged += TextBox_TextChangedDigit;
			TextBoxWeightW.TextChanged += TextBox_TextChangedDigit;

			TextBoxArmorWeightB.TextChanged += TextBox_TextChangedDigit;
			TextBoxArmorSpeedB.TextChanged += TextBox_TextChangedDigit;


			TextBoxYearOfAdopter.TextChanged += TextBoxYear_TextChanged;
			TextBoxYearOfDeveloper.TextChanged += TextBoxYear_TextChanged;

			DataGridArmor.ItemsSource = Model.WorkWithDB.ThisObject.ARCollection;
			DataGridWeapon.ItemsSource = Model.WorkWithDB.ThisObject.WPCollection;
			DataGridCartridge.ItemsSource = Model.WorkWithDB.ThisObject.CRCollection;
			DataGridWeaponCategory.ItemsSource = Model.WorkWithDB.ThisObject.WPCCollection;
			DataGridCountry.ItemsSource = Model.WorkWithDB.ThisObject.CCollection;

			/// Задание источников для List - ов
			ComboBoxCartridgeSize.ItemsSource = Model.WorkWithDB.ThisObject.NameCartridge;
			ComboBoxCategoryWeapon.ItemsSource = Model.WorkWithDB.ThisObject.NameCategoryWeapon;
			ComboBoxCountryOfDevelopment.ItemsSource = Model.WorkWithDB.ThisObject.NameCountry;

			DataGridWeapon.SelectionChanged += DataGridWeapons_SelectionChanged;
			DataGridCartridge.SelectionChanged += DataGridCartridge_SelectionChanged;
			DataGridArmor.SelectionChanged += DataGridArmor_SelectionChanged;
			DataGridCountry.SelectionChanged += DataGridCountry_SelectionChanged;
			DataGridWeaponCategory.SelectionChanged += DataGridWeaponCategory_SelectionChanged;

			TextBoxArmorClassKey.IsEnabled = false;

			TurnObject( false );

			UpdateEvent( null, null );

			_selectedIndex = 0;
		}

		private void FormEdit_ChangeTabItem() {
			TurnObject( false );
			try {
				switch (TabControlEdit.SelectedIndex) {
					case 0: {

							DataGridWeapon.IsEnabled = true;
							break;
						}
					case 1: {

							DataGridCartridge.IsEnabled = true;
							break;
						}
					case 2: {

							DataGridArmor.IsEnabled = true;
							TextBoxArmorClassKey.IsEnabled = false;
							break;
						}
					case 3: {

							DataGridCountry.IsEnabled = true;
							break;
						}
					case 4: {

							DataGridWeaponCategory.IsEnabled = true;
							break;
						}
					default: {
							throw new Exception( "Ошибка выбора" );
						}
				}

				ButtonClear.IsEnabled = true;
				ButtonSave.IsEnabled = true;
				ButtonNewRecord.IsEnabled = true;
			}
			catch (Exception error) {
				MessageBox.Show( "Ошибка выбора вкладок", "Ошибка" );
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}
		}

		private void TabControlEdit_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (_selectedIndex != TabControlEdit.SelectedIndex) {
				_selectedIndex = (Int16)TabControlEdit.SelectedIndex;
				if (ChangeTabItem != null)
					ChangeTabItem();
			}
		}

		private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
			if (e.Key == System.Windows.Input.Key.F5)
				UpdateEvent( null, null );
		}

		private void UpdateEvent(object sender, RoutedEventArgs e) {

			if (!WorkWithDB.ThisObject.RequestAllRecord()) {
				MessageBox.Show( "При запросе к БД произошла ошибка", "Ошибка" );
				TextBlocStatusUpdate.Text = "обновление завершено с ошибкой " + DateTime.Now.ToString( "hh:mm:ss" );
			}
			else
				TextBlocStatusUpdate.Text = "обновление прошло успешно " + DateTime.Now.ToString( "hh:mm:ss" );
		}

		private void ButtonSaveDB_Click(object sender, RoutedEventArgs e) {
			try {
				switch (TabControlEdit.SelectedIndex) {
					case 0: {
							for (int i = 0; i < WorkWithDB.ThisObject.WPCollection.Count; ++i) {
								if (WorkWithDB.ThisObject.WPCollection[ i ].ValueChanged()) {
									string codeType = "";
									foreach (WeaponsCategoryRecord obj in WorkWithDB.ThisObject.WPCCollection) {
										if (obj.WeaponName == ( DataGridWeapon.Items[ i ] as WeaponRecord ).Type)
											codeType = obj.CategoryKey;
									}

									string codeCountry = "";
									foreach (CountryRecord obj in WorkWithDB.ThisObject.CCollection) {
										if (obj.CountyName.ToString() == ( DataGridWeapon.Items[ i ] as WeaponRecord ).CountryOfDeveloper)
											codeCountry = obj.CountryID.ToString();
									}

									string codeCartridge = "";
									foreach (CartridgeRecord obj in WorkWithDB.ThisObject.CRCollection) {
										if (obj.FullSize() == ( DataGridWeapon.Items[ i ] as WeaponRecord ).CartridgeSize)
											codeCartridge = obj.CartridgeID.ToString();
									}

									Model.WorkWithDB.ThisObject.Request(
										"UPDATE Weapons SET  " +
										" Type = " + codeType +
										", NameWeapon = '" + ( DataGridWeapon.Items[ i ] as WeaponRecord ).NameWeapon.ToString() +
										"', YearOfDeveloper = #01/01/" + ( DataGridWeapon.Items[ i ] as WeaponRecord ).YearOfDeveloper.ToString() + "#" +
										", Adopted = #01/01/" + ( DataGridWeapon.Items[ i ] as WeaponRecord ).AdopterOfYear.ToString() + "#" +
										", СountryOfDeveloper = " + codeCountry +
										", CartridgeID = " + codeCartridge +
										", RateOfFire = " + ( DataGridWeapon.Items[ i ] as WeaponRecord ).RateOfFire.ToString() +
										", StartingSpeed = " + ( DataGridWeapon.Items[ i ] as WeaponRecord ).StartingSpeed.ToString() +
										", SightingRange = " + ( DataGridWeapon.Items[ i ] as WeaponRecord ).SightingRange.ToString() +
										", Weight = " + ( DataGridWeapon.Items[ i ] as WeaponRecord ).Weight.ToString().Replace( ',', '.' ) +
										" WHERE WeaponID =  " + WorkWithDB.ThisObject.WPCollection[ i ].WeaponID.ToString() + "" );

								}
							}

							for (int i = 0; i < WorkWithDB.ThisObject.WPCollection.Count; ++i) {
								if (WorkWithDB.ThisObject.WPCollection[ i ].NewValue()) {
									string codeType = "";
									foreach (WeaponsCategoryRecord obj in WorkWithDB.ThisObject.WPCCollection) {
										if (obj.WeaponName == ( DataGridWeapon.Items[ i ] as WeaponRecord ).Type)
											codeType = obj.CategoryKey;
									}

									string codeCountry = "";
									foreach (CountryRecord obj in WorkWithDB.ThisObject.CCollection) {
										if (obj.CountyName.ToString() == ( DataGridWeapon.Items[ i ] as WeaponRecord ).CountryOfDeveloper)
											codeCountry = obj.CountryID.ToString();
									}

									string codeCartridge = "";
									foreach (CartridgeRecord obj in WorkWithDB.ThisObject.CRCollection) {
										if (obj.FullSize() == ( DataGridWeapon.Items[ i ] as WeaponRecord ).CartridgeSize)
											codeCartridge = obj.CartridgeID.ToString();
									}

									Model.WorkWithDB.ThisObject.Request( "INSERT INTO Weapons (WeaponID, Type, NameWeapon, YearOfDeveloper, Adopted, " +
										"СountryOfDeveloper, CartridgeID, RateOfFire, StartingSpeed, SightingRange, Weight ) VALUES ( " +
										WorkWithDB.ThisObject.WPCollection[ i ].WeaponID.ToString() + ", " +
										codeType + ", " +
										( DataGridWeapon.Items[ i ] as WeaponRecord ).NameWeapon + ", " +
									   "#01/01/" + ( DataGridWeapon.Items[ i ] as WeaponRecord ).YearOfDeveloper.ToString() + "#" + ", " +
									   "#01/01/" + ( DataGridWeapon.Items[ i ] as WeaponRecord ).AdopterOfYear.ToString() + "#" + ", " +
									   codeCountry + ", " +
									   codeCartridge + ", " +
									   ( DataGridWeapon.Items[ i ] as WeaponRecord ).RateOfFire.ToString() + ", " +
									   ( DataGridWeapon.Items[ i ] as WeaponRecord ).StartingSpeed.ToString() + ", " +
									   ( DataGridWeapon.Items[ i ] as WeaponRecord ).SightingRange.ToString() + ", " +
									   ( DataGridWeapon.Items[ i ] as WeaponRecord ).Weight.ToString().Replace( ',', '.' ) + " )" );
								}
							}
							break;
						}
					case 1: {
							for (int i = 0; i < WorkWithDB.ThisObject.CRCollection.Count; ++i) {
								if (WorkWithDB.ThisObject.CRCollection[ i ].ValueChanged()) {
									Model.WorkWithDB.ThisObject.Request(
										"UPDATE CartridgeProperties SET  " +
										"Diameter = " + ( DataGridCartridge.Items[ i ] as CartridgeRecord ).Diameter.ToString().Replace( ',', '.' ) +
										", Lenght = " + ( DataGridCartridge.Items[ i ] as CartridgeRecord ).Lenght.ToString().Replace( ',', '.' ) +
										", Weight  = " + ( DataGridCartridge.Items[ i ] as CartridgeRecord ).Weight.ToString().Replace( ',', '.' ) +
										", Mark = '" + ( DataGridCartridge.Items[ i ] as CartridgeRecord ).Mark + "' " +
										" WHERE CartridgeID =  " + ( DataGridCartridge.Items[ i ] as CartridgeRecord ).CartridgeID.ToString() + "" );
								}
							}

							for (int i = 0; i < WorkWithDB.ThisObject.CRCollection.Count; ++i) {
								if (WorkWithDB.ThisObject.CRCollection[ i ].NewValue()) {
									Model.WorkWithDB.ThisObject.Request(
										"INSERT INTO CartridgeProperties (CartridgeID, Diameter, Lenght, Weight, Mark ) " +
										"VALUES ( " + ( DataGridCartridge.Items[ i ] as CartridgeRecord ).CartridgeID.ToString() + ", " +
										( DataGridCartridge.Items[ i ] as CartridgeRecord ).Diameter.ToString() + ", " +
										( DataGridCartridge.Items[ i ] as CartridgeRecord ).Lenght.ToString() + ", " +
										( DataGridCartridge.Items[ i ] as CartridgeRecord ).Weight.ToString() + ", '" +
										( DataGridCartridge.Items[ i ] as CartridgeRecord ).Mark + "' )" );
								}
							}
							break;
						}
					case 2: {
							for (int i = 0; i < WorkWithDB.ThisObject.ARCollection.Count; ++i) {
								if (WorkWithDB.ThisObject.ARCollection[ i ].ValueChanged()) {
									Model.WorkWithDB.ThisObject.Request(
										"UPDATE ArmorCategory SET  " +
										" NameMensDestruction = '" + ( DataGridArmor.Items[ i ] as ArmorRecord ).NameMensDestruction +
										"', WeightB = " + ( DataGridArmor.Items[ i ] as ArmorRecord ).WeightB.ToString().Replace( ',', '.' ) +
										", SpeedB = " + ( DataGridArmor.Items[ i ] as ArmorRecord ).SpeedB.ToString().Replace( ',', '.' ) +
										" WHERE ClassKey =  '" + ( DataGridArmor.Items[ i ] as ArmorRecord ).ClassKey.ToString() + "'" );
								}
							}

							for (int i = 0; i < WorkWithDB.ThisObject.ARCollection.Count; ++i) {
								if (( DataGridArmor.Items[ i ] as ArmorRecord ).NewValue()) {
									Model.WorkWithDB.ThisObject.Request(
										"INSERT INTO ArmorCategory (ClassKey, NameMensDestruction, WeightB, SpeedB ) " +
										"VALUES ( '" + ( DataGridArmor.Items[ i ] as ArmorRecord ).ClassKey + "', '" +
										( DataGridArmor.Items[ i ] as ArmorRecord ).NameMensDestruction + "', " +
										( DataGridArmor.Items[ i ] as ArmorRecord ).WeightB.ToString().Replace( ',', '.' ) + ", " +
										( DataGridArmor.Items[ i ] as ArmorRecord ).SpeedB.ToString().Replace( ',', '.' ) + " )" );
								}
							}
							break;
						}
					case 3: {
							for (int i = 0; i < WorkWithDB.ThisObject.CCollection.Count; ++i) {
								if (WorkWithDB.ThisObject.CCollection[ i ].ValueChanged()) {
									Model.WorkWithDB.ThisObject.Request(
										"UPDATE Country SET  " +
										" NameCountry = '" + ( DataGridCountry.Items[ i ] as CountryRecord ).CountyName +
										"' WHERE CountryID =  " + ( DataGridCountry.Items[ i ] as CountryRecord ).CountryID );
								}
							}

							for (int i = 0; i < WorkWithDB.ThisObject.CCollection.Count; ++i) {
								if (( DataGridCountry.Items[ i ] as CountryRecord ).NewValue()) {
									Model.WorkWithDB.ThisObject.Request(
										"INSERT INTO Country (CountryID, NameCountry ) " +
										"VALUES ( " + ( DataGridCountry.Items[ i ] as CountryRecord ).CountryID + ", '" +
										( DataGridCountry.Items[ i ] as CountryRecord ).CountyName + "' )" );
								}
							}
							break;
						}
					case 4: {
							for (int i = 0; i < WorkWithDB.ThisObject.WPCCollection.Count; ++i) {
								if (WorkWithDB.ThisObject.WPCCollection[ i ].ValueChanged()) {
									Model.WorkWithDB.ThisObject.Request(
										"UPDATE WeaponCategory SET  " +
										" WeaponName = '" + ( DataGridWeaponCategory.Items[ i ] as WeaponsCategoryRecord ).WeaponName +
										"', Description = '" + ( DataGridWeaponCategory.Items[ i ] as WeaponsCategoryRecord ).Description +
										"' WHERE CategoryKey =  " + ( DataGridWeaponCategory.Items[ i ] as WeaponsCategoryRecord ).CategoryKey );
								}
							}

							for (int i = 0; i < WorkWithDB.ThisObject.WPCCollection.Count; ++i) {
								if (( DataGridWeaponCategory.Items[ i ] as WeaponsCategoryRecord ).NewValue()) {
									Model.WorkWithDB.ThisObject.Request(
										"INSERT INTO WeaponCategory (CategoryKey, WeaponName, Description ) " +
										"VALUES ( " + ( DataGridWeaponCategory.Items[ i ] as WeaponsCategoryRecord ).CategoryKey + ", '"
										+ ( DataGridWeaponCategory.Items[ i ] as WeaponsCategoryRecord ).WeaponName + "','"
										+ ( DataGridWeaponCategory.Items[ i ] as WeaponsCategoryRecord ).Description + "')" );
								}
							}
							break;
						}
					default: {
							throw new Exception( "Ошибка выбора" );
						}
				}

				ButtonSave.IsEnabled = true;
				ButtonNewRecord.IsEnabled = true;
			}
			catch (Exception error) {
				MessageBox.Show( "Ошибка очистки полей", "Ошибка" );
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}
		}

		private void ButtonSave_Click(object sender, RoutedEventArgs e) {
			try {
				switch (TabControlEdit.SelectedIndex) {
					case 0: {
							if (WorkWithDB.ThisObject.NewItemWeapon == null)
								return;

							if (ComboBoxCategoryWeapon.SelectedIndex != -1)
								WorkWithDB.ThisObject.NewItemWeapon.Type = ( ComboBoxCategoryWeapon.SelectedItem as string );

							if (TextBoxNameWeapon.Text != "")
								WorkWithDB.ThisObject.NewItemWeapon.NameWeapon = TextBoxNameWeapon.Text;

							if (TextBoxYearOfDeveloper.Text != "" && TextBoxYearOfDeveloper.Text.Length == 4)
								WorkWithDB.ThisObject.NewItemWeapon.YearOfDeveloper = "01.01." + TextBoxYearOfDeveloper.Text;

							if (TextBoxYearOfAdopter.Text != "" && TextBoxYearOfAdopter.Text.Length == 4)
								WorkWithDB.ThisObject.NewItemWeapon.AdopterOfYear = "01.01." + TextBoxYearOfAdopter.Text;

							if (ComboBoxCountryOfDevelopment.SelectedIndex != -1)
								WorkWithDB.ThisObject.NewItemWeapon.CountryOfDeveloper = ( ComboBoxCountryOfDevelopment.SelectedItem as string );

							if (ComboBoxCartridgeSize.SelectedIndex != -1)
								WorkWithDB.ThisObject.NewItemWeapon.CartridgeSize = ComboBoxCartridgeSize.SelectedItem as string;

							if (TextBoxRateOfFire.Text != "")
								WorkWithDB.ThisObject.NewItemWeapon.RateOfFire = UInt16.Parse( TextBoxRateOfFire.Text );

							if (TextBoxStartingSpeed.Text != "")
								WorkWithDB.ThisObject.NewItemWeapon.StartingSpeed = UInt16.Parse( TextBoxStartingSpeed.Text );

							if (TextBoxSightingRange.Text != "")
								WorkWithDB.ThisObject.NewItemWeapon.SightingRange = UInt16.Parse( TextBoxSightingRange.Text );

							if (TextBoxWeightW.Text != "")
								WorkWithDB.ThisObject.NewItemWeapon.Weight = double.Parse( TextBoxWeightW.Text );

							if (WorkWithDB.ThisObject.WPCollection.IndexOf( WorkWithDB.ThisObject.NewItemWeapon ) == -1)
								WorkWithDB.ThisObject.WPCollection.Add( WorkWithDB.ThisObject.NewItemWeapon );

							DataGridWeapon.IsEnabled = true;
							ButtonSave.IsEnabled = true;
							ButtonNewRecord.IsEnabled = true;

							DataGridWeapon.ItemsSource = null;
							DataGridWeapon.ItemsSource = WorkWithDB.ThisObject.WPCollection;
							break;
						}
					case 1: {
							if (WorkWithDB.ThisObject.NewItemCartridge == null)
								return;

							if (TextBoxCartridgeDiameter.Text != "")
								WorkWithDB.ThisObject.NewItemCartridge.Diameter = float.Parse( TextBoxCartridgeDiameter.Text );

							if (TextBoxCartridgeLenght.Text != "")
								WorkWithDB.ThisObject.NewItemCartridge.Lenght = float.Parse( TextBoxCartridgeLenght.Text );

							if (TextBoxCartridgeWeight.Text != "")
								WorkWithDB.ThisObject.NewItemCartridge.Weight = float.Parse( TextBoxCartridgeWeight.Text );

							if (TextBoxCartridgeMark.Text != "")
								WorkWithDB.ThisObject.NewItemCartridge.Mark = TextBoxCartridgeMark.Text;

							if (WorkWithDB.ThisObject.CRCollection.IndexOf( WorkWithDB.ThisObject.NewItemCartridge ) == -1)
								WorkWithDB.ThisObject.CRCollection.Add( WorkWithDB.ThisObject.NewItemCartridge );

							DataGridCartridge.IsEnabled = true;
							ButtonSave.IsEnabled = true;
							ButtonNewRecord.IsEnabled = true;

							DataGridCartridge.ItemsSource = null;
							DataGridCartridge.ItemsSource = WorkWithDB.ThisObject.CRCollection;
							break;
						}
					case 2: {
							if (TextBoxArmorClassKey.IsEnabled == true) {
								foreach (ArmorRecord obj in WorkWithDB.ThisObject.ARCollection) {
									if (obj.ClassKey == TextBoxArmorClassKey.Text) {
										MessageBox.Show( "Значение в ключевом поле уже используется" );
										return;
									}
								}

								if (TextBoxArmorClassKey.Text != "") {
									WorkWithDB.ThisObject.NewItemArmor.ClassKey = TextBoxArmorClassKey.Text;
								}
								else {
									MessageBox.Show( "Ключевое поле пусто" );
									return;
								}
							}

							if (WorkWithDB.ThisObject.NewItemArmor == null)
								return;

							if (TextBoxArmorNameMensDestruction.Text != "")
								WorkWithDB.ThisObject.NewItemArmor.NameMensDestruction = TextBoxArmorNameMensDestruction.Text;

							if (TextBoxArmorWeightB.Text != "")
								WorkWithDB.ThisObject.NewItemArmor.WeightB = float.Parse( TextBoxArmorWeightB.Text );

							if (TextBoxArmorSpeedB.Text != "")
								WorkWithDB.ThisObject.NewItemArmor.SpeedB = float.Parse( TextBoxArmorSpeedB.Text );

							if (WorkWithDB.ThisObject.ARCollection.IndexOf( WorkWithDB.ThisObject.NewItemArmor ) == -1)
								WorkWithDB.ThisObject.ARCollection.Add( WorkWithDB.ThisObject.NewItemArmor );

							DataGridArmor.IsEnabled = true;
							ButtonSave.IsEnabled = true;
							ButtonNewRecord.IsEnabled = true;
							TextBoxArmorClassKey.IsEnabled = false;

							DataGridArmor.ItemsSource = null;
							DataGridArmor.ItemsSource = WorkWithDB.ThisObject.ARCollection;
							break;
						}
					case 3: {
							if (WorkWithDB.ThisObject.NewItemCountry == null)
								return;

							if (TextBoxCountryName.Text != "")
								WorkWithDB.ThisObject.NewItemCountry.CountyName = TextBoxCountryName.Text;

							if (WorkWithDB.ThisObject.CCollection.IndexOf( WorkWithDB.ThisObject.NewItemCountry ) == -1)
								WorkWithDB.ThisObject.CCollection.Add( WorkWithDB.ThisObject.NewItemCountry );

							DataGridCountry.IsEnabled = true;
							ButtonSave.IsEnabled = true;
							ButtonNewRecord.IsEnabled = true;

							DataGridCountry.ItemsSource = null;
							DataGridCountry.ItemsSource = WorkWithDB.ThisObject.CCollection;
							break;
						}
					case 4: {
							if (WorkWithDB.ThisObject.NewItemWeaponCategory == null)
								return;
							if (TextBoxWeaponCategoryName.Text != "")
								WorkWithDB.ThisObject.NewItemWeaponCategory.WeaponName = TextBoxWeaponCategoryName.Text;

							if (TextBoxWeaponCategoryDescription.Text != "")
								WorkWithDB.ThisObject.NewItemWeaponCategory.Description = TextBoxWeaponCategoryDescription.Text;

							if (WorkWithDB.ThisObject.WPCCollection.IndexOf( WorkWithDB.ThisObject.NewItemWeaponCategory ) == -1)
								WorkWithDB.ThisObject.WPCCollection.Add( WorkWithDB.ThisObject.NewItemWeaponCategory );

							DataGridWeaponCategory.IsEnabled = true;
							ButtonSave.IsEnabled = true;
							ButtonNewRecord.IsEnabled = true;

							DataGridWeaponCategory.ItemsSource = null;
							DataGridWeaponCategory.ItemsSource = WorkWithDB.ThisObject.WPCCollection;
							break;
						}
					default: {
							throw new Exception( "Ошибка выбора" );
						}
				}

				ButtonSave.IsEnabled = true;
				ButtonNewRecord.IsEnabled = true;
			}
			catch (Exception error) {
				MessageBox.Show( "Ошибка очистки полей", "Ошибка" );
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}
		}

		private void ButtonNewRecord_Click(object sender, RoutedEventArgs e) {
			try {
				switch (TabControlEdit.SelectedIndex) {
					case 0: {
							List<int> index = new List<int>();

							foreach (WeaponRecord obj in WorkWithDB.ThisObject.WPCollection) {
								index.Add( obj.WeaponID );
							}

							WorkWithDB.ThisObject.NewItemWeapon = new WeaponRecord( NewIndex( index ).ToString(), "error", "error", "01.01.2001", "01.01.2001", "error", "0", "0", "0", "0", "0" );

							WorkWithDB.ThisObject.NewItemWeapon.NewValue( true );

							DataGridWeapon.IsEnabled = false;
							ButtonSave.IsEnabled = false;
							ButtonNewRecord.IsEnabled = false;
							TurnObject( true );
							break;
						}
					case 1: {
							List<int> index = new List<int>();

							foreach (CartridgeRecord obj in WorkWithDB.ThisObject.CRCollection) {
								index.Add( obj.CartridgeID );
							}

							WorkWithDB.ThisObject.NewItemCartridge = new CartridgeRecord( NewIndex( index ).ToString(), "0", "0", "0", "error" );

							WorkWithDB.ThisObject.NewItemCartridge.NewValue( true );

							DataGridCartridge.IsEnabled = false;
							ButtonSave.IsEnabled = false;
							ButtonNewRecord.IsEnabled = false;
							TurnObject( true );
							break;
						}
					case 2: {
							WorkWithDB.ThisObject.NewItemArmor = new ArmorRecord( "error", "0", "0", "0" );

							WorkWithDB.ThisObject.NewItemArmor.NewValue( true );

							DataGridCartridge.IsEnabled = false;
							ButtonSave.IsEnabled = false;
							TextBoxArmorClassKey.IsEnabled = true;
							ButtonNewRecord.IsEnabled = false;
							TurnObject( true );
							break;
						}
					case 3: {
							List<int> index = new List<int>();

							foreach (CountryRecord obj in WorkWithDB.ThisObject.CCollection) {
								index.Add( obj.CountryID );
							}

							WorkWithDB.ThisObject.NewItemCountry = new CountryRecord( NewIndex( index ).ToString(), "error" );

							WorkWithDB.ThisObject.NewItemCountry.NewValue( true );

							DataGridCountry.IsEnabled = false;
							ButtonSave.IsEnabled = false;
							ButtonNewRecord.IsEnabled = false;
							TurnObject( true );
							break;
						}
					case 4: {
							List<int> index = new List<int>();

							foreach (WeaponsCategoryRecord obj in WorkWithDB.ThisObject.WPCCollection) {
								index.Add( int.Parse( obj.CategoryKey ) );
							}

							WorkWithDB.ThisObject.NewItemWeaponCategory = new WeaponsCategoryRecord( NewIndex( index ).ToString(), "error", "error" );

							WorkWithDB.ThisObject.NewItemWeaponCategory.NewValue( true );

							DataGridWeaponCategory.IsEnabled = false;
							ButtonSave.IsEnabled = false;
							ButtonNewRecord.IsEnabled = false;
							TurnObject( true );
							break;
						}
					default: {
							throw new Exception( "Ошибка выбора" );
						}
				}

				ButtonSave.IsEnabled = true;
				ButtonNewRecord.IsEnabled = true;
			}
			catch (Exception error) {
				MessageBox.Show( "Ошибка очистки полей", "Ошибка" );
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}
		}

		private void ButtonClear_Click(object sender, RoutedEventArgs e) {
			try {
				switch (TabControlEdit.SelectedIndex) {
					case 0: {
							WorkWithDB.ThisObject.NewItemWeapon = null;

							ComboBoxCategoryWeapon.SelectedIndex = -1;
							TextBoxNameWeapon.Text = "";
							TextBoxYearOfDeveloper.Text = "";
							TextBoxYearOfAdopter.Text = "";
							ComboBoxCountryOfDevelopment.SelectedIndex = -1;
							ComboBoxCartridgeSize.SelectedIndex = -1;
							TextBoxRateOfFire.Text = "";
							TextBoxStartingSpeed.Text = "";
							TextBoxSightingRange.Text = "";
							TextBoxWeightW.Text = "";

							DataGridWeapon.SelectedIndex = -1;

							DataGridWeapon.IsEnabled = true;
							break;
						}
					case 1: {
							WorkWithDB.ThisObject.NewItemCartridge = null;

							TextBoxCartridgeDiameter.Text = "";
							TextBoxCartridgeLenght.Text = "";
							TextBoxCartridgeWeight.Text = "";
							TextBoxCartridgeMark.Text = "";

							DataGridCartridge.IsEnabled = true;
							break;
						}
					case 2: {
							WorkWithDB.ThisObject.NewItemArmor = null;

							TextBoxArmorClassKey.Text = "";
							TextBoxArmorNameMensDestruction.Text = "";
							TextBoxArmorSpeedB.Text = "";
							TextBoxArmorWeightB.Text = "";

							DataGridArmor.IsEnabled = true;
							TextBoxArmorClassKey.IsEnabled = false;
							break;
						}
					case 3: {
							WorkWithDB.ThisObject.NewItemCountry = null;

							TextBoxCountryName.Text = "";

							DataGridCountry.IsEnabled = true;
							break;
						}
					case 4: {
							WorkWithDB.ThisObject.NewItemWeaponCategory = null;

							TextBoxWeaponCategoryDescription.Text = "";
							TextBoxWeaponCategoryName.Text = "";

							DataGridWeaponCategory.IsEnabled = true;
							break;
						}
					default: {
							throw new Exception( "Ошибка выбора" );
						}
				}

				ButtonSave.IsEnabled = true;
				ButtonNewRecord.IsEnabled = true;
			}
			catch (Exception error) {
				MessageBox.Show( "Ошибка очистки полей", "Ошибка" );
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}
		}

		#region SelectionChange
		private void DataGridWeaponCategory_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (DataGridWeaponCategory.SelectedIndex == -1) {
				TurnObject( false );
				return;
			}
			WorkWithDB.ThisObject.NewItemWeaponCategory = ( sender as DataGrid ).SelectedItem as WeaponsCategoryRecord;

			TextBoxWeaponCategoryName.Text = WorkWithDB.ThisObject.NewItemWeaponCategory.WeaponName;
			TextBoxWeaponCategoryDescription.Text = WorkWithDB.ThisObject.NewItemWeaponCategory.Description;

			TurnObject( true );
		}

		private void DataGridCountry_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (DataGridCountry.SelectedIndex == -1) {
				TurnObject( false );
				return;
			}
			WorkWithDB.ThisObject.NewItemCountry = ( sender as DataGrid ).SelectedItem as CountryRecord;

			TextBoxCountryName.Text = WorkWithDB.ThisObject.NewItemCountry.CountyName;

			TurnObject( true );
		}

		private void DataGridArmor_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (DataGridArmor.SelectedIndex == -1) {
				TurnObject( false );
				return;
			}
			WorkWithDB.ThisObject.NewItemArmor = ( sender as DataGrid ).SelectedItem as ArmorRecord;

			TextBoxArmorClassKey.Text = WorkWithDB.ThisObject.NewItemArmor.ClassKey;
			TextBoxArmorNameMensDestruction.Text = WorkWithDB.ThisObject.NewItemArmor.NameMensDestruction;
			TextBoxArmorWeightB.Text = WorkWithDB.ThisObject.NewItemArmor.WeightB.ToString();
			TextBoxArmorSpeedB.Text = WorkWithDB.ThisObject.NewItemArmor.SpeedB.ToString();

			TurnObject( true );
			TextBoxArmorClassKey.IsEnabled = false;
		}

		private void DataGridCartridge_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (DataGridCartridge.SelectedIndex == -1) {
				TurnObject( false );
				return;
			}
			WorkWithDB.ThisObject.NewItemCartridge = ( sender as DataGrid ).SelectedItem as CartridgeRecord;

			TextBoxCartridgeDiameter.Text = WorkWithDB.ThisObject.NewItemCartridge.Diameter.ToString();
			TextBoxCartridgeLenght.Text = WorkWithDB.ThisObject.NewItemCartridge.Lenght.ToString();
			TextBoxCartridgeWeight.Text = WorkWithDB.ThisObject.NewItemCartridge.Weight.ToString();
			TextBoxCartridgeMark.Text = WorkWithDB.ThisObject.NewItemCartridge.Mark;

			TurnObject( true );
		}

		private void DataGridWeapons_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			if (DataGridWeapon.SelectedIndex == -1) {
				TurnObject( false );
				return;
			}
			WorkWithDB.ThisObject.NewItemWeapon = ( sender as DataGrid ).SelectedItem as WeaponRecord;

			ComboBoxCategoryWeapon.SelectedIndex = WorkWithDB.ThisObject.NameCategoryWeapon.IndexOf( WorkWithDB.ThisObject.NewItemWeapon.Type );
			TextBoxNameWeapon.Text = WorkWithDB.ThisObject.NewItemWeapon.NameWeapon;
			TextBoxYearOfDeveloper.Text = WorkWithDB.ThisObject.NewItemWeapon.YearOfDeveloper;
			TextBoxYearOfAdopter.Text = WorkWithDB.ThisObject.NewItemWeapon.AdopterOfYear;
			ComboBoxCountryOfDevelopment.SelectedIndex = WorkWithDB.ThisObject.NameCountry.IndexOf( WorkWithDB.ThisObject.NewItemWeapon.CountryOfDeveloper );
			ComboBoxCartridgeSize.SelectedIndex = WorkWithDB.ThisObject.NameCartridge.IndexOf( WorkWithDB.ThisObject.NewItemWeapon.CartridgeSize );
			TextBoxRateOfFire.Text = WorkWithDB.ThisObject.NewItemWeapon.RateOfFire.ToString();
			TextBoxStartingSpeed.Text = WorkWithDB.ThisObject.NewItemWeapon.StartingSpeed.ToString();
			TextBoxSightingRange.Text = WorkWithDB.ThisObject.NewItemWeapon.SightingRange.ToString();
			TextBoxWeightW.Text = WorkWithDB.ThisObject.NewItemWeapon.Weight.ToString();

			TurnObject( true );
		}
		#endregion

		private void TextBoxYear_TextChanged(object sender, TextChangedEventArgs e) {
			if (( sender as TextBox ).Text.Length < 4)
				return;
			else {
				try {
					( sender as TextBox ).Text = ( sender as TextBox ).Text.Remove( 4, 1 );
					( sender as TextBox ).CaretIndex = ( sender as TextBox ).Text.Length;
				}
				catch {

				}
			}
		}

		private void TextBox_TextChangedDigit(object sender, TextChangedEventArgs e) {
			char? forFloat(string str) { foreach (char c in str) { if (!( char.IsDigit( c ) || c == '.' || c == ',' )) return c; } return null; }

			( (TextBox)sender ).Text = forFloat( ( (TextBox)sender ).Text ) == null ? ( (TextBox)sender ).Text : String.Join( "", ( (TextBox)sender ).Text.Split( (char)forFloat( ( (TextBox)sender ).Text ) ) );
		}

		private int NewIndex(List<int> arrayIndex) {
			arrayIndex.Sort();

			for (int i = 0; i < arrayIndex.Count - 1; ++i) {
				if (arrayIndex[ i + 1 ] - arrayIndex[ i ] > 1) {
					return arrayIndex[ i ] + 1;
				}
			}

			return arrayIndex[ arrayIndex.Count - 1 ] + 1;
		}

		private void TurnObject(bool value) {
			ComboBoxCartridgeSize.IsEnabled = value;
			ComboBoxCategoryWeapon.IsEnabled = value;
			ComboBoxCountryOfDevelopment.IsEnabled = value;

			TextBoxArmorClassKey.IsEnabled = value;
			TextBoxArmorNameMensDestruction.IsEnabled = value;
			TextBoxArmorSpeedB.IsEnabled = value;
			TextBoxArmorWeightB.IsEnabled = value;
			TextBoxCartridgeDiameter.IsEnabled = value;
			TextBoxCartridgeLenght.IsEnabled = value;
			TextBoxCartridgeMark.IsEnabled = value;
			TextBoxCartridgeWeight.IsEnabled = value;
			TextBoxCountryName.IsEnabled = value;
			TextBoxNameWeapon.IsEnabled = value;
			TextBoxRateOfFire.IsEnabled = value;
			TextBoxSightingRange.IsEnabled = value;
			TextBoxStartingSpeed.IsEnabled = value;
			TextBoxWeaponCategoryDescription.IsEnabled = value;
			TextBoxWeaponCategoryName.IsEnabled = value;
			TextBoxWeightW.IsEnabled = value;
			TextBoxYearOfAdopter.IsEnabled = value;
			TextBoxYearOfDeveloper.IsEnabled = value;

			ButtonClear.IsEnabled = value;

			ButtonSave.IsEnabled = value;
		}
	}
}