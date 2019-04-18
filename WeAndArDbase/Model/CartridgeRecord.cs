using System;
using System.Windows;

namespace WeAndArDbase.Model {
	/// <summary>
	/// Запись данных калибра
	/// </summary>
	public class CartridgeRecord : IRecord {
		/// <summary>
		/// Диаметр пули
		/// </summary>
		private float _diameter;
		/// <summary>
		/// Длинна пули
		/// </summary>
		private float _lenght;
		/// <summary>
		/// Вес пули
		/// </summary>
		private float _weight;
		/// <summary>
		/// Описание пули 
		/// </summary>
		private string _mark;
		/// <summary>
		/// Код боеприпаса
		/// </summary>
		private int _cartridgeID;
		/// <summary>
		/// Триггер изменения данных
		/// </summary>
		private bool _changeTrigger;
		/// <summary>
		/// Триггер новых данных
		/// </summary>
		private bool _newValue;
		/// <summary>
		/// Конструктор записи пули
		/// </summary>
		/// <param name="cartridgeID">код пули</param>
		/// <param name="diameter">диаметр пули</param>
		/// <param name="lenght">длина пули</param>
		/// <param name="weight">вес пули</param>
		/// <param name="mark">описание боеприпаса</param>
		public CartridgeRecord(string cartridgeID, string diameter, string lenght, string weight, string mark) {
			try {
				CartridgeID = int.Parse( cartridgeID );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				CartridgeID = 0;
			}

			try {
				Diameter = float.Parse( diameter );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				Diameter = 0f;
			}

			try {
				Lenght = float.Parse( lenght );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				Lenght = 0f;
			}

			try {
				Weight = float.Parse( weight );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				Weight = 0f;
			}

			try {
				Mark = mark;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				Mark = "error";
			}

			_changeTrigger = false;

			_newValue = false;
		}
		/// <summary>
		/// Код боеприпаса
		/// </summary>
		public int CartridgeID { get => _cartridgeID; set { _cartridgeID = value; _changeTrigger = true; } }
		/// <summary>
		/// Диаметр пули
		/// </summary>
		public float Diameter { get => _diameter; set { _diameter = value; _changeTrigger = true; } }
		/// <summary>
		/// Длина пули
		/// </summary>
		public float Lenght { get => _lenght; set { _lenght = value; _changeTrigger = true; } }
		/// <summary>
		/// Вес пули
		/// </summary>
		public float Weight { get => _weight; set { _weight = value; _changeTrigger = true; } }
		/// <summary>
		/// Описание боеприпаса
		/// </summary>
		public string Mark { get => _mark; set { _mark = value; _changeTrigger = true; } }
		/// <summary>
		/// Триггер изменения
		/// </summary>
		/// <returns>Были ли изменены данные</returns>
		public bool ValueChanged() => _changeTrigger;
		/// <summary>
		/// Сравенние объектов 
		/// </summary>
		/// <param name="objectRecord">Объект для сравненеия</param>
		/// <returns>True - эквивалент, False - разные объекты , Null - не сравнимые типы</returns>
		public bool? Comparison(IRecord objectRecord) {
			try {
				if (_diameter == ( objectRecord as CartridgeRecord )._diameter &&
					_lenght == ( objectRecord as CartridgeRecord )._lenght &&
					_weight == ( objectRecord as CartridgeRecord )._weight &&
					_mark == ( objectRecord as CartridgeRecord )._mark &&
					_cartridgeID == ( objectRecord as CartridgeRecord )._cartridgeID)
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
		/// Возращает полные данные объекта
		/// </summary>
		public string FullValues() {
			return "'" + _cartridgeID.ToString() + "', '" + Diameter.ToString() + "', '" + Lenght.ToString() + "', '" + Weight.ToString() + "', '" + Mark + "'";
		}
		/// <summary>
		/// Габаритные размеры пули
		/// </summary>
		/// <returns>Возращает размеры в формате ddxll</returns>
		public string FullSize() {
			return _diameter.ToString().Replace( ',', '.' ) + "x" + _lenght.ToString();
		}
		/// <summary>
		/// Полное значение dd:ll
		/// </summary>
		/// <param name="fullSize">Значние в виде строки</param>
		/// <returns>Кортеж из двух значение</returns>
		static public (double, double) ReturnTranscript(string fullSize) {
			return ( double.Parse( fullSize.Split( 'x' )[ 0 ] ) , double.Parse( fullSize.Split( 'x' )[ 1 ] ));
		}
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