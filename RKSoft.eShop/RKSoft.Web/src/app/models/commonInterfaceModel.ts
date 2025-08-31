
export interface ApiResponse<T> {
  data: T;
  success: boolean;
  statusCode: number;
  errors?: string[];
}

export interface StoreItem {
  id: number;
  storeName: string;
  description: string; // ✅ description should be string, not number
  createdAt: Date;
  updatedAt: Date;
  isActive: boolean;
  isDeleted: boolean;
}

export interface CategoryItem {
  id: number;
  categoryName: string;
  description: string;
  createdAt: Date;
  updatedAt: Date;
  isActive: boolean;
  isDeleted: boolean;
  storeId: number;
  storeName?: string;
}


