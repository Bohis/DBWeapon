using System;
using System.Windows;

namespace WeAndArDbase.Model {
	/// <summary>
	/// Класс записи оружия из БД
	/// </summary>
	public class WeaponRecord : IRecord {
		/// <summary>
		/// Код оружия
		/// </summary>
		private UInt16 _weaponID;
		/// <summary>
		/// Тип оружия
		/// </summary>
		private string _type;
		/// <summary>
		/// Название оружия
		/// </summary>
		private string _nameWeapon;
		/// <summary>
		/// Год разработки оружия
		/// </summary>
		private DateTime _yearOfDeveloper;
		/// <summary>
		/// Год принятия оружия
		/// </summary>
		private DateTime _adopterOfYear;
		/// <summary>
		/// Страна производитель
		/// </summary>
		private string _countryOfDeveloper;
		/// <summary>
		/// Код калибра
		/// </summary>
		private string _cartridgeSize;
		/// <summary>
		/// Скорострельность
		/// </summary>
		private UInt16 _rateOfFire;
		/// <summary>
		/// Скорость пули
		/// </summary>
		private UInt16 _startingSpeed;
		/// <summary>
		/// Дальность прицельной стрельбы
		/// </summary>
		private UInt16 _sightingRange;
		/// <summary>
		/// Полный вес  оружия
		/// </summary>
		private double _weight;
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
		/// <param name="weaponID">Код оружия</param>
		/// <param name="type">Код типа оружия</param>
		/// <param name="nameWeapon">Название оружия</param>
		/// <param name="yearOfDeveloper">Год разработки оружия</param>
		/// <param name="adopted">Год принятия оружия</param>
		/// <param name="countryOfDeveloper">Страна производитель</param>
		/// <param name="cartridgeID">Код калибра</param>
		/// <param name="rateOfFire">Скорострельность</param>
		/// <param name="startingSpeed">Скорость пули</param>
		/// <param name="sightingRange">Дальность прицельной стрельбы</param>
		/// <param name="weight">Полный вес  оружия</param>
		public WeaponRecord(string weaponID, string type, string nameWeapon, string yearOfDeveloper, string adopted, string countryOfDeveloper,
		string cartridgeSize, string rateOfFire, string startingSpeed, string sightingRange, string weight) {

			/// Внутрений метод приведения 
			UInt16 Parse(string str) { return (UInt16)( int.Parse( str ) ); }

			#region input error checking
			try {
				_weaponID = Parse( weaponID );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_weaponID = 0;
			}

			try {
				_type = type;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_type = "error";
			}

			try {
				_nameWeapon = nameWeapon;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_nameWeapon = "error";
			}

			try {
				_yearOfDeveloper = DateTime.Parse( yearOfDeveloper );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_yearOfDeveloper = new DateTime( 2001, 01, 01 );
			}

			try {
				_adopterOfYear = DateTime.Parse( adopted );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_adopterOfYear = new DateTime( 2001, 01, 01 );
			}

			try {
				_countryOfDeveloper = countryOfDeveloper;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_countryOfDeveloper = "XXXX";
			}

			try {
				_cartridgeSize = cartridgeSize;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_cartridgeSize = "error";
			}

			try {
				_rateOfFire = Parse( rateOfFire );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_rateOfFire = 0;
			}

			try {
				_startingSpeed = Parse( startingSpeed );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_startingSpeed = 0;
			}

			try {
				_sightingRange = Parse( sightingRange );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_sightingRange = 0;
			}

			try {
				_weight = double.Parse( weight );
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				_weight = 0;
			}
			#endregion

			_changeTrigger = false;
		}
		/// <summary>
		/// Код оружия
		/// </summary>
		public UInt16 WeaponID { get => _weaponID; set { _weaponID = value; _changeTrigger = true; } }
		/// <summary>
		/// Код типа оружия
		/// </summary>
		public string Type { get => _type; set { _type = value; _changeTrigger = true; } }
		/// <summary>
		/// Название оружия
		/// </summary>
		public string NameWeapon { get => _nameWeapon; set { _nameWeapon = value; _changeTrigger = true; } }
		/// <summary>
		/// Год разработки оружия
		/// </summary>
		public string YearOfDeveloper { get => _yearOfDeveloper.ToString( "yyyy" ); set { _yearOfDeveloper = DateTime.Parse( value ); _changeTrigger = true; } }
		/// <summary>
		/// Год принятия оружия
		/// </summary>
		public string AdopterOfYear { get => _adopterOfYear.ToString( "yyyy" ); set { _adopterOfYear = DateTime.Parse( value ); _changeTrigger = true; } }
		/// <summary>
		/// Страна производитель
		/// </summary>
		public string CountryOfDeveloper { get => _countryOfDeveloper; set { _countryOfDeveloper = value; _changeTrigger = true; } }
		/// <summary>
		/// Код калибра
		/// </summary>
		public string CartridgeSize { get => _cartridgeSize; set { _cartridgeSize = value; _changeTrigger = true; } }
		/// <summary>
		/// Скорострельность
		/// </summary>
		public UInt16 RateOfFire { get => _rateOfFire; set { _rateOfFire = value; _changeTrigger = true; } }
		/// <summary>
		/// Скорость пули
		/// </summary>
		public UInt16 StartingSpeed { get => _startingSpeed; set { _startingSpeed = value; _changeTrigger = true; } }
		/// <summary>
		/// Дальность прицельной стрельбы
		/// </summary>
		public UInt16 SightingRange { get => _sightingRange; set { _sightingRange = value; _changeTrigger = true; } }
		/// <summary>
		/// Полный вес  оружия
		/// </summary>
		public double Weight { get => _weight; set { _weight = value; _changeTrigger = true; } }
		/// <summary>
		/// Возращает полные данные объекта
		/// </summary>
		public string FullValues() {
			return "'" + _weaponID.ToString() + "', '" + _type + "', '" + _yearOfDeveloper.ToString() + "', '" + _adopterOfYear.ToString() + "', '" +
				_countryOfDeveloper.ToString() + "', '" + _cartridgeSize + "', '" + _rateOfFire.ToString() + "', '" + _startingSpeed.ToString() + "', '"
				+ _sightingRange.ToString() + "', '" + _weight.ToString() + "'";
		}
		/// <summary>
		/// Сравнение объектов 
		/// </summary>
		/// <param name="objectRecord">Объект для сравненеия</param>
		/// <returns>True - эквивалент, False - разные объекты , Null - не сравнимые типы</returns>
		public bool? Comparison(IRecord objectRecord) {
			try {
				if (_weaponID == ( objectRecord as WeaponRecord ).WeaponID &&
					_type == ( objectRecord as WeaponRecord ).Type &&
					_nameWeapon == ( objectRecord as WeaponRecord ).NameWeapon &&
					_yearOfDeveloper.ToString( "dd:mm:yyyy" ) == ( objectRecord as WeaponRecord ).YearOfDeveloper &&
					_adopterOfYear.ToString( "dd:mm:yyyy" ) == ( objectRecord as WeaponRecord ).AdopterOfYear &&
					_countryOfDeveloper == ( objectRecord as WeaponRecord ).CountryOfDeveloper &&
					_cartridgeSize == ( objectRecord as WeaponRecord ).CartridgeSize &&
					_rateOfFire == ( objectRecord as WeaponRecord ).RateOfFire &&
					_startingSpeed == ( objectRecord as WeaponRecord ).StartingSpeed &&
					_sightingRange == ( objectRecord as WeaponRecord )._sightingRange &&
					_weight == ( objectRecord as WeaponRecord ).Weight)
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