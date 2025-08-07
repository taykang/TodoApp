export interface Todo {
  _id: string;
  _title: string;
  _desc?: string;
  _tag?: string;
  _priority: "Low" | "Medium" | "High";
  _isCompleted: boolean;
  _submitted_date?: string;
  _completed_date?: string | null;
}

export interface TodoFilter {
  _search_text?: string;
  // add other filters like _tag?, _priority? if needed
}