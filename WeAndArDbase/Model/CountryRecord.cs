using System;
using System.Windows;

namespace WeAndArDbase.Model {
	/// <summary>
	/// Записи страны и з БД
	/// </summary>
	public class CountryRecord : IRecord {
		/// <summary>
		/// Уникальный код
		/// </summary>
		private UInt16 _countryID;
		/// <summary>
		/// Название страны
		/// </summary>
		private string _countryName;
		/// <summary>
		/// Триггер изменения данных
		/// </summary>
		private bool _changeTrigger;
		/// <summary>
		/// Триггер новых данных
		/// </summary>
		private bool _newValue;
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="code">уникальный код</param>
		/// <param name="countryName">название страны</param>
		public CountryRecord(string code, string countryName) {
			try {
				_countryID = UInt16.Parse( code );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message + " " + code );
#endif
				_countryID = 0;
			}

			try {
				_countryName = countryName;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_countryName = "error";
			}

			_changeTrigger = false;

			_newValue = false;
		}
		/// <summary>
		/// Код страны
		/// </summary>
		public UInt16 CountryID { get => _countryID; set { _countryID = value; _changeTrigger = true; } }
		/// <summary>
		/// Название страны
		/// </summary>
		public string CountyName { get => _countryName; set { _countryName = value; _changeTrigger = true; } }
		/// <summary>
		/// Полные данные
		/// </summary>
		/// <returns></returns>
		public string FullValues() {
			return _countryID + "'" + _countryName + "'";
		}
		/// <summary>
		/// Сравненеи объектов
		/// </summary>
		/// <param name="objectRecord">объект для сравнения</param>
		/// <returns>True - идентичные объекты, False - разные объекты, Null - ошибка сравнения</returns>
		public bool? Comparison(IRecord objectRecord) {
			try {
				if (( objectRecord as CountryRecord ).CountryID == _countryID && ( objectRecord as CountryRecord ).CountyName == _countryName)
					return true;
				return false;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return null;
			}
		}
		/// <summary>
		/// Триггер изменения
		/// </summary>
		/// <returns>Были ли изменены данные</returns>
		public bool ValueChanged() => _changeTrigger;
		/// <summary>
		/// Триггер новых данных
		/// </summary>
		/// <param name="value">значние тригера (True,False,Null)</param>
		/// <returns>значение триггера</returns>
		public bool NewValue(bool? value = null) {
			if (value != null) {
				_newValue = (bool)value;
			}
			return _newValue;
		}
	}
}