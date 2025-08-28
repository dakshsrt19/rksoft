import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface StoreItem {
  id: number;
  storeName: string;
  description: string; // ✅ description should be string, not number
  createdAt: Date;
  updatedAt: Date;
  isActive: boolean;
  isDeleted: boolean;
}

@Injectable({
  providedIn: 'root'
})
export class Storeservice {
  private apiUrl = 'https://localhost:7039/api/store';

  constructor(private http: HttpClient) {}

  // CREATE
  createStore(item: StoreItem): Observable<StoreItem> {
    return this.http.post<StoreItem>(`${this.apiUrl}/add`, item);
  }

  // GET ALL
  getAllStores(): Observable<StoreItem[]> {
    return this.http.get<StoreItem[]>(`${this.apiUrl}/all`);
  }

  // GET BY ID
  getStoreById(id: number): Observable<StoreItem> {
    return this.http.get<StoreItem>(`${this.apiUrl}/${id}`);
  }

  // UPDATE
  updateStore(id: number, item: StoreItem): Observable<StoreItem> {
    return this.http.put<StoreItem>(`${this.apiUrl}/update`, item);
  }

  // DELETE
  deleteStore(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/delete/${id}`);
  }
}
