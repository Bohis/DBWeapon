using System;
using System.Windows;

namespace WeAndArDbase.Model {
	/// <summary>
	/// Запись категории оружия
	/// </summary>
	class WeaponsCategoryRecord : IRecord {
		/// <summary>
		/// Код категории
		/// </summary>
		private string _categoryKey;
		/// <summary>
		/// Название категории оружия
		/// </summary>
		private string _weaponName;
		/// <summary>
		/// Описание
		/// </summary>
		private string _description;
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
		/// <param name="categoryKey">Код категории</param>
		/// <param name="weaponName">Название категории оружия</param>
		/// <param name="description">Описание</param>
		public WeaponsCategoryRecord(string categoryKey, string weaponName, string description) {
			try {
				_categoryKey = categoryKey;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_categoryKey = "error";
			}

			try {
				_weaponName = weaponName;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_weaponName = "error";
			}

			try {
				_description = description;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_description = "error";
			}

			_changeTrigger = false;

			_newValue = false;
		}
		/// <summary>
		/// Код категории
		/// </summary>
		public string CategoryKey { get => _categoryKey; set { _categoryKey = value; _changeTrigger = true; } }
		/// <summary>
		/// Название категории оружия
		/// </summary>
		public string WeaponName { get => _weaponName; set { _weaponName = value; _changeTrigger = true; } }
		/// <summary>
		/// Описание
		/// </summary>
		public string Description { get => _description; set { _description = value; _changeTrigger = true; } }
		/// <summary>
		/// Возращает полные данные объекта
		/// </summary>
		public string FullValues() {
			return "'" + _categoryKey + "', '" + _weaponName + "', '" + _description + "'";
		}
		/// <summary>
		/// Сравенние объектов 
		/// </summary>
		/// <param name="objectRecord">Объект для сравненеия</param>
		/// <returns>True - эквивалент, False - разные объекты , Null - не сравнимые типы</returns>
		public bool? Comparison(IRecord objectRecord) {
			try {
				if (( objectRecord as WeaponsCategoryRecord ).CategoryKey == _categoryKey &&
					( objectRecord as WeaponsCategoryRecord ).Description == _description &&
					( objectRecord as WeaponsCategoryRecord ).WeaponName == _weaponName)
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