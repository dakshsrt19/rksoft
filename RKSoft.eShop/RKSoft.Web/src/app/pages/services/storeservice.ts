import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AppConfigService } from './app-config.service';

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

  constructor(private http: HttpClient, private config: AppConfigService) {}

  // CREATE
  createStore(item: StoreItem): Observable<StoreItem> {
    return this.http.post<StoreItem>(`${this.config.apiUrls.stores}/add`, item);
  }

  // GET ALL
  getAllStores(): Observable<StoreItem[]> {
    return this.http.get<StoreItem[]>(`${this.config.apiUrls.stores}/all`);
  }

   // GET ALL
  getAllActiveStores(isActive: boolean): Observable<StoreItem[]> {
    return this.http.get<StoreItem[]>(`${this.config.apiUrls.stores}/all`, {
      params: { isActive: isActive.toString() }
    });
  }

  // GET BY ID
  getStoreById(id: number): Observable<StoreItem> {
    return this.http.get<StoreItem>(`${this.config.apiUrls.stores}/${id}`);
  }

  // UPDATE
  updateStore(id: number, item: StoreItem): Observable<StoreItem> {
    return this.http.put<StoreItem>(`${this.config.apiUrls.stores}/update`, item);
  }

  // PARTIAL UPDATE
  updateStorePartial(id: number, partialItem: Partial<StoreItem>): Observable<StoreItem> {
  return this.http.patch<StoreItem>(
    `${this.config.apiUrls.stores}/${id}`, partialItem);
}

  // DELETE
  deleteStore(id: number): Observable<any> {
    return this.http.delete(`${this.config.apiUrls.stores}/delete/${id}`);
  }
}
