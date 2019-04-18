using System;
using System.IO;
using System.Diagnostics;
using System.Windows;
using System.Collections.ObjectModel;
using WeAndArDbase.Model;

namespace WeAndArDbase {
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window {
		/// <summary>
		/// Объект обеспечивающий доступ к БД
		/// </summary>
		private Model.WorkWithDB _objectWorkWithDB;

		/// <summary>
		/// Форма изменения и добавление записей 
		/// </summary>
		private View.FormEdit _formEditAndNewRecord;
		/// <summary>
		/// Форма удаления записей
		/// </summary>
		private View.FormDeleted _formDeleted;
		/// <summary>
		/// Форма запроса данных из БД
		/// </summary>
		private View.FormRequest _formRequest;

		public MainWindow() {
			InitializeComponent();

			_objectWorkWithDB = new Model.WorkWithDB();
			_objectWorkWithDB.ConnectToBase();

			ButtonUpdate.Click += UpdateEvent;
			KeyDown += MainWindow_KeyDown;

			UpdateEvent( null, null );

			/// Задание ресурсов DataGrid - ов 
			DataGridArmor.ItemsSource = Model.WorkWithDB.ThisObject.ARCollection;
			DataGridWeapon.ItemsSource = Model.WorkWithDB.ThisObject.WPCollection;
			DataGridCartridge.ItemsSource = Model.WorkWithDB.ThisObject.CRCollection;
			DataGridWeaponCategory.ItemsSource = Model.WorkWithDB.ThisObject.WPCCollection;

			MenuItemNewOrEdit.Click += MenuItemNewOrEdit_Click;
			MenuItemDeletedRecord.Click += MenuItemDeletedRecord_Click;
			MenuItemRequest.Click += MenuItemRequest_Click;
			MenyItemInformation.Click += MenyItemInformation_Click;

			this.Closed += MainWindow_Closed;

		}

		private void MainWindow_KeyDown(object sender, System.Windows.Input.KeyEventArgs e) {
			if (e.Key == System.Windows.Input.Key.F5)
				UpdateEvent( null, null );
		}

		private void UpdateEvent(object sender, RoutedEventArgs e) {

			if (!WorkWithDB.ThisObject.RequestAllRecord() ) {
				MessageBox.Show( "При запросе к БД произошла ошибка", "Ошибка" );
				TextBlocStatusUpdate.Text = "обновление завершено с ошибкой " + DateTime.Now.ToString("hh:mm:ss");
			}
			else
				TextBlocStatusUpdate.Text = "обновление прошло успешно " + DateTime.Now.ToString( "hh:mm:ss" );
		}

		private void MenyItemInformation_Click(object sender, RoutedEventArgs e) {
			string information = "============================================\n" +
												"                                  Информация о базе данных                              \n" +
												" Статус подключения: " + _objectWorkWithDB.GetConnection.State.ToString() + "\n" +
												" Колличество таблиц в базе: 5 \n" +
												" Колличество записей в базе данных: " + ( DataGridArmor.Items.Count +
												DataGridCartridge.Items.Count + DataGridWeapon.Items.Count +
												DataGridWeaponCategory.Items.Count ).ToString() + "\n";
			MessageBox.Show( information );
		}

		private void MenuItemRequest_Click(object sender, RoutedEventArgs e) {
			_formRequest = new View.FormRequest();
			_formRequest.Closed += _formRequest_Closed;
			_formRequest.Show();
			MenuItemRequest.IsEnabled = false;
		}

		private void _formRequest_Closed(object sender, EventArgs e) {
			MenuItemRequest.IsEnabled = true;
		}

		private void _formEdit_Closed(object sender, EventArgs e) {
			MenuItemNewOrEdit.IsEnabled = true;
		}

		private void _formDeleted_Closed(object sender, EventArgs e) {
			MenuItemDeletedRecord.IsEnabled = true;
		}

		private void MenuItemDeletedRecord_Click(object sender, RoutedEventArgs e) {
			_formDeleted = new View.FormDeleted();
			_formDeleted.Closed += _formDeleted_Closed;
			_formDeleted.Show();
			MenuItemDeletedRecord.IsEnabled = false;
		}
		private void MenuItemNewOrEdit_Click(object sender, RoutedEventArgs e) {
			_formEditAndNewRecord = new View.FormEdit();
			_formEditAndNewRecord.Closed += _formEdit_Closed;
			_formEditAndNewRecord.Show();
			MenuItemNewOrEdit.IsEnabled = false;
		}

		private void MainWindow_Closed(object sender, EventArgs e) {
			_formEditAndNewRecord.Close();
			_formDeleted.Close();
			_formRequest.Close();
		}
	}
}