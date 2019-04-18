
namespace WeAndArDbase.Model {
	/// <summary>
	/// Интерфейс классов записей БД
	/// </summary>
	public interface IRecord {
		/// <summary>
		/// Полный список данных
		/// </summary>
		string FullValues();
		/// <summary>
		/// Сравенние объектов 
		/// </summary>
		/// <param name="objectRecord">Объект для сравненеия</param>
		/// <returns>True - эквивалент, False - разные объекты , Null - не сравнимые типы</returns>
		bool? Comparison(IRecord objectRecord);
		/// <summary>
		/// Триггер изменения
		/// </summary>
		/// <returns>Были ли изменены данные</returns>
		bool ValueChanged();
		/// <summary>
		/// Триггер новых данных
		/// </summary>
		/// <param name="value">значние тригера (True,False,Null)</param>
		/// <returns>значение триггера</returns>
		bool NewValue(bool? value = null);
	}
}