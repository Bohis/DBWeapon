using System.Data;
using System.Windows;
using System.Data.OleDb;
using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace WeAndArDbase.Model {
	/// <summary>
	/// Класс для работы с базой данных
	/// </summary>
	class WorkWithDB {
		/// <summary>
		/// Представляет соеденение для БД
		/// </summary>
		private OleDbConnection _connectionBase;
		/// <summary>
		///  Статичный объект для доступа к текущему объекта
		/// </summary>
		static private WorkWithDB _thisObject;
		/// <summary>
		/// Доступ к объекту доступа к БД
		/// </summary>
		static public WorkWithDB ThisObject { get => _thisObject; }
		/// <summary>
		/// Коллекция записей брони
		/// </summary>
		private ObservableCollection<ArmorRecord> _armorColl;
		/// <summary>
		/// Коллекция записей оружия
		/// </summary>
		private ObservableCollection<WeaponRecord> _weaponColl;
		/// <summary>
		/// Коллекция содержащая результаты поиска
		/// </summary>
		private ObservableCollection<WeaponRecord> _weaponSearchColl;
		/// <summary>
		/// Коллекция записей калибров
		/// </summary>
		private ObservableCollection<CartridgeRecord> _cartridgeColl;
		/// <summary>
		/// Коллекция записей типов оружия
		/// </summary>
		private ObservableCollection<WeaponsCategoryRecord> _weaponCategoryColl;
		/// <summary>
		/// Коллекция записей стран
		/// </summary>
		private ObservableCollection<CountryRecord> _countryColl;
		/// <summary>
		/// Коллекция записей брони
		/// </summary>
		public ObservableCollection<ArmorRecord> ARCollection { get => _armorColl; }
		/// <summary>
		/// Коллекция записей оружия
		/// </summary>
		public ObservableCollection<WeaponRecord> WPCollection { get => _weaponColl; }
		/// <summary>
		/// Коллекция содержащая результаты поиска
		/// </summary>
		public ObservableCollection<CartridgeRecord> CRCollection { get => _cartridgeColl; }
		/// <summary>
		/// Коллекция записей калибров
		/// </summary>
		public ObservableCollection<WeaponsCategoryRecord> WPCCollection { get => _weaponCategoryColl; }
		/// <summary>
		/// Коллекция записей стран
		/// </summary>
		public ObservableCollection<CountryRecord> CCollection { get => _countryColl; }
		/// <summary>
		/// Коллекция записей оружия
		/// </summary>
		public ObservableCollection<WeaponRecord> WSearchColl { get => _weaponSearchColl; }
		/// <summary>
		/// поле нового элемента Оружия
		/// </summary>
		private WeaponRecord _newItemWeapon;
		/// <summary>
		/// поле нового элемента Калибр
		/// </summary>
		private CartridgeRecord _newItemCartridge;
		/// <summary>
		/// поле нового элемента Бронежилет
		/// </summary>
		private ArmorRecord _newItemArmor;
		/// <summary>
		/// поле нового элемента Страна
		/// </summary>
		private CountryRecord _newItemCountry;
		/// <summary>
		/// поле нового элемента Категория оружия
		/// </summary>
		private WeaponsCategoryRecord _newItemWeaponCategory;
		/// <summary>
		/// поле нового элемента Оружия
		/// </summary>
		public WeaponRecord NewItemWeapon { get => _newItemWeapon; set => _newItemWeapon = value; }
		/// <summary>
		/// поле нового элемента Калибр
		/// </summary>
		public CartridgeRecord NewItemCartridge { get => _newItemCartridge; set => _newItemCartridge = value; }
		/// <summary>
		/// поле нового элемента Бронежилет
		/// </summary>
		public ArmorRecord NewItemArmor { get => _newItemArmor; set => _newItemArmor = value; }
		/// <summary>
		/// поле нового элемента Страна
		/// </summary>
		public CountryRecord NewItemCountry { get => _newItemCountry; set => _newItemCountry = value; }
		/// <summary>
		/// поле нового элемента Категория оружия
		/// </summary>
		public WeaponsCategoryRecord NewItemWeaponCategory { get => _newItemWeaponCategory; set => _newItemWeaponCategory = value; }
		/// <summary>
		/// Список калибров
		/// </summary>
		private List<string> _nameCartridge;
		/// <summary>
		/// Список типов оружия
		/// </summary>
		private List<string> _nameCategoryWeapon;
		/// <summary>
		/// Список стран
		/// </summary>
		private List<string> _nameCountry;
		/// <summary>
		/// Список классов бронежилетов
		/// </summary>
		public List<string> _classArmor;
		/// <summary>
		/// Список калибров
		/// </summary>
		public List<string> NameCartridge { get => _nameCartridge; }
		/// <summary>
		/// Список типов оружия
		/// </summary>
		public List<string> NameCategoryWeapon { get => _nameCategoryWeapon; }
		/// <summary>
		/// Список стран
		/// </summary>
		public List<string> NameCountry { get => _nameCountry; }
		/// <summary>
		/// Список классов бронежилетов
		/// </summary>
		public List<string> ClassArmor { get => _classArmor; }
		/// <summary>
		/// Соеденение с БД
		/// </summary>
		/// <param name="connectAddres">адрес БД и параметры</param>
		public void ConnectToBase(string connectAddres = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Resurses\WeaponAndArmor.mdb;") {
			try {
				_connectionBase = new OleDbConnection( connectAddres );
			}
			catch (Exception error) {
				_connectionBase = null;
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}
			/// Установка текущего объекта
			_thisObject = this;
		}
		/// <summary>
		/// Конструктор
		/// </summary>
		public WorkWithDB() {
			_connectionBase = null;

			_armorColl = new ObservableCollection<ArmorRecord>();
			_weaponColl = new ObservableCollection<WeaponRecord>();
			_cartridgeColl = new ObservableCollection<CartridgeRecord>();
			_weaponCategoryColl = new ObservableCollection<WeaponsCategoryRecord>();
			_countryColl = new ObservableCollection<CountryRecord>();
			_weaponSearchColl = new ObservableCollection<WeaponRecord>();

			_newItemWeapon = null;
			_newItemCartridge = null;
			_newItemArmor = null;
			_newItemCountry = null;
			_newItemWeaponCategory = null;

			_nameCountry = new List<string>();
			_nameCategoryWeapon = new List<string>();
			_nameCartridge = new List<string>();
			_classArmor = new List<string>();
		}
		/// <summary>
		/// Закрытие доступа к БД
		/// </summary>
		public void ClouseConnect() {
			try {
				_connectionBase.Close();
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
			}
		}
		/// <summary>
		/// Запрос к БД
		/// </summary>
		/// <param name="textRequest">Текст запроса</param>
		/// <returns>Результат запроса</returns>
		public string Request(string textRequest) {
			try {
				if (_connectionBase.State == ConnectionState.Closed)
					_connectionBase.Open();

				OleDbCommand command = new OleDbCommand( textRequest, _connectionBase );

				OleDbDataReader reader = command.ExecuteReader();

				string result = string.Empty;

				/// Чтение результата запроса к БД
				while (reader.Read()) {
					string str = string.Empty;
					for (int i = 0; i < reader.FieldCount; ++i)
						str += reader[ i ] + "_";
					result += str;
				}

				reader.Close();

				_connectionBase.Close();
				return result;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return null;
			}
		}
		/// <summary>
		/// Возращает объект соеденения с БД
		/// </summary>
		public OleDbConnection GetConnection { get => _connectionBase; }
		/// <summary>
		/// Запрос данных Бронежилетов
		/// </summary>
		/// <returns>True - информация получена, False - неудача запроса</returns>
		public bool RequestRecordArmor() {
			try {
				string resultRequest = Request( "SELECT * FROM ArmorCategory" );
				for (int i = 0; i < resultRequest.Split( '_' ).Length - 1; i += 4)
					_armorColl.Add( new ArmorRecord( resultRequest.Split( '_' )[ i ], resultRequest.Split( '_' )[ i + 1 ],
						resultRequest.Split( '_' )[ i + 2 ], resultRequest.Split( '_' )[ i + 3 ] ) );
				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Запрос данных Калибров
		/// </summary>
		/// <returns>True - информация получена, False - неудача запроса</returns>
		public bool RequestRecordCartridge() {
			try {
				string resultRequest = Request( "SELECT * FROM CartridgeProperties" );
				for (int i = 0; i < resultRequest.Split( '_' ).Length - 1; i += 5)
					_cartridgeColl.Add( new CartridgeRecord( resultRequest.Split( '_' )[ i ], resultRequest.Split( '_' )[ i + 1 ],
						resultRequest.Split( '_' )[ i + 2 ], resultRequest.Split( '_' )[ i + 3 ], resultRequest.Split( '_' )[ i + 4 ] ) );
				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Запрос данных Категории оружия
		/// </summary>
		/// <returns>True - информация получена, False - неудача запроса</returns>
		public bool RequestRecordWeaponCategory() {
			try {
				string resultRequest = Request( "SELECT * FROM WeaponCategory" );
				for (int i = 0; i < resultRequest.Split( '_' ).Length - 1; i += 3)
					_weaponCategoryColl.Add( new WeaponsCategoryRecord( resultRequest.Split( '_' )[ i ],
						resultRequest.Split( '_' )[ i + 1 ], resultRequest.Split( '_' )[ i + 2 ] ) );
				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Запрос данных Страны
		/// </summary>
		/// <returns>True - информация получена, False - неудача запроса</returns>
		public bool RequestRecordCountry() {
			try {
				string resultRequest = Request( "SELECT * FROM Country" );
				for (int i = 0; i < resultRequest.Split( '_' ).Length - 1; i += 2)
					_countryColl.Add( new CountryRecord( resultRequest.Split( '_' )[ i ], resultRequest.Split( '_' )[ i + 1 ] ) );
				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Запрос данных для списков
		/// </summary>
		/// <returns>True - информация получена, False - неудача запроса</returns>
		public bool RequestRecordListDate() {
			try {

				_nameCartridge.Clear();
				_nameCountry.Clear();
				_nameCategoryWeapon.Clear();
				_classArmor.Clear();

				foreach (CartridgeRecord CRObject in _cartridgeColl) {
					_nameCartridge.Add( CRObject.Diameter.ToString().Replace( ',', '.' ) + "x" + CRObject.Lenght.ToString().Replace( ',', '.' ) );
				}

				foreach (CountryRecord ARObject in _countryColl) {
					_nameCountry.Add( ARObject.CountyName );
				}

				foreach (WeaponsCategoryRecord WCRObject in _weaponCategoryColl) {
					_nameCategoryWeapon.Add( WCRObject.WeaponName );
				}

				foreach (ArmorRecord ARObject in _armorColl) {
					_classArmor.Add( ARObject.ClassKey );
				}

				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Запрос данных Оружия
		/// </summary>
		/// <returns>True - информация получена, False - неудача запроса</returns>
		public bool RequestRecordWeapon() {
			try {
				string SearchTypeName(string code) {
					foreach (WeaponsCategoryRecord obj in _weaponCategoryColl) {
						if (obj.CategoryKey == code) {
							return obj.WeaponName;
						}
					}

#if DEBUG
					MessageBox.Show( "Ошибка поиска категории" );
#endif
					return code;
				}

				string SearchCounty(string code) {
					foreach (CountryRecord obj in _countryColl) {
						if (obj.CountryID.ToString() == code) {
							return obj.CountyName;
						}
					}

#if DEBUG
					MessageBox.Show( "Ошибка поиска страны" );
#endif
					return code;
				}

				string SearchCartridge(string code) {
					foreach (CartridgeRecord obj in _cartridgeColl) {
						if (obj.CartridgeID.ToString() == code) {
							return obj.FullSize();
						}
					}

#if DEBUG
					MessageBox.Show( "Ошибка поиска калибра" );
#endif
					return code;
				}

				string resultRequest = Request( "SELECT * FROM Weapons" );
				for (int i = 0; i < resultRequest.Split( '_' ).Length - 1; i += 11)
					_weaponColl.Add( new WeaponRecord(
						resultRequest.Split( '_' )[ i ],
						SearchTypeName( resultRequest.Split( '_' )[ i + 1 ] ),
						resultRequest.Split( '_' )[ i + 2 ],
						resultRequest.Split( '_' )[ i + 3 ],
						resultRequest.Split( '_' )[ i + 4 ],
						SearchCounty( resultRequest.Split( '_' )[ i + 5 ] ),
						SearchCartridge( resultRequest.Split( '_' )[ i + 6 ] ),
						resultRequest.Split( '_' )[ i + 7 ],
						resultRequest.Split( '_' )[ i + 8 ],
						resultRequest.Split( '_' )[ i + 9 ],
						resultRequest.Split( '_' )[ i + 10 ]
						) );

				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Запрос всех данных
		/// </summary>
		/// <returns>True - информация получена, False - неудача запроса</returns>
		public bool RequestAllRecord() {
			try {
				_armorColl.Clear();
				_cartridgeColl.Clear();
				_countryColl.Clear();
				_weaponColl.Clear();
				_weaponCategoryColl.Clear();

				if (RequestRecordArmor() && RequestRecordCartridge() && RequestRecordWeaponCategory() && RequestRecordCountry() && RequestRecordListDate() && RequestRecordWeapon())
					return true;
				else
					return false;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Удаление записей оружия из БД
		/// </summary>
		/// <returns>True - удаления завершено, False - неудача </returns>
		public bool RequestDeleted(ObservableCollection<WeaponRecord> collection) {
			try {
				for (int i = 0; i < collection.Count; ++i) {
					try {
						Model.WorkWithDB.ThisObject.Request( "DELETE FROM Weapons" +
						" WHERE WeaponID =  " + collection[ i ].WeaponID.ToString() );
					}
					catch (Exception error) {
						MessageBox.Show( error.Message );
					}
				}
				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Удаление записей калибров из БД
		/// </summary>
		/// <returns>True - удаления завершено, False - неудача </returns>
		public bool RequestDeleted(ObservableCollection<CartridgeRecord> collection) {
			try {
				for (int i = 0; i < collection.Count; ++i) {
					try {
						Model.WorkWithDB.ThisObject.Request( "DELETE FROM CartridgeProperties	 " +
						" WHERE CartridgeID =  " + collection[ i ].CartridgeID.ToString() );
					}
					catch (Exception error) {
						MessageBox.Show( error.Message );
					}
				}
				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Удаление записей брони из БД
		/// </summary>
		/// <returns>True - удаления завершено, False - неудача </returns>
		public bool RequestDeleted(ObservableCollection<ArmorRecord> collection) {
			try {
				for (int i = 0; i < collection.Count; ++i) {
					try {
						Model.WorkWithDB.ThisObject.Request( "DELETE FROM ArmorCategory	 " +
						" WHERE ClassKey = '" + collection[ i ].ClassKey + "'" );
					}
					catch (Exception error) {
						MessageBox.Show( error.Message );
					}
				}
				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Удаление записей стран из БД
		/// </summary>
		/// <returns>True - удаления завершено, False - неудача </returns>
		public bool RequestDeleted(ObservableCollection<CountryRecord> collection) {
			try {
				for (int i = 0; i < collection.Count; ++i) {
					try {
						Model.WorkWithDB.ThisObject.Request( "DELETE FROM Country	 " +
						" WHERE CountryID =  " + collection[ i ].CountryID.ToString() );
					}
					catch (Exception error) {
						MessageBox.Show( error.Message );
					}
				}
				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Удаление записей типов оружия из БД
		/// </summary>
		/// <returns>True - удаления завершено, False - неудача </returns>
		public bool RequestDeleted(ObservableCollection<WeaponsCategoryRecord> collection) {
			try {
				for (int i = 0; i < collection.Count; ++i) {
					try {
						Model.WorkWithDB.ThisObject.Request( "DELETE FROM WeaponCategory	 " +
						" WHERE CategoryKey = " + collection[ i ].CategoryKey );
					}
					catch (Exception error) {
						MessageBox.Show( error.Message );
					}
				}
				return true;
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				return false;
			}
		}
		/// <summary>
		/// Поиск в БД по данным
		/// </summary>
		/// <param name="category">тип оружия или null</param>
		/// <param name="name">название или пустая строка</param>
		/// <param name="yearOfDeveloper">год или промежуток или пустая строка</param>
		/// <param name="yearOfAdopter">год или промежуток или пустая строка</param>
		/// <param name="country">название или пустая строка</param>
		/// <param name="cartridgeSize">тип калибра или null</param>
		/// <param name="armorDestroy">класс брони или null</param>
		/// <param name="rateOfFire">скорость стрельбы или пустая строка</param>
		/// <param name="startingSpeed">дульная скорость или пустая строка</param>
		/// <param name="sightingRange">дальность стрельбы или пустая строка</param>
		/// <param name="weight">вес оружия или пустая строка</param>
		/// <returns>True - поиск завершен, False - неудача </returns>
		public bool RequestSearch(string category, string name, string yearOfDeveloper,
			string yearOfAdopter, string country, string cartridgeSize, string armorDestroy, string rateOfFire,
			string startingSpeed, string sightingRange, string weight) {

			_weaponSearchColl.Clear();

			try {
				(string, string) Between(string objStr) {
					try {
						if (objStr.IndexOf( '-' ) != -1)
							return (objStr.Split( '-' )[ 0 ], objStr.Split( '-' )[ 1 ]);
						else
							return (objStr, objStr);
					}
					catch {
						MessageBox.Show( "Ошибка данных для поиска" );
					}
					return (null, null);
				}

				foreach (WeaponRecord obj in _weaponColl) {
					if (category == null || obj.Type == category) {

						if (name == "" || obj.NameWeapon.IndexOf( name ) != -1) {

							if (yearOfDeveloper == "" ||
								( int.Parse( Between( yearOfDeveloper ).Item1 ) <= int.Parse( obj.YearOfDeveloper ) && int.Parse( obj.YearOfDeveloper ) <= int.Parse( Between( yearOfDeveloper ).Item2 ) )) {

								if (yearOfAdopter == "" ||
									( int.Parse( Between( yearOfAdopter ).Item1 ) <= int.Parse( obj.AdopterOfYear ) && int.Parse( obj.AdopterOfYear ) <= int.Parse( Between( yearOfAdopter ).Item2 ) )) {

									if (country == null || obj.CountryOfDeveloper == country) {

										if (cartridgeSize == null || obj.CartridgeSize == cartridgeSize) {

											if (rateOfFire == "" ||
												( int.Parse( Between( rateOfFire ).Item1 ) <= obj.RateOfFire && obj.RateOfFire <= int.Parse( Between( rateOfFire ).Item2 ) )) {

												if (startingSpeed == "" ||
													( int.Parse( Between( startingSpeed ).Item1 ) <= obj.StartingSpeed && obj.StartingSpeed <= int.Parse( Between( startingSpeed ).Item2 ) )) {

													if (sightingRange == "" ||
														( int.Parse( Between( sightingRange ).Item1 ) <= obj.SightingRange && obj.SightingRange <= int.Parse( Between( sightingRange ).Item2 ) )) {

														if (weight == "" ||
															( double.Parse( Between( weight ).Item1 ) <= obj.Weight && obj.Weight <= double.Parse( Between( weight ).Item2 ) )) {

															if (armorDestroy == null || ArmorDestoy( obj, armorDestroy )) {
																_weaponSearchColl.Add( obj );
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			catch (Exception error) {
#if DEBUG
				MessageBox.Show( error.Message );
#endif
				MessageBox.Show( "Ошибка поиска" );
				_weaponSearchColl.Clear();
				return false;
			}
			return true;
		}
		/// <summary>
		/// Расчет пробития 
		/// </summary>
		/// <param name="weaponSample">данные оружия</param>
		/// <param name="armorClass">класс бронежилета</param>
		/// <returns>True - поиск завершен, False - неудача</returns>
		private bool ArmorDestoy(WeaponRecord weaponSample, string armorClass) {
			double kineticEnergyBullets = 0;
			foreach (CartridgeRecord obj in _cartridgeColl) {
				if (obj.FullSize() == weaponSample.CartridgeSize) {
					kineticEnergyBullets = obj.Weight * weaponSample.StartingSpeed;
					break;
				}
			}

			double kineticEnergyArmorResist = 0;
			foreach (ArmorRecord obj in _armorColl) {
				if (obj.ClassKey == armorClass) {
					kineticEnergyArmorResist = obj.SpeedB * obj.WeightB;
				}
			}

			return kineticEnergyBullets > kineticEnergyArmorResist ? true : false;
		}
	}
}