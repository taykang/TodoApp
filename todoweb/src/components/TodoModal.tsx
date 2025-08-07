import React, { useState, useEffect } from "react";
import { Todo } from "../types";

interface TodoModalProps {
  isOpen: boolean;
  onClose: () => void;
  onSave: (todo: Todo, id?: string) => void;
  editingTodo?: Todo | null;
}

const TodoModal: React.FC<TodoModalProps> = ({
  isOpen,
  onClose,
  onSave,
  editingTodo,
}) => {
  const [title, setTitle] = useState("");
  const [desc, setDesc] = useState("");
  const [tag, setTag] = useState("");
  const [priority, setPriority] = useState<"Low" | "Medium" | "High">("Low");
  const [isCompleted, setIsCompleted] = useState(false);

  useEffect(() => {
    if (isOpen) {
      if (editingTodo) {
        setTitle(editingTodo._title);
        setDesc(editingTodo._desc || "");
        setTag(editingTodo._tag || "");
        setPriority((editingTodo._priority as "Low" | "Medium" | "High") || "Low");
        setIsCompleted(editingTodo._isCompleted);
      } else {
        setTitle("");
        setDesc("");
        setTag("");
        setPriority("Low");
        setIsCompleted(false);
      }
    }
  }, [editingTodo, isOpen]);

  if (!isOpen) return null;

  const handleSubmit = () => {
    if (!title.trim()) {
      alert("Title is required");
      return;
    }

    onSave(
      {
        _id: editingTodo?._id ?? "",
        _title: title,
        _desc: desc,
        _tag: tag,
        _priority: priority,
        _isCompleted: isCompleted,
      },
      editingTodo?._id
    );

    onClose();
  };

  return (
    <div className="fixed inset-0 z-50 flex items-center justify-center bg-black bg-opacity-50 px-4">
      <div className="bg-white w-full max-w-md rounded-2xl shadow-lg p-6 space-y-5">
        <h2 className="text-2xl font-bold text-gray-800 border-b pb-2">
          {editingTodo ? "Edit Todo" : "Add Todo"}
        </h2>

        {/* Title */}
        <div>
          <label className="block text-sm font-semibold mb-1">Title</label>
          <input
            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Enter title"
            value={title}
            onChange={(e) => setTitle(e.target.value)}
          />
        </div>

        {/* Description */}
        <div>
          <label className="block text-sm font-semibold mb-1">Description</label>
          <textarea
            className="w-full p-2 border border-gray-300 rounded-lg resize-none h-24 focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="Enter description"
            value={desc}
            onChange={(e) => setDesc(e.target.value)}
          />
        </div>

        {/* Tag */}
        <div>
          <label className="block text-sm font-semibold mb-1">Tag</label>
          <input
            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            placeholder="e.g. work, personal"
            value={tag}
            onChange={(e) => setTag(e.target.value)}
          />
        </div>

        {/* Priority */}
        <div>
          <label className="block text-sm font-semibold mb-1">Priority</label>
          <select
            className="w-full p-2 border border-gray-300 rounded-lg focus:outline-none focus:ring-2 focus:ring-blue-500"
            value={priority}
            onChange={(e) => setPriority(e.target.value as "Low" | "Medium" | "High")}
          >
            <option value="Low">🟢 Low</option>
            <option value="Medium">🟡 Medium</option>
            <option value="High">🔴 High</option>
          </select>
        </div>

        {/* Completed Checkbox */}
        <div className="flex items-center space-x-2">
          <input
            type="checkbox"
            checked={isCompleted}
            onChange={(e) => setIsCompleted(e.target.checked)}
            className="accent-blue-600"
          />
          <label className="text-sm font-medium text-gray-700">Mark as completed</label>
        </div>

        {/* Buttons */}
        <div className="flex justify-end space-x-3 pt-4">
          <button
            onClick={onClose}
            className="px-4 py-2 bg-gray-200 text-gray-700 rounded-lg hover:bg-gray-300 transition"
          >
            Cancel
          </button>
          <button
            onClick={handleSubmit}
            className="px-4 py-2 bg-blue-600 text-white rounded-lg hover:bg-blue-700 transition"
          >
            Save
          </button>
        </div>
      </div>
    </div>
  );
};

export default TodoModal;