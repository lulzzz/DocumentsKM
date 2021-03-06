using DocumentsKM.Dtos;
using DocumentsKM.Models;
using System.Collections.Generic;

namespace DocumentsKM.Services
{
    public interface IMarkService
    {
        // Получить все марки по id подузла
        IEnumerable<Mark> GetAllBySubnodeId(int subnodeId);
        // Получить марку по id
        Mark GetById(int id);
        // Получить код для создания новой марки
        string GetNewMarkCode(int subnodeId);
        // Создать новую марку
        void Create(Mark mark,
            int subnodeId,
            int departmentId,
            int mainBuilderId,
            int? chiefSpecialistId,
            int? groupLeaderId);
        // Изменить марку
        void Update(int id, MarkUpdateRequest mark);
    }
}
