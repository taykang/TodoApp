import { Todo } from "../types";

interface TodoListProps {
  todos: Todo[];
  onEdit: (todo: Todo) => void;
  onDelete: (id: string) => void;
}

const TodoList: React.FC<TodoListProps> = ({ todos, onEdit, onDelete }) => {
  return (
    <ul className="space-y-4">
      {todos.map((todo) => (
        <li
          key={todo._id}
          className="p-4 bg-white border rounded-xl shadow hover:shadow-md transition"
        >
          <div className="flex justify-between items-center">
            <div>
              <h3 className={`text-lg font-semibold ${todo._isCompleted ? "line-through text-gray-400" : "text-gray-800"}`}>
                {todo._title}
              </h3>

              <div className="text-sm text-gray-600 mt-1 space-y-0.5">
                <p>
                  <span className="font-medium">Tag:</span> {todo._tag || "-"}
                </p>
                <p>
                  <span className="font-medium">Priority:</span>{" "}
                  <span
                    className={`inline-block px-2 py-0.5 rounded text-white text-xs font-semibold ${
                      todo._priority === "High"
                        ? "bg-red-600"
                        : todo._priority === "Medium"
                        ? "bg-yellow-500"
                        : "bg-green-500"
                    }`}
                  >
                    {todo._priority || "Low"}
                  </span>
                </p>
                <p>
                  <span className="font-medium">Submitted:</span>{" "}
                  {todo._submitted_date
                    ? new Date(todo._submitted_date).toLocaleString()
                    : "-"}
                </p>
                {todo._isCompleted && (
                  <p>
                    <span className="font-medium">Completed:</span>{" "}
                    {todo._completed_date
                      ? new Date(todo._completed_date).toLocaleString()
                      : "-"}
                  </p>
                )}
              </div>
            </div>

            <div className="space-x-2">
              <button
                onClick={() => onEdit(todo)}
                className="px-3 py-1 bg-yellow-500 text-white rounded-md hover:bg-yellow-600 transition"
              >
                Edit
              </button>
              <button
                onClick={() => onDelete(todo._id)}
                className="px-3 py-1 bg-red-600 text-white rounded-md hover:bg-red-700 transition"
              >
                Delete
              </button>
            </div>
          </div>
        </li>
      ))}
    </ul>
  );
};

export default TodoList;