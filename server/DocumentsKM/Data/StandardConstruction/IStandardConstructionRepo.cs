using System.Collections.Generic;
using DocumentsKM.Models;

namespace DocumentsKM.Data
{
    public interface IStandardConstructionRepo
    {
        // Получить все типовые конструкции по id выпуска спецификации
        IEnumerable<StandardConstruction> GetAllBySpecificationId(
            int specificationId);
        // Получить типовую конструкцию по id
        StandardConstruction GetById(int id);
        // Получить типовую конструкцию по unique key
        StandardConstruction GetByUniqueKey(int specificationId);
        // Добавить новую типовую конструкцию
        void Add(StandardConstruction standardconstruction);
        // Изменить типовую конструкцию
        void Update(StandardConstruction standardconstruction);
        // Удалить типовую конструкцию
        void Delete(StandardConstruction standardconstruction);
    }
}

