import React, { useEffect, useState } from "react";
import { getPagedTodos, addTodo, updateTodo, deleteTodo } from "./api/api";
import { Todo } from "./types";
import TodoList from "./components/TodoList";
import TodoModal from "./components/TodoModal";
import Swal from 'sweetalert2';

const App = () => {
  const [todos, setTodos] = useState<Todo[]>([]);
  const [currentPage, setCurrentPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);
  const [searchText, setSearchText] = useState("");
  const [editingTodo, setEditingTodo] = useState<Todo | null>(null);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const today = new Date();
  const [filter, setFilter] = useState({
    _search_text: "",
    _priority: "",
    _isCompleted: false,
    _month: today.getMonth() + 1, // JavaScript months are 0-indexed
    _year: today.getFullYear(),  
    _sort_field: "_submitted_date",      // default sort
    _sort_direction: "desc", 
  });

  // 🔄 Fetch todos with filter + pagination
  const fetchTodos = async () => {
    const response = await getPagedTodos(currentPage, 5, filter);
    setTodos(response.data);
    setTotalPages(response.total_pages);
  };

  useEffect(() => {
    fetchTodos();
  }, [currentPage]);

  const handleSearch = () => {
    setCurrentPage(1);
    fetchTodos();
  };

  const handleSave = async (todoData: Todo, id?: string) => {
    if (id) {
      const result = await Swal.fire({
        title: 'Are you sure?',
        text: 'This will update the todo item.',
        icon: 'warning',
        showCancelButton: true,
        confirmButtonText: 'Yes, update it!',
        cancelButtonText: 'Cancel'
      });

      if (!result.isConfirmed) return;

      await updateTodo(id, todoData);
    } else {
      await addTodo(todoData);
    }

    fetchTodos();
  };

  const handleDelete = async (id: string) => {
    const result = await Swal.fire({
      title: 'Are you sure?',
      text: 'This will delete the todo permanently.',
      icon: 'warning',
      showCancelButton: true,
      confirmButtonText: 'Yes, delete it!',
      cancelButtonText: 'Cancel'
    });

    if (!result.isConfirmed) return;

    await deleteTodo(id);
    fetchTodos();
  };

  const handleEdit = (todo: Todo) => {
    setEditingTodo(todo);
    setIsModalOpen(true);
  };

  return (
    <div className="max-w-3xl mx-auto p-6 bg-white shadow-lg rounded-lg">
      <h1 className="text-4xl font-bold mb-6 text-center text-gray-800">📋 Todo List</h1>

      {/* 🔍 Filter Section */}
      <div className="bg-gray-50 p-6 rounded-lg shadow-sm mb-6 border border-gray-200">
        <h2 className="text-xl font-semibold mb-4 text-gray-700">🎛️ Filters</h2>
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">

          {/* 🔎 Search */}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">🔎 Search</label>
            <input
              type="text"
              value={filter._search_text}
              onChange={(e) =>
                setFilter({ ...filter, _search_text: e.target.value })
              }
              placeholder="Title, tag, or description"
              className="p-2 border border-gray-300 rounded w-full"
            />
          </div>

          {/* 🎯 Priority */}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">🎯 Priority</label>
            <select
              value={filter._priority}
              onChange={(e) => setFilter({ ...filter, _priority: e.target.value })}
              className="p-2 border border-gray-300 rounded w-full"
            >
              <option value="">🎯All Priorities</option>
              <option value="Low">🟢 Low</option>
              <option value="Medium">🟡 Medium</option>
              <option value="High">🔴 High</option>
            </select>
          </div>

          {/* ✅ Completed */}
          <div className="flex items-center mt-2 space-x-2 md:col-span-2">
            <input
              type="checkbox"
              checked={filter._isCompleted}
              onChange={(e) =>
                setFilter({ ...filter, _isCompleted: e.target.checked })
              }
              className="h-4 w-4"
            />
            <span className="text-gray-600">Show only completed tasks</span>
          </div>

          {/* 📅 Month */}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">📅 Month</label>
            <input
              type="number"
              value={filter._month}
              onChange={(e) =>
                setFilter({ ...filter, _month: Number(e.target.value) })
              }
              placeholder="Month (1-12)"
              className="p-2 border border-gray-300 rounded w-full"
              min={1}
              max={12}
            />
          </div>

          {/* 📆 Year */}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">📆 Year</label>
            <input
              type="number"
              value={filter._year}
              onChange={(e) =>
                setFilter({ ...filter, _year: Number(e.target.value) })
              }
              placeholder="Year"
              className="p-2 border border-gray-300 rounded w-full"
            />
          </div>
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Sort By</label>
            <select
              value={filter._sort_field}
              onChange={(e) => setFilter({ ...filter, _sort_field: e.target.value })}
              className="p-2 border border-gray-300 rounded w-full"
            >
              <option value="_submitted_date">📆 Submitted Date</option>
              <option value="_title">📋 Title</option>
              <option value="_priority">🎯 Priority</option>
            </select>
          </div>

          {/* ↕️ Sort Direction */}
          <div>
            <label className="block text-sm font-medium text-gray-700 mb-1">Direction</label>
            <select
              value={filter._sort_direction}
              onChange={(e) => setFilter({ ...filter, _sort_direction: e.target.value })}
              className="p-2 border border-gray-300 rounded w-full"
            >
              <option value="asc">⬆️ Ascending</option>
              <option value="desc">⬇️ Descending</option>
            </select>
          </div>
          {/* 🔘 Buttons */}
          <div className="col-span-1 md:col-span-2 flex flex-col sm:flex-row gap-2 mt-2">
            <button
              onClick={() => {
                setCurrentPage(1);
                fetchTodos();
              }}
              className="w-full sm:w-auto px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700"
            >
              🔍 Apply Filters
            </button>

            <button
              onClick={() => {
                setFilter({
                  _search_text: "",
                  _priority: "",
                  _isCompleted: false,
                  _month: today.getMonth() + 1,
                  _year: today.getFullYear(),  
                  _sort_field: "_submitted_date",      // default sort
                  _sort_direction: "desc", 
                });
                setCurrentPage(1);
                fetchTodos();
              }}
              className="w-full sm:w-auto px-4 py-2 bg-gray-300 text-gray-800 rounded hover:bg-gray-400"
            >
              ♻️ Clear Filters
            </button>
          </div>
        </div>
      </div>

      {/* ➕ Add button */}
      <div className="mb-4 text-right">
        <button
          onClick={() => {
            setEditingTodo(null);
            setIsModalOpen(true);
          }}
          className="px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700 shadow"
        >
          ➕ Add Todo
        </button>
      </div>

      {/* ✅ Todo List */}
      <TodoList todos={todos} onEdit={handleEdit} onDelete={handleDelete} />

      {/* 🔁 Pagination */}
      <div className="flex justify-center mt-6 space-x-4 items-center">
        <button
          onClick={() => setCurrentPage((p) => Math.max(p - 1, 1))}
          disabled={currentPage === 1}
          className="px-3 py-1 bg-gray-200 rounded hover:bg-gray-300 disabled:opacity-50"
        >
          ◀ Prev
        </button>
        <span className="font-medium text-gray-700">
          Page {currentPage} of {totalPages}
        </span>
        <button
          onClick={() => setCurrentPage((p) => Math.min(p + 1, totalPages))}
          disabled={currentPage === totalPages}
          className="px-3 py-1 bg-gray-200 rounded hover:bg-gray-300 disabled:opacity-50"
        >
          Next ▶
        </button>
      </div>

      {/* ✏️ Modal */}
      <TodoModal
        isOpen={isModalOpen}
        onClose={() => setIsModalOpen(false)}
        onSave={handleSave}
        editingTodo={editingTodo}
      />
    </div>
  );
};

export default App;