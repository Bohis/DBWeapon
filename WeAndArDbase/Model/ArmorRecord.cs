using System.Windows;
using System;

namespace WeAndArDbase.Model {
	/// <summary>
	/// Класс обеспечивающий работу с записями ти БРОНЕЖИЛЕТЫ
	/// </summary>
	public class ArmorRecord : IRecord {
		/// <summary>
		/// Класс бронежидета по ГОСТ Р 50744-95
		/// </summary>
		private string _classKey;
		/// <summary>
		///  Описание 
		/// </summary>
		private string _nameMensDestruction;
		/// <summary>
		/// Вес пули, попадания которой, бронижилет может выдержать
		/// </summary>
		private double _weightB;
		/// <summary>		
		/// Скорость пули, попадания которой, бронижилет может выдержать
		/// </summary>
		private double _speedB;
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
		/// <param name="classKey">Класс</param>
		/// <param name="nameMensDestruction">Описание</param>
		/// <param name="weightB">Вес</param>
		/// <param name="speedB">Скорость</param>
		public ArmorRecord(string classKey, string nameMensDestruction, string weightB, string speedB) {
			try {
				_classKey = classKey;
			}
			catch (Exception error) {
				_classKey = "error";
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}
			try {
				_nameMensDestruction = nameMensDestruction;
			}
			catch (Exception error) {
				_nameMensDestruction = "error";
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}
			try {
				_weightB = double.Parse( weightB );
			}
			catch (Exception error) {
				_weightB = 0f;
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}
			try {
				_speedB = double.Parse( speedB );
			}
			catch (Exception error) {
				_speedB = 0f;
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}

			_changeTrigger = false;
			_newValue = false;
		}
		/// <summary>
		/// Устанавливает/возращает класс бронежилеты
		/// </summary>
		public string ClassKey { get => _classKey; set { _classKey = value; _changeTrigger = true; } }
		/// <summary>
		/// Устанавливает/возращает описание
		/// </summary>
		public string NameMensDestruction { get => _nameMensDestruction; set { _nameMensDestruction = value; _changeTrigger = true; } }
		/// <summary>
		/// Устанавливает/возращает вес пули
		/// </summary>
		public double WeightB { get => _weightB; set { _weightB = value; _changeTrigger = true; } }
		/// <summary>
		/// Устанавливает/возращает скорость пули
		/// </summary>
		public double SpeedB { get => _speedB; set { _speedB = value; _changeTrigger = true; } }
		/// <summary>
		/// Возращает полные данные объекта
		/// </summary>
		public string FullValues() {
			return "'" + _classKey + "', '" + _nameMensDestruction + "', '" + _weightB.ToString() + "', '" + _speedB.ToString() + "'";
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
		/// <summary>
		/// Сравненние объектов
		/// </summary>
		/// <param name="objectRecord">Объект для сравнения</param>
		/// <returns>True - эквивалент, False - разные объекты , Null - не сравнимые типы</returns>
		public bool? Comparison(IRecord objectRecord) {
			try {
				if (( objectRecord as ArmorRecord ).ClassKey == _classKey && ( objectRecord as ArmorRecord ).NameMensDestruction == _nameMensDestruction &&
					( objectRecord as ArmorRecord )._speedB == _speedB && ( objectRecord as ArmorRecord ).WeightB == _weightB)
					return true;
				else
					return false;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return null;
			}
		}
	}
}