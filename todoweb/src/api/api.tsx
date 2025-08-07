import axios from 'axios';
import { Todo,TodoFilter } from "../types";

const BASE_URL = 'https://localhost:7071/api/Todo';
export interface ApiResponse<T> {
  success: boolean;
  code: number;
  message: string;
  data: T;
}

export interface PagingResponse<T> extends ApiResponse<T> {
  current_page: number;
  page_size: number;
  total_pages: number;
  total_records: number;
}

export const getPagedTodos = async (
  page = 1,
  pageSize = 10,
  filter?: TodoFilter
): Promise<PagingResponse<Todo[]>> => {
  const params = {
    page,
    pageSize,
    ...filter, // will include _search_text if passed
  };

  const response = await axios.get<PagingResponse<Todo[]>>(
    `${BASE_URL}/paged`,
    { params }
  );

  return response.data;
};

// ✅ Get all todos (non-paged)
export const getTodos = async (): Promise<ApiResponse<Todo[]>> => {
  const response = await axios.get<ApiResponse<Todo[]>>(`${BASE_URL}/getall`);
  return response.data;
};

// ✅ Get a single todo by ID
export const getTodoById = async (id: string): Promise<ApiResponse<Todo>> => {
  const response = await axios.get<ApiResponse<Todo>>(`${BASE_URL}/${id}`);
  return response.data;
};

// ✅ Add a new todo
export const addTodo = async (todo: Omit<Todo, "_id">): Promise<ApiResponse<Todo>> => {
  const response = await axios.post<ApiResponse<Todo>>(`${BASE_URL}/create`, todo);
  return response.data;
};

// ✅ Update an existing todo
export const updateTodo = async (
  id: string,
  todo: Todo
): Promise<ApiResponse<Todo>> => {
  const response = await axios.put<ApiResponse<Todo>>(`${BASE_URL}/update/${id}`, todo);
  return response.data;
};

// ✅ Delete a todo
export const deleteTodo = async (id: string): Promise<ApiResponse<string>> => {
  const response = await axios.delete<ApiResponse<string>>(`${BASE_URL}/delete/${id}`);
  return response.data;
};