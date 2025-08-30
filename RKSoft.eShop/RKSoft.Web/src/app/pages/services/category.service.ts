import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { AppConfigService } from "./app-config.service";
import { Observable } from "rxjs";

export interface CategoryItem {
  id: number;
  categoryName: string;
  description: string;
  createdAt: Date;
  updatedAt: Date;
  isActive: boolean;
  isDeleted: boolean;
  storeId: number;
}
@Injectable({
  providedIn: "root",
})
export class CategoryService {
  constructor(private http: HttpClient, private config: AppConfigService) {}

  // CREATE
  createcategory(item: CategoryItem): Observable<CategoryItem> {
    return this.http.post<CategoryItem>(
      `${this.config.apiUrls.categories}/add`,
      item,
      { headers: { "Content-Type": "application/json" } }
    );
  }

  // GET ALL
  getAllcategorys(): Observable<CategoryItem[]> {
    return this.http.get<CategoryItem[]>(
      `${this.config.apiUrls.categories}/all`
    );
  }

  // GET BY ID
  getcategoryById(id: number): Observable<CategoryItem> {
    return this.http.get<CategoryItem>(
      `${this.config.apiUrls.categories}/${id}`
    );
  }

  // UPDATE
  updatecategory(id: number, item: CategoryItem): Observable<CategoryItem> {
    return this.http.put<CategoryItem>(
      `${this.config.apiUrls.categories}/update`,
      item,
      {
        headers: { "Content-Type": "application/json" },
      }
    );
  }

  // PARTIAL UPDATE
  updatecategoryPartial(
    id: number,
    partialItem: Partial<CategoryItem>
  ): Observable<CategoryItem> {
    return this.http.patch<CategoryItem>(
      `${this.config.apiUrls.categories}/${id}`,
      partialItem
    );
  }

  // DELETE
  deletecategory(id: number): Observable<any> {
    return this.http.delete(`${this.config.apiUrls.categories}/delete/${id}`);
  }
}
