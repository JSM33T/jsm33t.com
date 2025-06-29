import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ApiResponse } from '../interfaces/IApiResponse';


@Injectable({
	providedIn: 'root'
})
export class HttpApiService {

	constructor(private http: HttpClient) { }

	get<T>(url: string): Observable<ApiResponse<T>> {
		return this.http.get<ApiResponse<T>>(url);
	}

	post<T>(url: string, body: any): Observable<ApiResponse<T>> {
		return this.http.post<ApiResponse<T>>(url, body);
	}
}
