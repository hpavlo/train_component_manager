import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { TrainComponent, PaginatedResponse } from '../models/component.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ComponentService {
  private apiUrl = 'http://localhost:5051/api/components';

  constructor(private http: HttpClient) { }

  createComponent(component: TrainComponent): Observable<TrainComponent> {
    return this.http.post<TrainComponent>(this.apiUrl, component);
  }

  getComponents(pageNumber: number = 1, pageSize: number = 10): Observable<PaginatedResponse<TrainComponent>> {
    let params = new HttpParams()
        .set('pageNumber', pageNumber)
        .set('pageSize', pageSize);
    return this.http.get<PaginatedResponse<TrainComponent>>(this.apiUrl, { params });
  }

  getComponent(id: number): Observable<TrainComponent> {
    return this.http.get<TrainComponent>(`${this.apiUrl}/${id}`);
  }

  updateComponent(component: TrainComponent): Observable<any> {
    return this.http.put(`${this.apiUrl}/${component.id}`, component);
  }

  deleteComponent(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  searchComponents(searchTerm: string, pageNumber: number = 1, pageSize: number = 10): Observable<PaginatedResponse<TrainComponent>> {
    let params = new HttpParams()
        .set('searchTerm', searchTerm)
        .set('pageNumber', pageNumber)
        .set('pageSize', pageSize);

    return this.http.get<PaginatedResponse<TrainComponent>>(`${this.apiUrl}/search`, { params });
  }
}
