using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using WeAndArDbase.Model;

namespace WeAndArDbase.View {
	/// <summary>
	/// Логика взаимодействия для FormDeleted.xaml
	/// </summary>
	public partial class FormDeleted : Window {

		public FormDeleted() {
			InitializeComponent();

			ButtonUpdate.Click += UpdateEvent;
			KeyDown += MainWindow_KeyDown;

			DataGridArmor.ItemsSource = Model.WorkWithDB.ThisObject.ARCollection;
			DataGridWeapon.ItemsSource = Model.WorkWithDB.ThisObject.WPCollection;
			DataGridCartridge.ItemsSource = Model.WorkWithDB.ThisObject.CRCollection;
			DataGridWeaponCategory.ItemsSource = Model.WorkWithDB.ThisObject.WPCCollection;
			DataGridCountry.ItemsSource = Model.WorkWithDB.ThisObject.CCollection;

			ButtonDeleted.Click += ButtonDeleted_Click;

			UpdateEvent( null, null );
		}

		private void ButtonDeleted_Click(object sender, RoutedEventArgs e) {
			switch (TabControlEdit.SelectedIndex) {
				case 0: {
						if (DataGridWeapon.SelectedItems.Count != 0) {
							ObservableCollection<WeaponRecord> weaponCollectionDelete = new ObservableCollection<WeaponRecord>();

							for (int i = 0; i < DataGridWeapon.SelectedItems.Count; ++i)
								weaponCollectionDelete.Add( DataGridWeapon.SelectedItems[ i ] as WeaponRecord );

							if (MessageBox.Show( "Потверждение", "Потвердите удаление ячеек", MessageBoxButton.YesNo ) == MessageBoxResult.Yes)
								WorkWithDB.ThisObject.RequestDeleted( weaponCollectionDelete );
						}
						else
							MessageBox.Show( "Ячейки для удаления не выбраны", "Ошибка" );

						break;
					}
				case 1: {
						if (DataGridCartridge.SelectedItems.Count != 0) {
							ObservableCollection<CartridgeRecord> cartridgeCollectionDelete = new ObservableCollection<CartridgeRecord>();

							for (int i = 0; i < DataGridCartridge.SelectedItems.Count; ++i)
								cartridgeCollectionDelete.Add( DataGridCartridge.SelectedItems[ i ] as CartridgeRecord );

							if (MessageBox.Show( "Потверждение", "Потвердите удаление ячеек", MessageBoxButton.YesNo ) == MessageBoxResult.Yes)
								WorkWithDB.ThisObject.RequestDeleted( cartridgeCollectionDelete );
						}
						else
							MessageBox.Show( "Ячейки для удаления не выбраны", "Ошибка" );
						break;
					}
				case 2: {
						if (DataGridArmor.SelectedItems.Count != 0) {
							ObservableCollection<ArmorRecord> armorCollectionDelete = new ObservableCollection<ArmorRecord>();

							for (int i = 0; i < DataGridArmor.SelectedItems.Count; ++i)
								armorCollectionDelete.Add( DataGridArmor.SelectedItems[ i ] as ArmorRecord );

							if (MessageBox.Show( "Потверждение", "Потвердите удаление ячеек", MessageBoxButton.YesNo ) == MessageBoxResult.Yes)
								WorkWithDB.ThisObject.RequestDeleted( armorCollectionDelete );
						}
						else
							MessageBox.Show( "Ячейки для удаления не выбраны", "Ошибка" );
						break;
					}
				case 3: {
						if (DataGridCountry.SelectedItems.Count != 0) {
							ObservableCollection<CountryRecord> contryCollectionDelete = new ObservableCollection<CountryRecord>();

							for (int i = 0; i < DataGridCountry.SelectedItems.Count; ++i)
								contryCollectionDelete.Add( DataGridCountry.SelectedItems[ i ] as CountryRecord );

							if (MessageBox.Show( "Потверждение", "Потвердите удаление ячеек", MessageBoxButton.YesNo ) == MessageBoxResult.Yes)
								WorkWithDB.ThisObject.RequestDeleted( contryCollectionDelete );
						}
						else
							MessageBox.Show( "Ячейки для удаления не выбраны", "Ошибка" );
						break;
					}
				case 4: {
						if (DataGridWeaponCategory.SelectedItems.Count != 0) {
							ObservableCollection<WeaponsCategoryRecord> weaponCategoryCollectionDelete = new ObservableCollection<WeaponsCategoryRecord>();

							for (int i = 0; i < DataGridWeaponCategory.SelectedItems.Count; ++i)
								weaponCategoryCollectionDelete.Add( DataGridWeaponCategory.SelectedItems[ i ] as WeaponsCategoryRecord );

							if (MessageBox.Show( "Потверждение", "Потвердите удаление ячеек", MessageBoxButton.YesNo ) == MessageBoxResult.Yes)
								WorkWithDB.ThisObject.RequestDeleted( weaponCategoryCollectionDelete );
						}
						else
							MessageBox.Show( "Ячейки для удаления не выбраны", "Ошибка" );
						break;
					}
				default: {
						MessageBox.Show( "Ошибка выбора вкладки", "Ошибка" );
						break;
					}
			}

			UpdateEvent( null, null );
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
	}
}