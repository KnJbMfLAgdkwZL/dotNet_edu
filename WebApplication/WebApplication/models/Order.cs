using System;
using System.Collections.Generic;

namespace WebApplication.models
{
    public class Order : Model
    {
        /// <summary>
        /// Получить данные таблицы order по указаному id
        /// </summary>
        /// <param name="id">Условие поиска</param>
        /// <returns>Данные выборки</returns>
        public List<Dictionary<string, object>> SelectById(long id)
        {
            return Select(new Dictionary<string, string>()
            {
                {"id", id.ToString()}
            });
        }

        /// <summary>
        /// Добавляет новую запись в таблицу order
        /// </summary>
        /// <param name="name">Имя</param>
        /// <param name="description">Описание</param>
        /// <returns>Вернет true в случае успеха</returns>
        public bool Insert(string name, string description)
        {
            return base.Insert(new Dictionary<string, string>()
            {
                {"name", name},
                {"description", description},
                {"dateCreate", DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}
            });
        }
    }
}