using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Core.DataModel;
using TodoApp.Core.DTOs;

namespace TodoApp.Core.Mappers
{
    public static class TodoMapper
    {
        // ✅ Entity -> DTO
        public static TodoDto ToDto(TodoItemModel entity) => new TodoDto
        {
            _id = entity._id,
            _title = entity._title,
            _desc = entity._desc,
            _tag = entity._tag,
            _priority = entity._priority,
            _isCompleted = entity._isCompleted,
            _submitted_date = entity._submitted_date,
            _completed_date = entity._completed_date
        };

        // ✅ DTO -> Entity (for Create)
        public static TodoItemModel FromCreateDto(TodoCreateDto dto) => new TodoItemModel
        {
            _title = dto._title,
            _desc = dto._desc,
            _tag = dto._tag,
            _priority = dto._priority,
            _isCompleted = false,
            _submitted_date = DateTime.UtcNow
        };

        // ✅ Update Entity with DTO data (for Update)
        public static void UpdateEntity(TodoItemModel entity, TodoUpdateDto dto)
        {
            entity._title = dto._title;
            entity._desc = dto._desc;
            entity._tag = dto._tag;
            entity._priority = dto._priority;
            entity._isCompleted = dto._isCompleted;

            // ✅ Update completed_date only when it was not set before
            if (dto._isCompleted && entity._completed_date == null)
            {
                entity._completed_date = DateTime.UtcNow;
            }
        }
    }
}
